using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Dictionary dictionary;
                Console.WriteLine("Choose vocabylary type\n1.e-r\n2.r-e");
                int nVocabType = Convert.ToInt32(Console.ReadLine());
                bool VocabType;
                if (nVocabType == 1) VocabType = true;
                else if (nVocabType == 2) VocabType = false;
                else throw new Exception("Incorrect input information.");
                Console.WriteLine("Vocabulary path\n1.Default path\n2.Enter my path");
                int nPath = Convert.ToInt32(Console.ReadLine());
                if (nPath == 1) dictionary = new Dictionary(VocabType);
                else if (nPath == 2)
                {
                    Console.WriteLine("Enter path(.xml file)");
                    string path = Console.ReadLine();
                    if (!Regex.IsMatch(path, @"\w{1,}.xml")) throw new Exception("Wrong path.");
                    dictionary = new Dictionary(path,VocabType);
                }
                else throw new Exception("Incorrect input information.");
                Console.Clear();
                string StartMenu = "1.Add word/translation\n2.Delete word\n3.Delete translation\n4.Edit word\n5.Edit translation\n6.Search word\n7.Print\n8.Exit";
                Console.WriteLine(StartMenu);

              
                while(true)
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter word");
                                    string word = Console.ReadLine();
                                    Console.WriteLine("Enter translation");
                                    string translation = Console.ReadLine();
                                    dictionary.Add(word, translation);
                                    Console.Clear();
                                    Console.WriteLine("Word was added. Add one more?(Y/N)");
                                    string repeat = Console.ReadLine();
                                    if (repeat == "Y") Console.Clear();
                                    else if (repeat == "N")
                                    {
                                        Console.Clear();
                                        Console.WriteLine(StartMenu);
                                        break;
                                    }
                                    else throw new Exception("Incorrect input information.");
                                }
                                break;
                            }
                        case 2:
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter word");
                                    string word = Console.ReadLine();
                                    dictionary.DeleteWord(word);
                                    Console.Clear();
                                    Console.WriteLine("Word was deleted. Delete one more?(Y/N)");
                                    string repeat = Console.ReadLine();
                                    if (repeat == "Y") Console.Clear();
                                    else if (repeat == "N")
                                    {
                                        Console.Clear();
                                        Console.WriteLine(StartMenu);
                                        break;
                                    }
                                    else throw new Exception("Incorrect input information.");
                                }
                                break;
                            }
                        case 3:
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter word");
                                    string word = Console.ReadLine();
                                    Console.WriteLine("Enter translation");
                                    string translation = Console.ReadLine();
                                    dictionary.DeleteTranslation(word, translation);
                                    Console.Clear();
                                    Console.WriteLine("Translation was deleted. Delete one more?(Y/N)");
                                    string repeat = Console.ReadLine();
                                    if (repeat == "Y") Console.Clear();
                                    else if (repeat == "N")
                                    {
                                        Console.Clear();
                                        Console.WriteLine(StartMenu);
                                        break;
                                    }
                                    else throw new Exception("Incorrect input information.");
                                }
                                break;
                            }
                        case 4:
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter word");
                                    string word = Console.ReadLine();
                                    Console.WriteLine("Enter new word");
                                    string newWord = Console.ReadLine();
                                    dictionary.EditWord(word, newWord);
                                    Console.Clear();
                                    Console.WriteLine("Word was edited. Edit one more?(Y/N)");
                                    string repeat = Console.ReadLine();
                                    if (repeat == "Y") Console.Clear();
                                    else if (repeat == "N")
                                    {
                                        Console.Clear();
                                        Console.WriteLine(StartMenu);
                                        break;
                                    }
                                    else throw new Exception("Incorrect input information.");
                                }
                                break;
                            }
                        case 5:
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter word");
                                    string word = Console.ReadLine();
                                    Console.WriteLine("Enter translation");
                                    string translation = Console.ReadLine();
                                    Console.WriteLine("Enter new translation");
                                    string newTranslation = Console.ReadLine();
                                    dictionary.EditTranslation(word, translation, newTranslation);
                                    Console.WriteLine("Translation was edited. Edit one more?(Y/N)");
                                    string repeat = Console.ReadLine();
                                    if (repeat == "Y") Console.Clear();
                                    else if (repeat == "N")
                                    {
                                        Console.Clear();
                                        Console.WriteLine(StartMenu);
                                        break;
                                    }
                                    else throw new Exception("Incorrect input information.");
                                }
                                break;
                            }
                        case 6:
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Enter word");
                                    string word = Console.ReadLine();
                                    var arr = dictionary.SearchTranslation(word);
                                    Console.WriteLine(word);
                                    for (int i = 0; i < arr.Length; i++)
                                    {
                                        Console.Write(arr[i] + " | ");
                                    }
                                    Console.WriteLine();
                                    Console.WriteLine("Save result?(Y/N)");
                                    string chSave = Console.ReadLine();
                                    if (chSave == "Y")
                                    {
                                        Console.WriteLine("Enter path");
                                        string path = Console.ReadLine();
                                        if (!Regex.IsMatch(path, @"\w{1,}.xml")) throw new Exception("Wrong path.");
                                        XDocument res = new XDocument(new XElement("root", new XElement("record", new XAttribute("word", word))));
                                        var _word = res.Root.Elements("record").FirstOrDefault(obj => obj.Attribute("word").Value == word);
                                        if (_word != null)
                                        {
                                            for (int i = 0; i < arr.Length; i++)
                                            {
                                                _word.Add(new XElement("translation", arr[i]));
                                            }
                                        }
                                        res.Save(path);
                                        break;
                                    }
                                    else if (chSave == "N")
                                    {
                                        Console.Clear();
                                        Console.WriteLine(StartMenu);
                                        break;
                                    }
                                    else throw new Exception("Incorrect input information.");
                                }
                                break;
                            }
                        case 7:
                            {
                                Console.Clear();
                                dictionary.Print();
                                Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine(StartMenu);
                                break;
                            }
                        case 8:
                            {
                                Environment.Exit(0);
                                break;
                            }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            Console.Read();
        }
    }
}
