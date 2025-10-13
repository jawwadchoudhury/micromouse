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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.randomMaze = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // outputLabel1
            // 
            this.outputLabel1.AutoSize = true;
            this.outputLabel1.Location = new System.Drawing.Point(0, 0);
            this.outputLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.outputLabel1.Name = "outputLabel1";
            this.outputLabel1.Size = new System.Drawing.Size(51, 20);
            this.outputLabel1.TabIndex = 0;
            this.outputLabel1.Text = "label1";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(734, 803);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Width:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(728, 838);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Height:";
            // 
            // randomMaze
            // 
            this.randomMaze.Location = new System.Drawing.Point(109, 0);
            this.randomMaze.Name = "randomMaze";
            this.randomMaze.Size = new System.Drawing.Size(782, 782);
            this.randomMaze.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 945);
            this.Controls.Add(this.randomMaze);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel randomMaze;
    }
}

