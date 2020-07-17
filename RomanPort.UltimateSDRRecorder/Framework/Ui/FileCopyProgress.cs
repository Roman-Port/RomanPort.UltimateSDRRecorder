using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder.Framework.Ui
{
    public partial class FileCopyProgress : Form
    {
        public FileCopyProgress()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Takes in two arrays of fully qualified paths (C:\Users\Roman\Desktop\test.wav, for example)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public void BeginCopy(FileInfo[] source, string[] dest)
        {
            var t = new Thread(() =>
            {
                //Pause shortly to allow the FS to realize we've stopped using the file
                Thread.Sleep(800);
                
                //Count up the number of bytes
                long totalBytes = 0;
                long movedBytes = 0;
                foreach (var s in source)
                {
                    totalBytes += source[0].Length;
                }

                //Loop through
                for (int i = 0; i<source.Length; i+=1)
                {
                    //Update
                    UpdateUi(totalBytes, movedBytes, source.Length, i);

                    //Check the drive letters of both of these. If they match, use the quick move
                    if(source[i].FullName.Substring(0, source[i].FullName.IndexOf(':')) == dest[i].Substring(0, dest[i].IndexOf(':')))
                    {
                        //Exist on the same volume
                        File.Move(source[i].FullName, dest[i]);
                        movedBytes += new FileInfo(dest[i]).Length;
                        UpdateUi(totalBytes, movedBytes, source.Length, i);
                    } else
                    {
                        //Slow move
                        byte[] buffer = new byte[16384];
                        using(FileStream s = new FileStream(source[i].FullName, FileMode.Open))
                        using (FileStream d = new FileStream(dest[i], FileMode.Open))
                        {
                            int read = -1;
                            while(read != 0)
                            {
                                read = s.Read(buffer, 0, buffer.Length);
                                d.Write(buffer, 0, read);
                                movedBytes += read;
                                UpdateUi(totalBytes, movedBytes, source.Length, i);
                            }
                        }

                        //Delete source
                        File.Delete(source[i].FullName);
                    }
                }
                UpdateUi(totalBytes, movedBytes, source.Length, source.Length);

                //Close form
                Invoke((MethodInvoker) delegate
                {
                    //Close();
                });
            });
            t.IsBackground = true;
            t.Start();
            BringToFront();
        }

        public void UpdateUi(long totalBytes, long copiedBytes, int fileCount, int fileIndex)
        {
            Invoke((MethodInvoker)delegate
            {
                copyTitle.Text = $"Copying file {fileIndex + 1}/{fileCount} - {BytesToMegabytes(copiedBytes)}/{BytesToMegabytes(totalBytes)} MB";
                transferStatus.Value = (int)((copiedBytes / totalBytes) * 100);
            });
        }

        private string BytesToMegabytes(long bytes)
        {
            return Math.Round((double)bytes / 1000 / 1000, 1).ToString();
        }
    }
}
