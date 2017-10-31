using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using pdx_ymlValidator.Model;
using pdx_ymlValidator.Util;

namespace pdx_ymlValidator
{
    public partial class FrmMain : Form
    {
        string EngDir = Directory.GetCurrentDirectory() + "\\eng";
        string ChnDir = Directory.GetCurrentDirectory() + "\\chn";

        Dictionary<string, List<Line>> EngDictionary = new Dictionary<string, List<Line>>();
        Dictionary<string, List<Line>> ChnDictionary = new Dictionary<string, List<Line>>();

        public FrmMain()
        {
            Directory.CreateDirectory(EngDir);
            Directory.CreateDirectory(ChnDir);
            InitializeComponent();
        }

        private void BtnStartValidate_Click(object sender, EventArgs e)
        {
            //var text = @"||||||||||||||";
            //var result = YMLTools.RegexCountSpecificChar(text, '|');

            EngDictionary.Clear();
            ChnDictionary.Clear();

            StartValidate();
        }

        private void StartValidate()
        {
            string[] engFiles = Directory.GetFiles(EngDir, "*.yml", SearchOption.TopDirectoryOnly);
            //string[] chnFiles = Directory.GetFiles(ChnDir, "*.yml", SearchOption.TopDirectoryOnly);

            foreach (var file in engFiles)
            {
                var fileName = Path.GetFileName(file);
                if (File.Exists(ChnDir + "\\" + fileName) == false)
                {
                    WriteLog("不存在的需要被检查的文件" + fileName);
                    continue;
                }

                EngDictionary.Add(fileName, new List<Line>());

                using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
                {
                    var count = 0;
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        WriteLog(fileName + "为空文件");
                        reader.Close();
                        continue;
                    }

                    while ((line = reader.ReadLine()) != null)
                    {
                        ++count;
                        if (string.IsNullOrEmpty(line.Trim()))
                        {
                            WriteLog(fileName + "中第" + count + "行为空行");
                            continue;
                        }
                        var key = YMLTools.RegexGetNameOnly(line);
                        var value = YMLTools.RegexGetValue(line);
                        EngDictionary[fileName].Add(new Line(key, value));
                        WriteLog
                    }
                }
            }
        }

        private string ValidateChar(string text, string reference)
        {
            var problem = string.Empty;
            foreach (var ch in ConstVal.SpecialCharSet)
            {
                var value = YMLTools.RegexCountSpecificChar(text, ch);
                var refValue = YMLTools.RegexCountSpecificChar(reference, ch);
                if (value != refValue)
                {
                    problem += ch;
                }
            }
            return problem;
        }

        private void WriteLog(string text)
        {
            LogHelper.Write(text);
        }
    }
}
