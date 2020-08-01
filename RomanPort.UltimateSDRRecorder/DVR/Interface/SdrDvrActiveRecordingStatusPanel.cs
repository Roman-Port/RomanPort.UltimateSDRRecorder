using RomanPort.UltimateSDRRecorder.DVR.Entities;
using RomanPort.UltimateSDRRecorder.DVR.Program;
using SDRSharp.Radio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            //Update program list
            programList.SuspendLayout();
            programList.Items.Clear();
            foreach (var p in dvrInterface.programs)
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

            //Update history list
            eventListView.SuspendLayout();
            eventListView.Items.Clear();
            foreach (var p in dvrInterface.config.events)
            {
                DateTime time = p.GetTime();
                TimeSpan length = p.GetLength();
                var row = new string[] { $"{time.ToShortDateString()} {time.ToLongTimeString()}", p.program_name, $"{(length.Hours + (length.Days * 24)).ToString().PadLeft(2, '0')}:{length.Minutes.ToString().PadLeft(2, '0')}:{length.Seconds.ToString().PadLeft(2, '0')}", $"{Math.Round((double)p.recording_size / 1024 / 1024, 1) } MB", p.rds_radio_text };
                var lv = new ListViewItem(row);
                lv.Tag = p;
                if (!CheckIfEventFileIsValid(p))
                    lv.BackColor = Color.FromArgb(245, 71, 71);
                eventListView.Items.Add(lv);
            }
            UpdateEventButtonStatus();
            eventListView.ResumeLayout();
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

        private void eventListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEventButtonStatus();
        }

        /// <summary>
        /// Checks if the file for the DVR Event exists and is the correct file
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool CheckIfEventFileIsValid(DvrPastEvent e)
        {
            if (!File.Exists(e.file_path))
                return false;
            return new FileInfo(e.file_path).Length - Framework.Output.WavEncoder.WAV_HEADER_SIZE == e.recording_size; //Simple check to make sure that this is the *same* file. The likelyhood of us generating a file with the same name & length is very very low
        }

        private void UpdateEventButtonStatus()
        {
            bool enabled = eventListView.SelectedItems.Count == 1;
            eventMoveBtn.Enabled = enabled;
            deleteEventBtn.Enabled = enabled;
        }

        private void deleteEventBtn_Click(object sender, EventArgs e)
        {
            //Get the event
            var recordingEvent = (DvrPastEvent)eventListView.SelectedItems[0].Tag;

            //Check if we can remove the file
            bool fileValid = CheckIfEventFileIsValid(recordingEvent);
            if(fileValid)
            {
                //Prompt
                var result = MessageBox.Show($"Are you sure you want to remove this recording? This will delete the recorded file at \"{recordingEvent.file_path}\".", "Delete Recording", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(result == DialogResult.Yes)
                {
                    //Delete it from the filesystem
                    File.Delete(recordingEvent.file_path);
                    
                    //Remove it from the list
                    dvrInterface.config.events.Remove(recordingEvent);
                    dvrInterface.SaveConfig();
                }
            } else
            {
                //Remove it from the list
                dvrInterface.config.events.Remove(recordingEvent);
                dvrInterface.SaveConfig();

                //Show dialog
                MessageBox.Show($"Recording event was removed, however the recording file couldn't be found.", "Recording Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Update UI
            RefreshList();
        }

        private void eventMoveBtn_Click(object sender, EventArgs e)
        {
            //Get the event
            var recordingEvent = (DvrPastEvent)eventListView.SelectedItems[0].Tag;

            //Check if we can remove the file
            if (!CheckIfEventFileIsValid(recordingEvent))
            {
                //Can't find this file
                MessageBox.Show($"The file for this recording located at \"{recordingEvent.file_path}\" was not found or was invalid and cannot be moved.", "Recording Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Prompt where to put it
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "Save Location";
            fd.Filter = "WAV files (*.wav)|*.wav";
            var result = fd.ShowDialog();

            //Save
            if(result == DialogResult.OK)
            {
                //Move
                try
                {
                    File.Move(recordingEvent.file_path, fd.FileName);
                } catch (Exception ex)
                {
                    MessageBox.Show("There was an error moving the file: " + ex.Message, "Move Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Remove it from the list
                dvrInterface.config.events.Remove(recordingEvent);
                dvrInterface.SaveConfig();
            }

            //Update interface
            RefreshList();
        }
    }
}
