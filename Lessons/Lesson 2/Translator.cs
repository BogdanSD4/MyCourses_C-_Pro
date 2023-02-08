using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lessons
{
    public class Translator
    {
        public static string Language = "ukrainian";
        public static string Translate(string text)
        {
            var result = "";
            var html = "";

            var request = "https://www.google.com/search?q=" + $"google {Language} to english translate " + text;
            html = CreateWebRequest(Uri.EscapeUriString(request)).ReadToEnd();

            TextConformity(out result, html, startSymbol: "<input value=\"", stopSymbol: '\"');

            return result;
        }
        private static StreamReader CreateWebRequest(string linkRequest)
        {
            WebRequest web = WebRequest.Create(linkRequest);
            web.Proxy = new WebProxy();
            Stream objStream = web.GetResponse().GetResponseStream();
            StreamReader objReader = new StreamReader(objStream);
            return objReader;
        }

        private static void TextConformity(out string result, string target, string findWord = null,
            string startSymbol = null, char stopSymbol = ' ', int stopSymbolrepeat = 0)
        {
            var res = "";
            var wordIsFind = findWord == null ? true : false;
            var startSymbolFind = startSymbol == null ? true : false;
            var repeatCount = stopSymbolrepeat;

            for (int i = 0; i < target.Length; i++)
            {
                if (!startSymbolFind)
                {
                    var correct = 0;
                    for (int j = 0; j < startSymbol.Length; j++)
                    {

                        if (target[i + j] != startSymbol[j])
                        {
                            res = "";
                            break;
                        }
                        else correct++;
                    }
                    if (correct == startSymbol.Length)
                    {
                        startSymbolFind = true;
                        i += startSymbol.Length - 1;
                    }
                    res = "";
                    continue;
                }

                res += target[i];
                if (!wordIsFind)
                {
                    var correct = 0;
                    for (int j = 0; j < findWord.Length; j++)
                    {
                        if (target[i + j] != findWord[j])
                        {
                            res = "";
                            break;
                        }
                        else
                        {
                            correct++;
                        }
                    }
                    if (correct == findWord.Length)
                    {
                        wordIsFind = true;
                    }
                }
                else
                {
                    if (i != target.Length - 1)
                    {
                        if (target[i + 1] == stopSymbol)
                        {
                            if (repeatCount-- == 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            result = res;
        }
    }
}
