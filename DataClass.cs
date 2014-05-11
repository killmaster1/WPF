using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    class DataClass
    {
        public Dictionary<string, string[,]> d;
        public List<UcitelClass> uc;

        public DataClass()
        {
            d = new Dictionary<string,string[,]>();
        }

        public void loadFileData(string fp){
            //StreamReader reader = File.OpenText(fp);
            string[] readText = File.ReadAllLines(fp, Encoding.Default);
            bool citam_pred = false;
            //string line;
            UcitelClass u = new UcitelClass("bla");
            uc = new List<UcitelClass>();
            foreach(string line in readText) {
                if (line.Length == 0)
                {
                    citam_pred = false;
                    continue;
                }
                if (citam_pred)
                {
                    u.addItem(line);
                }
                if (line[0] != ' ' && line[0] != '*')
                {
                    citam_pred = true;
                    Console.WriteLine(line);
                    d.Add(line,new string[14,5]);
                    u = new UcitelClass(line);
                    uc.Add(u);
                    
                    //d.Add(line,new string[15,5]);
                }
            }
        }

      }
}
