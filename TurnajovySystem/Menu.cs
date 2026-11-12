// jedine misto, kde se vypisuje do konzole
public class Menu
{
    private Klub klub;
    private Uloziste uloziste;

    public Menu()
    {
        uloziste = new Uloziste(Uloziste.VYCHOZI_SOUBOR);
        klub = new Klub();
    }

    public void Spust()
    {
        throw new NotImplementedException();
    }
}
