using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WordFilterNS
{
    public class WordFilter
    {
        private List<string> lstBadWords = new List<string>();

        public WordFilter()
        {
            string badWords = File.ReadAllText("badwords.json");
            lstBadWords = JsonConvert.DeserializeObject<List<string>>(badWords);
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
