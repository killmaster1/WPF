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
            nope = new bool[14, 5];
        }

        public bool addPredmet(int den, int cas, int pocet_hodin, string nazov)
        {

            for (int i = 0; i < pocet_hodin;i++ )
            {
                if (cas < 14&&den<5)
                {
                    pred[cas, den] = nazov;
                }
                cas++;
            }

            return true;
        }

    }
}
