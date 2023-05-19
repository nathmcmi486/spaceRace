namespace spaceRace
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.player1ScoreLabel = new System.Windows.Forms.Label();
            this.player2ScoreLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer();

            this.timer.Interval = 50;
            this.timer.Tick += new System.EventHandler(this.mainLoop);
            this.timer.Enabled = true;
            this.timer.Start();

            this.player1ScoreLabel.Name = "player1Score";
            this.player1ScoreLabel.Size = new System.Drawing.Size(40, 80);
            this.player1ScoreLabel.Font = new Font("Arial", 25);
            this.player1ScoreLabel.TabIndex = 0;
            this.player1ScoreLabel.Text = "";
            this.player1ScoreLabel.Location = new System.Drawing.Point(140, 410);
            this.player1ScoreLabel.ForeColor = Color.White;

            this.player2ScoreLabel.Name = "player2Score";
            this.player2ScoreLabel.Size = new System.Drawing.Size(40, 80);
            this.player2ScoreLabel.Font = new Font("Arial", 25);
            this.player2ScoreLabel.TabIndex = 0;
            this.player2ScoreLabel.Text = "";
            this.player2ScoreLabel.Location = new System.Drawing.Point(640, 410);
            this.player2ScoreLabel.ForeColor = Color.White;

            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
            this.Paint += new PaintEventHandler(this.paintHandler);
            this.KeyUp += new KeyEventHandler(this.keyUpHandler);
            this.KeyDown += new KeyEventHandler(this.keyDownHandler);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            this.Controls.Add(this.player1ScoreLabel);
            this.Controls.Add(this.player2ScoreLabel);
        }

        #endregion

        System.Windows.Forms.Label player1ScoreLabel;
        System.Windows.Forms.Label player2ScoreLabel;
        System.Windows.Forms.Timer timer;
    }
}

