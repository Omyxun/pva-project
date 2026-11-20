// overeni tabulky na pripravenem turnaji
Dictionary<string, Hrac> hraci = new Dictionary<string, Hrac>();
Turnaj turnaj = null;
Kolo kolo = null;
List<string[]> zapasy = new List<string[]>();

try
{
    string cesta = Path.Combine(AppContext.BaseDirectory, "overeni_turnaj.txt");
    string[] radky = File.ReadAllLines(cesta);
    if (radky[0] != "TURNAJOVY_SYSTEM;1")
    {
        throw new Exception("Neplatná hlavička testovacího souboru.");
    }

    // tady nacitame jen pripravena testovaci data
    for (int i = 1; i < radky.Length; i++)
    {
        string[] pole = radky[i].Split(';');
        switch (pole[0])
        {
            case "HRAC":
                hraci.Add(pole[1], new Hrac(pole[2], pole[1]));
                break;
            case "TURNAJ":
                turnaj = new Turnaj(pole[1], pole[2], int.Parse(pole[3]));
                break;
            case "PRIHLASEN":
                turnaj.Prihlas(hraci[pole[1]]);
                break;
            case "KOLO":
                if (kolo != null)
                {
                    DokonciKolo(turnaj, kolo, zapasy, hraci);
                }
                kolo = turnaj.NoveKolo();
                if (kolo.Cislo != int.Parse(pole[1]))
                {
                    throw new Exception("Nesouhlasí číslo kola.");
                }
                zapasy.Clear();
                break;
            case "ZAPAS":
                zapasy.Add(pole);
                break;
            default:
                throw new Exception("Neznámý typ testovacího záznamu.");
        }
    }
    DokonciKolo(turnaj, kolo, zapasy, hraci);

    List<RadekTabulky> tabulka = Tabulka.Spocitej(turnaj);
    // hodnoty z rucniho vypoctu
    string[] ocekavane =
    {
        "1 | cyril | 5 | 2",
        "2 | eva | 5 | 2",
        "3 | boris | 3 | 3",
        "4 | david | 3 | 2",
        "5 | anna | 2 | 3"
    };
    if (tabulka.Count != ocekavane.Length)
    {
        throw new Exception("Nesouhlasí počet hráčů v tabulce.");
    }
    Console.WriteLine("Pořadí | Přezdívka | Body | Odehráno");
    for (int i = 0; i < tabulka.Count; i++)
    {
        RadekTabulky radek = tabulka[i];
        string skutecny = $"{radek.Poradi} | {radek.Hrac.Prezdivka} | {radek.Body} | {radek.Odehrano}";
        Console.WriteLine(skutecny);
        if (skutecny != ocekavane[i])
        {
            throw new Exception("Tabulka se liší od ručního výpočtu u hráče " + radek.Hrac.Prezdivka + ".");
        }
    }
    Console.WriteLine("Ověření prošlo. Body, počty zápasů i pořadí odpovídají ručnímu výpočtu.");
}
catch (Exception chyba)
{
    Console.Error.WriteLine("Ověření selhalo. " + chyba.Message);
    Environment.ExitCode = 1;
}

static void DokonciKolo(Turnaj turnaj, Kolo kolo, List<string[]> zapasy, Dictionary<string, Hrac> hraci)
{
    foreach (string[] zapas in zapasy)
    {
        kolo.PridejZapas(hraci[zapas[2]], hraci[zapas[3]]);
    }

    int pocetVolnych = 0;
    foreach (Hrac hrac in turnaj.Prihlaseni)
    {
        if (!kolo.ObsahujeHrace(hrac.Prezdivka))
        {
            kolo.NastavVolnyLos(hrac);
            pocetVolnych++;
        }
    }
    if (pocetVolnych != 1)
    {
        throw new Exception("V každém kole testovacího turnaje musí být jeden volný los.");
    }
    kolo.UkonciLosovani();

    foreach (string[] zapas in zapasy)
    {
        kolo.ZadejVysledek(int.Parse(zapas[1]), int.Parse(zapas[4]));
    }
    kolo.Uzavri();
}
