using System.Collections.Generic;
using System;
using System.Linq;

namespace Text_parser
{
    static class TextParser
    {
        // массив содержит основные разделители в тексте, чтобы разбить текст на слова
        private static char[] s_separators = new char[] 
        { (char)ConnectingLine.LongDash, ' ', '.', ',', '!', '?', ';', ':', '(', ')', '[', ']', '\r', '\t', '\n' };

        public static List<KeyValuePair<string, int>> GetAllUniqWordsInText(string text)
        {
            Dictionary<string, int> numberUniqueWords = new Dictionary<string, int>();
            string[] words = SplitTextIntoWords(text); // разделяем текст на слова

            foreach (string word in words)
            {
                // слово может являться чем-то таким: "«",
                // поэтому дополнительно проверяем на то, что оно состоит из букв и, возможно, дефиса
                string lowerCaseWord = new String(word.Where(ch => Char.IsLetter(ch) || ch.IsDash()).ToArray()).ToLower();
                AddWordToDict(numberUniqueWords, lowerCaseWord);
            }
            return numberUniqueWords.OrderByDescending(x => x.Value).ToList();
        }

        private static void AddWordToDict(Dictionary<string, int> numberUniqueWords, string lowerCaseWord)
        {
            if (lowerCaseWord.Length > 0)
            {
                if (numberUniqueWords.ContainsKey(lowerCaseWord))
                    numberUniqueWords[lowerCaseWord]++;
                else
                    numberUniqueWords.Add(lowerCaseWord, 1);
            }
        }

        private static string[] SplitTextIntoWords(string text)
        {
            // Есть возможность, что вместо длинного тире используется короткое/минус и тп,
            // поэтому нужно исключить из массива слов подобные варианты: "-", "--"
            return text.Split(s_separators, System.StringSplitOptions.RemoveEmptyEntries)
                        .Where(word => !word[0].IsDash()) // гарантировано существует первый непробельный символ
                        .ToArray();
        }
    }
}