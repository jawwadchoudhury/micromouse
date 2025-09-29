using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micromouse_demo1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        public class Lines
        {
            private Point[] Points;
            private Pen pen;

            public Lines(Point[] points, Color colour, float width)
            {
                Points = points;
                pen = new Pen(colour, width);
            }
            public void Draw(Graphics g)
            {
                g.DrawLines(pen, Points);
            }

            public void Dispose()
            {
                pen.Dispose();
            }
            
        }
        public Point[] shapePoints(int i, int bs)
        {
            Point[] points = new Point[]
            {
                new Point(0, 0),
                new Point(0, 0)
            };

            if (i == 1)
            {
                points = new Point[] {
                    new Point(0, 0),
                    new Point(0, bs),
                    new Point(bs, bs)
                };
            }
            else if (i == 2)
            {
                points = new Point[]
                {
                    new Point(0, bs),
                    new Point(0, 0),
                    new Point(bs, 0)
                };
            }
            else if (i == 3)
            {
                points = new Point[]
                {
                    new Point(0, 0),
                    new Point(bs, 0),
                    new Point(bs, bs)
                };
            }
            else if (i == 4)
            {
                points = new Point[]
                {
                    new Point(bs, 0),
                    new Point(bs, bs),
                    new Point(0, bs)
                };
            }
            else if (i == 5)
            {
                points = new Point[]
                {
                    new Point(0, 0),
                    new Point(0, bs)
                };
            }
            else if (i == 6)
            {
                points = new Point[]
                {
                    new Point(0, 0),
                    new Point(bs, 0)
                };
            }
            else if (i == 7)
            {
                points = new Point[]
                {
                    new Point(bs, 0),
                    new Point(bs, bs)
                };
            }
            else if (i == 8)
            {
                points = new Point[]
                {
                    new Point(0, bs),
                    new Point(bs, bs)
                };
            }
            else if (i == 9)
            {
                points = new Point[]
                {
                    new Point(0, 0),
                    new Point(0, bs),
                    new Point(bs, bs),
                    new Point(bs, 0)
                };
            }
            else if (i == 10)
            {
                points = new Point[]
                {
                    new Point(bs, 0),
                    new Point(0, 0),
                    new Point(0, bs),
                    new Point(bs, bs)
                };
            }
            else if (i == 11)
            {
                points = new Point[]
                {
                    new Point(0, 0),
                    new Point(bs, 0),
                    new Point(bs, bs),
                    new Point(0, bs)
                };
            }
            else if (i == 12)
            {
                points = new Point[]
                {
                    new Point(0, 0),
                    new Point(bs, 0),
                    new Point(bs, bs),
                    new Point(0, bs)
                };
            }
            else if (i == 13)
            {
                points = new Point[]
                {
                    new Point(0, 0),
                    new Point(bs, 0),
                    new Point(bs, bs),
                    new Point(0, bs),
                    new Point(0, 0),
                };
            }
            return points;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Lines lines = new Lines(shapePoints(j, boxSize / 2), Color.Black, 3);
            lines.Draw(e.Graphics);
            j++;
        }

        int i = 0;
        int j = 1;
        int x = 10;
        int y = 10;
        int boxSize = 50;
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 12; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Name = $"pictureBox_{i}";
                pictureBox.Size = new Size(boxSize, boxSize);
                pictureBox.Location = new Point(x, y);
                x = x + boxSize;
                this.Controls.Add(pictureBox);
                pictureBox.Paint += new PaintEventHandler(pictureBox_Paint);
            }
        }
    }
}
