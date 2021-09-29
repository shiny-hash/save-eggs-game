using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace save_eggs_game
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight;

        int speed = 8;
        int Score = 0;
        int Misses = 0;

        Random randX = new Random();
        Random randY = new Random();

        PictureBox slapsh = new PictureBox();

        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score:" + Score;
            txtMiss.Text = "Misses:" + Misses;

            if(goLeft == true && player.Left > 0)
            {
                player.Left -= 12;
            }
            if (goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += 12;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "eggs")
                {
                    x.Top += speed;
                    if (x.Top + x.Height > this.ClientSize.Height)
                    {

                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                        Misses += 1;
                    }
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                        Score += 1;
                    }
                }
            }
            if(Score>10)
            {
                speed = 12;
            }
            if(Misses >5)
            {
                GameTimer.Stop();
                MessageBox.Show("Game Over" + Environment.NewLine + "we've lost good eggs" + Environment.NewLine + "click ok to restart");
                RestartGame();
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

        }

        private void RestartGame()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "eggs")
                {
                    x.Top = randY.Next(80, 300) * -1;
                    x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                }
            }

            player.Left = this.ClientSize.Width / 2;

            Score = 0;
            Misses = 0;
            speed = 8;

            goLeft = false;
            goRight = false;

            GameTimer.Start();
        }
    }
}
