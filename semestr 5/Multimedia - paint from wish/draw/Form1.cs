using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace draw
{
    enum Tool { pointer, eraser, line, rectangle, elipse, polygon};
    public partial class Form1 : Form
    {
        
        Tool currentTool = Tool.line;
        bool dragging = false;
        Color color = Color.Black;
        int size = 3;
        int id = 0;
        double zoom = 1;

        string file;
        Image image;

        bool fill = false;
        bool shft = false;

        int widthCh;
        int heightCh;

        Point startP = new Point(0, 0);
        Point endP = new Point(0, 0);

        List<PointF> points = new List<PointF>();
        List<Drawing> drawings = new List<Drawing>();


        public Form1()
        {
            InitializeComponent();
            colorPicker.BackColor = color;
            toolStripStatusLabelTool.Text = "Pointer";
            toolStripStatusLabelSize.Text = size.ToString();
            toolStripStatusLabelColor.Text = color.Name;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startP = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabelMouse.Text = "X " + e.Location.X + " | Y " + e.Location.Y;
            if (dragging)
            {
                switch (currentTool)
                {
                    case (Tool.pointer):
                        break;
                    case (Tool.line):
                        points.Add(e.Location);
                        pictureBox1.Invalidate();
                        break;
                    case (Tool.eraser):
                        foreach (Drawing d in drawings)
                        {
                            double dist = PointToLineSquaredDistance(e.Location, d.p.ToArray());
                            if (dist <= Math.Pow(size,2))
                            {
                                drawings = drawings.Where(drawing => drawing.idD != d.idD).ToList();
                                pictureBox1.Invalidate();
                                break;
                            }
                        }
                        break;
                }
                
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            endP = e.Location;

            if (Control.ModifierKeys.HasFlag(Keys.Shift)) shft = true; else shft = false;

            switch (currentTool)
            {
                case (Tool.line):
                    if (points.Count > 1) drawings.Add(new Drawing(points, color, size, drawings.Count));
                    else drawings.Add(new Drawing(e.Location, color, size, drawings.Count));
                    
                    points.Clear();
                    break;
                case (Tool.eraser):
                    foreach (Drawing d in drawings)
                    {
                        double dist = PointToLineSquaredDistance(e.Location, d.col.ToArray());
                        if (dist <= Math.Pow(size, 2))
                        {
                            drawings = drawings.Where(drawing => drawing.idD != d.idD).ToList();
                            pictureBox1.Invalidate();
                            break;
                        }
                    }
                    break;
                case (Tool.rectangle):
                    List<PointF> corners = new List<PointF>();
                    if (!shft)
                    {
                        corners = new List<PointF> {
                        startP,
                        new PointF(startP.X, endP.Y),
                        endP,
                        new PointF(endP.X, startP.Y),
                        startP
                        };
                    }else
                    {
                        corners = new List<PointF> {
                        startP,
                        new PointF(startP.X, endP.Y),
                        new PointF(startP.X+(endP.Y - startP.Y), endP.Y),
                        new PointF(startP.X+(endP.Y - startP.Y), startP.Y),
                        startP
                        };
                    }
                    
                    drawings.Add(new Drawing(corners, color, size, drawings.Count, currentTool, fill));
                    corners.Clear();
                    break;
                case (Tool.elipse):
                    List<PointF> cornersE = new List<PointF> { startP, endP };

                    drawings.Add(new Drawing(cornersE, color, size, drawings.Count, currentTool, fill, shft));
                    cornersE.Clear();
                    break;
                case (Tool.polygon):
                    points.Add(e.Location);
                    
                    pictureBox1.Invalidate();
                    if (((Math.Pow(points[0].X - e.Location.X, 2) + Math.Pow(points[0].Y -e.Location.Y, 2)) < (100))&&points.Count > 1)
                    {
                        drawings.Add(new Drawing(points, color, size, drawings.Count, currentTool, fill));
                        pictureBox1.Invalidate();
                        points.Clear();
                        break;
                    }
                    break;
            }
            //id++;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(color, size);
            if(points.Count > 1)
            {
                g.DrawLines(pen, points.ToArray());
            }
            foreach (Drawing d in drawings)
            {
                d.g = g;
                d.Draw();
            }
        }

        private void colorChange_Click(object sender, EventArgs e)
        {
            colorDialog1.AllowFullOpen = false;
            colorDialog1.ShowHelp = true;
            colorDialog1.Color = color;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                color = colorDialog1.Color;
                colorPicker.BackColor = color;
                toolStripStatusLabelColor.Text = color.Name;
            }
                
        }

        //
        //  Chat GPT
        //
        //  distance between a polyline or a line from multiple points distanced from eachother
        //

        private float PointToLineSquaredDistance(Point p, PointF lineStart, PointF lineEnd)
        {
            float dx = lineEnd.X - lineStart.X;
            float dy = lineEnd.Y - lineStart.Y;

            float px = p.X - lineStart.X;
            float py = p.Y - lineStart.Y;

            float dotProduct = px * dx + py * dy;

            if (dotProduct <= 0)
            {
                return px * px + py * py;
            }

            float squaredLength = dx * dx + dy * dy;

            if (dotProduct >= squaredLength)
            {
                float sx = p.X - lineEnd.X;
                float sy = p.Y - lineEnd.Y;
                return sx * sx + sy * sy;
            }

            if (squaredLength == 0)
            {
                // Handle case where the line segment is just a point
                return px * px + py * py;
            }

            float squaredDistanceToLine = px * px + py * py - (dotProduct * dotProduct) / squaredLength;
            return squaredDistanceToLine;
        }


        private float PointToLineSquaredDistance(Point p, PointF[] linePoints)
        {
            if (linePoints.Length < 2)
                return int.MaxValue; // Handle degenerate cases

            float minSquaredDistance = int.MaxValue;

            for (int i = 0; i < linePoints.Length - 1; i++)
            {
                float currentSquaredDistance = PointToLineSquaredDistance(p, linePoints[i], linePoints[i + 1]);
                minSquaredDistance = Math.Min(minSquaredDistance, currentSquaredDistance);
            }

            return minSquaredDistance;
        }
        //
        //
        //
        private void lineBtn_Click(object sender, EventArgs e)
        {
            currentTool = Tool.line;
            toolStripStatusLabelTool.Text = "Line";
            points.Clear();
            pictureBox1.Invalidate();
        }

        private void EraserBtn_Click(object sender, EventArgs e)
        {
            currentTool = Tool.eraser;
            toolStripStatusLabelTool.Text = "Eraser";
            points.Clear();
            pictureBox1.Invalidate();
        }

        private void recBtn_Click(object sender, EventArgs e)
        {
            currentTool = Tool.rectangle;
            toolStripStatusLabelTool.Text = "Rectangle";
            points.Clear();
            pictureBox1.Invalidate();
        }

        private void elipseBtn_Click(object sender, EventArgs e)
        {
            currentTool = Tool.elipse;
            toolStripStatusLabelTool.Text = "Elipse";
            points.Clear();
            pictureBox1.Invalidate();
        }

        private void polyBtn_Click(object sender, EventArgs e)
        {
            currentTool = Tool.polygon;
            toolStripStatusLabelTool.Text = "Polygon";
            points.Clear();
            pictureBox1.Invalidate();
        }

        private void pointerBtn_Click(object sender, EventArgs e)
        {
            currentTool = Tool.pointer;
            toolStripStatusLabelTool.Text = "Pointer";
            points.Clear();
            pictureBox1.Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            fill = checkBox1.Checked;
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            drawings.Clear();
            points.Clear();
            pictureBox1.Invalidate();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            widthCh = Width - widthCh;
            heightCh = Height - heightCh;

            pictureBox1.Width += widthCh;
            pictureBox1.Height += heightCh;

            if(image != null)
            {
                zoom = (double)pictureBox1.Width / (double)image.Width;
                if ((image.Height * zoom) > pictureBox1.Height) zoom = (double)pictureBox1.Height / (double)image.Height;
            }

            Draw();

            pictureBox1.Invalidate();
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            widthCh = Width;
            heightCh = Height;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                file = openFileDialog1.FileName;
                try
                {
                    image = Image.FromFile(file);
                    if(image.Width > image.Height) zoom = (double)pictureBox1.Width / (double)image.Width;
                    else zoom = (double)pictureBox1.Height / (double)image.Height;
                    Draw();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void Draw()
        {
            if (image != null)
            {
                Bitmap bmp = new Bitmap(image, Convert.ToInt32(image.Width * zoom), Convert.ToInt32(image.Height * zoom));
                Graphics g = Graphics.FromImage(bmp);

                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                g.DrawImage(bmp, 0, 0);
                pictureBox1.Image = bmp;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp|All Files|*.*";
                saveFileDialog1.Title = "Save an Image File";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    int pictureBoxWidth = pictureBox1.Width;
                    int pictureBoxHeight = pictureBox1.Height;

                    // Create a Bitmap to hold the image
                    Bitmap bmp = new Bitmap(pictureBoxWidth, pictureBoxHeight);

                    // Create a Graphics object associated with the Bitmap
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        // Draw the contents of pictureBox1 onto the Bitmap
                        pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, pictureBoxWidth, pictureBoxHeight));

                        // Draw the custom drawings
                        Pen pen = new Pen(color, size);
                        if (points.Count > 1)
                        {
                            graphics.DrawLines(pen, points.ToArray());
                        }

                        foreach (Drawing d in drawings)
                        {
                            d.g = graphics;
                            d.Draw();
                        }

                        // Save the Bitmap to the selected image file
                        bmp.Save(saveFileDialog1.FileName);
                    }
                }
            }
        }

        private void sizeSelector_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void sizeSelector_TextChanged(object sender, EventArgs e)
        {
            if (sizeSelector.Text.Length > 0)
            {
                size = int.Parse(sizeSelector.Text);
                toolStripStatusLabelSize.Text = size.ToString();
            }
            else sizeSelector.Text = size.ToString();
        }  
    }
}
