using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder.Framework.Output
{
    public abstract class OutputMultitool
    {
        public const int WAV_FILE_SIZE_OFFSET = 4;
        public const int WAV_DATA_SIZE_OFFSET = 40;
        public const int WAV_MAX_SIZE = 2147483575; //-64 (for padding), -8 (for header)

        public int sampleRate;
        public short channels;
        public short bitsPerSample;
        public float amplification;
        public string requestedFilename;

        public long bytesWaiting; //Bytes waiting in memory to be written to the disk
        public long bytesWritten; //Total bytes written
        public bool outOfDiskSpace = false;
        public bool hasBlockingWriteBeenCompleted = false;

        private LockDunny ioLock; //Lock this whenever we're writing to the current file
        private ConcurrentQueue<byte[]> buffers; //Stores buffers to be written to the file
        public bool recording;
        private bool aborted;
        private Thread ioWriterThread;
        public List<string> filenames; //The filenames used
        private bool userSelectedPath; //Set to true when a user chooses the save path for the output file

        private int currentWavLength = -1;
        private FileStream currentFile;

        public OutputMultitool(int sampleRate, short channels, short bitsPerSample, float amplification, string requestedFilename)
        {
            this.sampleRate = sampleRate;
            this.channels = channels;
            this.bitsPerSample = bitsPerSample;
            this.amplification = amplification;
            this.requestedFilename = requestedFilename; //This can also be passed as null to generate a temp file and prompt later
            ioLock = new LockDunny();
            buffers = new ConcurrentQueue<byte[]>();
            filenames = new List<string>();
        }

        /// <summary>
        /// Called when we are all done, regardless of the exit status
        /// </summary>
        public abstract void OnEncodingFinished();

        /// <summary>
        /// Called when we stop recording and begin emptying out the buffer
        /// </summary>
        public abstract void OnBeginSaving();

        /// <summary>
        /// Handles an IO error and responds with what to do. Called when the disk is out of space
        /// </summary>
        /// <returns></returns>
        public abstract OutputMultitoolIoErrorAction HandleIOError();

        /// <summary>
        /// Called when this is aborted out of. Likely used for an error message.
        /// </summary>
        public abstract void OnAborted();

        /// <summary>
        /// Returns the output file to save the aborted file to. Returns null to delete it
        /// </summary>
        /// <returns></returns>
        public abstract string GetAbortedOutputFile();

        /// <summary>
        /// Begins encoding data after copying initial data. You won't be able to write to buffers until this returns
        /// 
        public void BeginEncoding()
        {
            //Make sure we haven't already started
            if (recording)
                throw new Exception("We've already started encoding!");

            //Set flag
            recording = true;

            //Begin thread
            ioWriterThread = new Thread(() =>
            {
                //Write the initial data...
                try
                {
                    //Begin the main loop
                    //Continuously read buffers and write them to the output stream
                    byte[] buf;
                    while (recording || buffers.Count > 0)
                    {
                        while (!buffers.TryDequeue(out buf))
                            Thread.Sleep(5);
                        //Write
                        _WriteWavSamples(buf, 0, buf.Length);
                        bytesWaiting -= buf.Length;
                    }
                }
                catch (OutputMultitoolAbortException)
                {
                    //Set flag
                    aborted = true;
                }

                //Finish
                _OnEncodingFinished();

            });
            ioWriterThread.IsBackground = true;
            ioWriterThread.Start();
        }

        /// <summary>
        /// Begins encoding data after copying initial data. You won't be able to write to buffers until this returns
        /// 
        /// Copies from the buffer into the file. This operation will be completed before any additional calls to Write will be processed.
        /// Potentially long and big copies can be done without consuming many resources using this, as data is not buffered into memory.
        /// This will spawn a new thread to complete the request and then end it. This function will wait until the thread is spawned and continue execution.
        /// Ensure that the buffer does not get modified while this is ongoing.
        /// </summary>
        /// <param name="buffer">Buffer from which to copy to</param>
        /// <param name="indexes">Indexes from the buffer to write, in order</param>
        /// <param name="bufferSetStatus">Requests when the buffer can be written to and when it should be locked</param>
        public void BeginEncoding(byte[] buffer, WriteIndexParams[] indexes, BlockingWriteBufferSetStatus bufferSetStatus)
        {
            //Make sure we haven't already started
            if (recording)
                throw new Exception("We've already started encoding!");

            //Set flag
            recording = true;

            //Pend bytes
            for (int i = 0; i < indexes.Length; i += 1)
            {
                bytesWaiting += indexes[i].length;
                bytesWritten += indexes[i].length;
            }

            //Begin thread
            bool block = true;
            ioWriterThread = new Thread(() =>
            {
                //Write the initial data...
                try
                {
                    //Lock the buffer
                    bufferSetStatus(true);

                    //We're now safe to continue exeuction in the caller thread
                    block = false;

                    //Copy from the requested indexes
                    for (int i = 0; i < indexes.Length; i += 1)
                    {
                        _WriteWavSamples(buffer, indexes[i].offset, indexes[i].length);
                        bytesWaiting -= indexes[i].length;
                    }

                    //Unlock the buffer
                    bufferSetStatus(false);

                    //Set this flag
                    hasBlockingWriteBeenCompleted = true;

                    //Begin the main loop
                    //Continuously read buffers and write them to the output stream
                    byte[] buf;
                    while (recording || buffers.Count > 0)
                    {
                        while (!buffers.TryDequeue(out buf))
                            Thread.Sleep(5);
                        //Write
                        _WriteWavSamples(buf, 0, buf.Length);
                        bytesWaiting -= buf.Length;
                    }
                }
                catch (OutputMultitoolAbortException)
                {
                    //Unlock the buffer if we haven't already, just in case
                    bufferSetStatus(false);

                    //Set flag
                    block = false;
                    aborted = true;
                }

                //Finish
                _OnEncodingFinished();

            });
            ioWriterThread.IsBackground = true;
            ioWriterThread.Start();

            //Wait for the initial block to finish. This won't take long
            while (block) ;
        }

        public delegate void BlockingWriteBufferSetStatus(bool locked);

        /// <summary>
        /// Flushes things and ends writing. Locks until all data is written. Also prompts the user where to save the file
        /// </summary>
        public void EndEncoding()
        {
            //Set flag
            userSelectedPath = false;
            recording = false;

            //Write an empty buffer. This allows us to escape the worker thread getting locked into a waiting position
            buffers.Enqueue(new byte[0]);

            //Prompt user for file location if required
            if (requestedFilename == null)
            {
                //Prompt
                requestedFilename = OpenFilePicker("wav");
                userSelectedPath = true;
            } else if(requestedFilename != null)
            {
                //We already have the desired path
                userSelectedPath = true;
            }

            //Begin saving
            OnBeginSaving();

            //We'll call back in a different thread
        }

        /// <summary>
        /// Called after the user calls EndEncoding once the system finishes writing all the bytes.
        /// This should ONLY be called by _AbortEncoding and _WriteThread
        /// </summary>
        private void _OnEncodingFinished()
        {
            //Begin saving if the trigger wasn't called earlier
            if(aborted)
                OnBeginSaving();

            //Send aborted trigger
            if (aborted)
                OnAborted();

            //Clear memory. At this point we assume all data that can be written has been
            buffers = null;
            GC.Collect();

            //Close the current file
            if(currentFile != null)
            {
                try
                {
                    currentFile.Flush();
                    currentFile.Close();
                }
                catch
                {
                    //If the above failed, close the file the hard way
                    currentFile.SafeFileHandle.Close();
                }
            }

            //Handle file path
            if(aborted && !userSelectedPath)
            {
                //Fetch
                if(requestedFilename == null)
                    requestedFilename = GetAbortedOutputFile();
                userSelectedPath = true;
            }
            else
            {
                //Wait for user to select the path. This is OK because we're on a new thread
                while (!userSelectedPath)
                    Thread.Sleep(10);
            }

            //Save using the path we got earlier
            SaveFiles();

            //Return control to the user
            OnEncodingFinished();
        }

        /// <summary>
        /// Uses the requestedFilename to move files to where they need to be
        /// </summary>
        private void SaveFiles()
        {
            if (requestedFilename == null)
            {
                //Delete all of the recordings
                foreach (var s in filenames)
                    File.Delete(s);
            }
            else
            {
                //Move all of the recordings
                for (int i = 0; i < filenames.Count; i += 1)
                {
                    string newName = requestedFilename;
                    if (i > 0)
                        newName = GetIndexedFilename(requestedFilename, i);
                    File.Move(filenames[i], newName);
                }
            }
        }

        public void Write(byte[] b)
        {
            //Drop if we are no longer recording
            if (!recording || aborted)
                return;

            //Copy and write to buffer
            bytesWaiting += b.Length;
            bytesWritten += b.Length;
            buffers.Enqueue(b);
        }

        /* WAV tools */

        //Make sure the function that calls this locks the io lock
        private void _WriteWavSamples(byte[] data, int offset, int length)
        {
            //Dispatch these to new files, if needed
            int bytesRemaining = length;
            while(bytesRemaining > 0)
            {
                //Get the space remaining on this file
                int spaceRemaining = WAV_MAX_SIZE - currentWavLength;

                //Create a new file if required
                if (spaceRemaining <= 0 || currentWavLength == -1) {
                    if (RunDiskIoOperation(() =>
                     {
                         _CreateNextFile();
                     })) { return; }
                    spaceRemaining = WAV_MAX_SIZE - currentWavLength;
                }

                //Write all that we can
                int writable = Math.Min(spaceRemaining, bytesRemaining);
                if (RunDiskIoOperation(() =>
                {
                    currentFile.Write(data, offset, writable);
                })) { return; }

                //Update values
                offset += writable;
                bytesRemaining -= writable;
                currentWavLength += writable;

                //Update length in file
                long pos = currentFile.Position;
                if (RunDiskIoOperation(() =>
                {
                    currentFile.Position = WAV_FILE_SIZE_OFFSET;
                    _WriteSignedInt(currentWavLength + 8);
                    currentFile.Position = WAV_DATA_SIZE_OFFSET;
                    _WriteSignedInt(currentWavLength);
                    currentFile.Position = pos;
                })) { return; }
                outOfDiskSpace = false;
            }
        }

        /// <summary>
        /// Runs an IO operation that may fail if the disk is out of storage. Attempts to repeat the call until it succeeds. Returns true to drop the buffer and return
        /// </summary>
        /// <param name="operation"></param>
        private bool RunDiskIoOperation(DiskIoOperation operation)
        {
            while(true)
            {
                //If we've aborted, stop
                if (aborted)
                    return true;

                //Attempt
                try
                {
                    /*if (File.ReadAllText("D:\\DISABLE.txt").StartsWith("1"))
                        throw new IOException();*/
                    operation();
                    return false; //succeeded!
                }
                catch (IOException)
                {
                    //Failed
                    outOfDiskSpace = true;

                    //Get the action to do
                    OutputMultitoolIoErrorAction action = HandleIOError();

                    //Handle
                    if (action == OutputMultitoolIoErrorAction.RetrySample)
                        Thread.Sleep(500);
                    else if (action == OutputMultitoolIoErrorAction.AbortRecording)
                    {
                        aborted = true;
                        throw new OutputMultitoolAbortException();
                    }
                    else
                        throw new Exception("Invalid Action");
                }
            }
        }

        private delegate void DiskIoOperation();

        private void _WriteTag(string tag)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(tag);
            currentFile.Write(bytes, 0, bytes.Length);
        }

        private void _WriteSignedInt(int value)
        {
            _WriteEndianBytes(BitConverter.GetBytes(value));
        }

        private void _WriteSignedShort(short value)
        {
            _WriteEndianBytes(BitConverter.GetBytes(value));
        }

        private void _WriteEndianBytes(byte[] data)
        {
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(data);
            currentFile.Write(data, 0, data.Length);
        }

        private void _WriteWavHeader()
        {
            //Calculate
            short blockAlign = (short)(channels * (bitsPerSample / 8));
            int avgBytesPerSec = sampleRate * (int)blockAlign;

            //Write
            _WriteTag("RIFF");
            _WriteSignedInt(0);
            _WriteTag("WAVE");
            _WriteTag("fmt ");
            _WriteSignedInt(16);
            _WriteSignedShort(1); //Format tag
            _WriteSignedShort(channels);
            _WriteSignedInt(sampleRate);
            _WriteSignedInt(avgBytesPerSec);
            _WriteSignedShort(blockAlign);
            _WriteSignedShort(bitsPerSample);
            _WriteTag("data");
            _WriteSignedInt(0);
        }

        /* Spanning file tools */

        private void _CreateNextFile()
        {
            //Close the current file
            if(currentFile != null)
            {
                currentFile.Flush();
                currentFile.Close();
            }
            
            //Create the next filename
            string filename;
            if (requestedFilename == null)
            {
                //We'll always save to a temp filename if we didn't have one requested
                filename = GetTempFilename();
            } else if (filenames.Count == 0)
            {
                //We can just use the requested filename
                filename = requestedFilename;
            } else
            {
                //Create an index of the requested name
                filename = GetIndexedFilename(requestedFilename, filenames.Count);
            }
            
            //Create new file
            currentFile = new FileStream(filename, FileMode.Create);
            filenames.Add(filename);

            //Write the WAV header
            _WriteWavHeader();

            //Reset values
            currentWavLength = 0;
        }

        public DriveInfo GetTempDiskLabel()
        {
            var f = new DirectoryInfo(Directory.GetCurrentDirectory());
            return new DriveInfo(f.Root.FullName);
        }

        /// <summary>
        /// Allows for spanning files. For example, if you needed to create a span of "TestFile.wav", this would return "TestFile_{index}.wav"
        /// </summary>
        /// <param name="original"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetIndexedFilename(string original, int index)
        {
            if(original.EndsWith(".wav"))
            {
                return original.Substring(0, original.Length - 4) + "_" + index + ".wav";
            } else
            {
                return original + "_" + index;
            }
        }

        public static string GetTempFilename()
        {
            int index = 0;
            while (File.Exists("TEMP_DATA_" + index + ".tmp"))
                index++;
            return "TEMP_DATA_" + index + ".tmp";
        }

        public static string OpenFilePicker(string fileExtension)
        {
            while(true)
            {
                //Prompt for output path
                SaveFileDialog fd = new SaveFileDialog();
                fd.Title = "Save Recording";
                fd.Filter = $"Output Files (*.{fileExtension})|*.{fileExtension}";

                //Open
                var result = fd.ShowDialog();
                if (result == DialogResult.OK)
                    return fd.FileName;

                //User cancelled. Prompt if they wish to delete the files
                result = MessageBox.Show("Are you sure you want to permanently delete the current recording?", "Cancel Recording", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    return null;
            }
        }

        /// <summary>
        /// Opens the file picker on the acitve form via invoke. This is to be used by other threads that can wait for user input.
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public static string OpenFilePickerInvoke(string fileExtension)
        {
            string result = null;
            bool done = false;
            Form.ActiveForm.Invoke((MethodInvoker)delegate
            {
                OpenFilePicker(fileExtension);
            });
            while (!done)
                Thread.Sleep(10);
            return result;
        }
    }

    public struct WriteIndexParams
    {
        public int offset;
        public int length;

        public WriteIndexParams(int offset, int length)
        {
            this.offset = offset;
            this.length = length;
        }
    }

    public class LockDunny
    {

    }

    public enum OutputMultitoolIoErrorAction
    {
        RetrySample,
        AbortRecording
    }

    public class OutputMultitoolAbortException : Exception
    {

    }
}
