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
using RomanPort.UltimateSDRRecorder.Updater;

namespace RomanPort.UltimateSDRRecorder
{
    public partial class MainControl : UserControl
    {
        public MainControl(UltimateSDRRecorderPlugin plugin)
        {
            this.plugin = plugin;
            InitializeComponent();
            SetUpdateBtnStatus(false);
        }

        public UltimateSDRRecorderPlugin plugin;
        private AppUpdateData update;

        public void OnUpdateReady(AppUpdateData update)
        {
            this.update = update;
            SetUpdateBtnStatus(true);
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

        private void SetUpdateBtnStatus(bool canUpdate)
        {
            updateMsgBtn.Visible = canUpdate;
            updateMsgText.Visible = canUpdate;
            appSettingsBtn.Visible = !canUpdate;
        }

        private void appSettingsBtn_Click(object sender, EventArgs e)
        {
            AppSettingsDialog fd = new AppSettingsDialog();
            fd.ShowDialog();
        }

        private void updateMsgBtn_Click(object sender, EventArgs e)
        {
            new AppUpdatePrompt(plugin, update).ShowDialog();
            SetUpdateBtnStatus(false);
        }
    }
}
