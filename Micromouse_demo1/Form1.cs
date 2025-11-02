using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Micromouse_demo1
{
    public partial class Form1 : Form
    {
        public List<MazeNode> currentMaze = new List<MazeNode>();
        public int[] currentMazeDimensions = new int[4]; // 0 = width, 1 = height, 2 = box size, 3 = node count
        private Node[] topNodes = new Node[8] { new Node(0), new Node(1), new Node(2), new Node(3), new Node(8), new Node(9), new Node(10), new Node(11) };
        private Node[] rightNodes = new Node[8] { new Node(0), new Node(1), new Node(2), new Node(3), new Node(4), new Node(5), new Node(6), new Node(7) };
        private Node[] bottomNodes = new Node[8] { new Node(0), new Node(2), new Node(4), new Node(6), new Node(8), new Node(10), new Node(12), new Node(14) };
        private Node[] leftNodes = new Node[8] { new Node(0), new Node(1), new Node(4), new Node(5), new Node(8), new Node(9), new Node(12), new Node(13) };
        private int[] topIndexes = new int[8] { 0, 1, 2, 3, 8, 9, 10, 11 };
        private int[] rightIndexes = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
        private int[] bottomIndexes = new int[8] { 0, 2, 4, 6, 8, 10, 12, 14 };
        private int[] leftIndexes = new int[8] { 0, 1, 4, 5, 8, 9, 12, 13 };
        public int nodeCount = 0;
        
        public class nodalPictureBox : System.Windows.Forms.PictureBox
        {
            public MazeNode MazeNode { get; private set; }

            public void setNode(MazeNode node)
            {
                MazeNode = node;
            }

            public void setWidthCount(int widthCount)
            {
                MazeNode.setWidthCount(widthCount);
            }

            public void setHeightCount(int heightCount)
            {
                MazeNode.setHeightCount(heightCount);
            }

            public void setNodeIndex(int nodeIndex)
            {
                MazeNode.setNodeIndex(nodeIndex);
            }
        }
        public class Node
        {
            public int DecimalValue { get; private set; }
            //T, R, B, L
            //1111 is a rectangle

            public Node(int decimalValue)
            {
                DecimalValue = decimalValue;
            }
            public bool getTopValue()
            {
                return ((uint)DecimalValue & 1) == 1;
            }
            public bool getRightValue()
            {
                return ((uint)DecimalValue >> 1 & 1) == 1;
            }
            public bool getBottomValue()
            {
                return ((uint)DecimalValue >> 2 & 1) == 1;
            }
            public bool getLeftValue()
            {
                return ((uint)DecimalValue >> 3 & 1) == 1;
            }

            public void toggleTopValue()
            {
                DecimalValue ^= 1;
            }
            public void toggleRightValue()
            {
                DecimalValue ^= 2;
            }
            public void toggleBottomValue()
            {
                DecimalValue ^= 4;
            }
            public void toggleLeftValue()
            {
                DecimalValue ^= 8;
            }

            public int getDecimalValue()
            {
                return DecimalValue;
            }
            public void setDecimalValue(int decimalValue)
            {
                DecimalValue = decimalValue;
            } 
        }
        public class MazeNode : Node
        {
            public int WidthCount { get; private set; }
            public int HeightCount { get; private set; }

            public int NodeIndex { get; private set; }
            public MazeNode(int decimalValue, int widthCount, int heightCount, int nodeIndex) : base(decimalValue)
            {
                WidthCount = widthCount;
                HeightCount = heightCount;
                NodeIndex = nodeIndex;
            }

            public void setWidthCount(int widthCount)
            {
                WidthCount = widthCount;
            }

            public void setHeightCount(int heightCount)
            {
                HeightCount = heightCount;
            }

            public void setNodeIndex(int nodeIndex)
            {
                NodeIndex = nodeIndex;
            }
        }
        public class Shape
        {
            private int DecimalValue;
            private Pen pen;
            private int BoxWidth;

            public Shape(Node node, int boxWidth, Color colour, float width)
            {
                DecimalValue = node.getDecimalValue();
                pen = new Pen(colour, width);
                BoxWidth = boxWidth;
            }


            public void DrawShape(Graphics g)
            {
                if (((uint)DecimalValue & 1) == 1)
                {
                    g.DrawLine(pen, new Point(0, 0), new Point(BoxWidth, 0));
                }
                if (((uint)DecimalValue >> 1 & 1) == 1)
                {
                    g.DrawLine(pen, new Point(BoxWidth, 0), new Point(BoxWidth, BoxWidth));
                }
                if (((uint)DecimalValue >> 2 & 1) == 1)
                {
                    g.DrawLine(pen, new Point(0, BoxWidth), new Point(BoxWidth, BoxWidth));
                }
                if (((uint)DecimalValue >> 3 & 1) == 1)
                {
                    g.DrawLine(pen, new Point(0, 0), new Point(0, BoxWidth));
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //generateMaze(maze, 3, 3, 50);
            //generateRandomMaze(3, 3, 50);
            //outputLabel1.Text = $"From 14 to 1/14/4/8: {intListToString(returnValidMoves(new Node(14), new Node[] { new Node(1), new Node(4), new Node(4), new Node(8) } ))}";
            //outputLabel1.Text = $"{validMove(new Node(5), new Node(3), 2)}";
        }
        
        private void createEmptyMaze(int width, int height,  int boxSize)
        {
            currentMaze.Clear();
            currentMazeDimensions[0] = width;
            currentMazeDimensions[1] = height;
            currentMazeDimensions[2] = boxSize;
            currentMazeDimensions[3] = width * height;
            int nodeIndex = 0;
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    currentMaze.Add(new MazeNode(15, w, h, nodeIndex));
                    nodeIndex++;
                }
            }

        }

        private void drawCurrentMaze()
        {
            mazePanel.Controls.Clear();
            int sx = 0; // Starting X
            int x = sx; // Initalise X to be Starting X
            int y = 0; // Intialise Y to be 0
            int width = currentMazeDimensions[0];
            int height = currentMazeDimensions[1];
            int boxSize = currentMazeDimensions[2];
            int nodeIndex = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    nodalPictureBox pictureBox = new nodalPictureBox();
                    pictureBox.Name = $"pictureBox_{nodeIndex}";
                    pictureBox.Size = new Size(boxSize, boxSize);
                    pictureBox.Location = new Point(x, y);

                    pictureBox.setNode(currentMaze[nodeIndex]);

                    nodeIndex++;

                    x = x + boxSize;
                    mazePanel.Controls.Add(pictureBox);
                    pictureBox.Paint += new PaintEventHandler(mazeBox_paint);
                }
                x = sx;
                y = y + boxSize;
            }
        }

        private void primsAlgorithm(int mazeIndex)
        {
            currentMaze[mazeIndex].setDecimalValue(0);
            
            MazeNode[] adjacentNodes = returnAdjacentNodes(currentMaze, mazeIndex);
            for (int i = 0; i < 1; i++)
            {
                
                Random random = new Random();
                int x = random.Next(0, 4);
                if (adjacentNodes[x] == null) continue;
                switch (x)
                {
                    case 0:
                        if (adjacentNodes[i].getBottomValue()) adjacentNodes[i].toggleBottomValue();
                        break;
                    case 1:
                        if (adjacentNodes[i].getLeftValue()) adjacentNodes[i].toggleLeftValue();
                        break;
                    case 2:
                        if (adjacentNodes[i].getTopValue()) adjacentNodes[i].toggleTopValue();
                        break;
                    case 3:
                        if (adjacentNodes[i].getRightValue()) adjacentNodes[i].toggleRightValue();
                        break;
                }
            }
            drawCurrentMaze();

            nodalPictureBox nodalPictureBox = (nodalPictureBox)mazePanel.Controls[mazeIndex];
            nodalPictureBox.BackColor = Color.FromArgb(255, 255, 155);
        }

        private MazeNode[] returnAdjacentNodes(List<MazeNode> maze, int nodeIndex)
        {
            MazeNode[] adjacentNodes = new MazeNode[4];

            MazeNode centralNode = maze[nodeIndex];

            MazeNode upperNode;
            MazeNode rightNode;
            MazeNode bottomNode;
            MazeNode leftNode;

            upperNode = (centralNode.NodeIndex - currentMazeDimensions[0] >= 0) ? currentMaze[centralNode.NodeIndex - currentMazeDimensions[0]] : null;
            rightNode = (centralNode.WidthCount + 1 <= currentMazeDimensions[0] - 1) ? currentMaze[centralNode.NodeIndex + 1] : null;
            bottomNode = (centralNode.NodeIndex + currentMazeDimensions[0] < currentMazeDimensions[3]) ? currentMaze[centralNode.NodeIndex + currentMazeDimensions[0]] : null;
            leftNode = (centralNode.WidthCount - 1 >= 0) ? currentMaze[centralNode.NodeIndex - 1] : null;

            outputLabel1.Text = $"U: {(upperNode != null ? upperNode.NodeIndex.ToString() : "null")}, R: {(rightNode != null ? rightNode.NodeIndex.ToString() : "null")}, D: {(bottomNode != null ? bottomNode.NodeIndex.ToString() : "null")}, L: {(leftNode != null ? leftNode.NodeIndex.ToString() : "null")}";

            if (upperNode != null) adjacentNodes[0] = upperNode;
            if (rightNode != null) adjacentNodes[1] = rightNode;
            if (bottomNode != null) adjacentNodes[2] = bottomNode;
            if (leftNode != null) adjacentNodes[3] = leftNode;

            return adjacentNodes;
        }

        
        private void mazeBox_paint(object sender, PaintEventArgs e)
        {
            nodalPictureBox currentBox = (nodalPictureBox)sender;
            Node node = currentBox.MazeNode;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Shape shape = new Shape(node, currentMazeDimensions[2], Color.Black, 5);
            shape.DrawShape(e.Graphics);
            
        }


        private void generateNewMaze_Click(object sender, EventArgs e)
        {
            mazePanel.Controls.Clear();
            if (newMaze_width.Text == "" || newMaze_height.Text == "")
            {
                MessageBox.Show("Please enter valid dimensions for the maze.");
                return;
            } else if (int.Parse(newMaze_width.Text) < 2 || int.Parse(newMaze_height.Text) < 2)
            {
                MessageBox.Show("Please enter dimensions greater than or equal to 2.");
                return;
            } else if (int.Parse(newMaze_width.Text) > 7 || int.Parse(newMaze_height.Text) > 7)
            {
                MessageBox.Show("Please enter dimensions less than or equal to 7.");
                return;
            }

            createEmptyMaze(int.Parse(newMaze_width.Text), int.Parse(newMaze_height.Text), 50);
            drawCurrentMaze();
        }

        private void prims_Button_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int node = rnd.Next(0, currentMazeDimensions[3]);
            primsAlgorithm(node);
        }
    }
}
