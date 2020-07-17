using RomanPort.UltimateSDRRecorder.Framework.Ui;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.UltimateSDRRecorder.Framework.Output
{
    /// <summary>
    /// Serves as a way for us to not have to name files when we hit record, only when we stop recording. Stores files in a temp container until we're ready
    /// </summary>
    public abstract class IUltimateOutputTempFileEncoder : IUltimateOutput
    {
        public IUltimateOutputTempFileEncoder(int sampleRate, string fileExtension) : base(sampleRate)
        {
            this.fileExtension = fileExtension;
            files = new List<string>();
        }

        public List<string> files;
        public string fileExtension;

        public abstract void CloseActiveFile();

        public override void OnEndSession()
        {
            //Close the active file
            CloseActiveFile();

            //If there are no files, do nothing
            if (files.Count == 0)
                return;

            //Prompt where to move the files to
            string location = OpenFilePicker(fileExtension);

            //Deal with output
            if(location == null)
            {
                //If the user cancelled, delete
                foreach (var s in files)
                    File.Delete(s);
            } else
            {
                //Get base path
                string basePath = location.Substring(0, location.Length - fileExtension.Length - 1);

                //Determine where to move the files
                FileInfo[] source = new FileInfo[files.Count];
                string[] dest = new string[files.Count];
                if(files.Count == 1)
                {
                    //We can just use the original path
                    source[0] = new FileInfo(files[0]);
                    dest[0] = basePath + "." + fileExtension;
                } else
                {
                    //Copy to parts
                    for (int i = 0; i < files.Count; i++)
                    {
                        source[i] = new FileInfo(files[i]);
                        dest[i] = basePath + "_PT" + i + "." + fileExtension;
                    }
                }

                //Start
                MoveFiles(source, dest);
            }
        }

        public void MoveFiles(FileInfo[] source, string[] dest)
        {
            Console.WriteLine("BEGINNING FILE MOVE...");
            for (int i = 0; i < source.Length; i++)
                File.Move(source[i].FullName, dest[i]);
            Console.WriteLine("FILE MOVE COMPLETED");
        }

        public FileStream RequestNextFile()
        {
            //Create new file
            string filename = GetTempFilename();
            FileStream fs = new FileStream(filename, FileMode.Create);

            //Add to files
            files.Add(filename);

            return fs;
        }

        public static string GetTempFilename()
        {
            int index = 0;
            while (File.Exists("TEMP_DATA_" + index + ".tmp"))
                index++;
            return "TEMP_DATA_" + index + ".tmp";
        }

        public static string OpenFilePicker(string fileExtension)
        {
            //Prompt for output path
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "Save Output";
            fd.Filter = $"Output Files (*.{fileExtension})|*.{fileExtension}";

            //Open
            var result = fd.ShowDialog();
            if (result == DialogResult.OK)
            {
                return fd.FileName;
            }
            else
            {
                return null;
            }
        }
    }
}
