using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.Framework
{
    public class RecorderSettings
    {
        public float amplification = 1;
        public int rewind_buffer_length = 60;
        public string rds_autoname_output_dir = "";
    }
}
