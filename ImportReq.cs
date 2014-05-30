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

                UcitelClass u;
                int poc = 0; // urcuje ci sme na riadku s menom alebo na riadku s hodnotami
                string[] pom;
                int ucit =0;
                for (int kl = 0; kl < readText.Length ; kl++ )
                {
                    if (kl % 15 == 0)
                    {
                        bool nic = true; //nenasla sa zhoda mena so zoznamom;
                        for (int i = 0; i < uc.Count; i++)
                        {
                            if (uc[i].Name == readText[kl])
                            {
                                Console.WriteLine(readText[kl]);
                                nic = false;
                                ucit = i;
                                break;
                            }
                        }
                        if (nic)
                        {
                            kl += 14;//preskocime daneho ucitela pretoze ho nemame v tabulke
                            continue;
                        }

                    }
                    if (kl % 15 != 0)
                    {
                        pom = readText[kl].Split(' ');
                        for (int i = 0; i < 5; i++)
                        {
                            uc[ucit].nope[(kl % 15) - 1, i] = Convert.ToBoolean(pom[i]);
                        }
                    }

                }
            }

            
        }
    }
}
