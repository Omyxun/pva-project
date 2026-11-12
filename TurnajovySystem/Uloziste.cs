// ukladani a nacitani do vlastniho textoveho formatu, popis je v NAVRH.md
public class Uloziste
{
    public const string VYCHOZI_SOUBOR = "turnaj.txt";

    private string cesta;

    public Uloziste(string cesta)
    {
        this.cesta = cesta;
    }

    public bool SouborExistuje()
    {
        return File.Exists(cesta);
    }

    public void Uloz(Klub klub)
    {
        throw new NotImplementedException();
    }

    // kdyz soubor chybi nebo je poskozeny, vyhodi TurnajException
    public Klub Nacti()
    {
        throw new NotImplementedException();
    }
}
