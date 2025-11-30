namespace Micromouse_demo1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.outputLabel1 = new System.Windows.Forms.Label();
            this.mazePanel = new System.Windows.Forms.Panel();
            this.newMazeHeight_Label = new System.Windows.Forms.Label();
            this.newMazeWidth_Label = new System.Windows.Forms.Label();
            this.newMaze_width = new System.Windows.Forms.TextBox();
            this.newMaze_height = new System.Windows.Forms.TextBox();
            this.generateNewMaze = new System.Windows.Forms.Button();
            this.prims_Button = new System.Windows.Forms.Button();
            this.primaryNodeLabel = new System.Windows.Forms.Label();
            this.frontierNodeLabel = new System.Windows.Forms.Label();
            this.outputLabel2 = new System.Windows.Forms.Label();
            this.outputLabel3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // outputLabel1
            // 
            this.outputLabel1.AutoSize = true;
            this.outputLabel1.Location = new System.Drawing.Point(0, 0);
            this.outputLabel1.Name = "outputLabel1";
            this.outputLabel1.Size = new System.Drawing.Size(69, 13);
            this.outputLabel1.TabIndex = 0;
            this.outputLabel1.Text = "outputLabel1";
            // 
            // mazePanel
            // 
            this.mazePanel.Location = new System.Drawing.Point(3, 41);
            this.mazePanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mazePanel.Name = "mazePanel";
            this.mazePanel.Size = new System.Drawing.Size(400, 400);
            this.mazePanel.TabIndex = 6;
            // 
            // newMazeHeight_Label
            // 
            this.newMazeHeight_Label.AutoSize = true;
            this.newMazeHeight_Label.Location = new System.Drawing.Point(13, 545);
            this.newMazeHeight_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.newMazeHeight_Label.Name = "newMazeHeight_Label";
            this.newMazeHeight_Label.Size = new System.Drawing.Size(41, 13);
            this.newMazeHeight_Label.TabIndex = 11;
            this.newMazeHeight_Label.Text = "Height:";
            // 
            // newMazeWidth_Label
            // 
            this.newMazeWidth_Label.AutoSize = true;
            this.newMazeWidth_Label.Location = new System.Drawing.Point(17, 522);
            this.newMazeWidth_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.newMazeWidth_Label.Name = "newMazeWidth_Label";
            this.newMazeWidth_Label.Size = new System.Drawing.Size(38, 13);
            this.newMazeWidth_Label.TabIndex = 10;
            this.newMazeWidth_Label.Text = "Width:";
            // 
            // newMaze_width
            // 
            this.newMaze_width.Location = new System.Drawing.Point(63, 520);
            this.newMaze_width.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.newMaze_width.Name = "newMaze_width";
            this.newMaze_width.Size = new System.Drawing.Size(110, 20);
            this.newMaze_width.TabIndex = 9;
            // 
            // newMaze_height
            // 
            this.newMaze_height.Location = new System.Drawing.Point(63, 541);
            this.newMaze_height.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.newMaze_height.Name = "newMaze_height";
            this.newMaze_height.Size = new System.Drawing.Size(110, 20);
            this.newMaze_height.TabIndex = 8;
            // 
            // generateNewMaze
            // 
            this.generateNewMaze.Location = new System.Drawing.Point(63, 562);
            this.generateNewMaze.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.generateNewMaze.Name = "generateNewMaze";
            this.generateNewMaze.Size = new System.Drawing.Size(109, 34);
            this.generateNewMaze.TabIndex = 7;
            this.generateNewMaze.Text = "Generate New Maze";
            this.generateNewMaze.UseVisualStyleBackColor = true;
            this.generateNewMaze.Click += new System.EventHandler(this.generateNewMaze_Click);
            // 
            // prims_Button
            // 
            this.prims_Button.Location = new System.Drawing.Point(177, 562);
            this.prims_Button.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.prims_Button.Name = "prims_Button";
            this.prims_Button.Size = new System.Drawing.Size(109, 34);
            this.prims_Button.TabIndex = 12;
            this.prims_Button.Text = "Run Prim\'s Algorithm";
            this.prims_Button.UseVisualStyleBackColor = true;
            this.prims_Button.Click += new System.EventHandler(this.prims_Button_Click);
            // 
            // primaryNodeLabel
            // 
            this.primaryNodeLabel.AutoSize = true;
            this.primaryNodeLabel.Location = new System.Drawing.Point(13, 492);
            this.primaryNodeLabel.Name = "primaryNodeLabel";
            this.primaryNodeLabel.Size = new System.Drawing.Size(78, 13);
            this.primaryNodeLabel.TabIndex = 13;
            this.primaryNodeLabel.Text = "Primary Nodes:";
            // 
            // frontierNodeLabel
            // 
            this.frontierNodeLabel.AutoSize = true;
            this.frontierNodeLabel.Location = new System.Drawing.Point(12, 505);
            this.frontierNodeLabel.Name = "frontierNodeLabel";
            this.frontierNodeLabel.Size = new System.Drawing.Size(79, 13);
            this.frontierNodeLabel.TabIndex = 14;
            this.frontierNodeLabel.Text = "Frontier Nodes:";
            // 
            // outputLabel2
            // 
            this.outputLabel2.AutoSize = true;
            this.outputLabel2.Location = new System.Drawing.Point(1, 14);
            this.outputLabel2.Name = "outputLabel2";
            this.outputLabel2.Size = new System.Drawing.Size(69, 13);
            this.outputLabel2.TabIndex = 15;
            this.outputLabel2.Text = "outputLabel2";
            // 
            // outputLabel3
            // 
            this.outputLabel3.AutoSize = true;
            this.outputLabel3.Location = new System.Drawing.Point(1, 27);
            this.outputLabel3.Name = "outputLabel3";
            this.outputLabel3.Size = new System.Drawing.Size(69, 13);
            this.outputLabel3.TabIndex = 16;
            this.outputLabel3.Text = "outputLabel3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 614);
            this.Controls.Add(this.outputLabel3);
            this.Controls.Add(this.outputLabel2);
            this.Controls.Add(this.frontierNodeLabel);
            this.Controls.Add(this.primaryNodeLabel);
            this.Controls.Add(this.prims_Button);
            this.Controls.Add(this.newMazeHeight_Label);
            this.Controls.Add(this.newMazeWidth_Label);
            this.Controls.Add(this.newMaze_width);
            this.Controls.Add(this.newMaze_height);
            this.Controls.Add(this.generateNewMaze);
            this.Controls.Add(this.mazePanel);
            this.Controls.Add(this.outputLabel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label outputLabel1;
        private System.Windows.Forms.Panel mazePanel;
        private System.Windows.Forms.Label newMazeHeight_Label;
        private System.Windows.Forms.Label newMazeWidth_Label;
        private System.Windows.Forms.TextBox newMaze_width;
        private System.Windows.Forms.TextBox newMaze_height;
        private System.Windows.Forms.Button generateNewMaze;
        private System.Windows.Forms.Button prims_Button;
        private System.Windows.Forms.Label primaryNodeLabel;
        private System.Windows.Forms.Label frontierNodeLabel;
        private System.Windows.Forms.Label outputLabel2;
        private System.Windows.Forms.Label outputLabel3;
    }
}

