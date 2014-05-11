using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    class UcitelClass
    {
        public Dictionary<string, string[,]> d; // toto asi nebudeme potreovat
        public string[,] pred; // sem sa budu ukladat predmety daneho ucitela
        public string Name; // meno ucitela
        public bool[,] nope; //oznacuje ktore casy ucitel nechce ucit

        public UcitelClass(string name_)
        {
            Name = name_;
            pred = new string[14, 5];
            //d = new Dictionary<string,string[,]>();
        }

        //dostane riadok s predmetom pre daneho ucitela
        public bool addItem(string predmet){
            Console.WriteLine(predmet);
            /*string[] sa = predmet.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in sa)
            {
                Console.WriteLine(s);
            }*/
            return true;
        }
    }
}
