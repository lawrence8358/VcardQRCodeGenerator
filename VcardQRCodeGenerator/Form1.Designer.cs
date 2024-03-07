namespace VcardQRCodeGenerator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Button_GenQRCode = new Button();
            Input_SourceExcel = new TextBox();
            Input_OutputDir = new TextBox();
            Button_FileSelector = new Button();
            Button_DirSelector = new Button();
            statusStrip1 = new StatusStrip();
            ProgressBar1 = new ToolStripProgressBar();
            Label_ProcessCount = new ToolStripStatusLabel();
            openFileDialog1 = new OpenFileDialog();
            folderBrowserDialog1 = new FolderBrowserDialog();
            Input_SetttingConfig = new CheckBox();
            Input_Config = new TextBox();
            Link_Document = new LinkLabel();
            Link_Template = new LinkLabel();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // Button_GenQRCode
            // 
            Button_GenQRCode.Location = new Point(18, 540);
            Button_GenQRCode.Margin = new Padding(2);
            Button_GenQRCode.Name = "Button_GenQRCode";
            Button_GenQRCode.Size = new Size(498, 28);
            Button_GenQRCode.TabIndex = 0;
            Button_GenQRCode.Text = "產生 QRCode";
            Button_GenQRCode.UseVisualStyleBackColor = true;
            Button_GenQRCode.Click += Button_GenQRCode_Click;
            // 
            // Input_SourceExcel
            // 
            Input_SourceExcel.Location = new Point(18, 22);
            Input_SourceExcel.Margin = new Padding(2);
            Input_SourceExcel.Name = "Input_SourceExcel";
            Input_SourceExcel.PlaceholderText = "請選擇 Excel 檔案";
            Input_SourceExcel.Size = new Size(460, 27);
            Input_SourceExcel.TabIndex = 1;
            // 
            // Input_OutputDir
            // 
            Input_OutputDir.Location = new Point(18, 59);
            Input_OutputDir.Margin = new Padding(2);
            Input_OutputDir.Name = "Input_OutputDir";
            Input_OutputDir.PlaceholderText = "請選擇下載資料夾路徑";
            Input_OutputDir.Size = new Size(460, 27);
            Input_OutputDir.TabIndex = 2;
            // 
            // Button_FileSelector
            // 
            Button_FileSelector.Location = new Point(482, 22);
            Button_FileSelector.Margin = new Padding(2);
            Button_FileSelector.Name = "Button_FileSelector";
            Button_FileSelector.Size = new Size(34, 25);
            Button_FileSelector.TabIndex = 4;
            Button_FileSelector.Text = "...";
            Button_FileSelector.UseVisualStyleBackColor = true;
            Button_FileSelector.Click += Button_FileSelector_Click;
            // 
            // Button_DirSelector
            // 
            Button_DirSelector.Location = new Point(482, 59);
            Button_DirSelector.Margin = new Padding(2);
            Button_DirSelector.Name = "Button_DirSelector";
            Button_DirSelector.Size = new Size(34, 25);
            Button_DirSelector.TabIndex = 6;
            Button_DirSelector.Text = "...";
            Button_DirSelector.UseVisualStyleBackColor = true;
            Button_DirSelector.Click += Button_DirSelector_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { ProgressBar1, Label_ProcessCount });
            statusStrip1.Location = new Point(0, 582);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 11, 0);
            statusStrip1.Size = new Size(537, 26);
            statusStrip1.TabIndex = 7;
            statusStrip1.Text = "statusStrip1";
            // 
            // ProgressBar1
            // 
            ProgressBar1.Name = "ProgressBar1";
            ProgressBar1.Size = new Size(82, 18);
            // 
            // Label_ProcessCount
            // 
            Label_ProcessCount.Name = "Label_ProcessCount";
            Label_ProcessCount.Size = new Size(80, 20);
            Label_ProcessCount.Text = "處理進度 : ";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Input_SetttingConfig
            // 
            Input_SetttingConfig.AutoSize = true;
            Input_SetttingConfig.Location = new Point(20, 96);
            Input_SetttingConfig.Margin = new Padding(2);
            Input_SetttingConfig.Name = "Input_SetttingConfig";
            Input_SetttingConfig.Size = new Size(91, 23);
            Input_SetttingConfig.TabIndex = 8;
            Input_SetttingConfig.Text = "修改參數";
            Input_SetttingConfig.UseVisualStyleBackColor = true;
            Input_SetttingConfig.CheckedChanged += Input_SetttingConfig_CheckedChanged;
            // 
            // Input_Config
            // 
            Input_Config.Location = new Point(18, 130);
            Input_Config.Margin = new Padding(2);
            Input_Config.Multiline = true;
            Input_Config.Name = "Input_Config";
            Input_Config.PlaceholderText = "參數設定，請參考說明進行設置，設置錯誤會導致 APP 損壞";
            Input_Config.ScrollBars = ScrollBars.Vertical;
            Input_Config.Size = new Size(499, 406);
            Input_Config.TabIndex = 9;
            // 
            // Link_Document
            // 
            Link_Document.AutoSize = true;
            Link_Document.Location = new Point(447, 97);
            Link_Document.Name = "Link_Document";
            Link_Document.Size = new Size(69, 19);
            Link_Document.TabIndex = 10;
            Link_Document.TabStop = true;
            Link_Document.Text = "設定說明";
            Link_Document.LinkClicked += Link_Document_LinkClicked;
            // 
            // Link_Template
            // 
            Link_Template.AutoSize = true;
            Link_Template.Location = new Point(372, 97);
            Link_Template.Name = "Link_Template";
            Link_Template.Size = new Size(77, 19);
            Link_Template.TabIndex = 11;
            Link_Template.TabStop = true;
            Link_Template.Text = "Excel 樣板";
            Link_Template.LinkClicked += Link_Template_LinkClicked;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(537, 608);
            Controls.Add(Link_Template);
            Controls.Add(Link_Document);
            Controls.Add(Input_Config);
            Controls.Add(Input_SetttingConfig);
            Controls.Add(statusStrip1);
            Controls.Add(Button_DirSelector);
            Controls.Add(Button_FileSelector);
            Controls.Add(Input_OutputDir);
            Controls.Add(Input_SourceExcel);
            Controls.Add(Button_GenQRCode);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "QRCode 電子名片";
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Button_GenQRCode;
        private TextBox Input_SourceExcel;
        private TextBox Input_OutputDir;
        private Button Button_FileSelector;
        private Button Button_DirSelector;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar ProgressBar1;
        private ToolStripStatusLabel Label_ProcessCount;
        private OpenFileDialog openFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;
        private CheckBox Input_SetttingConfig;
        private TextBox Input_Config;
        private LinkLabel Link_Document;
        private LinkLabel Link_Template;
    }
}