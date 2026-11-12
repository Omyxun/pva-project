public class SpravaHracu
{
    // klicem je prezdivka, diky tomu se hrac hleda rychle
    private Dictionary<string, Hrac> hraci;

    public SpravaHracu()
    {
        hraci = new Dictionary<string, Hrac>();
    }

    // prezdivka musi byt jedinecna
    public void Pridej(string jmeno, string prezdivka)
    {
        throw new NotImplementedException();
    }

    // kontrolu odehranych zapasu dela Klub.OdeberHrace
    public void Odeber(string prezdivka)
    {
        throw new NotImplementedException();
    }

    // kdyz hrac neexistuje, vyhodi TurnajException
    public Hrac Najdi(string prezdivka)
    {
        throw new NotImplementedException();
    }

    public bool Existuje(string prezdivka)
    {
        throw new NotImplementedException();
    }

    public List<Hrac> Vsichni()
    {
        throw new NotImplementedException();
    }
}
