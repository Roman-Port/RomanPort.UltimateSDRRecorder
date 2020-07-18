using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder.Framework
{
    public static class RecorderTools
    {
        public static void NotifyUserNewThread(string title, string subtitle, MessageBoxIcon icon)
        {
            var t = new Thread(() =>
            {
                MessageBox.Show(subtitle, title, MessageBoxButtons.OK, icon);
            });
            t.IsBackground = true;
            t.Start();
        }
    }
}
