public class Kolo
{
    // stav kola
    public const int LOSUJE_SE = 0;
    public const int HRAJE_SE = 1;
    public const int UZAVRENE = 2;

    private readonly List<Zapas> zapasy;

    public int Cislo { get; private set; }
    public int Stav { get; private set; }
    public IReadOnlyList<Zapas> Zapasy { get; private set; }
    // null, kdyz nikdo volny los nema
    public Hrac VolnyLos { get; private set; }

    public Kolo(int cislo)
    {
        Cislo = cislo;
        Stav = LOSUJE_SE;
        zapasy = new List<Zapas>();
        Zapasy = zapasy.AsReadOnly();
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

        zapasy.Add(new Zapas(zapasy.Count + 1, prvni, druhy, this));
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

    public void ZadejVysledek(int cisloZapasu, int vysledek)
    {
        foreach (Zapas zapas in zapasy)
        {
            if (zapas.Cislo == cisloZapasu)
            {
                zapas.NastavVysledek(vysledek);
                return;
            }
        }

        throw new TurnajException("Zápas číslo " + cisloZapasu + " v tomto kole neexistuje.");
    }

    public void Uzavri()
    {
        ZkontrolujHru();

        List<int> chybejici = new List<int>();
        foreach (Zapas zapas in zapasy)
        {
            if (!zapas.MaVysledek())
            {
                chybejici.Add(zapas.Cislo);
            }
        }

        if (chybejici.Count > 0)
        {
            throw new TurnajException("Chybí výsledek u zápasů: " + string.Join(", ", chybejici) + ".");
        }

        Stav = UZAVRENE;
    }

    internal void ZkontrolujHru()
    {
        if (Stav == LOSUJE_SE)
        {
            throw new TurnajException("Nejdřív dokončete losování.");
        }
        if (Stav == UZAVRENE)
        {
            throw new TurnajException("Kolo už je uzavřené.");
        }
    }

    private void ZkontrolujLosovani()
    {
        if (Stav != LOSUJE_SE)
        {
            throw new TurnajException("Losování už skončilo.");
        }
    }
}
