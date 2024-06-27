namespace MyProject.Models
{
    public class Commands
    {
        public const string SHOW_WORDS = "/show-words";
        public const string SCORE = "/score";
        public const string TOTAL_SCORE = "/total-score";

       public static List<string> CommandsList = new List<string> { SHOW_WORDS, SCORE, TOTAL_SCORE };
    }
}
