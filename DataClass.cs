using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    public class DataClass
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
            int d =0;
            int t=0;
            int h=0;
            string name;

            foreach(string line in readText) {
                if (line.Length == 0)
                {
                    citam_pred = false;
                    continue;
                }
                if (citam_pred)
                {
                    //u.addItem(line);
                    //72 nazov
                    name = line.Substring(26, 72);
                    if (line.Substring(3, 3) != "   ")
                    {
                        d = daytoindex(line.Substring(3, 3));
                    }
                    if(line.Substring(7, 5)!="     "){
                        t = timetoindex(line.Substring(7, 5));
                    }
                    if (line.Substring(14, 1) != " ")
                    {
                        h = Convert.ToInt32(line.Substring(14, 1));
                    }
                    u.addPredmet(d, t, h, name);

                }
                if (line[0] != ' ' && line[0] != '*')
                {
                    citam_pred = true;
                    //d.Add(line,new string[14,5]);
                    u = new UcitelClass(line);
                    uc.Add(u);
                    
                    //d.Add(line,new string[15,5]);
                }
            }
        }

        private int daytoindex(string pred)
        {
            switch (pred)
            {
                case "Pon":
                    return 0;
                case "Uto":
                    return 1;
                case "Str":
                    return 2;
                case "Stv":
                    return 3;
                case "Pia":
                    return 4;
                default:
                    return -1;
            }
        }

        private int timetoindex(string time)
        {
            DateTime dateN = new DateTime(2000, 1, 1, 19, 10, 00);
            int i = 0;
            for (DateTime date1 = new DateTime(2000, 1, 1, 8, 10, 00);
                date1 < dateN;
                date1 = date1.AddMinutes(50))
            {
                if (date1.ToString("HH mm") == time)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

      }
}
