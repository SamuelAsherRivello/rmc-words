using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BuddingMonkey
{

	public class WordGameDict
	{
		// In C# using a HashSet is an O(1) operation. It's a dictionary without the keys!
		private HashSet<string> words = new HashSet<string>();

		private TextAsset dictText;
		
		public const int MaxWordLength = 8; //Manually check the text file and see longest words.

		public WordGameDict()
		{
			InitializeDictionary("ospd");
		}

		public WordGameDict(string filename)
		{
			InitializeDictionary(filename);
		}

		protected void InitializeDictionary(string filename)
		{
			dictText = (TextAsset)Resources.Load(filename, typeof(TextAsset));
			var text = dictText.text;

			foreach (string s in text.Split('\n'))
			{
				words.Add(s);
			}
		}

		public bool CheckWord(string word, int minWordLength)
		{
			if (word.Length == 0)
			{
				throw new Exception($"Word of {word} will throw an exception. It is empty.");
			}
			
			if (word.Length > MaxWordLength)
			{
				throw new Exception($"Word.Length of {word.Length} is too long for current dictionary.");
			}
			
			//This ok, but false
			if (word.Length < minWordLength)
			{
				Debug.LogError($"Word.Length of {word.Length} is too short for parameters.");
				return false;
			}

			return (words.Contains(word.ToUpper()));
		}

		public string GetRandomWord(int minLength, int maxLength)
		{
			if (minLength > maxLength)
			{
				throw new Exception("Invalid input 1");
			}
			
			if (maxLength == 0)
			{
				throw new Exception("Invalid input 2");
			}
			
			if (maxLength > MaxWordLength)
			{
				throw new Exception("Invalid input 3");
			}
			
			//try 100 times then give up.
			for (int i = 0; i < 100; i++)
			{
				int nextIndex = Random.Range(0, words.Count);
				string nextWord = words.ElementAt(nextIndex);
				if (nextWord.Length >= minLength && nextWord.Length >= maxLength)
				{
					return nextWord;
				}
			}
			
			Debug.LogError("found nothing");
			return "";
		}
	}
}
