using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micromouse_demo1
{
    public partial class Form1 : Form
    {
        public bool primsAlgorithmStarted = false;
        public bool graphGenerationStarted = false;
        public List<MazeNode> currentMaze = new List<MazeNode>();
        public Dictionary<int, List<int>> adjacencyMatrix = new Dictionary<int, List<int>>();
        public WeightedAdjacencyMatrix weightedAdjacencyMatrix;
        public int[] currentMazeData = new int[6]; // 0 = width, 1 = height, 2 = box size, 3 = node count, 4 = exit node index, 5 = start node index
        public Random random = new Random();
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
        public class WeightedAdjacencyMatrix
        {
            // This is stupid so i have a better idea! Weighted adjacency matrix. Get a node, go straight in each direction till you hit a wall, add that weight into the adjacency matrix,
            // do this for all 4 directions, then iterate over each one of them, kind of like the frontier thing again
            // we'll see how that goes yeah
            private int StartingIndex;
            private Dictionary<int, List<int[]>> adjacencyDictionary;
            public WeightedAdjacencyMatrix(int startingIndex)
            {
                StartingIndex = startingIndex;
            }

            //public void addValue(int nodeIndex, int adjacentNodeIndex, int weight)
            //{
            //    int[] valuePair = new int[2] {adjacentNodeIndex, weight};
            //    if (adjacencyDictionary.TryGetValue(nodeIndex, out var weightedValues)) {
            //        if (!weightedValues.Contains(valuePair))
            //        {
            //            weightedValues.Add(valuePair);
            //            adjacencyDictionary.Add(nodeIndex, weightedValues);
            //        }
            //    } else
            //    {
            //        adjacencyDictionary.Add(nodeIndex, new List<int[]> { new int[] { adjacentNodeIndex, weight } });
            //    }
            //}
            public string test()
            {
                if (adjacencyDictionary.TryGetValue(0, out var list))
                {
                    return list.ToString();
                } else
                {
                    return "error...";
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void clearAll()
        {
            mazePanel.Controls.Clear();
            primsAlgorithmStarted = false;
            currentMaze.Clear();
            adjacencyMatrix.Clear();
            currentMazeData = new int[6];
            frontierNodes.Clear();
            primaryNodes.Clear();
            primaryNodeLabel.Text = "Primary Nodes: ";
            frontierNodeLabel.Text = "Frontier Nodes: ";
        }
        private void createEmptyMaze(int width, int height,  int boxSize)
        {
            currentMaze.Clear();
            currentMazeData[0] = width;
            currentMazeData[1] = height;
            currentMazeData[2] = boxSize;
            currentMazeData[3] = width * height;
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
                upperNode = (centralNode.NodeIndex - currentMazeData[0] >= 0) ? currentMaze[centralNode.NodeIndex - currentMazeData[0]] : null;
                rightNode = (centralNode.WidthCount + 1 <= currentMazeData[0] - 1) ? currentMaze[centralNode.NodeIndex + 1] : null;
                bottomNode = (centralNode.NodeIndex + currentMazeData[0] < currentMazeData[3]) ? currentMaze[centralNode.NodeIndex + currentMazeData[0]] : null;
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
            int width = currentMazeData[0];
            int height = currentMazeData[1];
            int boxSize = currentMazeData[2];
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
                    pictureBox.Paint += new PaintEventHandler(mazeBox_Paint);
                    pictureBox.MouseClick += new MouseEventHandler(mazeBox_MouseClick);
                }
                x = sx;
                y = y + boxSize;
            }
            foreach (nodalPictureBox nodalPictureBox in mazePanel.Controls)
            {
                nodalPictureBox.BackColor = DefaultBackColor;
            }
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
        private List<int> Shuffle(List<int> list) // Fisher-Yates Shuffle
        {
            List<int> shuffledList = new List<int>();
            int count = list.Count;
            while (count > 0)
            {
                count--;
                int randomIndex = random.Next(0, count);
                shuffledList.Add(list[randomIndex]);
                list.Remove(list[randomIndex]);
            }
            return shuffledList;
        }
        private async void primsAlgorithm(int startingNode)
        {
            primaryNodes.Add(startingNode); // Turn the starting node into a primary node
            refreshLabels();

            returnFrontierNodes(currentMaze, startingNode); // Get frontier nodes of the starting primary node

            // Iterate tunnel function
            while (frontierNodes.Count > 0)
            {
                await Task.Delay(20);
                List<int> frontierAdjacentNodes;
                int frontierNodeIndex = frontierNodes[random.Next(0, frontierNodes.Count)];
                adjacencyMatrix.TryGetValue(frontierNodeIndex, out frontierAdjacentNodes);
                foreach (int index in Shuffle(frontierAdjacentNodes))
                {
                    if (primaryNodes.Contains(index))
                    {
                        createTunnel(index, frontierNodeIndex);
                        break;
                    }
                }
                refreshColours();
            }

            //Block to create an exit (on one of the edge nodes)
            switch (random.Next(0, 4))
            {
                case 0:
                    currentMazeData[4] = random.Next(0, currentMazeData[0]);
                    currentMaze[currentMazeData[4]].removeTopValue();
                    
                    break;
                case 1:
                    List<int> rightEdges = new List<int>();
                    foreach (MazeNode node in currentMaze)
                    {
                        if (node.WidthCount == currentMazeData[0] - 1) rightEdges.Add(node.NodeIndex);
                    }
                    currentMazeData[4] = rightEdges[random.Next(0, rightEdges.Count)];
                    currentMaze[currentMazeData[4]].removeRightValue();
                    break;
                case 2:
                    currentMazeData[4] = random.Next(currentMazeData[3] - currentMazeData[0], currentMazeData[3]);
                    currentMaze[currentMazeData[4]].removeBottomValue();
                    break;
                case 3:
                    List<int> leftEdges = new List<int>();
                    foreach (MazeNode node in currentMaze)
                    {
                        if (node.WidthCount == 0) leftEdges.Add(node.NodeIndex);
                    }
                    currentMazeData[4] = leftEdges[random.Next(0, leftEdges.Count)];
                    currentMaze[currentMazeData[4]].removeLeftValue();
                    break;
            }

            drawCurrentMaze(); // Redraw the maze (without the weird buggy lines)
        }

        private async void graphGenerationAlgorithm(int startingNode)
        {
            graphGenerationStarted = true;
            weightedAdjacencyMatrix = new WeightedAdjacencyMatrix(startingNode);
            weightedAdjacencyMatrix.test();
        }

        private void createTunnel(int startIndex, int endIndex)
        {
            returnFrontierNodes(currentMaze, endIndex);
            frontierNodes.Remove(endIndex);
            primaryNodes.Add(endIndex);
            refreshLabels();
            switch (getDirection(startIndex, endIndex))
            {
                case 0:
                    currentMaze[startIndex].removeTopValue();
                    currentMaze[endIndex].removeBottomValue();
                    break;
                case 1:
                    currentMaze[startIndex].removeRightValue();
                    currentMaze[endIndex].removeLeftValue();
                    break;
                case 2:
                    currentMaze[startIndex].removeBottomValue();
                    currentMaze[endIndex].removeTopValue();
                    break;
                case 3:
                    currentMaze[startIndex].removeLeftValue();
                    currentMaze[endIndex].removeRightValue();
                    break;
                case -1:
                    MessageBox.Show("Invalid direction");
                    break;
            }
        }

        private int getDirection(int startIndex, int endIndex)
        {
            MazeNode startNode = currentMaze[startIndex];
            MazeNode endNode = currentMaze[endIndex];

            if (startIndex - currentMazeData[0] == endIndex) return 0; // Up
            if (startIndex + 1 == endIndex) return 1; // Right
            if (startIndex + currentMazeData[0] == endIndex) return 2; // Down
            if (startIndex - 1 == endIndex) return 3; // Left
            return -1;
        }

        private void returnFrontierNodes(List<MazeNode> maze, int nodeIndex)
        {
            MazeNode centralNode = maze[nodeIndex];

            MazeNode upperNode;
            MazeNode rightNode;
            MazeNode bottomNode;
            MazeNode leftNode;

            upperNode = (centralNode.NodeIndex - currentMazeData[0] >= 0) ? currentMaze[centralNode.NodeIndex - currentMazeData[0]] : null;
            rightNode = (centralNode.WidthCount + 1 <= currentMazeData[0] - 1) ? currentMaze[centralNode.NodeIndex + 1] : null;
            bottomNode = (centralNode.NodeIndex + currentMazeData[0] < currentMazeData[3]) ? currentMaze[centralNode.NodeIndex + currentMazeData[0]] : null;
            leftNode = (centralNode.WidthCount - 1 >= 0) ? currentMaze[centralNode.NodeIndex - 1] : null;

            outputLabel1.Text = $"U: {(upperNode != null ? upperNode.NodeIndex.ToString() : "null")}, R: {(rightNode != null ? rightNode.NodeIndex.ToString() : "null")}, D: {(bottomNode != null ? bottomNode.NodeIndex.ToString() : "null")}, L: {(leftNode != null ? leftNode.NodeIndex.ToString() : "null")}";

            if (upperNode != null && !primaryNodes.Contains(upperNode.NodeIndex) && !frontierNodes.Contains(upperNode.NodeIndex)) frontierNodes.Add(upperNode.NodeIndex);
            if (rightNode != null && !primaryNodes.Contains(rightNode.NodeIndex) && !frontierNodes.Contains(rightNode.NodeIndex)) frontierNodes.Add(rightNode.NodeIndex);
            if (bottomNode != null && !primaryNodes.Contains(bottomNode.NodeIndex) && !frontierNodes.Contains(bottomNode.NodeIndex)) frontierNodes.Add(bottomNode.NodeIndex);
            if (leftNode != null && !primaryNodes.Contains(leftNode.NodeIndex) && !frontierNodes.Contains(leftNode.NodeIndex)) frontierNodes.Add(leftNode.NodeIndex);
            refreshLabels();
        }

        private int[] returnAdjacentIndexes(int nodeIndex)
        {
            int[] adjacentIndexes = new int[4];
            MazeNode centralNode = currentMaze[nodeIndex];

            int upperIndex;
            int rightIndex;
            int bottomIndex;
            int leftIndex;

            upperIndex = (centralNode.NodeIndex - currentMazeData[0] >= 0) ? centralNode.NodeIndex - currentMazeData[0] : -1;
            rightIndex = (centralNode.WidthCount + 1 <= currentMazeData[0] - 1) ? centralNode.NodeIndex + 1 : -1;
            bottomIndex = (centralNode.NodeIndex + currentMazeData[0] < currentMazeData[3]) ? centralNode.NodeIndex + currentMazeData[0] : -1;
            leftIndex = (centralNode.WidthCount - 1 >= 0) ? centralNode.NodeIndex - 1 : -1;

            if (upperIndex != -1) adjacentIndexes[0] = upperIndex;
            if (rightIndex != -1) adjacentIndexes[1] = rightIndex;
            if (bottomIndex != -1) adjacentIndexes[2] = bottomIndex;
            if (leftIndex != -1) adjacentIndexes[3] = leftIndex;
            return adjacentIndexes;
        }

        private void mazeBox_Paint(object sender, PaintEventArgs e)
        {
            nodalPictureBox currentBox = (nodalPictureBox)sender;
            Node node = currentBox.MazeNode;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Shape shape = new Shape(node, currentMazeData[2], Color.Black, 40 / Math.Max(currentMazeData[0], currentMazeData[1]));
            shape.DrawShape(e.Graphics);
        }

        private void mazeBox_MouseClick(object sender, MouseEventArgs e)
        {
            nodalPictureBox currentBox = (nodalPictureBox)sender;
            MessageBox.Show(currentBox.MazeNode.NodeIndex.ToString());
            graphGenerationStarted = true;
            graphGenerationAlgorithm(currentBox.MazeNode.NodeIndex);
        }

        private void generateNewMaze_Click(object sender, EventArgs e)
        {
            clearAll();
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
            createEmptyMaze(width, height, 390 / Math.Max(width, height) );
            drawCurrentMaze();
        }

        private void prims_Button_Click(object sender, EventArgs e)
        {
            if (!primsAlgorithmStarted)
            {
                primsAlgorithmStarted = true;
                int node = random.Next(0, currentMazeData[3]);
                primsAlgorithm(node);
            } else
            {
                MessageBox.Show("Prims Algorithm has already been started.");
                return;
            }
        }

    }
}