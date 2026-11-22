public class Validace
{
    public static void ZkontrolujText(string text, string coTo)
    {
        if (text == null || text.Trim() == "")
        {
            throw new TurnajException(coTo + " nesmí být prázdné.");
        }
        if (text.Contains(";"))
        {
            throw new TurnajException(coTo + " nesmí obsahovat středník.");
        }
    }
}
