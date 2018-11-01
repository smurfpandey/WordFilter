using WordFilterNS;
using NUnit.Framework;

namespace WordFilterTestNS
{
    [TestFixture]
    class WordFilterTest
    {

        [Test]
        public void ShouldDetectBadWordsInString()
        {
            WordFilter _wordFilter = new WordFilter();

            Assert.IsTrue(_wordFilter.Blacklisted("this string contains the word skank"), "skank should be true");
            Assert.IsTrue(_wordFilter.Blacklisted("this string contains the word SkAnK"), "SkAnK should be true");
            Assert.IsTrue(_wordFilter.Blacklisted("this string contains the wordskank"), "wordskank should be true");
            Assert.IsTrue(_wordFilter.Blacklisted("this string contains the skankword"), "skankword should be true");
            Assert.IsFalse(_wordFilter.Blacklisted("this string is clean!"), "should be false");
        }

        [Test]
        public void ShouldAddWordToBlacklist()
        {
            WordFilter _wordFilter = new WordFilter();
            _wordFilter.AddWords(new string[] { "clean_word" });

            Assert.IsTrue(_wordFilter.Blacklisted("this string is not clean with clean_word"), "clean_word should be true");
            Assert.IsFalse(_wordFilter.Blacklisted("this string is clean"), "clean should be false");
        }

        [Test]
        public void ShouldRemoveWordFromBlacklist()
        {
            WordFilter _wordFilter = new WordFilter();
            _wordFilter.RemoveWord("skank");

            Assert.IsFalse(_wordFilter.Blacklisted("this string contains the word skank"), "skank should be false as it has been removed");
        }

        [Test]
        public void ShouldClearList()
        {
            WordFilter _wordFilter = new WordFilter();
            _wordFilter.ClearList();

            Assert.IsFalse(_wordFilter.Blacklisted("this string contains the word bitch"), "bitch should be false as the list is clear");

            _wordFilter.AddWords(new string[] { "bitch" });
            Assert.IsTrue(_wordFilter.Blacklisted("this string contains the word bitch"), "bitch should be true as it was readded");
        }
    }
}
