// ukladani a nacitani podle formatu v navrhu
public class Uloziste
{
    public const string VYCHOZI_SOUBOR = "turnaj.txt";

    private const string HLAVICKA = "TURNAJOVY_SYSTEM;1";
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
        if (klub == null)
        {
            throw new TurnajException("Není co uložit.");
        }

        List<string> radky = new List<string>();
        radky.Add(HLAVICKA);

        // nejdriv ulozime hrace, na ktere se pak odkazuje prezdivkou
        foreach (Hrac hrac in klub.Hraci.Vsichni())
        {
            ZkontrolujText(hrac.Prezdivka);
            ZkontrolujText(hrac.Jmeno);
            radky.Add($"HRAC;{hrac.Prezdivka};{hrac.Jmeno}");
        }

        Turnaj turnaj = klub.AktualniTurnaj;
        if (turnaj != null)
        {
            ZkontrolujText(turnaj.Nazev);
            ZkontrolujText(turnaj.Disciplina);
            radky.Add($"TURNAJ;{turnaj.Nazev};{turnaj.Disciplina};{turnaj.PocetKol}");

            foreach (Hrac hrac in turnaj.Prihlaseni)
            {
                radky.Add($"PRIHLASEN;{hrac.Prezdivka}");
            }

            foreach (Kolo kolo in turnaj.Kola)
            {
                radky.Add($"KOLO;{kolo.Cislo};{kolo.Stav}");
                foreach (Zapas zapas in kolo.Zapasy)
                {
                    radky.Add($"ZAPAS;{zapas.Cislo};{zapas.Prvni.Prezdivka};{zapas.Druhy.Prezdivka};{zapas.Vysledek}");
                }
            }
        }

        // puvodni soubor nahradime az po uspesnem zapisu
        string docasnySoubor = cesta + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllLines(docasnySoubor, radky);
            File.Move(docasnySoubor, cesta, true);
        }
        catch (IOException chyba)
        {
            throw new TurnajException("Soubor se nepodařilo uložit. " + chyba.Message);
        }
        catch (UnauthorizedAccessException)
        {
            throw new TurnajException("K zápisu do souboru nemáte oprávnění.");
        }
        catch (ArgumentException)
        {
            throw new TurnajException("Cesta k souboru není platná.");
        }
        // po chybe zkusime uklidit docasny soubor
        finally
        {
            try
            {
                File.Delete(docasnySoubor);
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (ArgumentException)
            {
            }
        }
    }

    public Klub Nacti()
    {
        string[] radky;
        try
        {
            radky = File.ReadAllLines(cesta);
        }
        catch (FileNotFoundException)
        {
            throw new TurnajException("Uložený soubor neexistuje.");
        }
        catch (DirectoryNotFoundException)
        {
            throw new TurnajException("Složka s uloženým souborem neexistuje.");
        }
        catch (IOException chyba)
        {
            throw new TurnajException("Soubor se nepodařilo načíst. " + chyba.Message);
        }
        catch (UnauthorizedAccessException)
        {
            throw new TurnajException("Ke čtení souboru nemáte oprávnění.");
        }
        catch (ArgumentException)
        {
            throw new TurnajException("Cesta k souboru není platná.");
        }

        // novy klub vratime az po nacteni celeho souboru
        Klub klub = new Klub();
        Turnaj turnaj = null;
        Kolo kolo = null;
        int stavKola = Kolo.LOSUJE_SE;
        int radekKola = 0;
        int cisloRadku = 1;
        List<int> vysledky = new List<int>();

        try
        {
            if (radky.Length == 0 || radky[0] != HLAVICKA)
            {
                throw new TurnajException("Chybí platná hlavička s verzí formátu.");
            }

            for (int i = 1; i < radky.Length; i++)
            {
                cisloRadku = i + 1;
                string[] pole = radky[i].Split(';');

                switch (pole[0])
                {
                    case "HRAC":
                        ZkontrolujPole(pole, 3);
                        if (turnaj != null)
                        {
                            throw new TurnajException("Hráči musí být uvedeni před turnajem.");
                        }
                        klub.Hraci.Pridej(pole[2], pole[1]);
                        break;

                    case "TURNAJ":
                        ZkontrolujPole(pole, 4);
                        if (turnaj != null)
                        {
                            throw new TurnajException("V souboru smí být jen jeden turnaj.");
                        }
                        klub.ZalozTurnaj(pole[1], pole[2], NactiCislo(pole[3]));
                        turnaj = klub.AktualniTurnaj;
                        break;

                    case "PRIHLASEN":
                        ZkontrolujPole(pole, 2);
                        klub.PrihlasDoTurnaje(pole[1]);
                        break;

                    case "KOLO":
                        ZkontrolujPole(pole, 3);
                        if (turnaj == null)
                        {
                            throw new TurnajException("Kolo musí patřit k turnaji.");
                        }
                        if (kolo != null)
                        {
                            cisloRadku = radekKola;
                            // pred dalsim kolem obnovime stav predchoziho
                            ObnovStavKola(turnaj, stavKola, vysledky);
                            cisloRadku = i + 1;
                        }

                        int cisloKola = NactiCislo(pole[1]);
                        stavKola = NactiCislo(pole[2]);
                        if (stavKola < Kolo.LOSUJE_SE || stavKola > Kolo.UZAVRENE)
                        {
                            throw new TurnajException("Neplatný stav kola.");
                        }
                        if (cisloKola != turnaj.Kola.Count + 1)
                        {
                            throw new TurnajException("Čísla kol musí navazovat od 1.");
                        }

                        kolo = turnaj.NoveKolo();
                        radekKola = cisloRadku;
                        vysledky.Clear();
                        break;

                    case "ZAPAS":
                        ZkontrolujPole(pole, 5);
                        if (kolo == null)
                        {
                            throw new TurnajException("Zápas musí být uvedený za kolem.");
                        }

                        int cisloZapasu = NactiCislo(pole[1]);
                        int vysledek = NactiCislo(pole[4]);
                        if (cisloZapasu != kolo.Zapasy.Count + 1)
                        {
                            throw new TurnajException("Čísla zápasů musí v kole navazovat od 1.");
                        }
                        if (vysledek < Zapas.BEZ_VYSLEDKU || vysledek > Zapas.REMIZA)
                        {
                            throw new TurnajException("Neplatný výsledek zápasu.");
                        }
                        if (stavKola == Kolo.LOSUJE_SE && vysledek != Zapas.BEZ_VYSLEDKU)
                        {
                            throw new TurnajException("Při losování nesmí být zadaný výsledek.");
                        }
                        if (stavKola == Kolo.UZAVRENE && vysledek == Zapas.BEZ_VYSLEDKU)
                        {
                            throw new TurnajException("Uzavřené kolo musí mít všechny výsledky.");
                        }

                        turnaj.PridejDvojici(pole[2], pole[3]);
                        // vysledky zadame az po dokonceni losovani
                        vysledky.Add(vysledek);
                        break;

                    default:
                        throw new TurnajException("Neznámý typ záznamu.");
                }
            }

            if (kolo != null)
            {
                cisloRadku = radekKola;
                ObnovStavKola(turnaj, stavKola, vysledky);
            }
        }
        catch (TurnajException chyba)
        {
            throw new TurnajException("Poškozený soubor, řádek " + cisloRadku + ". " + chyba.Message);
        }

        return klub;
    }

    private void ObnovStavKola(Turnaj turnaj, int stav, List<int> vysledky)
    {
        if (stav == Kolo.LOSUJE_SE)
        {
            return;
        }

        // pri dokonceni losovani se znovu urci volny los
        turnaj.DokonciLosovani();
        for (int i = 0; i < vysledky.Count; i++)
        {
            if (vysledky[i] != Zapas.BEZ_VYSLEDKU)
            {
                turnaj.ZadejVysledek(i + 1, vysledky[i]);
            }
        }
        if (stav == Kolo.UZAVRENE)
        {
            turnaj.UzavriKolo();
        }
    }

    private void ZkontrolujPole(string[] pole, int pocet)
    {
        if (pole.Length != pocet)
        {
            throw new TurnajException("Nesprávný počet polí v záznamu.");
        }
        foreach (string hodnota in pole)
        {
            if (string.IsNullOrWhiteSpace(hodnota))
            {
                throw new TurnajException("Pole v záznamu nesmí být prázdné.");
            }
        }
    }

    private int NactiCislo(string text)
    {
        int cislo;
        if (!int.TryParse(text, out cislo))
        {
            throw new TurnajException("Místo celého čísla je neplatná hodnota.");
        }
        return cislo;
    }

    private void ZkontrolujText(string text)
    {
        Validace.ZkontrolujText(text, "Text");
        if (text.Contains('\r') || text.Contains('\n'))
        {
            throw new TurnajException("Text nesmí obsahovat konec řádku.");
        }
    }
}
