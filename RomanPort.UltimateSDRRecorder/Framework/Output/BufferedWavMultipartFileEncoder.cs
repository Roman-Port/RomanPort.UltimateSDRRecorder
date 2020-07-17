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
    public class BufferedWavMultipartFileEncoder : WavMultipartFileEncoder
    {
        public BufferedWavMultipartFileEncoder(int sampleRate) : base(sampleRate)
        {
            buffers = new ConcurrentQueue<byte[]>();
            recording = false;
            started = false;
        }

        private ConcurrentQueue<byte[]> buffers;
        private Thread worker;

        private EndRecordingCallbackArgs endRecordingCallback;

        public bool recording;
        public bool started;
        public bool ended;
        public long bytesWaiting;
        public long bytesWritten;

        public override void OnGetSamples(byte[] data)
        {
            //POSSIBLE BUG: We may want to copy the bytes here.
            buffers.Enqueue(data);
            bytesWaiting += data.Length;

            //Start if we haven't
            if (!started)
                StartRecording();
        }

        public override void OnEndSession()
        {
            EndRecordingAndWait();
            base.OnEndSession();
        }

        /// <summary>
        /// Begins the buffer
        /// </summary>
        public void StartRecording()
        {
            if (recording == true)
                return;
            recording = true;
            started = true;
            worker = new Thread(() =>
            {
                _WorkerThread();
            });
            worker.IsBackground = true;
            worker.Start();
        }

        /// <summary>
        /// Safely ends recording. Responds with a callback when bytes are finished being written. Note that this calls back on a different thread
        /// </summary>
        public void EndRecording(EndRecordingCallbackArgs callback)
        {
            ended = true;
            endRecordingCallback = callback;
            recording = false;
        }

        /// <summary>
        /// Safely ends the recording. Will hang the current thread until all bytes are finished being written
        /// </summary>
        public void EndRecordingAndWait()
        {
            bool wait = true;
            EndRecording(() =>
            {
                wait = false;
            });
            while (wait) ;
        }

        private void _WorkerThread()
        {
            byte[] buf;
            while (recording)
            {
                //Continuously read buffers and write them to the output stream
                while (!buffers.TryDequeue(out buf) && recording)
                    Thread.Sleep(5);
                if (!recording)
                    break;
                base.OnGetSamples(buf);
                bytesWaiting -= buf.Length;
                bytesWritten += buf.Length;
            }

            //End
            endRecordingCallback();
        }

        public delegate void EndRecordingCallbackArgs();
    }
}
