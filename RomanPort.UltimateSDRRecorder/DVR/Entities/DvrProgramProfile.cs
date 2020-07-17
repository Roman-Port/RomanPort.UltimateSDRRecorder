using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.DVR.Entities
{
    public class DvrProgramProfile
    {
        public DateTime created_time;
        public bool is_disabled;

        public string program_title;

        public SdrProgramProfileTrigger trigger_type;
        public List<string> trigger_args = new List<string>();

        public bool change_freq_enabled;
        public int change_freq_khz;

        public bool record_iq; //False will record AF only
        public string output_path;
    }
}
