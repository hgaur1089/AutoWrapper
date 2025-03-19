using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace AutoWrapper.Extensions
{
    internal static class StringExtension
    {
        public static bool IsValidJson(this string text)
        {
            text = text.Trim();
            if ((text.StartsWith("{") && text.EndsWith("}")) || //For object
                (text.StartsWith("[") && text.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(text);
                    return true;
                }
                catch(Exception) {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static (bool IsEncoded, string ParsedText) VerifyBodyContent(this string text)
        {
            if(!IsValidJsonText(text)) 
            {
                return (false, text);
            }
            try
            {
                var obj = JToken.Parse(text);
                return (true, obj.ToString());
            }
            catch (Exception)
            {
                return (false, text);
            }
        }

        public static bool IsHtml(this string text)
        {
            Regex tagRegex = new Regex(@"<\s*([^ >]+)[^>]*>.*?<\s*/\s*\1\s*>");

            return tagRegex.IsMatch(text);
        }


        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return Char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

        private static bool IsValidJsonText(string text)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                return false;
            }
            bool startsWithBrace = text.StartsWith("{") && text.EndsWith("}");
            bool startsWithBracket = text.StartsWith("[") && text.EndsWith("]");
            
            return startsWithBrace || startsWithBracket;
        }
        
    }
}
