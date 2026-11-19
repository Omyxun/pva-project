public class Turnaj
{
    public string Nazev { get; private set; }
    public string Disciplina { get; private set; }
    public int PocetKol { get; private set; }
    public List<Hrac> Prihlaseni { get; private set; }
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
        Prihlaseni = new List<Hrac>();
        kola = new List<Kolo>();
        Kola = kola.AsReadOnly();
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
