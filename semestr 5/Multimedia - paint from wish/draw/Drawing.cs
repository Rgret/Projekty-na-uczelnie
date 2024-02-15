using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace draw
{
    internal class Drawing
    {
        public Pen pen;
        public List<PointF> p = new List<PointF>();
        public List<PointF> col = new List<PointF>();
        public PointF point;
        public Graphics g;
        public int idD;
        public int width;
        public Tool tool;
        public bool fill;
        public PointF startP;
        public PointF endP;
        public bool shifted;
        public Drawing(List<PointF> points, Color c, int width, int id, Tool tool = Tool.line, bool fill = false, bool shifted = false)
        {
            foreach (PointF point in points)
            {
                this.p.Add(point);
                this.col.Add(point);
            }
            this.pen = new Pen(c, width);
            this.idD = id;
            this.width = width;
            this.tool = tool;
            this.fill = fill;
            this.shifted = shifted;
        }
        public Drawing(PointF point, Color c, int width, int id, Tool tool = Tool.line)
        {
            this.point = point;
            this.p.Add(point); this.p.Add(point);
            //this.col.Add(point); this.col.Add(point);
            this.pen = new Pen(c, width);
            this.idD = id;
            this.width = width;
            this.tool = tool;
        }
        public void Draw()
        {
            SolidBrush b = new SolidBrush(this.pen.Color);
            switch (this.tool)
            {
                case (Tool.line):
                    if (this.p.Count > 2)
                    {
                        g.DrawLines(this.pen, this.p.ToArray());
                    }
                    else
                    {
                        g.FillEllipse(b, new RectangleF(this.point.X - (width * 2) / 2, this.point.Y - (width * 2) / 2, (float)(width * 1.5), (float)(width * 1.5)));
                    }
                    break;
                case Tool.rectangle:
                    g.DrawLines(this.pen, this.p.ToArray());
                    if (this.fill)
                    {
                        float xR = this.p[0].X;
                        float yR = this.p[0].Y;
                        float wR = this.p[2].X - this.p[0].X; if (wR < 0) { wR *= -1; xR = this.p[2].X; }
                        float hR = this.p[2].Y - this.p[0].Y; if (hR < 0) { hR *= -1; yR = this.p[2].Y; }
                        g.FillRectangle(b, new RectangleF(xR, yR, wR, hR));
                        foreach (PointF point in this.p) this.col.Add(point);
                    }
                    break;
                case Tool.elipse:
                    if(startP.IsEmpty && endP.IsEmpty)
                    {
                        startP = this.p[0];
                        endP = this.p[1];
                        this.p.Clear();
                    }

                    float hE = endP.Y - startP.Y;
                    float wE;
                    if (!this.shifted) wE = endP.X - startP.X; else wE = hE;

                    RectangleF ellipseBounds = new RectangleF(startP.X, startP.Y, wE, hE);


                    int numPoints = 360;
                    PointF[] pointsE = new PointF[numPoints];

                    for (int i = 0; i < numPoints; i++)
                    {
                        double angle = 2 * Math.PI * i / numPoints;
                        float x = ellipseBounds.X - ellipseBounds.Width * (float)Math.Cos(angle);
                        float y = ellipseBounds.Y - ellipseBounds.Height * (float)Math.Sin(angle);
                        pointsE[i] = new PointF(x, y);
                    }

                    g.DrawLines(pen, pointsE);
                    if (this.fill)
                    {
                        float xE = startP.X - wE; 
                        float yE = startP.Y - hE;
                        g.FillEllipse(b, xE, yE, wE*2, hE*2);
                    }
                    foreach (PointF pnt in pointsE)
                    {
                        this.p.Add(pnt);
                    }
                    this.p = this.p.Distinct().ToList();
                    break;
                case Tool.polygon:
                    if (this.p[this.p.Count()-1] != this.p[0]) this.p[this.p.Count()-1] = this.p[0];
                    g.DrawLines(this.pen, this.p.ToArray());
                    if(fill) g.FillPolygon(new SolidBrush(this.pen.Color), this.p.ToArray());
                    break;
            }
        }
    }
}
