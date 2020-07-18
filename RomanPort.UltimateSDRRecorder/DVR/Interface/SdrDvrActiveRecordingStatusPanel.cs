using RomanPort.UltimateSDRRecorder.DVR.Entities;
using RomanPort.UltimateSDRRecorder.DVR.Program;
using SDRSharp.Radio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder.DVR.Interface
{
    public partial class SdrDvrActiveRecordingStatusPanel : Form
    {
        public SdrDvrInterface dvrInterface;

        private System.Timers.Timer quickUpdateTimer;

        public SdrDvrActiveRecordingStatusPanel(SdrDvrInterface dvrInterface)
        {
            InitializeComponent();
            this.dvrInterface = dvrInterface;
        }

        private void SdrDvrActiveRecordingStatusPanel_Load(object sender, EventArgs e)
        {
            RefreshList();

            //Set quick update timer
            quickUpdateTimer = new System.Timers.Timer(500);
            quickUpdateTimer.AutoReset = true;
            quickUpdateTimer.Elapsed += QuickUpdateTimer_Elapsed;
            quickUpdateTimer.Start();
        }

        private void QuickUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            programList.SuspendLayout();
            foreach(var r in programList.Items)
            {
                //Get item and program
                var lv = (ListViewItem)r;
                DvrProgram p = (DvrProgram)lv.Tag;

                //Update
                try
                {
                    lv.SubItems[4].Text = GetStatusText(p);
                    lv.SubItems[5].Text = p.isRecording ? p.GetTimeRecordingString() : "";
                    lv.SubItems[6].Text = p.isRecording ? $"{Math.Round((double)p.GetBytesWritten() / 1024 / 1024, 1) } MB" : "";
                    lv.BackColor = GetBackgroundColor(p);
                } catch (Exception ex)
                {
                    //Ignore. Sometimes, if we line up exactly, the form will close while this is executing and case an error
                }
            }
            programList.ResumeLayout();
        }

        public void RefreshList()
        {
            programList.Items.Clear();
            programList.SuspendLayout();
            foreach(var p in dvrInterface.programs)
            {
                if (p.isDeleted)
                    continue;
                var row = new string[] { p.profile.program_title, GetTriggerName(p.profile.trigger_type), p.profile.record_iq ? "IQ" : "AF", p.profile.change_freq_enabled ? p.profile.change_freq_khz.ToString() + " kHz" : "No", GetStatusText(p), p.isRecording ? p.GetTimeRecordingString() : "", p.isRecording ? $"{Math.Round((double)p.GetBytesWritten() / 1024 / 1024, 1) } MB" : "" };
                var lv = new ListViewItem(row);
                lv.BackColor = GetBackgroundColor(p);
                lv.Tag = p;
                programList.Items.Add(lv);
            }
            programList.ResumeLayout();
        }

        private static Color GetBackgroundColor(DvrProgram p)
        {
            if (p.isRecording)
                return Color.FromArgb(95, 242, 75);
            else if (p.profile.is_disabled)
                return Color.FromArgb(184, 184, 184);
            else if (p.isCancelled)
                return Color.FromArgb(224, 7, 116);
            else
                return Color.Transparent;
        }

        private static string GetStatusText(DvrProgram p)
        {
            if (p.isRecording)
                return "Recording...";
            else if (p.profile.is_disabled)
                return "Disabled";
            else if (p.isCancelled)
                return "Stopped";
            else
                return "Waiting...";
        }

        private static string GetTriggerName(SdrProgramProfileTrigger trigger)
        {
            switch(trigger)
            {
                case SdrProgramProfileTrigger.NullTrigger: return "Null";
                case SdrProgramProfileTrigger.RDSRadioTextTrigger: return "RDS RadioText";
                case SdrProgramProfileTrigger.TimeTrigger: return "Scheduled";
            }
            return "INVALID";
        }

        private void createProgramBtn_Click(object sender, EventArgs e)
        {
            var f = new SdrDvrProgramCreationPanel();
            var result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Save
                quickUpdateTimer.Stop();
                dvrInterface.AddProgram(f.profile);
                RefreshList();
                quickUpdateTimer.Start();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(programList.SelectedItems.Count > 0)
            {
                //Get program
                DvrProgram program = (DvrProgram)programList.SelectedItems[0].Tag;

                //Set active buttons
                UpdateButtons(program);
            } else
            {
                //Set active buttons
                ClearButtons();
            }
        }

        private void UpdateButtons(DvrProgram program)
        {
            deleteProgramBtn.Enabled = !program.isRecording;
            toggleStatusProgramBtn.Enabled = !program.isRecording;
            toggleStatusProgramBtn.Text = program.profile.is_disabled ? "Enable" : "Disable";
            stopProgramBtn.Enabled = program.isRecording;
        }

        private void ClearButtons()
        {
            deleteProgramBtn.Enabled = false;
            toggleStatusProgramBtn.Enabled = false;
            toggleStatusProgramBtn.Text = "Disable";
            stopProgramBtn.Enabled = false;
        }

        private void stopProgramBtn_Click(object sender, EventArgs e)
        {
            //Get program
            DvrProgram program = (DvrProgram)programList.SelectedItems[0].Tag;
            program.CancelRecording();
            MessageBox.Show("This program will begin recording next time it is triggered.", $"Program Recording Stopped", MessageBoxButtons.OK);
            UpdateButtons(program);
        }

        private void disableProgramBtn_Click(object sender, EventArgs e)
        {
            //Get program
            DvrProgram program = (DvrProgram)programList.SelectedItems[0].Tag;
            if(!program.profile.is_disabled)
            {
                //Do disable
                program.DisableProgram();
                MessageBox.Show("This program will not trigger until you reenable it.", $"Program Disabled", MessageBoxButtons.OK);
            } else
            {
                //Do enable
                program.EnableProgram();
                MessageBox.Show("This program will begin recording when triggered.", $"Program Enabled", MessageBoxButtons.OK);
            }
            UpdateButtons(program);
        }

        private void deleteProgramBtn_Click(object sender, EventArgs e)
        {
            //Get program
            DvrProgram program = (DvrProgram)programList.SelectedItems[0].Tag;
            if (MessageBox.Show("You can't recover a deleted program.", $"Delete {program.profile.program_title}?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //Delete program
                program.DeleteProgram();

                //Force a full refresh
                RefreshList();
            }
            UpdateButtons(program);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SdrDvrActiveRecordingStatusPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (quickUpdateTimer.Enabled)
                quickUpdateTimer.Stop();
        }
    }
}
