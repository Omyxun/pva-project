public class RadekTabulky
{
    public int Poradi { get; set; }
    public Hrac Hrac { get; private set; }
    public int Body { get; set; }
    public int Odehrano { get; set; }

    public RadekTabulky(Hrac hrac)
    {
        Hrac = hrac;
        Poradi = 0;
        Body = 0;
        Odehrano = 0;
    }
}
