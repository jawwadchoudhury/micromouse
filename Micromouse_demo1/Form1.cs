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
        
        private int[] mazeArray = {3, 3, 2, 6, 3, 14, 4, 4, 1, 8, 8};
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
                    new Point(0, bs),
                    new Point(0, 0),
                    new Point(bs, 0),
                    new Point(bs, bs)
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
                    new Point(bs + 10, 0),
                    new Point(bs + 10, bs),
                    new Point(0, bs)
                };
            }
            else if (i == 14)
            {
                points = new Point[]
                {
                    new Point(0, 0),
                    new Point(0, bs + 10),
                    new Point(bs, bs + 10),
                    new Point(bs, 0)
                };
            }
            else if (i == 15)
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

        int j;
        int BoxSize = 50;
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Lines lines = new Lines(shapePoints(j, BoxSize), Color.Black, 5);
            lines.Draw(e.Graphics);
            j++;
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            generateMaze(mazeArray, 50);
            //generateAllIcons(10, 10, 50);

            //Console.Write("Maze height: ");
            //int.TryParse(Console.ReadLine(), out mazeHeight);
            //Console.Write("Maze width: ");
            //int.TryParse(Console.ReadLine(), out mazeWidth);
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.Write($"Enter number {i}/{mazeHeight * mazeWidth}: ");
            //}
        }

        int x;
        private void generateAllIcons(int startX, int startY, int boxSize)
        {
            j = 1;
            x = startX;
            for (int i = 0; i < 15; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Name = $"pictureBox_{i}";
                pictureBox.Size = new Size(boxSize, boxSize);
                pictureBox.Location = new Point(x, startY);
                x = x + boxSize + 10;
                this.Controls.Add(pictureBox);
                pictureBox.Paint += new PaintEventHandler(pictureBox_Paint);
            }
        }

        private void generateMaze(int[] maze, int boxSize)
        {
            int x = 10;
            int y = 50;
            mazeArrayInt = 2;
            for (int i = 0; i < maze[0]; i++)
            {
                for (int j = 0; j < maze[1]; j++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Name = $"pictureBox_{i}";
                    pictureBox.Size = new Size(boxSize, boxSize);
                    pictureBox.Location = new Point(x, y);
                    x = x + boxSize;
                    this.Controls.Add(pictureBox);
                    pictureBox.Paint += new PaintEventHandler(mazeBox_paint);
                };
                x = 10;
                y = y + boxSize;
            };
        }

        int mazeArrayInt;
        private void mazeBox_paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Lines lines = new Lines(shapePoints(mazeArray[mazeArrayInt], BoxSize), Color.Black, 5);
            lines.Draw(e.Graphics);
            mazeArrayInt++;
        }

    }
}
