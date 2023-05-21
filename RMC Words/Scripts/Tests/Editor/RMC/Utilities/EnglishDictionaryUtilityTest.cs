using System;
using NUnit.Framework;
using RMC_Words.Scripts.Runtime.RMC.Utilities;
using UnityEngine;

namespace RMC.Core.Templates
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    [Category("RMC.Core")]
    public class EnglishDictionaryUtilityTest
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------


        //  Fields ----------------------------------------
        private static string[] validWords = new string[]
        {
            "Hello","HELLO",
            "car","CAR", 
            "DIERETIC", "dieREtic"
        };
        
        private static string[] invalidWords = new string[] { 
            "barp","prello","dooce", "BARP","PRELLO","DOOCE",
            "a","1", "123", "1234" 
        };
        
        private static string[] exceptionWords = new string[] { 
            "", //too short
            "DIERETICS",  //too long
            "dictionary", //too long
        };
        
        private static int[] validLengths = new int[] { 
            1,2,3,4,5,6,7, 
            EnglishDictionaryUtility.MaxWordLength
        };
        
        private static int[] exceptionLengths = new int[] { 
            0,
            EnglishDictionaryUtility.MaxWordLength + 1
        };
        
        //  Initialization --------------------------------
        [SetUp]
        public void Setup()
        {
            //Turn off chatty logging (Good general tip for testing)
            Debug.unityLogger.logEnabled = false;
        }

        
        [TearDown]
        public void TearDown()
        {
            //Turn on chatty logging (Good general tip for testing)
            Debug.unityLogger.logEnabled = true;
        }
        
       

        //  Methods ---------------------------------------
        [Test]
        public void IsValidWord_ResultIsTrue_WhenValidWord(
            [ValueSource("validWords")] string word)
        {       
            // Arrange
            EnglishDictionaryUtility englishDictionaryUtility = new EnglishDictionaryUtility();

            // Act
            bool isValidWord = englishDictionaryUtility.IsValidWord(word, 0);

            // Assert
            Assert.That(isValidWord, Is.EqualTo(true));
            
        }
        
        [Test]
        public void IsValidWord_ResultIsFalse_WhenInvalidWord(
            [ValueSource("invalidWords")] string word)
        {       
            // Arrange
            EnglishDictionaryUtility englishDictionaryUtility = new EnglishDictionaryUtility();

            // Act
            bool isValidWord = englishDictionaryUtility.IsValidWord(word, 0);

            // Assert
            Assert.That(isValidWord, Is.EqualTo(false));
        }
        
        [Test]
        public void IsValidWord_DoesThrowException_WhenExceptionWord(
            [ValueSource("exceptionWords")] string word)
        {       
            // Arrange
            EnglishDictionaryUtility englishDictionaryUtility = new EnglishDictionaryUtility();

            // Assert
            Assert.Throws<Exception>(() =>
            {
                // Act
                bool isValidWord = englishDictionaryUtility.IsValidWord(word, 0);
            });
        }

        [Test]
        public void GetRandomWord_ResultLengthValueIsX_WhenRequestedLengthIsX(
            [ValueSource("validLengths")] int length)
        {       
            // Arrange
            EnglishDictionaryUtility englishDictionaryUtility = new EnglishDictionaryUtility();

            // Act
            string word = englishDictionaryUtility.GetRandomWord(0, length);

            // Assert
            Assert.That(word.Length, Is.GreaterThan(0));
        }

        [Test]
        public void GetRandomWord_DoesThrowException_WhenExceptionLength(
            [ValueSource("exceptionLengths")] int length)
        {       
            // Arrange
            EnglishDictionaryUtility englishDictionaryUtility = new EnglishDictionaryUtility();

            // Assert
            Assert.Throws<Exception>(() =>
            {
                // Act
                string word = englishDictionaryUtility.GetRandomWord(0, length);
            });
        }
        
        [Test]
        public void GetRandomWord_ResultIsValid_WhenLengthIsValid(
            [ValueSource("validLengths")] int length)
        {       
            // Arrange
            EnglishDictionaryUtility englishDictionaryUtility = new EnglishDictionaryUtility();

            // Act 1
            string word = englishDictionaryUtility.GetRandomWord(0, length);
            
            // Act 2
            bool isValidWord = englishDictionaryUtility.IsValidWord(word, length);
            
            // Assert
            Assert.That(isValidWord, Is.True);
        }

        
        //  Event Handlers --------------------------------
    }
}