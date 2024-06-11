public static class Commands
{
    public const string SHOW_WORDS = "/show-words";
    public const string SCORE = "/score";
    public const string TOTAL_SCORE = "/total-score";
    public const string CLEAR_FILE = "/clear-file";

    public static List<string> CommandsList()
    {
        List<string> list = new List<string> { SHOW_WORDS, SCORE, TOTAL_SCORE, CLEAR_FILE };
        return list;
    }
}
