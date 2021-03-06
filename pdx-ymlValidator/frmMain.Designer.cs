﻿namespace pdx_ymlValidator
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.ProgressbarFile = new System.Windows.Forms.ProgressBar();
            this.lblTipCurrentProgress = new System.Windows.Forms.Label();
            this.lblTipAmount = new System.Windows.Forms.Label();
            this.ProgressbarTotal = new System.Windows.Forms.ProgressBar();
            this.btnStartValidate = new System.Windows.Forms.Button();
            this.lblTipCurrentFileName = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProgressbarFile
            // 
            this.ProgressbarFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressbarFile.Location = new System.Drawing.Point(14, 28);
            this.ProgressbarFile.Name = "ProgressbarFile";
            this.ProgressbarFile.Size = new System.Drawing.Size(438, 23);
            this.ProgressbarFile.TabIndex = 0;
            // 
            // lblTipCurrentProgress
            // 
            this.lblTipCurrentProgress.AutoSize = true;
            this.lblTipCurrentProgress.Location = new System.Drawing.Point(12, 9);
            this.lblTipCurrentProgress.Name = "lblTipCurrentProgress";
            this.lblTipCurrentProgress.Size = new System.Drawing.Size(89, 12);
            this.lblTipCurrentProgress.TabIndex = 1;
            this.lblTipCurrentProgress.Text = "当前文件进度：";
            // 
            // lblTipAmount
            // 
            this.lblTipAmount.AutoSize = true;
            this.lblTipAmount.Location = new System.Drawing.Point(12, 58);
            this.lblTipAmount.Name = "lblTipAmount";
            this.lblTipAmount.Size = new System.Drawing.Size(77, 12);
            this.lblTipAmount.TabIndex = 2;
            this.lblTipAmount.Text = "总处理进度：";
            // 
            // ProgressbarTotal
            // 
            this.ProgressbarTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressbarTotal.Location = new System.Drawing.Point(14, 77);
            this.ProgressbarTotal.Name = "ProgressbarTotal";
            this.ProgressbarTotal.Size = new System.Drawing.Size(438, 23);
            this.ProgressbarTotal.TabIndex = 0;
            // 
            // btnStartValidate
            // 
            this.btnStartValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartValidate.Location = new System.Drawing.Point(377, 110);
            this.btnStartValidate.Name = "btnStartValidate";
            this.btnStartValidate.Size = new System.Drawing.Size(75, 23);
            this.btnStartValidate.TabIndex = 3;
            this.btnStartValidate.Text = "检查文本";
            this.btnStartValidate.UseVisualStyleBackColor = true;
            this.btnStartValidate.Click += new System.EventHandler(this.BtnStartValidate_Click);
            // 
            // lblTipCurrentFileName
            // 
            this.lblTipCurrentFileName.AutoSize = true;
            this.lblTipCurrentFileName.Location = new System.Drawing.Point(10, 115);
            this.lblTipCurrentFileName.Name = "lblTipCurrentFileName";
            this.lblTipCurrentFileName.Size = new System.Drawing.Size(65, 12);
            this.lblTipCurrentFileName.TabIndex = 4;
            this.lblTipCurrentFileName.Text = "处理状态：";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(81, 115);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(29, 12);
            this.lblFileName.TabIndex = 5;
            this.lblFileName.Text = "空闲";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 146);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.lblTipCurrentFileName);
            this.Controls.Add(this.btnStartValidate);
            this.Controls.Add(this.lblTipAmount);
            this.Controls.Add(this.lblTipCurrentProgress);
            this.Controls.Add(this.ProgressbarTotal);
            this.Controls.Add(this.ProgressbarFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "yml文件特殊符号检查工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressbarFile;
        private System.Windows.Forms.Label lblTipCurrentProgress;
        private System.Windows.Forms.Label lblTipAmount;
        private System.Windows.Forms.ProgressBar ProgressbarTotal;
        private System.Windows.Forms.Button btnStartValidate;
        private System.Windows.Forms.Label lblTipCurrentFileName;
        private System.Windows.Forms.Label lblFileName;
    }
}

