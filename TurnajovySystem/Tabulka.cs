public class Tabulka
{
    // pocita ze vsech zapasu, ktere uz maji vysledek, i v neuzavrenem kole
    // volny los dava body, ale nepocita se jako odehrany zapas
    // serazeno podle bodu, pri shode podle prezdivky
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

        // bubble sort, vic bodu je vys, pri shode podle prezdivky
        for (int i = 0; i < radky.Count - 1; i++)
        {
            for (int j = 0; j < radky.Count - 1 - i; j++)
            {
                if (JeDriv(radky[j + 1], radky[j]))
                {
                    RadekTabulky pomocny = radky[j];
                    radky[j] = radky[j + 1];
                    radky[j + 1] = pomocny;
                }
            }
        }

        for (int i = 0; i < radky.Count; i++)
        {
            radky[i].Poradi = i + 1;
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

    // true, kdyz ma byt prvni radek v tabulce vys nez druhy
    private static bool JeDriv(RadekTabulky a, RadekTabulky b)
    {
        if (a.Body != b.Body)
        {
            return a.Body > b.Body;
        }
        return string.Compare(a.Hrac.Prezdivka, b.Hrac.Prezdivka) < 0;
    }
}
