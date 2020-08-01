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
using System.Net.Http;
using System.Net;
using System.Xml.Serialization;
using RomanPort.UltimateSDRRecorder.Updater;
using System.Xml;

namespace RomanPort.UltimateSDRRecorder
{
    public class UltimateSDRRecorderPlugin : ISharpPlugin
    {
        private const string _displayName = "Ultimate SDR Recorder";
        private ISharpControl _control;
        private MainControl _guiControl;

        public PluginConfigFile config;

        public UltimateRecorder audioRecorderPlugin;
        public UltimateRecorder basebandRecorderPlugin;

        public const int CURRENT_VERSION = 1;
        public const string UPDATE_URL = "https://raw.githubusercontent.com/Roman-Port/RomanPort.UltimateSDRRecorder/master/updater.xml";

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
            _guiControl = new MainControl(this);

            //Load config
            config = ConfigFileManager.LoadConfigFile<PluginConfigFile>(ConfigFileManager.SAVEKEY_APP);
            if (config == null)
                config = new PluginConfigFile();

            //Prompt tp delete temp files
            PromptDeleteTempFiles();

            //Create recorders
            audioRecorderPlugin = new UltimateRecorder(control, _guiControl.GetAudioRecorder(), new MemorySwap(2048), new AudioSource(), "Audio");
            basebandRecorderPlugin = new UltimateRecorder(control, _guiControl.GetBasebandRecorder(), new MemorySwap(2048), new BasebandSource(), "Baseband");

            //Create DVR
            _guiControl.ConfigureDvr(control, audioRecorderPlugin, basebandRecorderPlugin);

            //Look for updates
            CheckForUpdates();
        }

        public void SaveConfig()
        {
            ConfigFileManager.SaveConfigFile(ConfigFileManager.SAVEKEY_APP, config);
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
                    Thread.Sleep(3000); //Allow the UI to finish loading
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

        private void CheckForUpdates()
        {
            //Check to see if we should look for updates
            if (!config.check_updates)
                return;
            
            //Checks for updates in a new thread
            Thread t = new Thread(() =>
            {
                //Fetch
                AppUpdateData update = null;
                try
                {
                    //Request
                    WebClient wc = new WebClient();
                    string response = wc.DownloadString(UPDATE_URL + "?cache=" + DateTime.UtcNow.Ticks + "&current_version=" + CURRENT_VERSION);

                    //Deserialize
                    XmlSerializer ser = new XmlSerializer(typeof(AppUpdateData));
                    using(StringReader sr = new StringReader(response))
                    using (XmlReader reader = XmlReader.Create(sr))
                    {
                        update = (AppUpdateData)ser.Deserialize(reader);
                    }
                } catch
                {
                    return;
                }

                //Check
                if (update == null)
                    return;
                if (update.latest_version <= CURRENT_VERSION)
                    return;
                if (update.latest_version <= config.latest_skipped_version)
                    return;

                //There's an update!
                _guiControl.Invoke((MethodInvoker)delegate
                {
                    _guiControl.OnUpdateReady(update);
                });
            });
            t.IsBackground = true;
            t.Start();
        }
    }
}
