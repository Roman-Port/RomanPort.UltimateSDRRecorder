using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RomanPort.UltimateSDRRecorder.Framework;
using RomanPort.UltimateSDRRecorder.Framework.Ui;
using SDRSharp.Common;

namespace RomanPort.UltimateSDRRecorder
{
    public partial class MainControl : UserControl
    {
        public MainControl()
        {
            InitializeComponent();
        }

        public RecorderControl GetAudioRecorder()
        {
            return audioRecorder;
        }

        public RecorderControl GetBasebandRecorder()
        {
            return basebandRecorder;
        }

        public void ConfigureDvr(ISharpControl control, UltimateRecorder audioRecorder, UltimateRecorder basebandRecorder)
        {
            sdrDvr.Init(control, audioRecorder, basebandRecorder);
        }
    }
}
