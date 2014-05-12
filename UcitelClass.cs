using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    public class UcitelClass
    {
        public Dictionary<string, string[,]> d; // toto asi nebudeme potreovat
        public string[,] pred; // sem sa budu ukladat predmety daneho ucitela
        public string Name; // meno ucitela
        public bool[,] nope; //oznacuje ktore casy v poli kt. ucitel nechce ucit

        public UcitelClass(string name_)
        {
            Name = name_;
            pred = new string[14, 5];
            //d = new Dictionary<string,string[,]>();
        }

        //dostane riadok s predmetom pre daneho ucitela
        public bool addItem(string predmet){
            //Console.WriteLine(Name+" "+predmet);
            //string[] sa = predmet.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(predmet.Substring(14, 1));
            int d = daytoindex(predmet.Substring(3, 3));
            int t = timetoindex(predmet.Substring(7, 5));
            if (predmet.Substring(14, 1) != " ")
            {
                int h = Convert.ToInt32(predmet.Substring(14, 1));
            }



            return true;
        }

        // preklada nazvy dni do indexov do pola
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

        // preklada casi do indexov do pola
        private int timetoindex(string time)
        {
            DateTime dateN = new DateTime(2000, 1, 1, 19, 10,00);
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
