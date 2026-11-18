using System;
using System.Collections.Generic;

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
        bool bezi = true;
        while (bezi)
        {
            Console.WriteLine();
            Console.WriteLine("--- Turnajový systém ---");
            if (klub.AktualniTurnaj != null)
            {
                Console.WriteLine("Turnaj: " + klub.AktualniTurnaj.Nazev + " (" + klub.AktualniTurnaj.Disciplina + ")");
            }
            else
            {
                Console.WriteLine("Turnaj není založený.");
            }

            Console.WriteLine("1 - Přidat hráče");
            Console.WriteLine("2 - Odebrat hráče");
            Console.WriteLine("3 - Vypsat hráče");
            Console.WriteLine("4 - Založit turnaj");
            Console.WriteLine("5 - Zrušit turnaj");
            Console.WriteLine("6 - Přihlásit hráče do turnaje");
            Console.WriteLine("7 - Odhlásit hráče z turnaje");
            Console.WriteLine("0 - Konec");

            int volba = Vstup.NactiCislo("Zadejte volbu (0-7): ", 0, 7);
            Console.WriteLine();

            try
            {
                switch (volba)
                {
                    case 1:
                        PridatHrace();
                        break;
                    case 2:
                        OdebratHrace();
                        break;
                    case 3:
                        VypsatHrace();
                        break;
                    case 4:
                        ZalozTurnaj();
                        break;
                    case 5:
                        ZrusTurnaj();
                        break;
                    case 6:
                        PrihlasitHrace();
                        break;
                    case 7:
                        OdhlasitHrace();
                        break;
                    case 0:
                        bezi = false;
                        Console.WriteLine("Konec programu.");
                        break;
                }
            }
            catch (TurnajException ex)
            {
                Console.WriteLine("Chyba: " + ex.Message);
            }
        }
    }

    private void PridatHrace()
    {
        string jmeno = Vstup.NactiText("Zadejte jméno: ");
        string prezdivka = Vstup.NactiText("Zadejte přezdívku: ");
        klub.Hraci.Pridej(jmeno, prezdivka);
        Console.WriteLine("Hráč byl úspěšně přidán.");
    }

    private void OdebratHrace()
    {
        string prezdivka = Vstup.NactiText("Zadejte přezdívku hráče pro odebrání: ");
        klub.OdeberHrace(prezdivka);
        Console.WriteLine("Hráč byl odebrán.");
    }

    private void VypsatHrace()
    {
        List<Hrac> hraci = klub.Hraci.Vsichni();
        if (hraci.Count == 0)
        {
            Console.WriteLine("V klubu nejsou žádní hráči.");
            return;
        }

        Console.WriteLine("Seznam hráčů v klubu:");
        foreach (Hrac hrac in hraci)
        {
            Console.WriteLine("- " + hrac.Prezdivka + " (" + hrac.Jmeno + ")");
        }
    }

    private void ZalozTurnaj()
    {
        if (klub.AktualniTurnaj != null)
        {
            Console.WriteLine("Turnaj už je založený.");
            return;
        }

        string nazev = Vstup.NactiText("Zadejte název turnaje: ");
        string disciplina = Vstup.NactiText("Zadejte disciplínu: ");
        int pocetKol = Vstup.NactiCislo("Zadejte počet kol: ", 1, 100);

        klub.ZalozTurnaj(nazev, disciplina, pocetKol);
        Console.WriteLine("Turnaj byl úspěšně založen.");
    }

    private void ZrusTurnaj()
    {
        if (klub.AktualniTurnaj == null)
        {
            Console.WriteLine("Turnaj není založený.");
            return;
        }

        bool potvrdit = Vstup.NactiAnoNe("Opravdu chcete zrušit turnaj? (a/n): ");
        if (potvrdit)
        {
            klub.ZrusTurnaj();
            Console.WriteLine("Turnaj byl zrušen.");
        }
        else
        {
            Console.WriteLine("Zrušení turnaje bylo stornováno.");
        }
    }

    private void PrihlasitHrace()
    {
        if (klub.AktualniTurnaj == null)
        {
            Console.WriteLine("Turnaj není založený.");
            return;
        }

        string prezdivka = Vstup.NactiText("Zadejte přezdívku hráče k přihlášení: ");
        klub.PrihlasDoTurnaje(prezdivka);
        Console.WriteLine("Hráč byl přihlášen do turnaje.");
    }

    private void OdhlasitHrace()
    {
        if (klub.AktualniTurnaj == null)
        {
            Console.WriteLine("Turnaj není založený.");
            return;
        }

        string prezdivka = Vstup.NactiText("Zadejte přezdívku hráče k odhlášení: ");
        klub.OdhlasZTurnaje(prezdivka);
        Console.WriteLine("Hráč byl odhlášen z turnaje.");
    }
}
