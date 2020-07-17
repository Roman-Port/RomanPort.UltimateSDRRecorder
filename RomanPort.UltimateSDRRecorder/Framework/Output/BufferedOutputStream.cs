using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.Framework.Output
{
    public class BufferedOutputStream : IOneWayStream
    {
        private ConcurrentQueue<byte[]> buffers;
        private Thread worker;

        private EndRecordingCallbackArgs endRecordingCallback;

        public bool recording;
        public long bytesWaiting;
        public long bytesWritten;

        public BufferedOutputStream(Stream underlyingStream) : base(underlyingStream)
        {
            buffers = new ConcurrentQueue<byte[]>();
            StartRecording();
        }

        /// <summary>
        /// Begins the buffer
        /// </summary>
        public void StartRecording()
        {
            recording = true;
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
            endRecordingCallback = callback;
            recording = false;
        }

        public void EndRecordingAndWait()
        {
            bool wait = true;
            EndRecording(() =>
            {
                wait = false;
            });
            while (wait) ;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            byte[] b = new byte[count];
            bytesWaiting += count;
            Array.Copy(buffer, offset, b, 0, count);
            buffers.Enqueue(b);
        }

        private void _WorkerThread()
        {
            byte[] buf;
            while(recording)
            {
                //Continuously read buffers and write them to the output stream
                while (!buffers.TryDequeue(out buf) && recording)
                    Thread.Sleep(5);
                if (!recording)
                    break;
                underlyingStream.Write(buf, 0, buf.Length);
                bytesWaiting -= buf.Length;
                bytesWritten += buf.Length;
            }

            //End
            endRecordingCallback();
        }

        public delegate void EndRecordingCallbackArgs();
    }
}
