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
        ZkontrolujLosovani();

        if (prvni == null || druhy == null)
        {
            throw new TurnajException("Zápas musí mít dva hráče.");
        }
        if (string.Equals(prvni.Prezdivka, druhy.Prezdivka, StringComparison.OrdinalIgnoreCase))
        {
            throw new TurnajException("Hráč nemůže hrát sám se sebou.");
        }
        if (ObsahujeHrace(prvni.Prezdivka) || ObsahujeHrace(druhy.Prezdivka))
        {
            throw new TurnajException("Hráč už je v tomto kole.");
        }

        Zapasy.Add(new Zapas(Zapasy.Count + 1, prvni, druhy));
    }

    // hrac nemuze mit volny los a zaroven hrat
    public void NastavVolnyLos(Hrac hrac)
    {
        ZkontrolujLosovani();

        if (hrac == null)
        {
            throw new TurnajException("Pro volný los musí být vybraný hráč.");
        }
        if (VolnyLos != null)
        {
            throw new TurnajException("V tomto kole už je volný los.");
        }
        if (ObsahujeHrace(hrac.Prezdivka))
        {
            throw new TurnajException("Hráč už je v tomto kole.");
        }

        VolnyLos = hrac;
    }

    // prepne stav na HRAJE_SE
    public void UkonciLosovani()
    {
        ZkontrolujLosovani();
        Stav = HRAJE_SE;
    }

    public bool ObsahujeHrace(string prezdivka)
    {
        if (VolnyLos != null && string.Equals(VolnyLos.Prezdivka, prezdivka, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        foreach (Zapas zapas in Zapasy)
        {
            if (zapas.Hraje(prezdivka))
            {
                return true;
            }
        }

        return false;
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

    private void ZkontrolujLosovani()
    {
        if (Stav != LOSUJE_SE)
        {
            throw new TurnajException("Losování už skončilo.");
        }
    }
}
