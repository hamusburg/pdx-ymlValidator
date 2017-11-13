using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private async void BtnStartValidate_Click(object sender, EventArgs e)
        {
            TimeToken = DateTime.Now.ToString("yyyyMMddHHmmss");
            ProgressbarTotal.Value = 0;
            lblFileName.Text = "摸鱼中……";
            using (Task<bool> getStartValidateResult = new Task<bool>(StartValidate))
            {
                getStartValidateResult.Start();
                if (await getStartValidateResult)
                {
                    lblFileName.Text = "空闲";

                    var dialog = MessageBox.Show(this, "文本检查完成，是否打开处理日志目录。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialog == DialogResult.Yes)
                    {
                        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe")
                        {
                            Arguments = "/e,/select," + Directory.GetCurrentDirectory() + "\\Logs\\" + TimeToken + ".txt"
                        };
                        System.Diagnostics.Process.Start(psi);
                    }
                }
            }
        }

        /// <summary>
        /// 验证文本
        /// </summary>
        /// <returns></returns>
        private bool StartValidate()
        {
            //TODO:将检查结果变为datatable 将所有文本结合完毕后 直接生成CSV结果 减少不必要的IO次数提高效率

            string[] engFiles = Directory.GetFiles(EngDir, "*.yml", SearchOption.TopDirectoryOnly);

            UpdateProgressBar(ProgressbarTotal, engFiles.Length, ProgressBarValueOption.Maximum);

            foreach (var file in engFiles)
            {
                UpdateProgressBar(ProgressbarFile, 0, ProgressBarValueOption.Current);

                var fileName = Path.GetFileName(file);
                var chnFileName = ChnDir + "\\" + fileName;

                if (File.Exists(chnFileName) == false)
                {
                    WriteLog("不存在被检查的文件" + fileName);
                    continue;
                }

                GenerateDictionary(file, fileName, EngDictionary);
                GenerateDictionary(chnFileName, fileName, ChnDictionary);

                UpdateProgressBar(ProgressbarFile, EngDictionary.Count, ProgressBarValueOption.Maximum);

                foreach (var line in EngDictionary)
                {
                    UpdateProgressBar(ProgressbarFile, 1, ProgressBarValueOption.Push);

                    var key = line.Key;
                    var value = line.Value;
                    if (ChnDictionary.ContainsKey(key))
                    {
                        var result = YMLTools.CompareLine(ChnDictionary[key], EngDictionary[key], key, fileName);
                        if (string.IsNullOrEmpty(result))
                        {
                            continue;
                        }
                        else
                        {
                            WriteLog(result);
                        }
                    }
                    else
                    {
                        WriteLog("被校对文件" + fileName + "缺少key为" + key + "的行");
                    }
                }
                UpdateProgressBar(ProgressbarTotal, 1, ProgressBarValueOption.Push);
            }
            return true;
        }

        /// <summary>
        /// 生成词典
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="dictionary">词典</param>
        private void GenerateDictionary(string file, string fileName, Dictionary<string, string> dictionary)
        {
            dictionary.Clear();
            using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
            {
                var count = 0;
                var line = reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    WriteLog(fileName + "为空文件");
                    reader.Close();
                }
                else
                {
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
                        if (dictionary.ContainsKey(key))
                        {
                            dictionary[key] = value;
                        }
                        else
                        {
                            dictionary.Add(key, value);
                        }
                    }
                }
            }
        }

        enum ProgressBarValueOption
        {
            Maximum = 0,
            Current = 1,
            Push = 2
        }

        private void UpdateProgressBar(ProgressBar bar, int value, ProgressBarValueOption option)
        {
            Invoke((EventHandler)delegate
            {
                switch (option)
                {
                    case ProgressBarValueOption.Maximum:
                        bar.Maximum = value;
                        break;
                    case ProgressBarValueOption.Current:
                        if (value < bar.Maximum)
                        {
                            bar.Value = value;
                        }
                        else
                        {
                            bar.Value = bar.Maximum;
                        }
                        break;
                    case ProgressBarValueOption.Push:
                        if (bar.Value + value < bar.Maximum)
                        {
                            bar.Value += value;
                        }
                        else
                        {
                            bar.Value = bar.Maximum;
                        }
                        break;
                    default:
                        break;
                }
            });
        }

        private void WriteLog(string text)
        {
            LogHelper.Write(text, TimeToken);
        }
    }
}
