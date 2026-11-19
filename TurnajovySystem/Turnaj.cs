public class Turnaj
{
    private readonly List<Hrac> prihlaseni;

    public string Nazev { get; private set; }
    public string Disciplina { get; private set; }
    public int PocetKol { get; private set; }
    public IReadOnlyList<Hrac> Prihlaseni { get; private set; }
    public IReadOnlyList<Kolo> Kola { get; private set; }

    private readonly List<Kolo> kola;

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
        kola = new List<Kolo>();
        Kola = kola.AsReadOnly();
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

    public Kolo AktualniKolo()
    {
        if (kola.Count == 0)
        {
            return null;
        }

        return kola[kola.Count - 1];
    }

    public Kolo NoveKolo()
    {
        Kolo predchozi = AktualniKolo();
        if (predchozi != null && predchozi.Stav != Kolo.UZAVRENE)
        {
            throw new TurnajException("Nejdřív uzavřete předchozí kolo.");
        }
        if (kola.Count >= PocetKol)
        {
            throw new TurnajException("Všechna kola už byla odehrána.");
        }
        if (Prihlaseni.Count < 2)
        {
            throw new TurnajException("Pro nové kolo musí být přihlášení aspoň dva hráči.");
        }

        Kolo kolo = new Kolo(kola.Count + 1);
        kola.Add(kolo);
        return kolo;
    }

    public void PridejDvojici(string prezdivka1, string prezdivka2)
    {
        Kolo kolo = VyberAktualniKolo();
        if (string.IsNullOrWhiteSpace(prezdivka1) || string.IsNullOrWhiteSpace(prezdivka2))
        {
            throw new TurnajException("Zadejte přezdívky obou hráčů.");
        }

        prezdivka1 = prezdivka1.Trim();
        prezdivka2 = prezdivka2.Trim();
        Hrac prvni = null;
        Hrac druhy = null;
        foreach (Hrac hrac in Prihlaseni)
        {
            string prezdivka = hrac.Prezdivka.Trim();
            if (string.Equals(prezdivka, prezdivka1, StringComparison.OrdinalIgnoreCase))
            {
                prvni = hrac;
            }
            if (string.Equals(prezdivka, prezdivka2, StringComparison.OrdinalIgnoreCase))
            {
                druhy = hrac;
            }
        }

        if (prvni == null || druhy == null)
        {
            throw new TurnajException("Oba hráči musí být přihlášení do turnaje.");
        }

        kolo.PridejZapas(prvni, druhy);
    }

    public void DokonciLosovani()
    {
        Kolo kolo = VyberAktualniKolo();
        if (kolo.Stav != Kolo.LOSUJE_SE)
        {
            throw new TurnajException("Losování už skončilo.");
        }

        Hrac volnyLos = null;
        foreach (Hrac hrac in Prihlaseni)
        {
            if (!kolo.ObsahujeHrace(hrac.Prezdivka))
            {
                if (volnyLos != null)
                {
                    throw new TurnajException("Bez soupeře zbývá víc než jeden hráč.");
                }
                volnyLos = hrac;
            }
        }

        if (volnyLos != null)
        {
            kolo.NastavVolnyLos(volnyLos);
        }
        kolo.UkonciLosovani();
    }

    public void ZadejVysledek(int cisloZapasu, int vysledek)
    {
        VyberAktualniKolo().ZadejVysledek(cisloZapasu, vysledek);
    }

    public void UzavriKolo()
    {
        VyberAktualniKolo().Uzavri();
    }

    public bool JeDokonceny()
    {
        return kola.Count == PocetKol && AktualniKolo().Stav == Kolo.UZAVRENE;
    }

    public bool HracMaZapas(string prezdivka)
    {
        foreach (Kolo kolo in kola)
        {
            if (kolo.ObsahujeHrace(prezdivka))
            {
                return true;
            }
        }

        return false;
    }

    private Kolo VyberAktualniKolo()
    {
        Kolo kolo = AktualniKolo();
        if (kolo == null)
        {
            throw new TurnajException("Nejdřív vytvořte kolo.");
        }

        return kolo;
    }
}
