using BingoCLI;

Random rnd = new();

BingoJatekos bj = new("Teszt Elek", null)
{
    Jelolesek = new bool[5, 5]
    {
        { true, false, true, false, true},
        { true, false, false, true, true},
        { false, false, true, false, false},
        { true, true, true, true, false},
        { false, false, true, false, true}
    },
};

Console.WriteLine(bj.BingoEll);

List<BingoJatekos> jatekosok = new();

string[] fileok = Directory.GetFiles(@"..\..\..\src");
foreach (var file in fileok)
{
    if (file.EndsWith(".txt"))
    {
        string nev = file.Split('\\')[^1].Replace(".txt", "");
        using StreamReader sr = new(file);
        string[,] kartya = new string[5, 5];
        int si = 0;
        while (!sr.EndOfStream)
        {
            string sor = sr.ReadLine();
            var v = sor.Split(';');
            for (int oi = 0; oi < v.Length; oi++)
            {
                kartya[si, oi] = v[oi];
            }
            si++;
        }
        jatekosok.Add(new(nev, kartya));
    }
}

Console.WriteLine($"jatekosok szama: {jatekosok.Count}");

List<int> huzasok = new();
bool vanBingo = false;
int huzas = -1;
int ssz = 1;
List<BingoJatekos> lehetsegesNyertesek = new();
do
{
    do
    {
        huzas = rnd.Next(1, 76);
    } while (huzasok.Contains(huzas));
    huzasok.Add(huzas);
    foreach (var jatekos in jatekosok)
    {
        jatekos.SorsoltSzamotJelol($"{huzasok.Last()}");
        if(jatekos.BingoEll)
        {
            vanBingo = true;
            lehetsegesNyertesek.Add(jatekos);
        }
    }
    Console.Write($"{ssz}.->{huzasok.Last()}  ");
    ssz++;

} while (!vanBingo);

Console.WriteLine("\nhlehetseges nyertesek:");

foreach (var jatekos in lehetsegesNyertesek)
{
    Console.WriteLine(jatekos.Nev);

    for (int s = 0; s < jatekos.Kartya.GetLength(0); s++)
    {
        for (int o = 0; o < jatekos.Kartya.GetLength(1); o++)
        {
            if (jatekos.Jelolesek[s, o])
            {
                Console.Write("{0,-3}", jatekos.Kartya[s, o]);
            }
            else Console.Write("0  ");
        }
        Console.Write('\n');
    }
    Console.Write('\n');
}
