using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RomanPort.UltimateSDRRecorder.DVR.Program;
using RomanPort.UltimateSDRRecorder.DVR.Entities;
using RomanPort.UltimateSDRRecorder.Framework;
using System.Timers;
using SDRSharp.Common;

namespace RomanPort.UltimateSDRRecorder.DVR.Interface
{
    public partial class SdrDvrInterface : UserControl
    {
        public SdrDvrInterface()
        {
            InitializeComponent();
            programs = new List<DvrProgram>();
            recordingPrograms = new List<DvrProgram>();
            config = new DvrConfig();
            UpdateInterface();
        }

        public List<DvrProgram> programs;
        private List<DvrProgram> recordingPrograms;
        public DvrConfig config;

        private ISharpControl control;
        private UltimateRecorder audioRecorder;
        private UltimateRecorder basebandRecorder;

        private System.Timers.Timer tickTimer;
        private int newRecordings;

        public void Init(ISharpControl control, UltimateRecorder audioRecorder, UltimateRecorder basebandRecorder)
        {
            //Set recorders
            this.control = control;
            this.audioRecorder = audioRecorder;
            this.basebandRecorder = basebandRecorder;

            //Load config
            config = ConfigFileManager.LoadConfigFile<DvrConfig>(ConfigFileManager.SAVEKEY_DVR);
            if (config == null)
                config = new DvrConfig();
            UpdateInterface();

            //Set timer
            tickTimer = new System.Timers.Timer(1000);
            tickTimer.AutoReset = true;
            tickTimer.Elapsed += TickTimer_Elapsed;
            tickTimer.Start();

            //Create all of the programs that already exist
            foreach (var p in config.profiles)
                InitProgram(p);
        }

        private void TickTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                //Tick all programs
                foreach (var p in programs)
                {
                    if (!p.profile.is_disabled)
                        p.Tick(control);
                }

                //Update interface
                UpdateInterface();
            } catch
            {
                //Ignore, as we likely just lost the window
            }
        }

        public void SaveConfig()
        {
            ConfigFileManager.SaveConfigFile<DvrConfig>(ConfigFileManager.SAVEKEY_DVR, config);
        }

        public void OnProgramBegin(DvrProgram program)
        {
            recordingPrograms.Add(program);
            OnRecordingProgramsChanged();
        }

        public void OnProgramEnd(DvrProgram program)
        {
            //End
            recordingPrograms.Remove(program);

            //Add to history
            newRecordings++;
            config.events.Add(new DvrPastEvent
            {
                occured_at = program.startedRecordingAt.Ticks,
                program_name = program.profile.program_title,
                recording_length_seconds = (int)program.GetTimeRecording().TotalSeconds,
                recording_size = program.GetBytesWritten(),
                rds_radio_text = program.recordingRadioText,
                file_path = program.recordingFilepath
            });
            SaveConfig();

            //Update UI
            OnRecordingProgramsChanged();
        }

        public void OnRecordingProgramsChanged()
        {
            UpdateInterface();
        }

        public void AddProgram(DvrProgramProfile p)
        {
            //Add to config and save
            config.profiles.Add(p);
            SaveConfig();

            //Init
            InitProgram(p);
        }

        public void InitProgram(DvrProgramProfile p)
        {
            //Update
            UltimateRecorder rec;
            if (p.record_iq)
                rec = basebandRecorder;
            else
                rec = audioRecorder;
            programs.Add(new DvrProgram(p, rec, this));

            //Update interface
            UpdateInterface();
        }

        private void UpdateInterface()
        {
            //Check if there are any programs added
            if(programs.Count == 0)
            {
                SetStatusLight(config.show_error_on_no_profiles ? StatusLightColor.Error : StatusLightColor.Idle);
                SetStatusText("No programs registered.");
                return;
            }

            //Calculate total number of bytes in-air.
            long totalBytesWaiting = 0;
            foreach (var p in programs)
                totalBytesWaiting += p.GetBytesWaiting();
            bool waitingBytesWarning = totalBytesWaiting >= 1500000; //Warning threshold
            string waitingBytesString = Math.Round((double)totalBytesWaiting / 1024 / 1024, 1).ToString() + " MB";

            //Continue as usual
            if (recordingPrograms.Count == 0)
            {
                SetStatusLight(newRecordings == 0 ? StatusLightColor.Idle : StatusLightColor.NewRecording);
                SetStatusText(newRecordings == 0 ? "Ready. Waiting for start..." : $"Ready. {newRecordings} NEW recording{(newRecordings != 1 ? "s" : "")}.");
            } else if (waitingBytesWarning)
            {
                SetStatusLight(StatusLightColor.Error);
                SetStatusText($"Rec. {recordingPrograms.Count}. {waitingBytesString} waiting!");
            } else if (recordingPrograms.Count == 1)
            {
                SetStatusLight(StatusLightColor.Recording);
                TimeSpan time = recordingPrograms[0].GetTimeRecording();
                SetStatusText($"Rec. {recordingPrograms[0].GetTimeRecordingString()} - {Math.Round((double)recordingPrograms[0].GetBytesWritten() / 1024 / 1024, 1) } MB");
            } else 
            {
                SetStatusLight(StatusLightColor.Recording);
                SetStatusText($"Recording {recordingPrograms.Count} programs.");
            }
        }

        private StatusLightColor currentLight = StatusLightColor.Idle;

        private void SetStatusLight(StatusLightColor color)
        {
            if (color == currentLight)
                return;
            currentLight = color;
            switch(color)
            {
                case StatusLightColor.Idle: statusLight.ForeColor = Color.FromArgb(255, 255, 255); break;
                case StatusLightColor.Recording: statusLight.ForeColor = Color.FromArgb(95, 242, 75); break;
                case StatusLightColor.Error: statusLight.ForeColor = Color.FromArgb(255, 89, 74); break;
                case StatusLightColor.NewRecording: statusLight.ForeColor = Color.FromArgb(74, 222, 255); break;
            }
        }

        private void SetStatusText(string text)
        {
            dvrStatus.Text = text;
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            newRecordings = 0;
            var f = new SdrDvrActiveRecordingStatusPanel(this);
            var result = f.ShowDialog();
        }
    }
}
