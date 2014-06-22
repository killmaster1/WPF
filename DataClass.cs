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
        public List<UcitelClass> kr;
        private List<string[]> aliasy;

        public DataClass()
        {
            d = new Dictionary<string, string[,]>();
            uc = new List<UcitelClass>();
            kr = new List<UcitelClass>();
            
        }

        public UcitelClass findUcitel(string name)
        {
            foreach (UcitelClass u in uc)
            {
                if (u.Name == name)
                {
                    return u;
                }
            }
            return null;
        }

        public void loadFileData(string fp)
        {
            string[] readText = File.ReadAllLines(fp, Encoding.Default);
            bool citam_pred = false;
            UcitelClass u = new UcitelClass("bla");
            uc = new List<UcitelClass>();
            int d = 0;
            int t = 0;
            int h = 0;
            string name;
            loadAlias();
            foreach (string line in readText)
            {
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
                    if (line.Substring(7, 5) != "     ")
                    {
                        t = timetoindex(line.Substring(7, 5));
                    }
                    if (line.Substring(14, 1) != " ")
                    {
                        h = Convert.ToInt32(line.Substring(14, 1));
                    }
                    u.addPredmet(d, t, h, name);
                    addKruzok(d, t, h, name,line.Substring(98, line.Length - 98));

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

        public List<UcitelClass> conflict()
        {
            List<UcitelClass> pom = new List<UcitelClass>();
            bool conf;
            foreach (UcitelClass a in uc)
            {
                conf = false;
                for (int i = 0; i < 14; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (a.pred[i, j] != null && a.nope[i, j])
                        {
                            conf = true;
                        }
                    }
                }
                if (conf)
                {
                    pom.Add(a);
                }
            }
            return pom;

        }

        private void loadAlias()
        {
            aliasy = new List<string[]>();
            string[] readText = File.ReadAllLines("_aliasy.txt", Encoding.Default);
            string[] pom;
            string[] a = new String[]{"=", ","};
            
            foreach (string line in readText)
            {
                pom = line.Split(a, StringSplitOptions.None);
                aliasy.Add(pom);
            }

        }

        private void addKruzok(int d, int t, int h,string name,string kruz_)
        {
            //Console.WriteLine(kruz_);
            string[] kruz = kruz_.Split(' ');
            UcitelClass kruzok;
            bool najd = false;
            foreach (string a in kruz)
            {
                if (a.Contains("*"))
                {
                    foreach (string[] b in aliasy)
                    {
                        if (a == b[0])
                        {
                            for (int i = 1; i < b.Length; i++)
                            {
                                kruzok = new UcitelClass("bla");
                                najd = false;
                                foreach (UcitelClass k in kr)
                                {
                                    if (k.Name == b[i])
                                    {
                                        kruzok = k;
                                        najd = true;
                                    }
                                }
                                if (!najd)
                                {
                                    kruzok = new UcitelClass(b[i]);
                                    kr.Add(kruzok);
                                }
                                Console.WriteLine("Som tu");
                                kruzok.addPredmet(d, t, h, name);
                            }
                        }
                    }
                }
                else
                {
                    najd = false;
                    kruzok = new UcitelClass(a);
                    foreach (UcitelClass k in kr)
                    {
                        if (k.Name == a)
                        {
                            kruzok = k;
                            najd = true;
                        }
                    }
                    if (!najd)
                    {
                        kr.Add(kruzok);
                    }
                    kruzok.addPredmet(d, t, h, name);
                }
            }
        }
    }
}
