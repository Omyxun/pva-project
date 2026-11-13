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
        Validace.ZkontrolujText(jmeno, "Jméno");
        Validace.ZkontrolujText(prezdivka, "Přezdívka");
        jmeno = jmeno.Trim();
        prezdivka = prezdivka.Trim();

        // prezdivky se porovnavaji bez ohledu na velikost pismen
        if (hraci.ContainsKey(prezdivka.ToLower()))
        {
            throw new TurnajException("Hráč s přezdívkou " + prezdivka + " už existuje.");
        }
        hraci.Add(prezdivka.ToLower(), new Hrac(jmeno, prezdivka));
    }

    // kontrolu odehranych zapasu dela Klub.OdeberHrace
    public void Odeber(string prezdivka)
    {
        if (!Existuje(prezdivka))
        {
            throw new TurnajException("Hráč s přezdívkou " + prezdivka + " neexistuje.");
        }
        hraci.Remove(prezdivka.Trim().ToLower());
    }

    // kdyz hrac neexistuje, vyhodi TurnajException
    public Hrac Najdi(string prezdivka)
    {
        if (!Existuje(prezdivka))
        {
            throw new TurnajException("Hráč s přezdívkou " + prezdivka + " neexistuje.");
        }
        return hraci[prezdivka.Trim().ToLower()];
    }

    public bool Existuje(string prezdivka)
    {
        return hraci.ContainsKey(prezdivka.Trim().ToLower());
    }

    public List<Hrac> Vsichni()
    {
        List<Hrac> vysledek = new List<Hrac>();
        foreach (Hrac hrac in hraci.Values)
        {
            vysledek.Add(hrac);
        }
        return vysledek;
    }
}
