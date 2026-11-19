public class Tabulka
{
    // pocita ze vsech zapasu, ktere uz maji vysledek, i v neuzavrenem kole
    // volny los dava body, ale nepocita se jako odehrany zapas
    public static List<RadekTabulky> Spocitej(Turnaj turnaj)
    {
        List<RadekTabulky> radky = new List<RadekTabulky>();
        foreach (Hrac hrac in turnaj.Prihlaseni)
        {
            radky.Add(new RadekTabulky(hrac));
        }

        foreach (Kolo kolo in turnaj.Kola)
        {
            foreach (Zapas zapas in kolo.Zapasy)
            {
                if (!zapas.MaVysledek())
                {
                    continue;
                }

                RadekTabulky prvni = NajdiRadek(radky, zapas.Prvni);
                RadekTabulky druhy = NajdiRadek(radky, zapas.Druhy);
                prvni.Odehrano++;
                druhy.Odehrano++;

                if (zapas.Vysledek == Zapas.VYHRA_PRVNIHO)
                {
                    prvni.Body += Bodovani.VYHRA;
                    druhy.Body += Bodovani.PROHRA;
                }
                else if (zapas.Vysledek == Zapas.VYHRA_DRUHEHO)
                {
                    prvni.Body += Bodovani.PROHRA;
                    druhy.Body += Bodovani.VYHRA;
                }
                else
                {
                    prvni.Body += Bodovani.REMIZA;
                    druhy.Body += Bodovani.REMIZA;
                }
            }

            // volny los se pocita jen u kol, ktera uz se hraji nebo jsou uzavrena
            if (kolo.VolnyLos != null && (kolo.Stav == Kolo.HRAJE_SE || kolo.Stav == Kolo.UZAVRENE))
            {
                RadekTabulky radek = NajdiRadek(radky, kolo.VolnyLos);
                radek.Body += Bodovani.VOLNY_LOS;
            }
        }

        return radky;
    }

    private static RadekTabulky NajdiRadek(List<RadekTabulky> radky, Hrac hrac)
    {
        for (int i = 0; i < radky.Count; i++)
        {
            if (radky[i].Hrac.Prezdivka == hrac.Prezdivka)
            {
                return radky[i];
            }
        }
        throw new TurnajException("Hráč " + hrac.Prezdivka + " není v turnaji přihlášený.");
    }
}
