// chyba v pravidlech turnaje (napr. hrac uz v kole je)
// logika ji vyhodi, menu ji chyti a vypise zpravu
public class TurnajException : Exception
{
    public TurnajException(string zprava) : base(zprava)
    {
    }
}
