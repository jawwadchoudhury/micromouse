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
            this.generateRandomMaze_Button = new System.Windows.Forms.Button();
            this.height_textBox = new System.Windows.Forms.TextBox();
            this.width_textBox = new System.Windows.Forms.TextBox();
            this.randomMazeWidth_Label = new System.Windows.Forms.Label();
            this.randomMazeHeight_Label = new System.Windows.Forms.Label();
            this.randomMaze = new System.Windows.Forms.Panel();
            this.newMazeHeight_Label = new System.Windows.Forms.Label();
            this.newMazeWidth_Label = new System.Windows.Forms.Label();
            this.newMaze_width = new System.Windows.Forms.TextBox();
            this.newMaze_height = new System.Windows.Forms.TextBox();
            this.generateNewMaze = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // outputLabel1
            // 
            this.outputLabel1.AutoSize = true;
            this.outputLabel1.Location = new System.Drawing.Point(0, 0);
            this.outputLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.outputLabel1.Name = "outputLabel1";
            this.outputLabel1.Size = new System.Drawing.Size(103, 20);
            this.outputLabel1.TabIndex = 0;
            this.outputLabel1.Text = "outputLabel1";
            // 
            // generateRandomMaze_Button
            // 
            this.generateRandomMaze_Button.Location = new System.Drawing.Point(803, 864);
            this.generateRandomMaze_Button.Name = "generateRandomMaze_Button";
            this.generateRandomMaze_Button.Size = new System.Drawing.Size(163, 53);
            this.generateRandomMaze_Button.TabIndex = 1;
            this.generateRandomMaze_Button.Text = "Generate Maze";
            this.generateRandomMaze_Button.UseVisualStyleBackColor = true;
            this.generateRandomMaze_Button.Click += new System.EventHandler(this.generateRandomMaze_Button_Click);
            // 
            // height_textBox
            // 
            this.height_textBox.Location = new System.Drawing.Point(803, 832);
            this.height_textBox.Name = "height_textBox";
            this.height_textBox.Size = new System.Drawing.Size(163, 26);
            this.height_textBox.TabIndex = 2;
            // 
            // width_textBox
            // 
            this.width_textBox.Location = new System.Drawing.Point(803, 800);
            this.width_textBox.Name = "width_textBox";
            this.width_textBox.Size = new System.Drawing.Size(163, 26);
            this.width_textBox.TabIndex = 3;
            // 
            // randomMazeWidth_Label
            // 
            this.randomMazeWidth_Label.AutoSize = true;
            this.randomMazeWidth_Label.Location = new System.Drawing.Point(734, 803);
            this.randomMazeWidth_Label.Name = "randomMazeWidth_Label";
            this.randomMazeWidth_Label.Size = new System.Drawing.Size(54, 20);
            this.randomMazeWidth_Label.TabIndex = 4;
            this.randomMazeWidth_Label.Text = "Width:";
            // 
            // randomMazeHeight_Label
            // 
            this.randomMazeHeight_Label.AutoSize = true;
            this.randomMazeHeight_Label.Location = new System.Drawing.Point(728, 838);
            this.randomMazeHeight_Label.Name = "randomMazeHeight_Label";
            this.randomMazeHeight_Label.Size = new System.Drawing.Size(60, 20);
            this.randomMazeHeight_Label.TabIndex = 5;
            this.randomMazeHeight_Label.Text = "Height:";
            // 
            // randomMaze
            // 
            this.randomMaze.Location = new System.Drawing.Point(130, 40);
            this.randomMaze.Name = "randomMaze";
            this.randomMaze.Size = new System.Drawing.Size(750, 750);
            this.randomMaze.TabIndex = 6;
            // 
            // newMazeHeight_Label
            // 
            this.newMazeHeight_Label.AutoSize = true;
            this.newMazeHeight_Label.Location = new System.Drawing.Point(19, 838);
            this.newMazeHeight_Label.Name = "newMazeHeight_Label";
            this.newMazeHeight_Label.Size = new System.Drawing.Size(60, 20);
            this.newMazeHeight_Label.TabIndex = 11;
            this.newMazeHeight_Label.Text = "Height:";
            // 
            // newMazeWidth_Label
            // 
            this.newMazeWidth_Label.AutoSize = true;
            this.newMazeWidth_Label.Location = new System.Drawing.Point(25, 803);
            this.newMazeWidth_Label.Name = "newMazeWidth_Label";
            this.newMazeWidth_Label.Size = new System.Drawing.Size(54, 20);
            this.newMazeWidth_Label.TabIndex = 10;
            this.newMazeWidth_Label.Text = "Width:";
            // 
            // newMaze_width
            // 
            this.newMaze_width.Location = new System.Drawing.Point(94, 800);
            this.newMaze_width.Name = "newMaze_width";
            this.newMaze_width.Size = new System.Drawing.Size(163, 26);
            this.newMaze_width.TabIndex = 9;
            // 
            // newMaze_height
            // 
            this.newMaze_height.Location = new System.Drawing.Point(94, 832);
            this.newMaze_height.Name = "newMaze_height";
            this.newMaze_height.Size = new System.Drawing.Size(163, 26);
            this.newMaze_height.TabIndex = 8;
            // 
            // generateNewMaze
            // 
            this.generateNewMaze.Location = new System.Drawing.Point(94, 864);
            this.generateNewMaze.Name = "generateNewMaze";
            this.generateNewMaze.Size = new System.Drawing.Size(163, 53);
            this.generateNewMaze.TabIndex = 7;
            this.generateNewMaze.Text = "Generate New Maze";
            this.generateNewMaze.UseVisualStyleBackColor = true;
            this.generateNewMaze.Click += new System.EventHandler(this.generateNewMaze_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 945);
            this.Controls.Add(this.newMazeHeight_Label);
            this.Controls.Add(this.newMazeWidth_Label);
            this.Controls.Add(this.newMaze_width);
            this.Controls.Add(this.newMaze_height);
            this.Controls.Add(this.generateNewMaze);
            this.Controls.Add(this.randomMaze);
            this.Controls.Add(this.randomMazeHeight_Label);
            this.Controls.Add(this.randomMazeWidth_Label);
            this.Controls.Add(this.width_textBox);
            this.Controls.Add(this.height_textBox);
            this.Controls.Add(this.generateRandomMaze_Button);
            this.Controls.Add(this.outputLabel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label outputLabel1;
        private System.Windows.Forms.Button generateRandomMaze_Button;
        private System.Windows.Forms.TextBox height_textBox;
        private System.Windows.Forms.TextBox width_textBox;
        private System.Windows.Forms.Label randomMazeWidth_Label;
        private System.Windows.Forms.Label randomMazeHeight_Label;
        private System.Windows.Forms.Panel randomMaze;
        private System.Windows.Forms.Label newMazeHeight_Label;
        private System.Windows.Forms.Label newMazeWidth_Label;
        private System.Windows.Forms.TextBox newMaze_width;
        private System.Windows.Forms.TextBox newMaze_height;
        private System.Windows.Forms.Button generateNewMaze;
    }
}

