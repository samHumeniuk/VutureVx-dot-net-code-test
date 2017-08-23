using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Vuture.CodingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test Task 1:
            String task1TestPhrase = "I have some cheese";
            Char task1TestLetter = 'e';
            int task1Result = findNumberOfOccurrences(task1TestLetter, task1TestPhrase);
            Console.WriteLine("\"" + task1TestPhrase + "\" contains " + task1Result + " "+ task1TestLetter +"'s\n" );

            //Test Task 2:
            String task2TestPhrase = "God saved Eva's dog";
            Boolean task2Result = isWordAPalendrome(task2TestPhrase);
            Console.WriteLine("\"" + task2TestPhrase + "\" " + (task2Result ? "is": "is not")  + " a palendrome \n" );

            //Test Task 3 Part A
            String task3aTestPhrase = "I have a cat named Meow and a dog name Woof. I love the dog a lot. He is larger than a small horse.";
            List<String> task3TestList = new List<String> { "dog", "cat", "large" };
            String task3Result = countNumberOfCensoredWords(task3TestList, task3aTestPhrase);
            Console.WriteLine("\"" + task3aTestPhrase + "\" contains: " + task3Result + "\n" );

            //Test Task 3 Part B
            String task3bTestPhrase = "I have a cat named Meow and a dog name Woof. I love the dog a lot. He is larger than a small horse.";
            List<String> task3bTestList = new List<String> { "meow", "woof"};
            String task3bResult = censorText (task3bTestList, task3bTestPhrase);
            Console.WriteLine(task3bResult + "\n");

            //Test Task3 Part C
            String task3cTestPhrase = "Anna went to vote in the election to fulfil her civic duty";
            String task3cResult = censorPalendromes(task3cTestPhrase);
            Console.WriteLine(task3cResult + "\n");

            //stops programme from closing immediately:
            int a = Console.Read();
        }

        //Task 1
        static int findNumberOfOccurrences(Char letterToLookFor, String stringToBeSearched)
        {
            int numberOfOccurences = 0;

            for (int i = 0; i < stringToBeSearched.Length; i++)
            {
                Char characterToBeSearched = stringToBeSearched[i];
                if (characterToBeSearched == letterToLookFor)
                {
                    numberOfOccurences++;
                }
            }
            return numberOfOccurences;
        }

        //Task 2
        static String reverseWord(String word)
        {
            String reversedWord = "";
            int wordLength = word.Length;
            for (int i=wordLength-1; i>=0; i--)
            {
                reversedWord = reversedWord + word[i];
            }
            return reversedWord;
        }

        static String removePunctuation(String text)
        {
            String textWithoutPunctuation = "";
            foreach (char character in text)
            {
                if (char.IsLetter(character))
                {
                    textWithoutPunctuation = textWithoutPunctuation + character;
                }
            }
            return textWithoutPunctuation;
        }

        static Boolean isWordAPalendrome(String word)
        {
            word = removePunctuation(word);

            //Could have used built-in reverse method
            String reversedWord = reverseWord(word);
            return reversedWord.ToLower() == word.ToLower();
        }

        //Task 3

        //Part A)
        static int findNumberOfOccurrences(String stringToLookFor, String stringToBeSearched)
        {
            int numberOfOccurences = 0;
            int lengthOfStringToLookFor = stringToLookFor.Length;

            for (int i = 0; i < stringToBeSearched.Length - lengthOfStringToLookFor; i++)
            {
                String substring = stringToBeSearched.Substring(i, lengthOfStringToLookFor);
                if (substring.ToLower() == stringToLookFor.ToLower())
                {
                    numberOfOccurences++;
                }
            }
            return numberOfOccurences;
        }

        static String countNumberOfCensoredWords(List<String> censoredWords, String textToBeChecked)
        {
            String listOfOccurances = "";
            int totalOccurances = 0;

            foreach (string censoredWord in censoredWords)
            {
                int numberOfOccurences = findNumberOfOccurrences(censoredWord, textToBeChecked);
                listOfOccurances = listOfOccurances + censoredWord + ": " + numberOfOccurences + ", ";
                totalOccurances = totalOccurances + numberOfOccurences;
            }
            listOfOccurances = listOfOccurances + "total: " + totalOccurances; 
            return listOfOccurances;
        }

        //Part B)
        static String censorWord(String word)
        {
            int lengthOfWord = word.Length;
            String dollarSigns = new String('*', lengthOfWord - 2);
            String replacementWord = word[0] + dollarSigns + word[lengthOfWord - 1];
            return replacementWord;
        }

        static String censorText(List<String> censoredWords, String textToBeCensored)
        {
            foreach (String censoredWord in censoredWords)
            {
                int lengthOfWord = censoredWord.Length;
                
                //Could have used a regex here:
                for (int i = 0; i < textToBeCensored.Length - lengthOfWord; i++)
                {
                    String potentialBadWord = textToBeCensored.Substring(i, lengthOfWord);
                    if (potentialBadWord.ToLower() == censoredWord.ToLower())
                    {
                        String replacementWord = censorWord(potentialBadWord);
                        textToBeCensored = textToBeCensored.Substring(0, i) + replacementWord + textToBeCensored.Substring(i + lengthOfWord, textToBeCensored.Length -i -lengthOfWord);
                    }
                }
            }
            return textToBeCensored;
        }

        //Part C
        static String censorPalendromes(String textToBeCensored)
        {
            String[] words = textToBeCensored.Split(' ');
            String censoredText = "";

            for (int i=0; i<words.Length; i++)
            {
                String word = words[i];
                int lengthOfWord = word.Length;
                if (isWordAPalendrome(word))
                {
                    word = censorWord(word);
                }
                censoredText = censoredText + word + " ";
            }
            return censoredText;
        }


    }
}
