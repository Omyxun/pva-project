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
        if (AktualniTurnaj != null && !AktualniTurnaj.JeDokonceny())
        {
            throw new TurnajException("Už je otevřený turnaj, nový jde založit až po jeho dokončení nebo zrušení.");
        }
        AktualniTurnaj = new Turnaj(nazev, disciplina, pocetKol);
    }

    public void ZrusTurnaj()
    {
        if (AktualniTurnaj == null)
        {
            throw new TurnajException("Žádný turnaj není otevřený.");
        }
        AktualniTurnaj = null;
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
        if (AktualniTurnaj == null)
        {
            throw new TurnajException("Žádný turnaj není otevřený.");
        }
        Hrac hrac = Hraci.Najdi(prezdivka);
        AktualniTurnaj.Prihlas(hrac);
    }

    public void OdhlasZTurnaje(string prezdivka)
    {
        if (AktualniTurnaj == null)
        {
            throw new TurnajException("Žádný turnaj není otevřený.");
        }
        Hrac hrac = Hraci.Najdi(prezdivka);
        AktualniTurnaj.Odhlas(hrac.Prezdivka);
    }
}
