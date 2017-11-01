using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

        /// <summary>
        /// 获取匹配正则表达式的拼接值
        /// </summary>
        /// <param name="RegText"></param>
        /// <param name="RegexRule"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取匹配的次数
        /// </summary>
        /// <param name="RegText"></param>
        /// <param name="RegexRule"></param>
        /// <returns></returns>
        public static int RegexGetMatchCount(string RegText, string RegexRule)
        {
            Regex regex = new Regex(RegexRule, RegexOptions.None);
            return regex.Matches(RegText).Count;
        }

        /// <summary>
        /// 获取键和覆盖标记
        /// </summary>
        /// <param name="RegText"></param>
        /// <returns></returns>
        public static string RegexGetName(string RegText)
        {
            return RegexGetValue(RegText, RegexFilterKeyAndNum);
        }

        /// <summary>
        /// 获取YML中的值
        /// </summary>
        /// <param name="RegText"></param>
        /// <returns></returns>
        public static string RegexGetValue(string RegText)
        {
            return RegexGetValue(RegText, RegexFilterValue);
        }

        /// <summary>
        /// 只获取YML中的键
        /// </summary>
        /// <param name="RegText"></param>
        /// <returns></returns>
        public static string RegexGetNameOnly(string RegText)
        {
            //RegText = RegText.Replace(" ", string.Empty);
            return RegexGetValue(RegexGetName(RegText.Trim()), RegexFilterKey);
        }

        /// <summary>
        /// 计算彩色文本格式数量
        /// </summary>
        /// <param name="RegText"></param>
        /// <returns></returns>
        public static int RegexColorSignCount(string RegText)
        {
            return RegexGetMatchCount(RegText, "(?<=(§.)).+(?=(§!))");
        }

        /// <summary>
        /// 查找计算换行符的数量
        /// </summary>
        /// <param name="RegText"></param>
        /// <returns></returns>
        public static int RegexLineSignCount(string RegText)
        {
            return RegexGetMatchCount(RegText, @"\\n");
        }

        /// <summary>
        /// 检查指定字符在目标文本中的数量
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="sign">字符</param>
        /// <returns>数量</returns>
        public static int RegexCountSpecificChar(string text, char sign)
        {
            return text.Count(t => t == sign);
        }

        /// <summary>
        /// 校对文本内容中的特殊符号数量
        /// </summary>
        /// <param name="text">被校对文本</param>
        /// <param name="reference">参照文本</param>
        /// <returns>校对建议</returns>
        public static string CompareLine(string text, string reference, string key, string fileName)
        {
            var result = new StringBuilder();
            var value = 0;
            var refValue = 0;

            foreach (var ch in ConstVal.SpecialCharSet)
            {
                value = RegexCountSpecificChar(text, ch);
                refValue = RegexCountSpecificChar(reference, ch);
                if (value != refValue)
                {
                    result.AppendFormat("文件{0}的{1}行，\"{2}\"字符，原文数量{3}，参照数量{4}\r\n", fileName, key, ch, value, refValue);
                }
            }

            value = RegexLineSignCount(text);
            refValue = RegexLineSignCount(reference);
            if (value != refValue)
            {
                result.AppendFormat("文件{0}的{1}行，换行\\n符，原文数量{2}，参照数量{3}\r\n", fileName, key, value, refValue);
            }

            value = RegexLineSignCount(text);
            refValue = RegexLineSignCount(reference);
            if (value != refValue)
            {
                result.AppendFormat("文件{0}的{1}行，换行\\n符，原文数量{2}，参照数量{3}\r\n", fileName, key, value, refValue);
            }

            value = RegexColorSignCount(text);
            refValue = RegexColorSignCount(reference);
            if (value != refValue)
            {
                result.AppendFormat("文件{0}的{1}行，彩色文本数量，原文数量{2}，参照数量{3}\r\n", fileName, key, value, refValue);
            }

            if (result.Length > 0)
            {
                result.Length -= 2;
            }
            return result.ToString();
        }
    }
}
