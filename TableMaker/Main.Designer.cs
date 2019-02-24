namespace TableMaker
{
    partial class TableMaker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableMaker));
            this.logoImg = new System.Windows.Forms.PictureBox();
            this.logo = new System.Diagnostics.Process();
            this.startButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.cbVisible = new System.Windows.Forms.CheckBox();
            this.rtbLogs = new System.Windows.Forms.RichTextBox();
            this.tbPathFile = new System.Windows.Forms.TextBox();
            this.lblPathFile = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logoImg)).BeginInit();
            this.SuspendLayout();
            // 
            // logoImg
            // 
            this.logoImg.Image = ((System.Drawing.Image)(resources.GetObject("logoImg.Image")));
            this.logoImg.Location = new System.Drawing.Point(12, 12);
            this.logoImg.Name = "logoImg";
            this.logoImg.Size = new System.Drawing.Size(264, 48);
            this.logoImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoImg.TabIndex = 0;
            this.logoImg.TabStop = false;
            // 
            // logo
            // 
            this.logo.StartInfo.Domain = "";
            this.logo.StartInfo.LoadUserProfile = false;
            this.logo.StartInfo.Password = null;
            this.logo.StartInfo.StandardErrorEncoding = null;
            this.logo.StartInfo.StandardOutputEncoding = null;
            this.logo.StartInfo.UserName = "";
            this.logo.SynchronizingObject = this;
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startButton.Location = new System.Drawing.Point(348, 377);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(157, 35);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 377);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(330, 35);
            this.progressBar.TabIndex = 2;
            // 
            // cbVisible
            // 
            this.cbVisible.AutoSize = true;
            this.cbVisible.Location = new System.Drawing.Point(426, 58);
            this.cbVisible.Name = "cbVisible";
            this.cbVisible.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbVisible.Size = new System.Drawing.Size(71, 21);
            this.cbVisible.TabIndex = 3;
            this.cbVisible.Text = "Visible";
            this.cbVisible.UseVisualStyleBackColor = true;
            this.cbVisible.CheckedChanged += new System.EventHandler(this.cbVisible_CheckedChanged);
            // 
            // rtbLogs
            // 
            this.rtbLogs.Location = new System.Drawing.Point(12, 153);
            this.rtbLogs.Name = "rtbLogs";
            this.rtbLogs.ReadOnly = true;
            this.rtbLogs.Size = new System.Drawing.Size(493, 218);
            this.rtbLogs.TabIndex = 4;
            this.rtbLogs.Text = "";
            // 
            // tbPathFile
            // 
            this.tbPathFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPathFile.Location = new System.Drawing.Point(12, 102);
            this.tbPathFile.Multiline = true;
            this.tbPathFile.Name = "tbPathFile";
            this.tbPathFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbPathFile.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tbPathFile.Size = new System.Drawing.Size(392, 29);
            this.tbPathFile.TabIndex = 5;
            // 
            // lblPathFile
            // 
            this.lblPathFile.AutoSize = true;
            this.lblPathFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPathFile.Location = new System.Drawing.Point(12, 82);
            this.lblPathFile.Name = "lblPathFile";
            this.lblPathFile.Size = new System.Drawing.Size(196, 18);
            this.lblPathFile.TabIndex = 6;
            this.lblPathFile.Text = "Choose full path to excel file:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBrowse.Location = new System.Drawing.Point(410, 102);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(95, 30);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // TableMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(526, 424);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblPathFile);
            this.Controls.Add(this.tbPathFile);
            this.Controls.Add(this.rtbLogs);
            this.Controls.Add(this.cbVisible);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.logoImg);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TableMaker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Table Formater";
            this.Load += new System.EventHandler(this.TableMaker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logoImg;
        private System.Diagnostics.Process logo;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox cbVisible;
        private System.Windows.Forms.Label lblPathFile;
        private System.Windows.Forms.TextBox tbPathFile;
        private System.Windows.Forms.RichTextBox rtbLogs;
        private System.Windows.Forms.Button btnBrowse;
    }
}

