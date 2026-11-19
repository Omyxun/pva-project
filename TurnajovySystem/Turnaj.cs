public class Turnaj
{
    private readonly List<Hrac> prihlaseni;

    public string Nazev { get; private set; }
    public string Disciplina { get; private set; }
    public int PocetKol { get; private set; }
    public IReadOnlyList<Hrac> Prihlaseni { get; private set; }
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
        prihlaseni = new List<Hrac>();
        Prihlaseni = prihlaseni.AsReadOnly();
        Kola = new List<Kolo>();
    }

    public void Prihlas(Hrac hrac)
    {
        if (Zacal())
        {
            throw new TurnajException("Turnaj už začal, hráče nejde přihlásit.");
        }
        if (hrac == null)
        {
            throw new TurnajException("Vyberte hráče k přihlášení.");
        }

        Validace.ZkontrolujText(hrac.Prezdivka, "Přezdívka");
        if (JePrihlasen(hrac.Prezdivka))
        {
            throw new TurnajException("Hráč " + hrac.Prezdivka + " už je přihlášený.");
        }

        prihlaseni.Add(hrac);
    }

    public void Odhlas(string prezdivka)
    {
        if (Zacal())
        {
            throw new TurnajException("Turnaj už začal, hráče nejde odhlásit.");
        }

        Hrac hrac = NajdiPrihlaseneho(prezdivka);
        if (hrac == null)
        {
            throw new TurnajException("Hráč " + prezdivka + " není přihlášený.");
        }

        prihlaseni.Remove(hrac);
    }

    public bool JePrihlasen(string prezdivka)
    {
        return NajdiPrihlaseneho(prezdivka) != null;
    }

    public bool Zacal()
    {
        return Kola.Count > 0;
    }

    private Hrac NajdiPrihlaseneho(string prezdivka)
    {
        if (string.IsNullOrWhiteSpace(prezdivka))
        {
            return null;
        }

        prezdivka = prezdivka.Trim();
        foreach (Hrac hrac in prihlaseni)
        {
            if (string.Equals(hrac.Prezdivka.Trim(), prezdivka, StringComparison.OrdinalIgnoreCase))
            {
                return hrac;
            }
        }

        return null;
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
