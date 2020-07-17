using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDRSharp.Common;
using RomanPort.UltimateSDRRecorder.Framework;
using RomanPort.UltimateSDRRecorder.Framework.Swap;
using RomanPort.UltimateSDRRecorder.Framework.Sources;

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

            //Create recorders
            audioRecorderPlugin = new UltimateRecorder(control, _guiControl.GetAudioRecorder(), new MemorySwap(2048), new AudioSource(), "Audio");
            basebandRecorderPlugin = new UltimateRecorder(control, _guiControl.GetBasebandRecorder(), new MemorySwap(2048), new BasebandSource(), "Baseband");

            //Create DVR
            _guiControl.ConfigureDvr(control, audioRecorderPlugin, basebandRecorderPlugin);
        }
    }
}
