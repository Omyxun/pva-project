public class Zapas
{
    public const int BEZ_VYSLEDKU = 0;
    public const int VYHRA_PRVNIHO = 1;
    public const int VYHRA_DRUHEHO = 2;
    public const int REMIZA = 3;

    private readonly Kolo kolo;

    // cislo zapasu v kole, cisluje se od 1
    public int Cislo { get; private set; }
    public Hrac Prvni { get; private set; }
    public Hrac Druhy { get; private set; }
    public int Vysledek { get; private set; }

    public Zapas(int cislo, Hrac prvni, Hrac druhy, Kolo kolo = null)
    {
        Cislo = cislo;
        Prvni = prvni;
        Druhy = druhy;
        Vysledek = BEZ_VYSLEDKU;
        this.kolo = kolo;
    }

    // povolene jsou jen VYHRA_PRVNIHO, VYHRA_DRUHEHO a REMIZA
    public void NastavVysledek(int vysledek)
    {
        if (kolo != null)
        {
            kolo.ZkontrolujHru();
        }

        if (vysledek != VYHRA_PRVNIHO && vysledek != VYHRA_DRUHEHO && vysledek != REMIZA)
        {
            throw new TurnajException("Neplatný výsledek zápasu.");
        }

        Vysledek = vysledek;
    }

    public bool MaVysledek()
    {
        return Vysledek != BEZ_VYSLEDKU;
    }

    public bool Hraje(string prezdivka)
    {
        return string.Equals(Prvni.Prezdivka, prezdivka, StringComparison.OrdinalIgnoreCase)
            || string.Equals(Druhy.Prezdivka, prezdivka, StringComparison.OrdinalIgnoreCase);
    }
}
