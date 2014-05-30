using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace WPF
{
    static class ImportReq
    {
        static public void load(List<UcitelClass> uc)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            Nullable<bool> result = openFileDialog1.ShowDialog();
            if (result == true)
            {

                Console.WriteLine("Opening: " + openFileDialog1.FileName);
                string[] readText = File.ReadAllLines(openFileDialog1.FileName, Encoding.Default);
                int riadok =-1;
                UcitelClass u;
                foreach (string line in readText)
                {
                    if (riadok == -1)
                    {
                        for (int i = 0; i < uc.Count; i++)
                        {
                            if (uc[i].Name == line)
                            {
                                u = uc[i];
                                riadok++;
                                break;
                            }
                        }
                    }
                    if (riadok != -1)
                    {

                    }

                }
            }

            
        }
    }
}
