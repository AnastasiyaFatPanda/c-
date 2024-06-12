public static class Commands
{
    public const string SHOW_WORDS = "/show-words";
    public const string SCORE = "/score";
    public const string TOTAL_SCORE = "/total-score";

    public static List<string> CommandsList()
    {
        List<string> list = new List<string> { SHOW_WORDS, SCORE, TOTAL_SCORE };
        return list;
    }
}
