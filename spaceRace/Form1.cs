namespace spaceRace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int STARTING_Y_POS = 380;
        const string GAME = "game";
        const string STARTING = "starting";
        const string RESTARTING = "restarting";
        const int PLAYER_SPEED = 5;
        string currentState = STARTING;

        // Asteroids
        List<int> asteroidSpeeds = new List<int>();
        List<Rectangle> asteroids = new List<Rectangle>();

        // Player bodies
        Rectangle player1Body = new Rectangle(140, STARTING_Y_POS, 30, 30);
        Rectangle player2Body = new Rectangle(640, STARTING_Y_POS, 30, 30);

        SolidBrush whiteBrush = new SolidBrush(Color.White);

        // Player1 movements
        bool wKeyDown = false;
        bool sKeyDown = false;

        // Player2 movements
        bool upKeyDown = false;
        bool downKeyDown = false;

        // Other key presses
        bool spaceKeyDown = false;
        bool escapeKeyDown = false;

        int player1Score = 0;
        int player2Score = 0;

        void restartGame()
        {
            asteroids.Clear();
            asteroidSpeeds.Clear();
            player1Body.Y = STARTING_Y_POS;
            player2Body.Y = STARTING_Y_POS;

            if (currentState != RESTARTING)
            {
                player1Score = 0;
                player2Score = 0;
            }
        }

        private void mainLoop(object _o, EventArgs _e)
        {
            if (escapeKeyDown)
            {
                System.Windows.Forms.Application.Exit();
            }

            if (currentState == STARTING || currentState == RESTARTING)
            {
                if (spaceKeyDown)
                {
                    currentState = GAME;
                    restartGame();
                }

                this.Refresh();
                return;
            }

            Random random = new Random();

            if (wKeyDown)
            {
                player1Body.Y -= PLAYER_SPEED;
            }

            if (sKeyDown && player1Body.Y <= STARTING_Y_POS)
            {
                player1Body.Y += PLAYER_SPEED;
            }

            if (upKeyDown)
            {
                player2Body.Y -= PLAYER_SPEED;
            }

            if (downKeyDown && player2Body.Y <= STARTING_Y_POS)
            {
                player2Body.Y += PLAYER_SPEED;
            }

            if (player1Body.Y <= player1Body.Height + 40)
            {
                for (int i = 0; i < 3; i++)
                {
                    System.Console.Beep(500, 75);
                }

                player1Score += 1;
                player1Body.Y = STARTING_Y_POS;
            }

            if (player2Body.Y <= player2Body.Height + 40)
            {
                for (int i = 0; i < 4; i++)
                {
                    System.Console.Beep(500, 75);
                }

                player2Score += 1;
                player2Body.Y = STARTING_Y_POS;
            }

            int chance = random.Next(0, 101);

            if (chance <= 11)
            {
                chance = random.Next(0, 101);

                if (chance <= 50)
                {
                    asteroids.Add(new Rectangle(0, random.Next(70, 310), random.Next(5, 35), 5));
                    asteroidSpeeds.Add(random.Next(3, 6));
                } else
                {
                    asteroids.Add(new Rectangle(this.Width, random.Next(70, 310), random.Next(5, 35), 5));
                    asteroidSpeeds.Add(random.Next(-6, -2));
                }
            }

            for (int i = 0; i < asteroids.Count; i++)
            {
                Rectangle tmpRect = asteroids[i];
                tmpRect.X += asteroidSpeeds[i];

                Rectangle tmpPlayer1Body = player1Body;
                Rectangle tmpPlayer2Body = player2Body;

                // Change the Y position and height so it counts the "triangle"
                tmpPlayer1Body.Height += 40;
                tmpPlayer1Body.Y -= 30;

                tmpPlayer2Body.Height += 40;
                tmpPlayer2Body.Y -= 30;

                if (tmpPlayer1Body.IntersectsWith(asteroids[i]))
                {
                    System.Console.Beep(750, 100);
                    player1Body.Y = STARTING_Y_POS;
                }

                if (tmpPlayer2Body.IntersectsWith(asteroids[i]))
                {
                    System.Console.Beep(750, 100);
                    player2Body.Y = STARTING_Y_POS;
                }

                if (tmpRect.X < 0 || tmpRect.X > 850)
                {
                    asteroids.RemoveAt(i);
                    asteroidSpeeds.RemoveAt(i);

                    continue;
                }

                asteroids[i] = tmpRect;
            }

            if (player1Score == 3 || player2Score == 3)
            {
                currentState = RESTARTING;
                restartGame();
                this.player1ScoreLabel.Text = "";
                this.player2ScoreLabel.Text = "";
            }

            this.Refresh();
        }

        private void paintHandler(object _o, PaintEventArgs e)
        {
            if (currentState == GAME)
            {
                e.Graphics.FillRectangle(whiteBrush, player1Body);
                e.Graphics.FillPie(whiteBrush, player1Body.X - 19, player1Body.Y - 65, 70, 70, 65, 55);

                e.Graphics.FillRectangle(whiteBrush, player2Body);
                e.Graphics.FillPie(whiteBrush, player2Body.X - 19, player2Body.Y - 65, 70, 70, 65, 55);

                for (int i = 0; i <= asteroids.Count - 1; i++)
                {
                    e.Graphics.FillRectangle(whiteBrush, asteroids[i]);
                }

                this.player1ScoreLabel.Text = $"{player1Score}";
                this.player2ScoreLabel.Text = $"{player2Score}";
            }
            else
            {
                Font titleFont = new Font("Arial", 20, FontStyle.Italic);
                Font font = new Font("Arial", 20, FontStyle.Regular);

                e.Graphics.DrawString("Space Race", titleFont, whiteBrush, 50, 100);

                e.Graphics.DrawString("Press Escape key to exit", font, whiteBrush, 50, 150);

                if (currentState == STARTING)
                {
                    e.Graphics.DrawString("Press Space to start", font, whiteBrush, 50, 200);
                } else
                {
                    e.Graphics.DrawString("Press Space to restart", font, whiteBrush, 50, 200);

                    if (player1Score > player2Score)
                    {
                        e.Graphics.DrawString("Player 1 Wins!", font, whiteBrush, 250, 100);
                    } else
                    {
                        e.Graphics.DrawString("Player 2 Wins!", font, whiteBrush, 250, 100);
                    }
                }
            }
        }

        private void keyDownHandler(object _o, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wKeyDown = true;
                    break;
                case Keys.S:
                    sKeyDown = true;
                    break;
                case Keys.Up:
                    upKeyDown = true;
                    break;
                case Keys.Down:
                    downKeyDown = true;
                    break;
                case Keys.Escape:
                    escapeKeyDown = true;
                    break;
                case Keys.Space:
                    spaceKeyDown = true;
                    break;
            }
        }

        private void keyUpHandler(object _o, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wKeyDown = false;
                    break;
                case Keys.S:
                    sKeyDown = false;
                    break;
                case Keys.Up:
                    upKeyDown = false;
                    break;
                case Keys.Down:
                    downKeyDown = false;
                    break;
                case Keys.Escape:
                    escapeKeyDown = false;
                    break;
                case Keys.Space:
                    spaceKeyDown = false;
                    break;
            }
        }
    }
}
