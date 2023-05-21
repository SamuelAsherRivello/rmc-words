
using BuddingMonkey;

namespace RMC_Words.Scripts.Runtime.RMC.Utilities
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------

    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class EnglishDictionaryUtility
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        public static int MaxWordLength => WordGameDict.MaxWordLength;
        
        //  Fields ----------------------------------------
        private WordGameDict _wordGameDict;


        //  Initialization --------------------------------
        public EnglishDictionaryUtility()
        {
            _wordGameDict = new WordGameDict();
        }


        //  Methods ---------------------------------------
        public bool IsValidWord(string word, int minWordLength)
        {
            return _wordGameDict.CheckWord(word, minWordLength);
        }
        
        public string GetRandomWord(int minWordLength, int maxWordLength)
        {
            return _wordGameDict.GetRandomWord(minWordLength, maxWordLength);
        }

        //  Event Handlers --------------------------------
    }
}