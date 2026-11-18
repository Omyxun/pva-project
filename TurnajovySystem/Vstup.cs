using System;

public class Vstup
{
    public static string NactiText(string otazka)
    {
        while (true)
        {
            Console.Write(otazka);
            string vstup = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(vstup))
            {
                Console.WriteLine("Vstup nesmi byt prazdny.");
                continue;
            }

            return vstup.Trim();
        }
    }

    public static int NactiCislo(string otazka, int min, int max)
    {
        while (true)
        {
            Console.Write(otazka);
            string vstup = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(vstup))
            {
                Console.WriteLine("Vstup nesmi byt prazdny.");
                continue;
            }

            if (!int.TryParse(vstup, out int cislo))
            {
                Console.WriteLine("Musite zadat cislo, ne text.");
                continue;
            }

            if (cislo < min || cislo > max)
            {
                Console.WriteLine($"Cislo musi byt od {min} do {max}.");
                continue;
            }

            return cislo;
        }
    }

    public static bool NactiAnoNe(string otazka)
    {
        while (true)
        {
            Console.Write(otazka);
            string vstup = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(vstup))
            {
                Console.WriteLine("Vstup nesmi byt prazdny.");
                continue;
            }

            vstup = vstup.Trim().ToLower();

            if (vstup == "a" || vstup == "ano")
            {
                return true;
            }

            if (vstup == "n" || vstup == "ne")
            {
                return false;
            }

            Console.WriteLine("Zadejte 'a' pro ano nebo 'n' pro ne.");
        }
    }
}
