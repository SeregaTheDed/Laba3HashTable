using MyHashTable;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Laba3HashTable
{
    public static class TextUtil
    {
        private static IEnumerable<string> GetWordsFromText(string text)
        {
            Regex wordRegex = new Regex(@"[а-яА-Я]+");
            foreach (Match word in wordRegex.Matches(text))
            {
                yield return word.Value;
            }
        }
        private static string GetWarAndWorldText()
        {
            return File.ReadAllText("WarAndWorld.txt");
        }
        public static string[] GetWarAndWorldWords()
        {
            return GetWordsFromText(GetWarAndWorldText()).ToArray();
        }
    }
    public static class CollectionSpeedTester
    {
        private readonly static string[] _warAndWorldWords = TextUtil.GetWarAndWorldWords();

        private static long GetMyHashTableTime()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            MyHashTable<string, int> collection = new MyHashTable<string, int>();
            foreach (var item in _warAndWorldWords)
            {
                collection[item] = collection.GetValueOrDefault(item, 0) + 1;
            }
            var deletingWords = collection.Where(x => x.Value > 27).Select(x => x.Key).ToArray();
            foreach (var deletingWord in deletingWords)
            {
                collection.Remove(deletingWord);
            }
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private static long GetMSDictionaryTime()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Dictionary<string, int> collection = new Dictionary<string, int>();
            foreach (var item in _warAndWorldWords)
            {
                collection[item] = collection.GetValueOrDefault(item, 0) + 1;
            }
            var deletingWords = collection.Where(x => x.Value > 27).Select(x => x.Key).ToArray();
            foreach (var deletingWord in deletingWords)
            {
                collection.Remove(deletingWord);
            }
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public static void PrintTestResult()
        {
            Console.WriteLine("MSDictionary Time: " + GetMSDictionaryTime());
            Console.WriteLine("MyHashTable Time: " + GetMyHashTableTime());
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            CollectionSpeedTester.PrintTestResult();
        }
    }
}