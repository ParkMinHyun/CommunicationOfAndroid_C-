using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EyeExercise_ScreenColor
{
    public partial class Alrimchang : Form
    {
        public Rectangle workingArea;
        public Boolean b = false;
        public Alrimchang()
        {
            InitializeComponent();
            workingArea = Screen.GetWorkingArea(this);

            this.Location = new Point(workingArea.Right,
                                      workingArea.Bottom - Size.Height - 30);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point location = new Point(0, 0);
            if (!b)
            {
                for (int i = 0; i < 488; i++)
                {
                    location = new Point(workingArea.Right - i, workingArea.Bottom - Size.Height - 30);
                    this.Location = location;
                }
                timer1.Stop();
            }
            else
            {
                for (int i = 488; i >0; i--)
                {
                    location = new Point(workingArea.Right+412 - i, workingArea.Bottom - Size.Height - 30);
                    this.Location = location;
                }
                timer1.Stop();
            }

            if( b.Equals(true))
                b = false;
            else
                b = true;
        }

        public void cancel()
        {
            timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
