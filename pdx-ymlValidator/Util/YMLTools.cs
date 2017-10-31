using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pdx_ymlValidator.Util
{
    /// <summary>
    /// YML处理类
    /// </summary>
    public static class YMLTools
    {
        private const string RegexFilterKey = "^.*(?=:)";
        private const string RegexFilterValue = "(?<=(\\s\")).+(?=\")";
        private const string RegexFilterKeyAndNum = "(^.*?):.*?(?=\")";
        private const string RegexFilterColorSign = "(^.*?):.*?(?=\")";

        public static string RegexGetValue(string RegText, string RegexRule)
        {
            Regex regex = new Regex(RegexRule, RegexOptions.None);
            StringBuilder returnString = new StringBuilder();
            var matches = regex.Matches(RegText);

            foreach (var item in matches)
            {
                returnString.Append(item.ToString());
            }
            return returnString.ToString();
        }

        public static int RegexGetMatchCount(string RegText, string RegexRule)
        {
            Regex regex = new Regex(RegexRule, RegexOptions.None);
            return regex.Matches(RegText).Count;
        }

        public static string RegexGetName(string RegText)
        {
            return RegexGetValue(RegText, RegexFilterKeyAndNum);
        }

        public static string RegexGetValue(string RegText)
        {
            return RegexGetValue(RegText, RegexFilterValue);
        }

        public static string RegexGetNameOnly(string RegText)
        {
            RegText = RegText.Replace(" ", string.Empty);
            return RegexGetValue(RegText, RegexFilterKey);
        }

        public static int RegexColorSignCount(string RegText)
        {
            return RegexGetMatchCount(RegText, "(?<=(§.)).+(?=(§!))");
        }

        public static int RegexLineSignCount(string RegText)
        {
            return RegexGetMatchCount(RegText, @"\\n");
        }

        public static int RegexCountSpecificChar(string text, char sign)
        {
            return text.Count(t => t == sign);
        }
    }
}
