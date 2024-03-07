using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using VcardQRCodeGenerator.Model;
using VcardQRCodeGenerator.Utility;

namespace VcardQRCodeGenerator
{
    public partial class Form1 : Form
    {
        #region Members

        private BackgroundWorker Background_Execute;
        private LogObj logObj = new LogObj();

        #endregion

        #region Constructor

        public Form1()
        {
            InitializeComponent();

            // ��l�ƭI�����檫�� BackgroundWorker
            Background_Execute = new BackgroundWorker();
            Background_Execute.WorkerReportsProgress = true;
            Background_Execute.WorkerSupportsCancellation = true;
            Background_Execute.DoWork += new DoWorkEventHandler(Background_Execute_DoWork);
            Background_Execute.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Background_Execute_RunWorkerCompleted);
        }

        #endregion

        #region Events

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitComponent();
            }
            catch (Exception ex)
            {
                ShowEventErrorMessage("Form1_Load", ex);
            }
        }

        private void Button_FileSelector_Click(object sender, EventArgs e)
        {
            try
            {
                // �}�Ҿ��ɮ׵����A���w�����O Excel �ɮסA�ñN�ȼg�^ Input_SourceExcel
                this.openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;";

                if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
                    this.Input_SourceExcel.Text = this.openFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                ShowEventErrorMessage("Button_FileSelector_Click", ex);
            }
        }

        private void Button_DirSelector_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    this.Input_OutputDir.Text = this.folderBrowserDialog1.SelectedPath;
            }
            catch (Exception ex)
            {
                ShowEventErrorMessage("Button_DirSelector_Click", ex);
            }
        }

        private void Button_GenQRCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckInput()) return;

                if (MessageBox.Show("�T�w��|�л\�{���ɮסA�T�w�n����H", "����QRCode", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ChangeUI(isExecute: true);
                    Background_Execute.RunWorkerAsync();
                    SaveConfig();
                }
            }
            catch (Exception ex)
            {
                ShowEventErrorMessage("Button_GenQRCode_Click", ex);
            }
        }

        // �}�l�I������ƥ�
        private void Background_Execute_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var excel = new ExcelObj();
                var dataSource = excel.GetDataSource(this.Input_SourceExcel.Text, this.Input_Config.Text);

                SetProgressMax(dataSource.Count);
                Execute(dataSource);
            }
            catch (Exception ex)
            {
                ShowEventErrorMessage("Background_Execute_DoWork", ex);
            }
        }

        // �I������i�ק����ƥ�
        private void Background_Execute_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ChangeUI(isExecute: false);
            }
            catch (Exception ex)
            {
                ShowEventErrorMessage("Background_Execute_RunWorkerCompleted", ex);
            }
        }

        private void Input_SetttingConfig_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Input_Config.Enabled = this.Input_SetttingConfig.Checked;
            }
            catch (Exception ex)
            {
                ShowEventErrorMessage("Input_SetttingConfig_CheckedChanged", ex);
            }
        }

        private void Link_Document_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Link_Document.LinkVisited = true;
                ProcessStart("https://lawrencetech.blogspot.com/2024/03/qr-code.html");
            }
            catch (Exception ex)
            {
                ShowEventErrorMessage("Link_Document_LinkClicked", ex);
            }
        }


        private void Link_Template_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Link_Template.LinkVisited = true;

                string fileName = "";

                var assembly = Assembly.GetExecutingAssembly();
                using (Stream resourceStream = assembly.GetManifestResourceStream("VcardQRCodeGenerator.ImportExcel.xlsx")!)
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.xlsx;";
                    saveFileDialog.Title = "�п�ܭn�x�s�� Excel �˪O���|";
                    saveFileDialog.ShowDialog();

                    if (!string.IsNullOrEmpty(saveFileDialog.FileName))
                    {
                        fileName = saveFileDialog.FileName;
                        using var fileStream = saveFileDialog.OpenFile();
                        resourceStream.CopyTo(fileStream);
                    }
                }

                if (!string.IsNullOrEmpty(fileName))
                    ProcessStart(fileName);
            }
            catch (Exception ex)
            {
                ShowEventErrorMessage("Link_Template_LinkClicked", ex);
            }
        }

        #endregion

        #region Private Methods

        private void InitComponent()
        {
            this.Input_Config.Text = Settings1.Default.SettingConfig;
            this.Input_SourceExcel.Text = Settings1.Default.ExcelFilePath;
            this.openFileDialog1.FileName = Settings1.Default.ExcelFilePath;
            this.openFileDialog1.InitialDirectory = Settings1.Default.ExcelFileDir;
            this.Input_OutputDir.Text = Settings1.Default.OutputDir;

            this.Input_Config.Enabled = false;

            var initConfig = @"[
  {
    ""lang"": ""�c��"",
    ""fields"": [
        { ""excel"": ""�m�W"", ""vcard"": ""FN:"", ""key"": true },
        { ""excel"": ""���q"", ""vcard"": ""ORG:"" },
        { ""excel"": ""¾��"", ""vcard"": ""TITLE:"" },
        { ""excel"": ""�a�}"", ""vcard"": ""ADR:;;"" },
        { ""excel"": ""���"", ""vcard"": ""TEL;WORK;VOICE:"" },
        { ""excel"": ""�q��"", ""vcard"": ""TEL;CELL:"" },
        { ""excel"": ""�ǯu"", ""vcard"": ""TEL;FAX:"" },
        { ""excel"": ""�q�l�l��"", ""vcard"": ""EMAIL;WORK;INTERNET:"" },
        { ""excel"": ""����"", ""vcard"": ""URL:"" }
    ]
  },
  {
    ""lang"": ""�^��"",
    ""fields"": [
        { ""excel"": ""Name"", ""vcard"": ""FN:"", ""key"": true },
        { ""excel"": ""Company"", ""vcard"": ""ORG:"" },
        { ""excel"": ""JobTitle"", ""vcard"": ""TITLE:"" },
        { ""excel"": ""Address"", ""vcard"": ""ADR:;;"" },
        { ""excel"": ""���"", ""vcard"": ""TEL;WORK;VOICE:"" },
        { ""excel"": ""�q��"", ""vcard"": ""TEL;CELL:"" },
        { ""excel"": ""�ǯu"", ""vcard"": ""TEL;FAX:"" },
        { ""excel"": ""�q�l�l��"", ""vcard"": ""EMAIL;WORK;INTERNET:"" },
        { ""excel"": ""����"", ""vcard"": ""URL:"" }
    ]
  }
]";
            //if (string.IsNullOrEmpty(this.Input_Config.Text))
            this.Input_Config.Text = initConfig;
        }

        private void SaveConfig()
        {
            Settings1.Default.SettingConfig = this.Input_Config.Text;
            Settings1.Default.ExcelFilePath = this.Input_SourceExcel.Text;
            Settings1.Default.ExcelFileDir = Path.GetDirectoryName(this.Input_SourceExcel.Text);
            Settings1.Default.OutputDir = this.Input_OutputDir.Text;
            Settings1.Default.Save();
        }

        /// <summary>
        /// �]�w UI �ާ@���A
        /// </summary>
        /// <param name="isExecute">�O�_���}�l�B�z</param>
        private void ChangeUI(bool isExecute)
        {
            this.Input_SourceExcel.Enabled = !isExecute;
            this.Input_OutputDir.Enabled = !isExecute;
            this.Button_DirSelector.Enabled = !isExecute;
            this.Button_FileSelector.Enabled = !isExecute;
            this.Button_GenQRCode.Enabled = !isExecute;
            this.Input_SetttingConfig.Enabled = !isExecute;
            this.Input_Config.Enabled = isExecute ? false : this.Input_SetttingConfig.Checked;
        }

        private bool CheckInput()
        {
            bool result = true;
            string message = string.Empty;

            if (string.IsNullOrEmpty(this.Input_SourceExcel.Text))
            {
                message = "�п�ܨӷ� Excel �ɮ�";
                result = false;
            }
            else if (!File.Exists(this.Input_SourceExcel.Text))
            {
                message = "�ӷ� Excel �ɮפ��s�b";
                result = false;
            }
            else if (string.IsNullOrEmpty(this.Input_OutputDir.Text))
            {
                message = "�п�ܿ�X�ؿ�";
                result = false;
            }
            else if (!Directory.Exists(this.Input_OutputDir.Text))
            {
                message = "��X�ؿ���Ƨ����s�b";
                result = false;
            }

            List<ConfigModel>? config = null;
            try
            {
                config = JsonConvert.DeserializeObject<List<ConfigModel>>(this.Input_Config.Text);
            }
            catch { }
            finally
            {
                if (config == null)
                {
                    message = "�ѼƳ]�w�����D�A�Э��s�ˬd�O�_���T";
                    result = false;
                }
            }

            if (!result)
            {
                MessageBox.Show(message, "�t�ο��~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logObj.Info(message);
            }

            return result;
        }

        private void Execute(List<CardModel> dataSource)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int count = 0;

            try
            {
                var totalCount = dataSource.Count;
                SetProgress(0);
                SetStatusLabel(string.Format("{0}/{1}", count, totalCount));
                logObj.Info("===== ���� QRCode �}�l =====");

                var qrCodeObj = new QRCodeObj();

                // �ϥ� Task �D�P�B�B�z QRCode �ç�s�i�ױ�
                for (int i = 0; i < dataSource.Count; i++)
                {
                    var data = dataSource[i];
                    count++;
                    SetProgress(count);
                    SetStatusLabel(string.Format("{0}/{1}", count, totalCount));

                    qrCodeObj.Generator(data, this.Input_OutputDir.Text);
                    logObj.Info($"���u {data.FileName}:{data.Lang} �B�z����");
                }

                MessageBox.Show($"���ɧ����A�@���� {dataSource.Count} ��");
            }
            catch (Exception ex)
            {
                var message = "*** QRCode ���͹L�{�o�Ϳ��~�A�Բӿ��~��T�Ьd�� Log ***";
                MessageBox.Show(message, "�t�ο��~", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (ex.InnerException is KeyNotFoundException) message = ex.InnerException.Message;
                else message = ex.Message;

                SetProgress(0);

                logObj.Info(message);
                SetStatusLabel(message.Replace("***", string.Empty).Trim());
            }
            finally
            {
                watch.Stop();
                logObj.Info($"*** ���ͦ@�p�ɶ� : {watch.Elapsed} ***");
                logObj.Info("===== ���� QRCode  ���� =====" + Environment.NewLine);
            }
        }

        /// <summary> �]�w�B�z�i�׳̤j�� </summary> 
        private void SetProgressMax(int maxValue)
        {
            this.ProgressBar1.Control.Invoke(() => this.ProgressBar1.Maximum = maxValue);
        }

        /// <summary> �]�w�B�z�i�� Bar </summary> 
        private void SetProgress(int value)
        {
            this.ProgressBar1.Control.Invoke(() => this.ProgressBar1.Value = value);
        }

        /// <summary> �]�w�B�z�i�׻��� </summary> 
        private void SetStatusLabel(string value)
        {
            this.Label_ProcessCount.GetCurrentParent().Invoke(() => this.Label_ProcessCount.Text = $"�B�z�i�� : {value}");
        }

        private void ShowEventErrorMessage(string type, Exception error)
        {
            string errorMessage = $"Error Type : {type}{Environment.NewLine}{error}";
            MessageBox.Show(errorMessage, "�t�ο��~", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // LogObj.Error(errorMessage);
            Application.Exit();
        }

        private void ProcessStart(string url)
        {
            var p = new System.Diagnostics.Process();
            p.StartInfo = new System.Diagnostics.ProcessStartInfo(url)
            {
                UseShellExecute = true
            };
            p.Start();
        }

        #endregion 
    }
}