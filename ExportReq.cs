using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;

namespace WPF
{
    static class ExportReq
    {
        static public void create(List<UcitelClass> uc)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "rozvrh"; // Default file name
            dlg.DefaultExt = ".text"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension 

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                // Save document 
                string path = dlg.FileName;
                    string pom = "";
                    foreach (UcitelClass u in uc)
                    {
                        pom = pom + u.Name + Environment.NewLine;
                        for (int i = 0; i < 14; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                pom = pom + u.nope[i, j] + " ";
                            }
                            pom = pom + Environment.NewLine;
                        }
                    }
                    /*Byte[] info = new AN
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);*/

                    StreamWriter sw = new StreamWriter(path, true, Encoding.GetEncoding(1252));
                    sw.Write(pom);
                    sw.Close();
            }
        }
    }
}
