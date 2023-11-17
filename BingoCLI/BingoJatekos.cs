using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoCLI
{
    internal class BingoJatekos
    {
        public string Nev { get; set; }
        public string[,] Kartya { get; set; }
        public bool[,] Jelolesek { get; set; }

        public bool BingoEll
        {
            get
            {
                int cAtlo1 = 0;
                int cAtlo2 = 0;
                for (int i = 0; i < 5; i++)
                {
                    int cSor = 0;
                    int cOszlop = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        if (Jelolesek[i, j]) cSor++;
                        if (Jelolesek[j, i]) cOszlop++;
                    }
                    if (cSor == 5) return true;
                    if (cOszlop == 5) return true;
                    if (Jelolesek[i, i]) cAtlo1++;
                    if (Jelolesek[i, 4 - i]) cAtlo2++;
                }
                if (cAtlo1 == 5) return true;
                if (cAtlo2 == 5) return true;
                return false;
            }
        }

        public void SorsoltSzamotJelol(string szam)
        {
            for (int s = 0; s < Kartya.GetLength(0); s++)
            {
                for (int o = 0; o < Kartya.GetLength(1); o++)
                {
                    if (szam == Kartya[s, o]) Jelolesek[s, o] = true;
                }
            }
        }

        public BingoJatekos(string nev, string[,] kartya)
        {
            Nev = nev;
            Kartya = kartya;
            Jelolesek = new bool[5, 5]
            {
                { false, false, false, false, false},
                { false, false, false, false, false},
                { false, false,  true, false, false},
                { false, false, false, false, false},
                { false, false, false, false, false},
            };
        }
    }
}
