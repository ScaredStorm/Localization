using System;
using System.IO;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Localization.Instance.Parse("nl_NL.txt");
            Localization.Instance.iterate();
            Console.WriteLine(Localization.Instance.GetString("Nederlands", "main.kijkhier"));
        }
    }

    public class Localization
    {
        private static Localization m_instance;
        public static Localization Instance {
            get
            {
                if(m_instance == null)
                {
                    m_instance = new Localization();
                }
                return m_instance;
            }
        }
        private Dictionary<string, Dictionary<string, string>> m_strings;

        public Localization()
        {
            m_strings = new Dictionary<string, Dictionary<string, string>>();
        }

        public void Parse(string fileName)
        {
            if (File.Exists(fileName))
            {
                var lines = File.ReadLines(fileName);
                string language = "";
                Dictionary<string, string> allStrings = new Dictionary<string, string>();
                
                foreach (var line in lines)
                {
                    if (line == "" || line == string.Empty) continue;
                    string[] kv = line.Split('=');
                    if (kv[0].Equals("language")) language = kv[1];
                    allStrings.Add(kv[0], kv[1]);
                }

                if (allStrings.Count > 0 && language != string.Empty)
                {
                    m_strings.Add(language, allStrings);
                }
            }
        }

        public void iterate()
        {
            foreach (var x in m_strings)
            {
                foreach (var z in x.Value)
                {
                    Console.WriteLine($"Key: {z.Key}, Value: {z.Value}");
                }
            }
        }

        public string GetString(string language, string key)
        {
            if (m_strings.ContainsKey(language))
            {
                if (m_strings[language].ContainsKey(key))
                {
                    return m_strings[language][key];
                }
            }
            return "";
        }
    }
}
