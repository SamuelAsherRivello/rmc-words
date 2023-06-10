using System.Collections.Generic;
using System.Linq;

namespace RMC_Words.Scripts.Runtime.RMC.Utilities
{

    /// <summary>
    /// Replace with comments...
    /// </summary>
    public static class WordsHelper
    {

        //  Methods ---------------------------------------
        public static bool IsASingleLetter(string input)
        {
            return input.Length == 1 && char.IsLetter(char.Parse(input));
        }

        public static List<string> ConvertStringToList(string input)
        {
            return input.Select(x => x.ToString()).ToList();
        }
    }
}