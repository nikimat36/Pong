using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pong {
    public partial class Form1 : Form {

        bool goUp;
        bool goDown;
        int speed = 5;
        int ballX = 5;  // horizontal speed
        int ballY = 5;  // vertical speed
        int score = 0;  //score for Player
        int cpuPoint = 0;   // score for CPU

        public Form1()
        {
            InitializeComponent();
        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) {
                //if pleyer press up key, 
                //goUp is changed to true
                goUp = true;    
            }
            if (e.KeyCode == Keys.Down) {
                //if pleyer press down key, 
                //goUp is changed to true
                goDown = true;
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) {
                //if pleyer leaves up key, 
                //goUp is changed to true
                goUp = false;
            }
            if (e.KeyCode == Keys.Down) {
                //if pleyer leaves down key, 
                //goUp is changed to true
                goDown = false;
            }

        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            playerScore.Text = "" + score;
            cpuLabel.Text = "" + cpuPoint;
            endMsg.Text = "";

            Ball.Top -= ballY;
            Ball.Left -= ballX;

            cpu.Top += speed;

            //if cpu reached top/bottom, it will change direction
            if (score < 5) {
                if (cpu.Top < 0 || cpu.Top > 455) {
                    speed = -speed;
                }
            //if the score is higher than 5, cpu will follow the ball
            } 
            if (score > 5){
                cpu.Top = Ball.Top + 30;
            }

            //if ball  passes left side
            if (Ball.Left < 0) {
                Ball.Left = 434;    // reset ball to the middle
                ballX = -ballX;     // change the ball direction
                ballX -= 1;         // increase the speed
                cpuPoint++;         // add 1 to the CPU score
            }

            //if ball passes the right side
            if (Ball.Left + Ball.Width > ClientSize.Width) {
                Ball.Left = 434;    // set ball to the centre
                ballX = -ballX;     // change direction of the ball
                ballX += 1;         // increase the speed of the ball
                score++;            // add 1 to players score
            }

            //if ball reaches top/bottom of the screen, chenges direction
            if (Ball.Top < 0 || Ball.Top + Ball.Height > ClientSize.Height) {
                ballY = -ballY;
            }

            //if the ball hits cpu/player
            if (Ball.Bounds.IntersectsWith(player.Bounds) || Ball.Bounds.IntersectsWith(cpu.Bounds)) {
                ballX = -ballX;
            }

            //if player reaches top/bottom, he will move towards...
            if (goUp == true && player.Top > 0) {       //the top
                player.Top -= 8;
            }
            if (goDown == true && player.Top < 455) {   // the bottom
                player.Top += 8;
            }

            //final score, ending game
            if (score == 11) {
                gameTimer.Stop();
                endMsg.Text = "You win this game!!";
                Console.Write("You win this game!!");
            }

            if (cpuPoint == 11) {
                gameTimer.Stop();
                endMsg.Text = "CPU wins, you lose";
                
                Console.Write("CPU wins, you lose.");
            }
        }

    }
}
