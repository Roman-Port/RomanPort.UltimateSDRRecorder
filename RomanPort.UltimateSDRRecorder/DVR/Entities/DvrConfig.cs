using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.UltimateSDRRecorder.DVR.Entities
{
    public class DvrConfig
    {
        public List<DvrProgramProfile> profiles = new List<DvrProgramProfile>();
        public bool show_error_on_no_profiles = true;
    }
}
