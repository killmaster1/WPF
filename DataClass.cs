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
            uc = new List<UcitelClass>();
        }

        public UcitelClass findUcitel(string name){
            foreach (UcitelClass u in uc)
            {
                if (u.Name == name)
                {
                    return u;
                }
            }
            return null;
        }

        public void loadFileData(string fp){
            string[] readText = File.ReadAllLines(fp, Encoding.Default);
            bool citam_pred = false;
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
                    u = new UcitelClass(line);
                    uc.Add(u);
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

        private int timetoindex(string time_)
        {

            switch (time_)
            {
                case " 8 10":
                    return 0;
                case " 9 00":
                    return 1;
                case " 9 50":
                    return 2;
                case "10 40":
                    return 3;
                case "11 30":
                    return 4;
                case "12 20":
                    return 5;
                case "13 10":
                    return 6;
                case "14 00":
                    return 7;
                case "14 50":
                    return 8;
                case "15 40":
                    return 9;
                case "16 30":
                    return 10;
                case "17 20":
                    return 11;
                case "18 10":
                    return 12;
                case "19 00":
                    return 13;
                default:
                    return -1;
            }
        }

      }
}
