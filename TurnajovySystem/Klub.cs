// drzi vsechno, co program ma: hrace klubu a otevreny turnaj
public class Klub
{
    public SpravaHracu Hraci { get; private set; }
    // null, kdyz zadny turnaj neni otevreny
    public Turnaj AktualniTurnaj { get; private set; }

    public Klub()
    {
        Hraci = new SpravaHracu();
        AktualniTurnaj = null;
    }

    // otevreny muze byt jen jeden turnaj, novy jde zalozit az kdyz je stary dokonceny nebo zruseny
    public void ZalozTurnaj(string nazev, string disciplina, int pocetKol)
    {
        throw new NotImplementedException();
    }

    public void ZrusTurnaj()
    {
        throw new NotImplementedException();
    }

    // hrace, ktery uz hral nebo je prihlaseny v rozehranem turnaji, smazat nejde
    // kdyz je prihlaseny v nezacatem turnaji, nejdriv se odhlasi
    public void OdeberHrace(string prezdivka)
    {
        Hrac hrac = Hraci.Najdi(prezdivka);

        if (AktualniTurnaj != null)
        {
            if (AktualniTurnaj.HracMaZapas(hrac.Prezdivka))
            {
                throw new TurnajException("Hráč " + hrac.Prezdivka + " už hrál zápas, smazat ho nejde.");
            }
            if (AktualniTurnaj.JePrihlasen(hrac.Prezdivka))
            {
                if (AktualniTurnaj.Zacal())
                {
                    throw new TurnajException("Hráč " + hrac.Prezdivka + " je přihlášený v rozehraném turnaji, smazat ho nejde.");
                }
                // turnaj jeste nezacal, hrace nejdriv odhlasime
                AktualniTurnaj.Odhlas(hrac.Prezdivka);
            }
        }
        Hraci.Odeber(hrac.Prezdivka);
    }

    public void PrihlasDoTurnaje(string prezdivka)
    {
        throw new NotImplementedException();
    }

    public void OdhlasZTurnaje(string prezdivka)
    {
        throw new NotImplementedException();
    }
}
