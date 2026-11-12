public class Zapas
{
    public const int BEZ_VYSLEDKU = 0;
    public const int VYHRA_PRVNIHO = 1;
    public const int VYHRA_DRUHEHO = 2;
    public const int REMIZA = 3;

    // cislo zapasu v kole, cisluje se od 1
    public int Cislo { get; private set; }
    public Hrac Prvni { get; private set; }
    public Hrac Druhy { get; private set; }
    public int Vysledek { get; private set; }

    public Zapas(int cislo, Hrac prvni, Hrac druhy)
    {
        Cislo = cislo;
        Prvni = prvni;
        Druhy = druhy;
        Vysledek = BEZ_VYSLEDKU;
    }

    // povolene jsou jen VYHRA_PRVNIHO, VYHRA_DRUHEHO a REMIZA
    public void NastavVysledek(int vysledek)
    {
        throw new NotImplementedException();
    }

    public bool MaVysledek()
    {
        throw new NotImplementedException();
    }

    public bool Hraje(string prezdivka)
    {
        throw new NotImplementedException();
    }
}
