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
                Kolo aktKolo = klub.AktualniTurnaj.AktualniKolo();
                if (aktKolo != null)
                {
                    Console.WriteLine("Aktuální kolo: " + aktKolo.Cislo + " (" + TextStavuKola(aktKolo.Stav) + ")");
                }
            }
            else
            {
                Console.WriteLine("Turnaj není založený.");
            }

            Console.WriteLine();
            Console.WriteLine("Hráči a turnaj:");
            Console.WriteLine("1 - Přidat hráče");
            Console.WriteLine("2 - Odebrat hráče");
            Console.WriteLine("3 - Vypsat hráče");
            Console.WriteLine("4 - Založit turnaj");
            Console.WriteLine("5 - Zrušit turnaj");
            Console.WriteLine("6 - Přihlásit hráče do turnaje");
            Console.WriteLine("7 - Odhlásit hráče z turnaje");
            Console.WriteLine();
            Console.WriteLine("Kola a výsledky:");
            Console.WriteLine("8 - Nové kolo");
            Console.WriteLine("9 - Losování");
            Console.WriteLine("10 - Zadání výsledku");
            Console.WriteLine("11 - Uzavření kola");
            Console.WriteLine("12 - Výpis kola");
            Console.WriteLine();
            Console.WriteLine("0 - Konec");

            int volba = Vstup.NactiCislo("Zadejte volbu (0-12): ", 0, 12);
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
                    case 8:
                        NoveKolo();
                        break;
                    case 9:
                        Losovani();
                        break;
                    case 10:
                        ZadatVysledek();
                        break;
                    case 11:
                        UzavritKolo();
                        break;
                    case 12:
                        VypsatKolo();
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

    private void NoveKolo()
    {
        if (klub.AktualniTurnaj == null)
        {
            Console.WriteLine("Turnaj není založený.");
            return;
        }

        Kolo kolo = klub.AktualniTurnaj.NoveKolo();
        Console.WriteLine("Kolo " + kolo.Cislo + " bylo úspěšně vytvořeno.");
    }

    private void Losovani()
    {
        if (klub.AktualniTurnaj == null)
        {
            Console.WriteLine("Turnaj není založený.");
            return;
        }

        Kolo kolo = klub.AktualniTurnaj.AktualniKolo();
        if (kolo == null)
        {
            Console.WriteLine("Není vytvořeno žádné kolo.");
            return;
        }

        if (kolo.Stav != Kolo.LOSUJE_SE)
        {
            Console.WriteLine("Losování tohoto kola už bylo dokončeno.");
            return;
        }

        while (true)
        {
            List<Hrac> nesparovani = new List<Hrac>();
            foreach (Hrac hrac in klub.AktualniTurnaj.Prihlaseni)
            {
                if (!kolo.ObsahujeHrace(hrac.Prezdivka))
                {
                    nesparovani.Add(hrac);
                }
            }

            if (nesparovani.Count <= 1)
            {
                klub.AktualniTurnaj.DokonciLosovani();
                Console.WriteLine("Losování bylo dokončeno.");
                if (kolo.VolnyLos != null)
                {
                    Console.WriteLine("Volný los: " + kolo.VolnyLos.Prezdivka);
                }
                else
                {
                    Console.WriteLine("Volný los: nikdo");
                }
                break;
            }

            Console.WriteLine("Hráči, kteří ještě nemají soupeře:");
            foreach (Hrac h in nesparovani)
            {
                Console.WriteLine("- " + h.Prezdivka);
            }

            try
            {
                string p1 = Vstup.NactiText("Zadejte prvního hráče: ");
                string p2 = Vstup.NactiText("Zadejte druhého hráče: ");
                klub.AktualniTurnaj.PridejDvojici(p1, p2);
                Console.WriteLine("Dvojice byla přidána.");
                Console.WriteLine();
            }
            catch (TurnajException ex)
            {
                Console.WriteLine("Chyba: " + ex.Message);
                Console.WriteLine();
            }
        }
    }

    private void ZadatVysledek()
    {
        if (klub.AktualniTurnaj == null)
        {
            Console.WriteLine("Turnaj není založený.");
            return;
        }

        Kolo kolo = klub.AktualniTurnaj.AktualniKolo();
        if (kolo == null)
        {
            Console.WriteLine("Není vytvořeno žádné kolo.");
            return;
        }

        if (kolo.Zapasy.Count == 0)
        {
            Console.WriteLine("V kole nejsou žádné zápasy.");
            return;
        }

        Console.WriteLine("Zápasy v kole:");
        for (int i = 0; i < kolo.Zapasy.Count; i++)
        {
            Zapas z = kolo.Zapasy[i];
            Console.WriteLine(z.Cislo + ". " + z.Prvni.Prezdivka + " vs " + z.Druhy.Prezdivka + " (" + TextVysledku(z) + ")");
        }

        int cisloZapasu = Vstup.NactiCislo("Číslo zápasu: ", 1, kolo.Zapasy.Count);
        Console.WriteLine("1 - Výhra prvního (" + kolo.Zapasy[cisloZapasu - 1].Prvni.Prezdivka + ")");
        Console.WriteLine("2 - Výhra druhého (" + kolo.Zapasy[cisloZapasu - 1].Druhy.Prezdivka + ")");
        Console.WriteLine("3 - Remíza");
        int vysledek = Vstup.NactiCislo("Volba (1, 2, 3): ", 1, 3);

        klub.AktualniTurnaj.ZadejVysledek(cisloZapasu, vysledek);
        Console.WriteLine("Výsledek byl zaznamenán.");
    }

    private void UzavritKolo()
    {
        if (klub.AktualniTurnaj == null)
        {
            Console.WriteLine("Turnaj není založený.");
            return;
        }

        Kolo kolo = klub.AktualniTurnaj.AktualniKolo();
        if (kolo == null)
        {
            Console.WriteLine("Není vytvořeno žádné kolo.");
            return;
        }

        klub.AktualniTurnaj.UzavriKolo();
        Console.WriteLine("Kolo " + kolo.Cislo + " bylo uzavřeno.");
    }

    private void VypsatKolo()
    {
        if (klub.AktualniTurnaj == null)
        {
            Console.WriteLine("Turnaj není založený.");
            return;
        }

        Kolo kolo = klub.AktualniTurnaj.AktualniKolo();
        if (kolo == null)
        {
            Console.WriteLine("Zatím nebylo vytvořeno žádné kolo.");
            return;
        }

        Console.WriteLine("Kolo " + kolo.Cislo + " (" + TextStavuKola(kolo.Stav) + "):");
        if (kolo.Zapasy.Count == 0)
        {
            Console.WriteLine("Zatím nejsou vytvořeny žádné zápasy.");
        }
        else
        {
            for (int i = 0; i < kolo.Zapasy.Count; i++)
            {
                Zapas z = kolo.Zapasy[i];
                Console.WriteLine("- Zápas " + z.Cislo + ": " + z.Prvni.Prezdivka + " vs " + z.Druhy.Prezdivka + " [" + TextVysledku(z) + "]");
            }
        }

        if (kolo.VolnyLos != null)
        {
            Console.WriteLine("Volný los: " + kolo.VolnyLos.Prezdivka + " (" + kolo.VolnyLos.Jmeno + ")");
        }
    }

    private string TextStavuKola(int stav)
    {
        switch (stav)
        {
            case Kolo.LOSUJE_SE:
                return "losuje se";
            case Kolo.HRAJE_SE:
                return "hraje se";
            case Kolo.UZAVRENE:
                return "uzavřené";
            default:
                return "neznámý";
        }
    }

    private string TextVysledku(Zapas z)
    {
        switch (z.Vysledek)
        {
            case Zapas.VYHRA_PRVNIHO:
                return "1:0 (výhra " + z.Prvni.Prezdivka + ")";
            case Zapas.VYHRA_DRUHEHO:
                return "0:1 (výhra " + z.Druhy.Prezdivka + ")";
            case Zapas.REMIZA:
                return "remíza";
            default:
                return "neodehráno";
        }
    }
}
