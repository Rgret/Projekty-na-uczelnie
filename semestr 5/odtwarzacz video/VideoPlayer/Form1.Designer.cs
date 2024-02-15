namespace VideoPlayer
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.durationTime = new System.Windows.Forms.Label();
            this.currentTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.PlayBttn = new System.Windows.Forms.Button();
            this.PauseBttn = new System.Windows.Forms.Button();
            this.NextBttn = new System.Windows.Forms.Button();
            this.PreviousBttn = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.fdurLabel = new System.Windows.Forms.Label();
            this.saveAsTxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromTxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.axWindowsMediaPlayer1);
            this.panel1.Location = new System.Drawing.Point(87, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(498, 314);
            this.panel1.TabIndex = 0;
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(76, 60);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(331, 185);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(87, 370);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(498, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 1;
            this.progressBar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.progressBar1_MouseDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.saveAsTxtToolStripMenuItem,
            this.loadFromTxtToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // durationTime
            // 
            this.durationTime.AutoSize = true;
            this.durationTime.Location = new System.Drawing.Point(550, 399);
            this.durationTime.Name = "durationTime";
            this.durationTime.Size = new System.Drawing.Size(34, 13);
            this.durationTime.TabIndex = 3;
            this.durationTime.Text = "00:00";
            // 
            // currentTime
            // 
            this.currentTime.AutoSize = true;
            this.currentTime.Location = new System.Drawing.Point(87, 399);
            this.currentTime.Name = "currentTime";
            this.currentTime.Size = new System.Drawing.Size(34, 13);
            this.currentTime.TabIndex = 4;
            this.currentTime.Text = "00:00";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(591, 50);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(197, 314);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // PlayBttn
            // 
            this.PlayBttn.Location = new System.Drawing.Point(168, 415);
            this.PlayBttn.Name = "PlayBttn";
            this.PlayBttn.Size = new System.Drawing.Size(75, 23);
            this.PlayBttn.TabIndex = 6;
            this.PlayBttn.Text = "Play";
            this.PlayBttn.UseVisualStyleBackColor = true;
            this.PlayBttn.Click += new System.EventHandler(this.PlayBttn_Click);
            // 
            // PauseBttn
            // 
            this.PauseBttn.Location = new System.Drawing.Point(250, 415);
            this.PauseBttn.Name = "PauseBttn";
            this.PauseBttn.Size = new System.Drawing.Size(75, 23);
            this.PauseBttn.TabIndex = 7;
            this.PauseBttn.Text = "Pause";
            this.PauseBttn.UseVisualStyleBackColor = true;
            this.PauseBttn.Click += new System.EventHandler(this.PauseBttn_Click);
            // 
            // NextBttn
            // 
            this.NextBttn.Location = new System.Drawing.Point(332, 415);
            this.NextBttn.Name = "NextBttn";
            this.NextBttn.Size = new System.Drawing.Size(75, 23);
            this.NextBttn.TabIndex = 8;
            this.NextBttn.Text = "Next";
            this.NextBttn.UseVisualStyleBackColor = true;
            this.NextBttn.Click += new System.EventHandler(this.NextBttn_Click);
            // 
            // PreviousBttn
            // 
            this.PreviousBttn.Location = new System.Drawing.Point(87, 415);
            this.PreviousBttn.Name = "PreviousBttn";
            this.PreviousBttn.Size = new System.Drawing.Size(75, 23);
            this.PreviousBttn.TabIndex = 9;
            this.PreviousBttn.Text = "Previous";
            this.PreviousBttn.UseVisualStyleBackColor = true;
            this.PreviousBttn.Click += new System.EventHandler(this.PreviousBttn_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(13, 50);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(27, 13);
            this.titleLabel.TabIndex = 10;
            this.titleLabel.Text = "Title";
            // 
            // fdurLabel
            // 
            this.fdurLabel.AutoSize = true;
            this.fdurLabel.Location = new System.Drawing.Point(588, 34);
            this.fdurLabel.Name = "fdurLabel";
            this.fdurLabel.Size = new System.Drawing.Size(64, 13);
            this.fdurLabel.TabIndex = 11;
            this.fdurLabel.Text = "Full duration";
            // 
            // saveAsTxtToolStripMenuItem
            // 
            this.saveAsTxtToolStripMenuItem.Name = "saveAsTxtToolStripMenuItem";
            this.saveAsTxtToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsTxtToolStripMenuItem.Text = "Save as txt";
            this.saveAsTxtToolStripMenuItem.Click += new System.EventHandler(this.saveAsTxtToolStripMenuItem_Click);
            // 
            // loadFromTxtToolStripMenuItem
            // 
            this.loadFromTxtToolStripMenuItem.Name = "loadFromTxtToolStripMenuItem";
            this.loadFromTxtToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadFromTxtToolStripMenuItem.Text = "Load from txt";
            this.loadFromTxtToolStripMenuItem.Click += new System.EventHandler(this.loadFromTxtToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.fdurLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.PreviousBttn);
            this.Controls.Add(this.NextBttn);
            this.Controls.Add(this.PauseBttn);
            this.Controls.Add(this.PlayBttn);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.currentTime);
            this.Controls.Add(this.durationTime);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label durationTime;
        private System.Windows.Forms.Label currentTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button PlayBttn;
        private System.Windows.Forms.Button PauseBttn;
        private System.Windows.Forms.Button NextBttn;
        private System.Windows.Forms.Button PreviousBttn;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label fdurLabel;
        private System.Windows.Forms.ToolStripMenuItem saveAsTxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFromTxtToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

