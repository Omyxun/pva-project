public class Kolo
{
    // stav kola
    public const int LOSUJE_SE = 0;
    public const int HRAJE_SE = 1;
    public const int UZAVRENE = 2;

    public int Cislo { get; private set; }
    public int Stav { get; private set; }
    public List<Zapas> Zapasy { get; private set; }
    // null, kdyz nikdo volny los nema
    public Hrac VolnyLos { get; private set; }

    public Kolo(int cislo)
    {
        Cislo = cislo;
        Stav = LOSUJE_SE;
        Zapasy = new List<Zapas>();
        VolnyLos = null;
    }

    // jen ve stavu LOSUJE_SE, hrac smi byt v kole jen jednou
    public void PridejZapas(Hrac prvni, Hrac druhy)
    {
        throw new NotImplementedException();
    }

    // hrac nemuze mit volny los a zaroven hrat
    public void NastavVolnyLos(Hrac hrac)
    {
        throw new NotImplementedException();
    }

    // prepne stav na HRAJE_SE
    public void UkonciLosovani()
    {
        throw new NotImplementedException();
    }

    public bool ObsahujeHrace(string prezdivka)
    {
        throw new NotImplementedException();
    }

    // jen ve stavu HRAJE_SE, vysledek jde prepsat dokud kolo neni uzavrene
    public void ZadejVysledek(int cisloZapasu, int vysledek)
    {
        throw new NotImplementedException();
    }

    // vsechny zapasy musi mit vysledek
    public void Uzavri()
    {
        throw new NotImplementedException();
    }
}
