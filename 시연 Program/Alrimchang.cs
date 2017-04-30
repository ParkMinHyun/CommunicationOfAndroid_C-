using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EyeExercise_ScreenColor
{
    public partial class Alrimchang : Form
    {
        public Rectangle workingArea;
        public Boolean show_close_flag = false;

        public Alrimchang()
        {
            InitializeComponent();
            workingArea = Screen.GetWorkingArea(this);

            this.Location = new Point(workingArea.Right,
                                      workingArea.Bottom - Size.Height - 30);
            
        }

        public void startAnimation()
        {
            Point location = new Point(0, 0);
            if (!show_close_flag)
            {
                for (int i = 0; i < 488; i++)
                {
                    location = new Point(workingArea.Right -Convert.ToInt32(i), workingArea.Bottom - Size.Height - 30);
                    this.Location = location;
                    Thread.Sleep(50);
                }
            }
            else
            {
                for (int i = 488; i > 0; i--)
                {
                    location = new Point(workingArea.Right + 412 - Convert.ToInt32(i), workingArea.Bottom - Size.Height - 30);
                    this.Location = location;
                    Thread.Sleep(50);
                }
            }

            if (show_close_flag.Equals(true))
                show_close_flag = false;
            else
                show_close_flag = true;

            Thread.Sleep(100);
        }
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
