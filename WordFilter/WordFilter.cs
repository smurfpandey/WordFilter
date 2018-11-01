using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace WordFilterNS
{
    public class WordFilter
    {
        private List<string> lstBadWords = new List<string>();
        private readonly string BADWORDS_FILE_NAME = "badwords.json";

        public WordFilter()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(BADWORDS_FILE_NAME));
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                lstBadWords = JsonConvert.DeserializeObject<List<string>>(result);
            }            
        }

        public bool Blacklisted(string sentence)
        {
            sentence = sentence.ToLower();
            return lstBadWords.Exists(word => sentence.Contains(word));
        }

        public void AddWords(string[] arrWords)
        {
            lstBadWords.AddRange(arrWords);
        }

        public void RemoveWord(string word)
        {
            lstBadWords.Remove(word);
        }

        public void ClearList()
        {
            lstBadWords = new List<string>();
        }
    }
}
