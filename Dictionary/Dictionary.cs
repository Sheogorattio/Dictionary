using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Dictionary
{
    public class Dictionary:IAddWord, IPrint,IDelete,ISearch
    {
        string dictType;
        XDocument xdoc;
        public string DictType { get { return dictType; } }
        readonly string pathXML;
        public string PathXML { get { return pathXML; } }

        SortedList<string, string[]> words;//key - word, value - array of translations
        public Dictionary(bool TranslateFromEngToRus) 
        {
            if(TranslateFromEngToRus == true) dictType = "e-r".ToString();
            else dictType = "r-e".ToString() ;
            words = new SortedList<string, string[]>();
            pathXML = "dictionary.xml";
            xdoc = new XDocument();
        }
        public Dictionary(string pathXML, bool TranslateFromEngToRus) : this(TranslateFromEngToRus)
        {
            this.pathXML = pathXML;
            words = new SortedList<string, string[]>();

            //xml document creating
            XDocument doc = new XDocument(new XElement("root", "\n"));
            doc.Save(pathXML);
        }
        public Dictionary(string pathXML, SortedList<string, string[]> words, bool TranslateFromEngToRus) : this(pathXML, TranslateFromEngToRus)
        {
            this.words = words;
        }

        public void Add(string word, string translation)
        {
            switch (dictType)//is entry correct
            {
                case "e-r":
                    {
                        if (!(((word[0] >= 97 && word[0] <= 122) || (word[0] >= 65 && word[0] <= 90)) && (Regex.IsMatch(translation, @"\p{IsCyrillic}")))) throw new Exception("Wrong langauge(e-r)");
                        break;
                    }
                case "r-e":
                    {
                        if (!((Regex.IsMatch(word, @"\p{IsCyrillic}") && ((translation[0] >= 97 && translation[0] <= 122) || (translation[0] >= 65 && translation[0] <= 90))))) throw new Exception("Wrong langauge(r-e)");
                        break;
                    }
            }
          
            if(words.ContainsKey(word)) //add one more translation
            {
                string[] translations = words[word];
                string[] _translations = new string[translations.Length+1];
                for(int i = 0; i< translations.Length; i++) _translations[i] = translations[i];//making old translations copy 
                _translations[_translations.Length-1] = translation;
                words[word] = _translations;

                //xml part
                xdoc = XDocument.Load(pathXML);
                XElement elem = xdoc.Element("root")?.Elements("record").FirstOrDefault(obj => obj.Attribute("word")?.Value == word);
                elem.Add(new XElement("translation", translation));
                xdoc.Save(pathXML);

            }
            else // if there is no such word in the vocabulary
            {
                string[] TranslationsArr = new string[1] { translation};
                words.Add(word, TranslationsArr);

                //xml part
                xdoc = XDocument.Load(pathXML);
                XElement root = xdoc.Root;
                if(root!= null)
                {
                    root.Add(new XElement("record", new XAttribute("word", word), new XElement("translation", translation)));
                    xdoc.Save(pathXML);
                }
            }
        }

        public void Print()
        {
            foreach(var i in words)
            {
                Console.Write(i.Key + " - ");
                foreach (var j in i.Value)
                {
                    Console.Write(j + " | ");
                }
                Console.WriteLine();
            }
        }

        public void DeleteTranslation(string word, string translation)
        {
            if (words.ContainsKey(word))
            {
                if (words[word].Length ==1 && words[word][0].CompareTo(translation) == 0) DeleteWord(word);

                string[] _translations = new string[words[word].Length-1];
                int j = 0;
                for(int i=0; i< _translations.Length+1; i++)//creating new translations list
                {
                    if (words[word][i] == translation) continue;
                    _translations[j] = words[word][i];
                    j++;
                }
                words[word] = _translations;
            }
            else
            {
                throw new Exception(("Vocabulary does not contain such word: {0}", word).ToString());
            }
        }

        public void DeleteWord(string word)
        {
            if(words.ContainsKey(word))
            {
                words.Remove(word);
                return;
            }
            throw new Exception(("Vocabulary does not contain such word: {0}", word).ToString());
        }

        public string[] SearchTranslation(string word)
        {
            if (words.ContainsKey(word))
            {
                return words[word];
            }
            else
            {
                throw new Exception(("SearchTranslation: Vocabulary does not contain such word: {0}", word).ToString());
            }
        }
    }
}