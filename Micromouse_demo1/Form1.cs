using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micromouse_demo1
{
    public partial class Form1 : Form
    {

        //private int[] mazeArray = {3, 3, 9, 1, 3, 10, 14, 14, 12, 4, 4};
        private int[] mazeArray = {0, 9, 0, 10, 12, 13, 0};
        private Node[] maze = { new Node(9), new Node(1), new Node(3), new Node(10), new Node(14), new Node(14), new Node(12), new Node(4), new Node(4) };
        private int[] mazeSize = { 3, 3 };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            generateMaze(maze, 50);
            Random rnd = new Random();
            int rnd1 = rnd.Next(0, 16);
            int rnd2 = rnd.Next(0, 16);
            outputLabel1.Text = $"From 6 to 9/10/12/13: {intListToString(returnValidMoves(new Node(6), new Node[] { new Node(9), new Node(10), new Node(12), new Node(13) } ))}";
        }
        private bool validMove(Node startNode, Node endNode, int direction) //Eg. validMove(new Node(1), new Node(2), 2) would be true, final int is TRBL notation again.
        {
            if (direction == 1) //Top
            {
                if (startNode.getTopValue() == 0 && endNode.getBottomValue() == 0)
                {
                    return true;
                }
            }
            else if (direction == 2) //Right
            {
                if (startNode.getRightValue() == 0 && endNode.getLeftValue() == 0)
                {
                    return true;
                }
            }
            else if (direction == 3) //Bottom
            {
                if (startNode.getBottomValue() == 0 && endNode.getTopValue() == 0)
                {
                    return true;
                }
            }
            else if (direction == 4) //Left
            {
                if (startNode.getLeftValue() == 0 && endNode.getRightValue() == 0)
                {
                    return true;
                }
            }
            return false;
        }
        private string intListToString(List<int> list)
        {
            string output = "";
            foreach (int i in list)
            {
                output = output + i.ToString() + ", ";
            }
            return output;
        }
        private List<int> returnValidMoves(Node startNode, Node[] surroundingNodes)
        {
            List<int> validMoves = new List<int>();
            if (startNode.getTopValue() == 0 && surroundingNodes[0].getBottomValue() == 0)
            {
                validMoves.Add(1);
            }
            if (startNode.getRightValue() == 0 && surroundingNodes[1].getLeftValue() == 0)
            {
                validMoves.Add(2);
            }
            if (startNode.getBottomValue() == 0 && surroundingNodes[2].getTopValue() == 0)
            {
                validMoves.Add(3);
            }
            if (startNode.getLeftValue() == 0 && surroundingNodes[3].getRightValue() == 0)
            {
                validMoves.Add(4);
            }
            return validMoves;
        }

        public class Node
        {
            private uint BinaryValue;
            private int DecimalValue;
            //T, R, B, L
            //1111 is a rectangle

            public Node(int decimalValue)
            {
                DecimalValue = decimalValue;
                BinaryValue = (uint)decimalValue;
            }
            public int getTopValue()
            {
                return (int)(BinaryValue) & 1;
            }
            public int getRightValue()
            {
                return (int)(BinaryValue >> 1) & 1;
            }
            public int getBottomValue()
            {
                return (int)(BinaryValue >> 2) & 1;
            }
            public int getLeftValue()
            {
                return (int)(BinaryValue >> 3) & 1;
            }
            public uint getBinaryValue()
            {
                return BinaryValue;
            }
            public string getBinaryStringValue()
            {
                return Convert.ToString(DecimalValue, 2).PadLeft(4, '0');
            }
            public int getDecimalValue()
            {
                return DecimalValue;
            }
        }
        public class Shape
        {
            private uint BinaryValue;
            private int DecimalValue;
            private Pen pen;
            private int BoxWidth;

            public Shape(Node node, int boxWidth, Color colour, float width)
            {
                DecimalValue = node.getDecimalValue();
                BinaryValue = node.getBinaryValue();
                pen = new Pen(colour, width);
                BoxWidth = boxWidth;
            }


            public void DrawShape(Graphics g)
            {
                if ((BinaryValue & 1) == 1)
                {
                    g.DrawLine(pen, new Point(0, 0), new Point(BoxWidth, 0));
                }
                if ((BinaryValue >> 1 & 1) == 1)
                {
                    g.DrawLine(pen, new Point(BoxWidth, 0), new Point(BoxWidth, BoxWidth));
                }
                if ((BinaryValue >> 2 & 1) == 1)
                {
                    g.DrawLine(pen, new Point(0, BoxWidth), new Point(BoxWidth, BoxWidth));
                }
                if ((BinaryValue >> 3 & 1) == 1)
                {
                    g.DrawLine(pen, new Point(0, 0), new Point(0, BoxWidth));
                }
            }
        }
        private void generateMaze(Node[] maze, int boxSize)
        {
            int sx = 10; // start x, like a typewriter on a new line sort of thing
            int x = sx;
            int y = 50;
            mazeArrayInt = 0;
            BoxSize = boxSize;
            for (int i = 0; i < mazeSize[0]; i++)
            {
                for (int j = 0; j < mazeSize[1]; j++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Name = $"pictureBox_{i}";
                    pictureBox.Size = new Size(boxSize, boxSize);
                    pictureBox.Location = new Point(x, y);
                    x = x + boxSize;
                    this.Controls.Add(pictureBox);
                    pictureBox.Paint += new PaintEventHandler(mazeBox_paint);
                };
                x = sx;
                y = y + boxSize;
            };
        }

        int mazeArrayInt;
        int BoxSize = 50;
        private void mazeBox_paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Shape shape = new Shape(maze[mazeArrayInt], BoxSize, Color.Black, 5);
            shape.DrawShape(e.Graphics);
            mazeArrayInt++;
        }

    }
}
