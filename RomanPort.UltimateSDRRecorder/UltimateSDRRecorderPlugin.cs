using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using SDRSharp.Common;
using RomanPort.UltimateSDRRecorder.Framework;
using RomanPort.UltimateSDRRecorder.Framework.Swap;
using RomanPort.UltimateSDRRecorder.Framework.Sources;
using System.Threading;

namespace RomanPort.UltimateSDRRecorder
{
    public class UltimateSDRRecorderPlugin : ISharpPlugin
    {
        private const string _displayName = "Ultimate SDR Recorder";
        private ISharpControl _control;
        private MainControl _guiControl;

        public UltimateRecorder audioRecorderPlugin;
        public UltimateRecorder basebandRecorderPlugin;

        public UserControl Gui
        {
            get { return _guiControl; }
        }

        public string DisplayName
        {
            get { return _displayName; }
        }

        public void Close()
        {

        }

        public void Initialize(ISharpControl control)
        {
            _control = control;
            _guiControl = new MainControl();

            //Prompt tp delete temp files
            PromptDeleteTempFiles();

            //Create recorders
            audioRecorderPlugin = new UltimateRecorder(control, _guiControl.GetAudioRecorder(), new MemorySwap(2048), new AudioSource(), "Audio");
            basebandRecorderPlugin = new UltimateRecorder(control, _guiControl.GetBasebandRecorder(), new MemorySwap(2048), new BasebandSource(), "Baseband");

            //Create DVR
            _guiControl.ConfigureDvr(control, audioRecorderPlugin, basebandRecorderPlugin);
        }

        private void PromptDeleteTempFiles()
        {
            //Detect any remaining temp files and total their size
            int tempFileCount = 0;
            long tempFileSize = 0;
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            List<FileInfo> tempFiles = new List<FileInfo>();
            foreach (var f in files)
            {
                var info = new FileInfo(f);
                if (info.Name.StartsWith("TEMP_DATA_"))
                {
                    tempFileCount++;
                    tempFileSize += info.Length;
                    tempFiles.Add(info);
                }
            }

            //Prompt
            if (tempFileCount > 0)
            {
                Thread t = new Thread(() =>
                {
                    var r = MessageBox.Show($"There are {tempFileCount} unsaved reccordings, making up {Math.Round((decimal)tempFileSize / 1024 / 1024, 1)} MB. Is it OK to delete these recordings?", "Unsaved Recordings", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (r == DialogResult.Yes)
                    {
                        foreach (var f in tempFiles)
                            f.Delete();
                    }
                });
                t.IsBackground = true;
                t.Start();
            }
        }
    }
}
