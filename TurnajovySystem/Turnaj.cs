public class Turnaj
{
    public string Nazev { get; private set; }
    public string Disciplina { get; private set; }
    public int PocetKol { get; private set; }
    public List<Hrac> Prihlaseni { get; private set; }
    public List<Kolo> Kola { get; private set; }

    public Turnaj(string nazev, string disciplina, int pocetKol)
    {
        Validace.ZkontrolujText(nazev, "Název turnaje");
        Validace.ZkontrolujText(disciplina, "Disciplína");
        if (pocetKol < 1)
        {
            throw new TurnajException("Turnaj musí mít aspoň jedno kolo.");
        }
        Nazev = nazev;
        Disciplina = disciplina;
        PocetKol = pocetKol;
        Prihlaseni = new List<Hrac>();
        Kola = new List<Kolo>();
    }

    // jen dokud nezacalo prvni kolo
    public void Prihlas(Hrac hrac)
    {
        throw new NotImplementedException();
    }

    public void Odhlas(string prezdivka)
    {
        throw new NotImplementedException();
    }

    public bool JePrihlasen(string prezdivka)
    {
        throw new NotImplementedException();
    }

    public bool Zacal()
    {
        throw new NotImplementedException();
    }

    // posledni kolo, nebo null
    public Kolo AktualniKolo()
    {
        throw new NotImplementedException();
    }

    // predchozi kolo musi byt uzavrene a nesmi se prekrocit PocetKol
    // prihlaseni musi byt aspon 2 hraci
    public Kolo NoveKolo()
    {
        throw new NotImplementedException();
    }

    // oba hraci musi byt prihlaseni
    public void PridejDvojici(string prezdivka1, string prezdivka2)
    {
        throw new NotImplementedException();
    }

    // kdo zbyl bez soupere, dostane volny los
    // kdyz zbyl vic nez jeden hrac, je to chyba
    public void DokonciLosovani()
    {
        throw new NotImplementedException();
    }

    public void ZadejVysledek(int cisloZapasu, int vysledek)
    {
        throw new NotImplementedException();
    }

    public void UzavriKolo()
    {
        throw new NotImplementedException();
    }

    // vsechna kola odehrana a posledni uzavrene
    public bool JeDokonceny()
    {
        throw new NotImplementedException();
    }

    // hrac ma v nekterem kole zapas nebo volny los
    public bool HracMaZapas(string prezdivka)
    {
        throw new NotImplementedException();
    }
}
