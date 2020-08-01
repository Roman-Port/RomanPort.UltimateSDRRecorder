using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.DVR.Entities
{
    public class DvrPastEvent
    {
        public long occured_at;
        public string program_name;
        public int recording_length_seconds;
        public long recording_size;
        public string rds_radio_text;
        public string file_path;

        public DateTime GetTime()
        {
            return new DateTime(occured_at, DateTimeKind.Utc);
        }

        public TimeSpan GetLength()
        {
            return new TimeSpan(0, 0, recording_length_seconds);
        }
    }
}
