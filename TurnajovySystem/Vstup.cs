using System;

namespace TurnajovySystem
{
    public static class Vstup
    {
        public static string NactiText(string otazka)
        {
            while (true)
            {
                Console.Write(otazka);
                string radek = Console.ReadLine();

                if (radek != null)
                {
                    string orezany = radek.Trim();
                    if (orezany.Length > 0)
                    {
                        return orezany;
                    }
                }

                Console.WriteLine("Text nesmi byt prazdny.");
            }
        }

        public static int NactiCislo(string otazka, int min, int max)
        {
            while (true)
            {
                Console.Write(otazka);
                string radek = Console.ReadLine();

                if (radek == null || radek.Trim().Length == 0)
                {
                    Console.WriteLine("Vstup nesmi byt prazdny.");
                    continue;
                }

                string orezany = radek.Trim();
                int cislo;
                bool jeCislo = int.TryParse(orezany, out cislo);

                if (!jeCislo)
                {
                    Console.WriteLine("To neni cislo.");
                }
                else if (cislo < min || cislo > max)
                {
                    Console.WriteLine($"Cislo musi byt od {min} do {max}.");
                }
                else
                {
                    return cislo;
                }
            }
        }

        public static bool NactiAnoNe(string otazka)
        {
            while (true)
            {
                Console.Write(otazka + " (a/n): ");
                string radek = Console.ReadLine();

                if (radek != null)
                {
                    string orezany = radek.Trim().ToLower();
                    if (orezany == "a")
                    {
                        return true;
                    }
                    if (orezany == "n")
                    {
                        return false;
                    }
                }

                Console.WriteLine("Zadej 'a' pro ano nebo 'n' pro ne.");
            }
        }
    }
}
