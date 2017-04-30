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
using System.Runtime.InteropServices;
using System.Timers;

namespace EyeExercise_ScreenColor
{
    public partial class Form1 : Form
    {
        Alrimchang a;
        public static Form1 mainForm; //외부 클래스에서 접근하도록 하는 객체
        ShowImage showImage;
       
        public Form1()
        {
            InitializeComponent();
            BroadcastServer.start(9000);
            mainForm = this; //객체 가르키기
        }

        //외부 클래스에서 점근할 메서드- 메인폼의 텍스트박스 텍스트 수정
        public void update(string message)
        {
            textBox1.Text = message;
        }

        EyeMovementClass eyeClass = EyeMovementClass.getInstance();
        private void clockwise_Click(object sender, EventArgs e)
        {
            //3초후에 메인폼이 최소화된 상태에서도 되는 지 확인
            System.Timers.Timer timerClock = new System.Timers.Timer();
            timerClock.Elapsed += new ElapsedEventHandler(OnTimer);
            timerClock.Interval = 3000;
            timerClock.Enabled = true;

            //eyeClass.move_Clockwise();
        }
        public void OnTimer(Object source, ElapsedEventArgs e)
        {
            //Your code here 
            eyeClass.move_Clockwise();
        }

        private void counterclockwise_Click(object sender, EventArgs e)
        {
            eyeClass.move_CounterClockwise();
        }

        private void leftAndRight_Click(object sender, EventArgs e)
        {
            eyeClass.move_LeftAndRight();
        }

        private void diagonal_RightTop_Click(object sender, EventArgs e)
        {
            eyeClass.move_DiagonalRightTop();
        }

        private void diagonal_RightBelow_Click(object sender, EventArgs e)
        {
            eyeClass.move_DiagonalRightBelow();
        }

        private void infinity_Click(object sender, EventArgs e)
        {
            eyeClass.move_infinity();
        }

        Random rand = new Random();

        private void random_Click(object sender, EventArgs e)
        {
            switch (rand.Next(1, 7))
            {
                case 1:
                    MessageBox.Show("1");
                    eyeClass.move_Clockwise();
                    break;
                case 2:
                    MessageBox.Show("2");
                    eyeClass.move_CounterClockwise();
                    break;
                case 3:
                    MessageBox.Show("3");
                    eyeClass.move_LeftAndRight();
                    break;
                case 4:
                    MessageBox.Show("4");
                    eyeClass.move_DiagonalRightTop();
                    break;
                case 5:
                    MessageBox.Show("5");
                    eyeClass.move_DiagonalRightBelow();
                    break;
                case 6:
                    MessageBox.Show("6");
                    eyeClass.move_infinity();
                    break;
            }
        }


        int selectedIndex; // listbox 선택 항목

        [DllImport("GDI32.dll")]
        private unsafe static extern bool SetDeviceGammaRamp(IntPtr hdc, void* ramp);
        private static IntPtr hdc;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = listBox1.SelectedIndex;
            switch (selectedIndex)
            {
                case 0: //1200
                    //MessageBox.Show("선택 항목: "+ listBox1.SelectedItem);
                    setLCDbrightness(255, 83, 0);
                    //doTest();
                    break;
                case 1: //1900
                    //MessageBox.Show("선택 항목: " + listBox1.SelectedItem);
                    setLCDbrightness(255, 131, 0);
                    break;
                case 2: //2300
                    //MessageBox.Show("선택 항목: " + listBox1.SelectedItem);
                    setLCDbrightness(255, 152, 54);
                    break;
                case 3: //2700
                    //MessageBox.Show("선택 항목: " + listBox1.SelectedItem);
                    setLCDbrightness(255, 169, 87);
                    break;
                case 4: //3400
                    //MessageBox.Show("선택 항목: " + listBox1.SelectedItem);
                    setLCDbrightness(255, 193, 132);
                    break;
                case 5: //4200
                    //MessageBox.Show("선택 항목: " + listBox1.SelectedItem);
                    setLCDbrightness(255, 213, 173);
                    break;
                case 6: //5000
                    //MessageBox.Show("선택 항목: " + listBox1.SelectedItem);
                    setLCDbrightness(255, 228, 206);
                    break;
                case 7: //default
                    setLCDbrightness(255, 255, 255);
                    break;
            }
        }


        public static unsafe bool setLCDbrightness(short red, short green, short blue)
        {
            Graphics gg = Graphics.FromHwnd(IntPtr.Zero);
            hdc = gg.GetHdc();

            short* gArray = stackalloc short[3 * 256];
            short* idx = gArray;
            short brightness = 0;
            for (int j = 0; j < 3; j++)
            {
                if (j == 0) brightness = red;
                if (j == 1) brightness = green;
                if (j == 2) brightness = blue;
                for (int i = 0; i < 256; i++)
                {
                    int arrayVal = i * (brightness);
                    if (arrayVal > 65535) arrayVal = 65535;
                    *idx = (short)arrayVal;
                    idx++;
                }
            }
            // For some reason, this always returns false?
            bool retVal = SetDeviceGammaRamp(hdc, gArray);
            gg.Dispose();
            return false;
        }

        private void ShowImage_MouseClick(object sender, MouseEventArgs e)
        {
            // MainForm 띄우기 
            showImage = new ShowImage();
            showImage.Show();
        }

        private void AlrimchangBtn_Click(object sender, EventArgs e)
        {
            a = new Alrimchang();
            a.Show();
            a.Activate();

        }
        
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            a.startAnimation();

        }

        private void closeImage_Click(object sender, EventArgs e)
        {
            showImage.Close();
        }
    }
}
