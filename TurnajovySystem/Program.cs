try
{
    Menu menu = new Menu();
    menu.Spust();
}
catch (EndOfStreamException)
{
    Console.WriteLine("\nKonec vstupu.");
}
