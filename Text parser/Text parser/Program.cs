using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Text_parser
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите полный путь к текстовому файлу:");
            string path = Console.ReadLine();
            try
            {
                if (!CheckCorrectFormatFile(path))
                    throw new ArgumentException("\nФормат файла не .txt\n");
            
                List<KeyValuePair<string, int>> allUniqWords = TextProcessing(path);
                WriteToFileStatistics(allUniqWords);
                PrintInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private static List<KeyValuePair<string, int>> TextProcessing(string path)
        {
            string text = File.ReadAllText(path);
            List<KeyValuePair<string, int>> allUniqWords = TextParser.GetAllUniqWordsInText(text);
            return allUniqWords;
        }

        private static bool CheckCorrectFormatFile(string path)
        {
            int offset = (".txt").Length;
            int idx = path.LastIndexOf(".txt");
            return path.Length - offset == idx;
        }

        private static void PrintInfo()
        {
            Console.WriteLine("Количество всех уникальных слов подсчитано!" +
                                "\nТекстовый файл со статистикой находится в папке Debug данного проекта." +
                                "\nНажмите любую кнопку для выхода");
            Console.ReadLine();
        }

        private static void WriteToFileStatistics(List<KeyValuePair<string, int>> dict)
        {
            using (FileStream fs = File.Create("TextStatistics.txt"))
            {
                if (dict.Count > 0)
                {
                    foreach (var word in dict)
                    {
                        string message = $"{word.Key}  {word.Value}\n";
                        Write(fs, message);
                    }
                }
                else
                    Write(fs, "Файл был без слов\n");
            }
        }

        private static void Write(FileStream fs, string message)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(message);
            fs.Write(info, 0, info.Length);
        }
    }
}
