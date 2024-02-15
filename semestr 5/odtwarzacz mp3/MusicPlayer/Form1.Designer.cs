namespace MusicPlayer
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.playButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.skipForwardBtn = new System.Windows.Forms.Button();
            this.skipButton = new System.Windows.Forms.Button();
            this.currentTLabel = new System.Windows.Forms.Label();
            this.durationLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.waveBox = new System.Windows.Forms.PictureBox();
            this.skipBackBtn = new System.Windows.Forms.Button();
            this.loopOne = new System.Windows.Forms.CheckBox();
            this.loopInf = new System.Windows.Forms.CheckBox();
            this.loopAll = new System.Windows.Forms.CheckBox();
            this.authorLabel = new System.Windows.Forms.Label();
            this.albumLabel = new System.Windows.Forms.Label();
            this.genreLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waveBox)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(38, 363);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(694, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 0;
            this.progressBar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.progressBar1_MouseDown);
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(137, 392);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(40, 23);
            this.playButton.TabIndex = 1;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(52, 392);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(45, 23);
            this.pauseButton.TabIndex = 2;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // skipForwardBtn
            // 
            this.skipForwardBtn.Location = new System.Drawing.Point(183, 392);
            this.skipForwardBtn.Name = "skipForwardBtn";
            this.skipForwardBtn.Size = new System.Drawing.Size(29, 23);
            this.skipForwardBtn.TabIndex = 3;
            this.skipForwardBtn.Text = ">>";
            this.skipForwardBtn.UseVisualStyleBackColor = true;
            this.skipForwardBtn.Click += new System.EventHandler(this.skipForwardBtn_Click);
            // 
            // skipButton
            // 
            this.skipButton.Location = new System.Drawing.Point(218, 392);
            this.skipButton.Name = "skipButton";
            this.skipButton.Size = new System.Drawing.Size(39, 23);
            this.skipButton.TabIndex = 4;
            this.skipButton.Text = "Skip";
            this.skipButton.UseVisualStyleBackColor = true;
            this.skipButton.Click += new System.EventHandler(this.skipButton_Click);
            // 
            // currentTLabel
            // 
            this.currentTLabel.AutoSize = true;
            this.currentTLabel.Location = new System.Drawing.Point(35, 347);
            this.currentTLabel.Name = "currentTLabel";
            this.currentTLabel.Size = new System.Drawing.Size(34, 13);
            this.currentTLabel.TabIndex = 5;
            this.currentTLabel.Text = "00:00";
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.Location = new System.Drawing.Point(698, 347);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(34, 13);
            this.durationLabel.TabIndex = 6;
            this.durationLabel.Text = "00:00";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(268, 80);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(294, 220);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(97, 266);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // waveBox
            // 
            this.waveBox.Location = new System.Drawing.Point(38, 299);
            this.waveBox.Name = "waveBox";
            this.waveBox.Size = new System.Drawing.Size(694, 61);
            this.waveBox.TabIndex = 11;
            this.waveBox.TabStop = false;
            // 
            // skipBackBtn
            // 
            this.skipBackBtn.Location = new System.Drawing.Point(103, 392);
            this.skipBackBtn.Name = "skipBackBtn";
            this.skipBackBtn.Size = new System.Drawing.Size(28, 23);
            this.skipBackBtn.TabIndex = 12;
            this.skipBackBtn.Text = "<<";
            this.skipBackBtn.UseVisualStyleBackColor = true;
            this.skipBackBtn.Click += new System.EventHandler(this.skipBackBtn_Click);
            // 
            // loopOne
            // 
            this.loopOne.AutoSize = true;
            this.loopOne.Location = new System.Drawing.Point(422, 398);
            this.loopOne.Name = "loopOne";
            this.loopOne.Size = new System.Drawing.Size(77, 17);
            this.loopOne.TabIndex = 13;
            this.loopOne.Text = "Loop once";
            this.loopOne.UseVisualStyleBackColor = true;
            this.loopOne.CheckedChanged += new System.EventHandler(this.loopOne_CheckedChanged);
            // 
            // loopInf
            // 
            this.loopInf.AutoSize = true;
            this.loopInf.Location = new System.Drawing.Point(505, 398);
            this.loopInf.Name = "loopInf";
            this.loopInf.Size = new System.Drawing.Size(50, 17);
            this.loopInf.TabIndex = 14;
            this.loopInf.Text = "Loop";
            this.loopInf.UseVisualStyleBackColor = true;
            this.loopInf.CheckedChanged += new System.EventHandler(this.loopInf_CheckedChanged);
            // 
            // loopAll
            // 
            this.loopAll.AutoSize = true;
            this.loopAll.Location = new System.Drawing.Point(561, 398);
            this.loopAll.Name = "loopAll";
            this.loopAll.Size = new System.Drawing.Size(84, 17);
            this.loopAll.TabIndex = 15;
            this.loopAll.Text = "Loop playlist";
            this.loopAll.UseVisualStyleBackColor = true;
            this.loopAll.CheckedChanged += new System.EventHandler(this.loopAll_CheckedChanged);
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Location = new System.Drawing.Point(103, 28);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(67, 13);
            this.authorLabel.TabIndex = 16;
            this.authorLabel.Text = "current track";
            // 
            // albumLabel
            // 
            this.albumLabel.AutoSize = true;
            this.albumLabel.Location = new System.Drawing.Point(103, 45);
            this.albumLabel.Name = "albumLabel";
            this.albumLabel.Size = new System.Drawing.Size(36, 13);
            this.albumLabel.TabIndex = 17;
            this.albumLabel.Text = "Album";
            // 
            // genreLabel
            // 
            this.genreLabel.AutoSize = true;
            this.genreLabel.Location = new System.Drawing.Point(103, 62);
            this.genreLabel.Name = "genreLabel";
            this.genreLabel.Size = new System.Drawing.Size(36, 13);
            this.genreLabel.TabIndex = 18;
            this.genreLabel.Text = "Genre";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.genreLabel);
            this.Controls.Add(this.albumLabel);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.loopAll);
            this.Controls.Add(this.loopInf);
            this.Controls.Add(this.loopOne);
            this.Controls.Add(this.skipBackBtn);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.durationLabel);
            this.Controls.Add(this.currentTLabel);
            this.Controls.Add(this.skipButton);
            this.Controls.Add(this.skipForwardBtn);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.waveBox);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waveBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button skipForwardBtn;
        private System.Windows.Forms.Button skipButton;
        private System.Windows.Forms.Label currentTLabel;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.PictureBox waveBox;
        private System.Windows.Forms.Button skipBackBtn;
        private System.Windows.Forms.CheckBox loopOne;
        private System.Windows.Forms.CheckBox loopInf;
        private System.Windows.Forms.CheckBox loopAll;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.Label albumLabel;
        private System.Windows.Forms.Label genreLabel;
    }
}

