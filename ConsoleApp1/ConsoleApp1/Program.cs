using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace Wordcount
{
    class count
    {
        int cnum = 0;
        int lines = 0;

        public int linescount(string txt)
        {
            var path = txt;
            char[] txt1 = txt.ToArray();
            using (var sr = new StreamReader(path))
            {
                var ls = "";
                while ((ls = sr.ReadLine()) != null)
                {
                    lines++;
                }
            }
            return lines;
        }

        public int charcount(string txt)
        {
            char[] txt1 = txt.ToArray();
            for (int i = 0; i < txt1.Length; i++)
            {
                if (txt1[i] >= 0 && txt1[i] <= 127)
                    cnum++;
            }
            return cnum;

        }

        public void wordcount(string text)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            StreamReader sr = new StreamReader(text);
            string line = sr.ReadLine();
            line = line.ToLower();
            string[] wordArr = null;
            int num = 0;
            while (!sr.EndOfStream)
            {

                wordArr = line.Split(' ');
                foreach (string s in wordArr)
                {
                    if (s.Length == 0)
                        continue;
                    //去除标点
                    line = Regex.Replace(line, @"[\p{P}*]", "", RegexOptions.Compiled);
                    //将单词加入dic表中
                    if (dic.ContainsKey(s))
                    {
                        num = Convert.ToInt32(dic[s]) + 1;
                        dic[s] = num;
                    }
                    else
                    {
                        dic.Add(s, 1);
                    }
                }
                line = sr.ReadLine();
            }

            ArrayList wordList = new ArrayList(dic.Keys);

            wordList.Sort();

            string tmp = String.Empty;
            int valueTmp = 0;
            for (int i = 1; i < wordList.Count; i++)
            {
                tmp = wordList[i].ToString();
                valueTmp = dic[wordList[i].ToString()];//次数
                int j = i;
                while (j > 0 && valueTmp > (int)dic[wordList[j - 1].ToString()])
                {
                    wordList[j] = wordList[j - 1];
                    j--;
                }
                wordList[j] = tmp;//j=0
            }

            Console.WriteLine("word：{wordList.Count}");
            foreach (object item in wordList)
            {
                Console.WriteLine((string)item + ":" + (int)dic[item.ToString()]);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            count cc = new count();
            string gettext = Console.ReadLine();
            string text = @gettext;
            if (!File.Exists(text))
            {
                Console.WriteLine("文件不存在！");
                return;
            }
            string txt = File.ReadAllText(@text);

            Console.WriteLine("characters:" + cc.charcount(txt));
            Console.WriteLine("lines:" + cc.linescount(text));
            cc.wordcount(text);
            Console.ReadKey();
        }
    }
}