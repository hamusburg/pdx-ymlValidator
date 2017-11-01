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

        Dictionary<string, string> EngDictionary = new Dictionary<string, string>();
        Dictionary<string, string> ChnDictionary = new Dictionary<string, string>();

        string TimeToken = "default";

        public FrmMain()
        {
            Directory.CreateDirectory(EngDir);
            Directory.CreateDirectory(ChnDir);
            InitializeComponent();
        }

        private void BtnStartValidate_Click(object sender, EventArgs e)
        {
            TimeToken = DateTime.Now.ToString("yyyyMMddHHmmss");
            StartValidate();
        }

        private void StartValidate()
        {
            string[] engFiles = Directory.GetFiles(EngDir, "*.yml", SearchOption.TopDirectoryOnly);
            //string[] chnFiles = Directory.GetFiles(ChnDir, "*.yml", SearchOption.TopDirectoryOnly);

            foreach (var file in engFiles)
            {
                EngDictionary.Clear();
                ChnDictionary.Clear();

                var fileName = Path.GetFileName(file);
                var chnFileName = ChnDir + "\\" + fileName;

                if (File.Exists(chnFileName) == false)
                {
                    WriteLog("不存在被检查的文件" + fileName);
                    continue;
                }

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
                        var clean = line.Trim();
                        if (string.IsNullOrEmpty(clean) || clean[0] == '#')
                        {
                            continue;
                        }
                        var key = YMLTools.RegexGetNameOnly(clean);
                        var value = YMLTools.RegexGetValue(clean);
                        EngDictionary.Add(key, value);
                    }
                }

                using (StreamReader reader = new StreamReader(chnFileName, Encoding.UTF8))
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
                        var clean = line.Trim();
                        if (string.IsNullOrEmpty(clean) || clean[0] == '#')
                        {
                            continue;
                        }
                        var key = YMLTools.RegexGetNameOnly(clean);
                        var value = YMLTools.RegexGetValue(clean);
                        ChnDictionary.Add(key, value);
                    }
                }

                foreach (var line in EngDictionary)
                {
                    var key = line.Key;
                    var value = line.Value;
                    if (ChnDictionary.ContainsKey(key))
                    {
                        var result = YMLTools.CompareLine(ChnDictionary[key], EngDictionary[key], key, fileName);
                        if (string.IsNullOrEmpty(result))
                        {
                            continue;
                        }
                        WriteLog(result);
                    }
                    else
                    {
                        WriteLog("被校对文件" + fileName + "缺少key为" + key + "的行");
                    }
                }
            }
        }

        private void WriteLog(string text)
        {
            LogHelper.Write(text, TimeToken);
        }
    }
}
