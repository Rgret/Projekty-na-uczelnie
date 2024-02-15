namespace draw
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.eraserBtn = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorPicker = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMouse = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelColor = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelTool = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lineBtn = new System.Windows.Forms.Button();
            this.recBtn = new System.Windows.Forms.Button();
            this.elipseBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.polyBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.sizeSelector = new System.Windows.Forms.TextBox();
            this.pointerBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(801, 395);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // eraserBtn
            // 
            this.eraserBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.eraserBtn.Location = new System.Drawing.Point(251, 1);
            this.eraserBtn.Name = "eraserBtn";
            this.eraserBtn.Size = new System.Drawing.Size(49, 23);
            this.eraserBtn.TabIndex = 1;
            this.eraserBtn.Text = "Eraser";
            this.eraserBtn.UseVisualStyleBackColor = true;
            this.eraserBtn.Click += new System.EventHandler(this.EraserBtn_Click);
            // 
            // colorPicker
            // 
            this.colorPicker.Location = new System.Drawing.Point(105, 0);
            this.colorPicker.Name = "colorPicker";
            this.colorPicker.Size = new System.Drawing.Size(26, 23);
            this.colorPicker.TabIndex = 2;
            this.colorPicker.UseVisualStyleBackColor = true;
            this.colorPicker.Click += new System.EventHandler(this.colorChange_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMouse,
            this.toolStripStatusLabelColor,
            this.toolStripStatusLabelTool,
            this.toolStripStatusLabelSize});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelMouse
            // 
            this.toolStripStatusLabelMouse.Name = "toolStripStatusLabelMouse";
            this.toolStripStatusLabelMouse.Size = new System.Drawing.Size(69, 17);
            this.toolStripStatusLabelMouse.Text = "Mouse X | Y";
            // 
            // toolStripStatusLabelColor
            // 
            this.toolStripStatusLabelColor.Name = "toolStripStatusLabelColor";
            this.toolStripStatusLabelColor.Size = new System.Drawing.Size(36, 17);
            this.toolStripStatusLabelColor.Text = "Color";
            // 
            // toolStripStatusLabelTool
            // 
            this.toolStripStatusLabelTool.Name = "toolStripStatusLabelTool";
            this.toolStripStatusLabelTool.Size = new System.Drawing.Size(29, 17);
            this.toolStripStatusLabelTool.Text = "Tool";
            // 
            // toolStripStatusLabelSize
            // 
            this.toolStripStatusLabelSize.Name = "toolStripStatusLabelSize";
            this.toolStripStatusLabelSize.Size = new System.Drawing.Size(27, 17);
            this.toolStripStatusLabelSize.Text = "Size";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(750, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(38, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Fill";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lineBtn
            // 
            this.lineBtn.Location = new System.Drawing.Point(306, 1);
            this.lineBtn.Name = "lineBtn";
            this.lineBtn.Size = new System.Drawing.Size(49, 23);
            this.lineBtn.TabIndex = 6;
            this.lineBtn.Text = "Line";
            this.lineBtn.UseVisualStyleBackColor = true;
            this.lineBtn.Click += new System.EventHandler(this.lineBtn_Click);
            // 
            // recBtn
            // 
            this.recBtn.Location = new System.Drawing.Point(361, 1);
            this.recBtn.Name = "recBtn";
            this.recBtn.Size = new System.Drawing.Size(69, 23);
            this.recBtn.TabIndex = 7;
            this.recBtn.Text = "Rectangle";
            this.recBtn.UseVisualStyleBackColor = true;
            this.recBtn.Click += new System.EventHandler(this.recBtn_Click);
            // 
            // elipseBtn
            // 
            this.elipseBtn.Location = new System.Drawing.Point(436, 0);
            this.elipseBtn.Name = "elipseBtn";
            this.elipseBtn.Size = new System.Drawing.Size(53, 23);
            this.elipseBtn.TabIndex = 8;
            this.elipseBtn.Text = "Elipse";
            this.elipseBtn.UseVisualStyleBackColor = true;
            this.elipseBtn.Click += new System.EventHandler(this.elipseBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(43, 1);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(56, 23);
            this.clearBtn.TabIndex = 9;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // polyBtn
            // 
            this.polyBtn.Location = new System.Drawing.Point(495, 1);
            this.polyBtn.Name = "polyBtn";
            this.polyBtn.Size = new System.Drawing.Size(61, 23);
            this.polyBtn.TabIndex = 10;
            this.polyBtn.Text = "Polygon";
            this.polyBtn.UseVisualStyleBackColor = true;
            this.polyBtn.Click += new System.EventHandler(this.polyBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // sizeSelector
            // 
            this.sizeSelector.Location = new System.Drawing.Point(137, 0);
            this.sizeSelector.Name = "sizeSelector";
            this.sizeSelector.Size = new System.Drawing.Size(41, 20);
            this.sizeSelector.TabIndex = 11;
            this.sizeSelector.Text = "3";
            this.sizeSelector.TextChanged += new System.EventHandler(this.sizeSelector_TextChanged);
            this.sizeSelector.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sizeSelector_KeyPress);
            // 
            // pointerBtn
            // 
            this.pointerBtn.Location = new System.Drawing.Point(184, 2);
            this.pointerBtn.Name = "pointerBtn";
            this.pointerBtn.Size = new System.Drawing.Size(61, 23);
            this.pointerBtn.TabIndex = 12;
            this.pointerBtn.Text = "Pointer";
            this.pointerBtn.UseVisualStyleBackColor = true;
            this.pointerBtn.Click += new System.EventHandler(this.pointerBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pointerBtn);
            this.Controls.Add(this.sizeSelector);
            this.Controls.Add(this.polyBtn);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.elipseBtn);
            this.Controls.Add(this.recBtn);
            this.Controls.Add(this.lineBtn);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.colorPicker);
            this.Controls.Add(this.eraserBtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResizeBegin += new System.EventHandler(this.Form1_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button eraserBtn;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button colorPicker;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMouse;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button lineBtn;
        private System.Windows.Forms.Button recBtn;
        private System.Windows.Forms.Button elipseBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button polyBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox sizeSelector;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelColor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTool;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSize;
        private System.Windows.Forms.Button pointerBtn;
    }
}

