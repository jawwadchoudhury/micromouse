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
using static Micromouse_demo1.Form1;
using System.Reflection;

namespace Micromouse_demo1
{
    public partial class Form1 : Form
    {
        public bool primsAlgorithmStarted = false;
        public List<MazeNode> currentMaze = new List<MazeNode>();
        public Dictionary<int, List<int>> adjacencyMatrix = new Dictionary<int, List<int>>();
        public int[] currentMazeDimensions = new int[4]; // 0 = width, 1 = height, 2 = box size, 3 = node count
        public int nodeCount = 0;
        List<int> frontierNodes = new List<int>();
        List<int> primaryNodes = new List<int>();

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

            public void removeTopValue()
            {
                DecimalValue &= ~1;
            }

            public void removeRightValue()
            {
                DecimalValue &= ~2;
            }

            public void removeBottomValue()
            {
                DecimalValue &= ~4;
            }

            public void removeLeftValue()
            {
                DecimalValue &= ~8;
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

        private void clearVariables()
        {
            primsAlgorithmStarted = false;
            currentMaze.Clear();
            adjacencyMatrix.Clear();
            currentMazeDimensions = new int[4];
            nodeCount = 0;
            frontierNodes.Clear();
            primaryNodes.Clear();
            primaryNodeLabel.Text = "Primary Nodes: ";
            frontierNodeLabel.Text = "Frontier Nodes: ";
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
            createAdjacencyMatrix(currentMaze);
        }

        private void createAdjacencyMatrix(List<MazeNode> maze)
        {
            for (int i = 0; i < maze.Count; i++)
            {
                List<int> adjacentNodes = new List<int>();
                MazeNode centralNode = maze[i];
                MazeNode upperNode;
                MazeNode rightNode;
                MazeNode bottomNode;
                MazeNode leftNode;
                upperNode = (centralNode.NodeIndex - currentMazeDimensions[0] >= 0) ? currentMaze[centralNode.NodeIndex - currentMazeDimensions[0]] : null;
                rightNode = (centralNode.WidthCount + 1 <= currentMazeDimensions[0] - 1) ? currentMaze[centralNode.NodeIndex + 1] : null;
                bottomNode = (centralNode.NodeIndex + currentMazeDimensions[0] < currentMazeDimensions[3]) ? currentMaze[centralNode.NodeIndex + currentMazeDimensions[0]] : null;
                leftNode = (centralNode.WidthCount - 1 >= 0) ? currentMaze[centralNode.NodeIndex - 1] : null;
                if (upperNode != null) adjacentNodes.Add(upperNode.NodeIndex);
                if (rightNode != null) adjacentNodes.Add(rightNode.NodeIndex);
                if (bottomNode != null) adjacentNodes.Add(bottomNode.NodeIndex);
                if (leftNode != null) adjacentNodes.Add(leftNode.NodeIndex);
                adjacencyMatrix.Add(centralNode.NodeIndex, adjacentNodes);
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
            refreshLabels();
            refreshColours();
        }

        private void refreshLabels()
        {
            primaryNodeLabel.Text = "Primary Nodes: " + string.Join(", ", primaryNodes);
            frontierNodeLabel.Text = "Frontier Nodes: " + string.Join(", ", frontierNodes);
        }
        private void refreshColours()
        {
            foreach (int index in frontierNodes)
            {
                nodalPictureBox adjacentBox = (nodalPictureBox)mazePanel.Controls[index];
                adjacentBox.BackColor = Color.FromArgb(155, 255, 155);
            }
            foreach (int index in primaryNodes)
            {
                nodalPictureBox primaryBox = (nodalPictureBox)mazePanel.Controls[index];
                primaryBox.BackColor = Color.FromArgb(255, 255, 155);
            }
        }

        private async void primsAlgorithm(int startingNode)
        {
            primaryNodes.Add(startingNode);
            refreshLabels();

            Random random = new Random();

            returnAdjacentNodes(currentMaze, startingNode);

            while (frontierNodes.Count > 0)
            {
                await Task.Delay(2);
                int primaryIndex = random.Next(0, primaryNodes.Count);
                int frontierIndex = random.Next(0, frontierNodes.Count);
                if (adjacencyMatrix[primaryNodes[primaryIndex]].Contains(frontierNodes[frontierIndex]))
                {
                    createTunnel(primaryNodes[primaryIndex], frontierNodes[frontierIndex]);
                }
                refreshColours();
            }
            switch (random.Next(0, 4))
            {
                case 0:
                    currentMaze[random.Next(0, currentMazeDimensions[0])].removeTopValue();
                    break;
                case 1:
                    List<int> rightEdges = new List<int>();
                    foreach (MazeNode node in currentMaze)
                    {
                        if (node.WidthCount == currentMazeDimensions[0] - 1) rightEdges.Add(node.NodeIndex);
                    }
                    currentMaze[rightEdges[random.Next(0, rightEdges.Count)]].removeRightValue();
                    break;
                case 2:
                    currentMaze[random.Next(currentMazeDimensions[3] - currentMazeDimensions[0], currentMazeDimensions[3])].removeBottomValue();
                    break;
                case 3:
                    List<int> leftEdges = new List<int>();
                    foreach (MazeNode node in currentMaze)
                    {
                        if (node.WidthCount == 0) leftEdges.Add(node.NodeIndex);
                    }
                    currentMaze[leftEdges[random.Next(0, leftEdges.Count)]].removeLeftValue();
                    break;
            }

            drawCurrentMaze();

            foreach (nodalPictureBox nodalPictureBox in mazePanel.Controls)
            {
                nodalPictureBox.BackColor = DefaultBackColor;
            }

            
        }

        private void createTunnel(int startIndex, int endIndex)
        {
            returnAdjacentNodes(currentMaze, endIndex);
            frontierNodes.Remove(endIndex);
            primaryNodes.Add(endIndex);
            refreshLabels();
            switch (getDirection(startIndex, endIndex))
            {
                case 0: // Up
                    currentMaze[startIndex].removeTopValue();
                    currentMaze[endIndex].removeBottomValue();
                    break;
                case 1: // Right
                    currentMaze[startIndex].removeRightValue();
                    currentMaze[endIndex].removeLeftValue();
                    break;
                case 2: // Down
                    currentMaze[startIndex].removeBottomValue();
                    currentMaze[endIndex].removeTopValue();
                    break;
                case 3: // Left
                    currentMaze[startIndex].removeLeftValue();
                    currentMaze[endIndex].removeRightValue();
                    break;
                case -1:
                    MessageBox.Show("Error in creating tunnel: Invalid direction.");
                    break;
            }
        }

        private int getDirection(int startIndex, int endIndex)
        {
            MazeNode startNode = currentMaze[startIndex];
            MazeNode endNode = currentMaze[endIndex];

            if (startIndex - currentMazeDimensions[0] == endIndex) return 0; // Up
            if (startIndex + 1 == endIndex) return 1; // Right
            if (startIndex + currentMazeDimensions[0] == endIndex) return 2; // Down
            if (startIndex - 1 == endIndex) return 3; // Left
            return -1;
        }

        private void returnAdjacentNodes(List<MazeNode> maze, int nodeIndex)
        {
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

            if (upperNode != null && !primaryNodes.Contains(upperNode.NodeIndex) && !frontierNodes.Contains(upperNode.NodeIndex)) frontierNodes.Add(upperNode.NodeIndex);
            if (rightNode != null && !primaryNodes.Contains(rightNode.NodeIndex) && !frontierNodes.Contains(rightNode.NodeIndex)) frontierNodes.Add(rightNode.NodeIndex);
            if (bottomNode != null && !primaryNodes.Contains(bottomNode.NodeIndex) && !frontierNodes.Contains(bottomNode.NodeIndex)) frontierNodes.Add(bottomNode.NodeIndex);
            if (leftNode != null && !primaryNodes.Contains(leftNode.NodeIndex) && !frontierNodes.Contains(leftNode.NodeIndex)) frontierNodes.Add(leftNode.NodeIndex);
            refreshLabels();
        }
        
        private void mazeBox_paint(object sender, PaintEventArgs e)
        {
            nodalPictureBox currentBox = (nodalPictureBox)sender;
            Node node = currentBox.MazeNode;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Shape shape = new Shape(node, currentMazeDimensions[2], Color.Black, 40 / Math.Max(currentMazeDimensions[0], currentMazeDimensions[1]));
            shape.DrawShape(e.Graphics);
            
        }

        private void generateNewMaze_Click(object sender, EventArgs e)
        {
            mazePanel.Controls.Clear();
            clearVariables();
            if (newMaze_width.Text == "" || newMaze_height.Text == "")
            {
                MessageBox.Show("Please enter valid dimensions for the maze.");
                return;
            } else if (int.Parse(newMaze_width.Text) < 3 || int.Parse(newMaze_height.Text) < 3)
            {
                MessageBox.Show("Please enter dimensions greater than or equal to 3.");
                return;
            } else if (int.Parse(newMaze_width.Text) > 20 || int.Parse(newMaze_height.Text) > 20)
            {
                MessageBox.Show("Please enter dimensions less than or equal to 20.");
                return;
            }

            int width = int.Parse(newMaze_width.Text);
            int height = int.Parse(newMaze_height.Text);
            createEmptyMaze(width, height, 400 / Math.Max(width, height) );
            drawCurrentMaze();
        }

        private void prims_Button_Click(object sender, EventArgs e)
        {
            if (!primsAlgorithmStarted)
            {
                primsAlgorithmStarted = true;
                Random rnd = new Random();
                int node = rnd.Next(0, currentMazeDimensions[3]);
                primsAlgorithm(node);
            } else
            {
                MessageBox.Show("Prims Algorithm has already been started.");
                return;
            }
        }

    }
}
