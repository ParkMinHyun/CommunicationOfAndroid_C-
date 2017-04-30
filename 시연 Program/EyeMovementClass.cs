using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Timers;
using System.Drawing;
using System.Windows.Forms;

namespace EyeExercise_ScreenColor
{
    class EyeMovementClass
    {
        //싱글톤 패턴 생성을 위한 객체
        private static EyeMovementClass one;
        private EyeMovementClass() //생성자- 객체의 필드 초기화
        {
            //시계방향 초록원 이동 타이머
            timer_clockwise.Interval = 500; // 0.5초
            timer_clockwise.Tick += new EventHandler(moveGreenClockwise);
            //반시계방향 초록원 이동 타이머
            timer_antiClockwise.Interval = 500;
            timer_antiClockwise.Tick += new EventHandler(moveGreenAntiClockwise);
            //좌우 초록원 이동 타이머
            timer_leftAndRight.Interval = 20;
            timer_leftAndRight.Elapsed += new ElapsedEventHandler(moveGreenLeftAndRight);
            //대각선(우측상향) 이동 타이머
            timer_rightTop.Interval = 25;
            timer_rightTop.Elapsed += new ElapsedEventHandler(moveLimeRightTop);
            //대각선(우측하향) 이동 타이머
            timer_rightBelow.Interval = 25;
            timer_rightBelow.Elapsed += new ElapsedEventHandler(moveLimeRightBelow);
            //무한대(누운8자) 이동 타이머
            timer_infinity.Interval = 200;
            timer_infinity.Tick += new EventHandler(moveGreenInfinity);
        }
        public static EyeMovementClass getInstance()
        {
            if (one == null)
            {
                one = new EyeMovementClass();
            }
            return one;
        }

        //폼 사각형 모서리 둥글게
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        //시계, 반시계방향 운동
        Form showForm_clock = new Form();
        PictureBox whiteBox = new PictureBox();
        PictureBox orangeCenter = new PictureBox();
        PictureBox orange24 = new PictureBox();
        PictureBox orange3 = new PictureBox();
        PictureBox orange6 = new PictureBox();
        PictureBox orange9 = new PictureBox();
        PictureBox orange12 = new PictureBox();
        PictureBox orange15 = new PictureBox();
        PictureBox orange18 = new PictureBox();
        PictureBox orange21 = new PictureBox();
        PictureBox greenCir = new PictureBox();
        int positionNum; //초록 원 위치 
        Point greenPoint24;
        Point greenPoint3;
        Point greenPoint6;
        Point greenPoint9;
        Point greenPoint12;
        Point greenPoint15;
        Point greenPoint18;
        Point greenPoint21;
        System.Windows.Forms.Timer timer_clockwise = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer_antiClockwise = new System.Windows.Forms.Timer();
        PictureBox logoBox_clock = new PictureBox();
        Label labelLogo_clock = new Label();

        //좌우 운동
        Form showForm_LR = new Form();
        System.Timers.Timer timer_leftAndRight = new System.Timers.Timer();
        int directionLR = 1; //우측방향으로 초기화 
        int timesLR = 0; //좌우 바퀴 수
        PictureBox logoBox_LR = new PictureBox();
        Label labelLogo_LR = new Label();

        //대각선(우측상향) 이동
        Form showForm_RT = new Form();
        PictureBox limeCir = new PictureBox();
        System.Timers.Timer timer_rightTop = new System.Timers.Timer();
        int directionRT = 0; //좌측하단으로 초기화
        int timesRT = 0; //바퀴 수
        PictureBox logoBox_RT = new PictureBox();
        Label labelLogo_RT = new Label();

        //대각선(우측하향) 이동
        System.Timers.Timer timer_rightBelow = new System.Timers.Timer();

        //무한대(누운8) 이동
        Form showForm_infinity = new Form();
        PictureBox whiteBoxInfinity = new PictureBox();
        PictureBox leftOrangeCenter = new PictureBox();
        PictureBox leftOrange24 = new PictureBox();
        PictureBox leftOrange3 = new PictureBox();
        PictureBox leftOrange6 = new PictureBox();
        PictureBox leftOrange9 = new PictureBox();
        PictureBox leftOrange12 = new PictureBox();
        PictureBox leftOrange15 = new PictureBox();
        PictureBox leftOrange18 = new PictureBox();
        PictureBox leftOrange21 = new PictureBox();
        PictureBox rightOrangeCenter = new PictureBox();
        PictureBox rightOrange24 = new PictureBox();
        PictureBox rightOrange3 = new PictureBox();
        PictureBox rightOrange6 = new PictureBox();
        PictureBox rightOrange9 = new PictureBox();
        PictureBox rightOrange12 = new PictureBox();
        PictureBox rightOrange15 = new PictureBox();
        PictureBox rightOrange21 = new PictureBox();
        PictureBox greenCirInfinity = new PictureBox();
        System.Windows.Forms.Timer timer_infinity = new System.Windows.Forms.Timer();
        int greenPosition;
        Point leftGreenP24;
        Point leftGreenP3;
        Point centerGreenP;
        Point leftGreenP9;
        Point leftGreenP12;
        Point leftGreenP15;
        Point leftGreenP18;
        Point leftGreenP21;
        Point rightGreenP24;
        Point rightGreenP3;
        Point rightGreenP6;
        Point rightGreenP9;
        Point rightGreenP12;
        Point rightGreenP15;
        Point rightGreenP21;

        //무한대(누운8) 운동 메서드
        public void move_infinity()
        {

            Rectangle workingArea = Screen.GetWorkingArea(showForm_infinity);

            showForm_infinity.Text = "transparent form";
            showForm_infinity.ClientSize = new Size(660, 490);
            showForm_clock.Location = Screen.AllScreens[0].WorkingArea.Location;
            showForm_infinity.Location =new Point(workingArea.Right/3+30, workingArea.Height/3-50);
            showForm_infinity.FormBorderStyle = FormBorderStyle.None;
            //showForm_infinity.StartPosition = FormStartPosition.CenterScreen;
            showForm_infinity.Opacity = 0.8D;
            showForm_infinity.BackColor = Color.Gainsboro;
            showForm_infinity.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, showForm_infinity.Width, showForm_infinity.Height, 50, 50));

            //왼쪽 바탕 흰원 생성
            setTwoWhiteBoxes();
            //set Orange Circles
            setTwoOrangeCircles();
            //set green circle
            setGreenCircle_infinity();
            //move green circles
            timer_infinity.Start();
            //set Logo
            setLogoInfinity();
            //set Label Text
            setLabelInfinity();

            showForm_infinity.ShowDialog();
        }

        //대각선(우측하향) 운동 메서드
        public void move_DiagonalRightBelow()
        {

            Rectangle workingArea = Screen.GetWorkingArea(showForm_RT);
            showForm_RT.Location = Screen.AllScreens[0].WorkingArea.Location;
            showForm_RT.Location = new Point(workingArea.Right / 3 + 30, workingArea.Height / 3 - 50);

            showForm_RT.Text = "transparent form";
            showForm_RT.ClientSize = new Size(580, 380);
            showForm_RT.FormBorderStyle = FormBorderStyle.None;
            //showForm_RT.StartPosition = FormStartPosition.CenterScreen;
            showForm_RT.Opacity = 0.8D;
            showForm_RT.BackColor = Color.Gainsboro;
            showForm_RT.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, showForm_RT.Width, showForm_RT.Height, 30, 30));

            //좌우 초록원 설정
            setLimeCircle_RB();
            //대각선(우측상향) 이동
            timer_rightBelow.Start();
            //set Logo
            setLogoRT();
            //set Label
            setLabelRT();

            showForm_RT.ShowDialog();
        }

        //대각선(우측상향) 운동 메서드
        public void move_DiagonalRightTop()
        {
            showForm_RT.Text = "transparent form";
            showForm_RT.ClientSize = new Size(580, 380);
            showForm_RT.FormBorderStyle = FormBorderStyle.None;
            showForm_RT.StartPosition = FormStartPosition.CenterScreen;
            showForm_RT.Opacity = 0.8D;
            showForm_RT.BackColor = Color.Gainsboro;
            showForm_RT.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, showForm_RT.Width, showForm_RT.Height, 30, 30));

            //좌우 초록원 설정
            setLimeCircle_RT();
            //대각선(우측상향) 이동
            timer_rightTop.Start();
            //set Logo
            setLogoRT();
            //set Label
            setLabelRT();

            showForm_RT.ShowDialog();
        }

        //좌우 운동 메서드
        public void move_LeftAndRight()
        {
            Rectangle workingArea = Screen.GetWorkingArea(showForm_LR);
            showForm_LR.Location = Screen.AllScreens[0].WorkingArea.Location;
            showForm_LR.Location = new Point(workingArea.Right / 3 + 30, workingArea.Height / 3);

            showForm_LR.Text = "transparent form";
            showForm_LR.ClientSize = new Size(530, 200);
            showForm_LR.FormBorderStyle = FormBorderStyle.None;
            //showForm_LR.StartPosition = FormStartPosition.CenterScreen;
            showForm_LR.Opacity = 0.8D;
            showForm_LR.BackColor = Color.Gainsboro;
            showForm_LR.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, showForm_LR.Width, showForm_LR.Height, 30, 30));

            //좌우 초록원 설정
            setGreenCircle_LR();
            //좌우 초록원 이동
            timer_leftAndRight.Start();
            //set Logo
            setLogoLR();
            //set TextBox
            setTextLogoLR();

            showForm_LR.ShowDialog();
        }

        //반시계방향 운동 메서드
        public void move_CounterClockwise()
        {
            setText(positionNum.ToString());

            showForm_clock.Text = "transparent form";
            showForm_clock.ClientSize = new Size(500, 500);
            showForm_clock.Location = Screen.AllScreens[0].WorkingArea.Location;
            showForm_clock.FormBorderStyle = FormBorderStyle.None;
            showForm_clock.StartPosition = FormStartPosition.CenterParent;
            showForm_clock.Opacity = 0.8D;
            showForm_clock.BackColor = Color.AntiqueWhite;
            showForm_clock.TransparencyKey = Color.AntiqueWhite;

            //set White Background Circle
            setWhiteBox();
            //set Orange Circles
            setOrangeCircles();
            //set Green Circle
            setGreenCircle_clock();
            //move Green Circles();
            timer_antiClockwise.Start();
            //set Logo
            setLogoClock();
            //set TextBox
            setTextLogoAntiClockwise();

            showForm_clock.ShowDialog();
        }

        //시계방향 운동 메서드
        public void move_Clockwise()
        {
            //this.textBox1.Text = positionNum.ToString();
            setText(positionNum.ToString());

            showForm_clock.Text = "transparent form";
            showForm_clock.ClientSize = new Size(500, 500);
            showForm_clock.FormBorderStyle = FormBorderStyle.None;
            showForm_clock.StartPosition = FormStartPosition.CenterScreen;
            showForm_clock.Opacity = 0.8D;
            showForm_clock.BackColor = Color.AntiqueWhite;
            showForm_clock.TransparencyKey = Color.AntiqueWhite;

            //set White Background Circle
            setWhiteBox();
            //set Orange Circles
            setOrangeCircles();
            //set Green Circle
            setGreenCircle_clock();
            //move Green Circles();
            timer_clockwise.Start();
            //set Logo
            setLogoClock();
            //set TextBox
            setTextLogoClockwise();

            showForm_clock.ShowDialog();
        }



        //무한대(누운8) 로고설정
        private void setLogoInfinity()
        {
            logoBox_RT.Image = Properties.Resources.tempLogo;
            logoBox_RT.BackColor = Color.Transparent;
            logoBox_RT.Location = new Point(300, 25);
            logoBox_RT.Name = "logoBox_RT";
            logoBox_RT.Size = new Size(60, 60);
            logoBox_RT.SizeMode = PictureBoxSizeMode.StretchImage;
            showForm_infinity.Controls.Add(logoBox_RT);
            logoBox_RT.BringToFront();
        }
        //무한대(누운8) 라벨설정
        private void setLabelInfinity()
        {
            labelLogo_RT.Location = new Point(170, 95);
            labelLogo_RT.Name = "labelLogo_LR";
            labelLogo_RT.AutoSize = true;
            labelLogo_RT.Text = "It's time for a break!\nRoll your eyes following the green circle.";
            labelLogo_RT.Font = new Font("Arial", 13);
            labelLogo_RT.TextAlign = ContentAlignment.MiddleCenter;
            showForm_infinity.Controls.Add(labelLogo_RT);
            labelLogo_RT.BringToFront();
        }
        //무한대(누운8) 이동
        private void moveGreenInfinity(object sender, EventArgs e)
        {
            setText("greenPosition: " + greenPosition.ToString());
            switch (greenPosition)
            {
                case 0:
                case 16:
                case 32:
                    greenCirInfinity.Location = leftGreenP9;
                    greenPosition++;
                    break;
                case 1:
                case 17:
                case 33:
                    greenCirInfinity.Location = leftGreenP12;
                    greenPosition++;
                    break;
                case 2:
                case 18:
                case 34:
                    greenCirInfinity.Location = leftGreenP15;
                    greenPosition++;
                    break;
                case 3:
                case 19:
                case 35:
                    greenCirInfinity.Location = leftGreenP18;
                    greenPosition++;
                    break;
                case 4:
                case 20:
                case 36:
                    greenCirInfinity.Location = leftGreenP21;
                    greenPosition++;
                    break;
                case 5:
                case 21:
                case 37:
                    greenCirInfinity.Location = leftGreenP24;
                    greenPosition++;
                    break;
                case 6:
                case 22:
                case 38:
                    greenCirInfinity.Location = leftGreenP3;
                    greenPosition++;
                    break;
                case 7:
                case 15:
                case 23:
                case 31:
                    greenCirInfinity.Location = centerGreenP;
                    greenPosition++;
                    break;
                case 8:
                case 24:
                    greenCirInfinity.Location = rightGreenP15;
                    greenPosition++;
                    break;
                case 9:
                case 25:
                    greenCirInfinity.Location = rightGreenP12;
                    greenPosition++;
                    break;
                case 10:
                case 26:
                    greenCirInfinity.Location = rightGreenP9;
                    greenPosition++;
                    break;
                case 11:
                case 27:
                    greenCirInfinity.Location = rightGreenP6;
                    greenPosition++;
                    break;
                case 12:
                case 28:
                    greenCirInfinity.Location = rightGreenP3;
                    greenPosition++;
                    break;
                case 13:
                case 29:
                    greenCirInfinity.Location = rightGreenP24;
                    greenPosition++;
                    break;
                case 14:
                case 30:
                    greenCirInfinity.Location = rightGreenP21;
                    greenPosition++;
                    break;
                //프로그램 종료
                case 39:
                    timer_infinity.Stop();
                    showForm_infinity.Close();
                    break;
            }
        }
        //무한대(누운8) 초록원 설정
        private void setGreenCircle_infinity()
        {
            greenCirInfinity.Image = Properties.Resources.greenCircle;
            greenCirInfinity.BackColor = Color.Transparent;
            greenCirInfinity.Location = new Point(270 - 8, 140 - 8);
            greenCirInfinity.Name = "greenCirInfinity";
            greenCirInfinity.Size = new Size(56, 56);
            greenCirInfinity.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(greenCirInfinity);
            greenCirInfinity.BringToFront();
            greenPosition = 0;
        }
        //무한대(누운8) 오렌지원 설정
        private void setTwoOrangeCircles()
        {
            //leftOrangeCenter.Image = EyeMovement5_Programatically.Properties.Resources.orangeCircle;
            //leftOrangeCenter.BackColor = Color.Transparent;
            //leftOrangeCenter.Location = new Point(140, 140);
            //leftOrangeCenter.Name = "leftOrangeCenter";
            //leftOrangeCenter.Size = new Size(40, 40);
            //leftOrangeCenter.SizeMode = PictureBoxSizeMode.StretchImage;
            //whiteBoxInfinity.Controls.Add(leftOrangeCenter);
            //leftOrangeCenter.BringToFront();

            leftOrange24.Image = Properties.Resources.orangeCircle;
            leftOrange24.BackColor = Color.Transparent;
            leftOrange24.Location = new Point(140, 140 - 110);
            leftOrange24.Name = "leftOrange24";
            leftOrange24.Size = new Size(40, 40);
            leftOrange24.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(leftOrange24);
            leftOrange24.BringToFront();
            leftGreenP24 = new Point(leftOrange24.Location.X - 8, leftOrange24.Location.Y - 8);

            leftOrange3.Image = Properties.Resources.orangeCircle;
            leftOrange3.BackColor = Color.Transparent;
            leftOrange3.Location = new Point(140 + 78, 140 - 78);
            leftOrange3.Name = "leftOrange3";
            leftOrange3.Size = new Size(40, 40);
            leftOrange3.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(leftOrange3);
            leftOrange3.BringToFront();
            leftGreenP3 = new Point(leftOrange3.Location.X - 8, leftOrange3.Location.Y - 8);

            //두 하얀원의 가운데 오렌지원
            leftOrange6.Image = Properties.Resources.orangeCircle;
            leftOrange6.BackColor = Color.Transparent;
            leftOrange6.Location = new Point(140 + 130, 140);
            leftOrange6.Name = "leftOrange6";
            leftOrange6.Size = new Size(40, 40);
            leftOrange6.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(leftOrange6);
            leftOrange6.BringToFront();
            centerGreenP = new Point(leftOrange6.Location.X - 8, leftOrange6.Location.Y - 8);

            leftOrange9.Image = Properties.Resources.orangeCircle;
            leftOrange9.BackColor = Color.Transparent;
            leftOrange9.Location = new Point(140 + 78, 140 + 78);
            leftOrange9.Name = "leftOrange9";
            leftOrange9.Size = new Size(40, 40);
            leftOrange9.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(leftOrange9);
            leftOrange9.BringToFront();
            leftGreenP9 = new Point(leftOrange9.Location.X - 8, leftOrange9.Location.Y - 8);

            leftOrange12.Image = Properties.Resources.orangeCircle;
            leftOrange12.BackColor = Color.Transparent;
            leftOrange12.Location = new Point(140, 140 + 110);
            leftOrange12.Name = "leftOrange12";
            leftOrange12.Size = new Size(40, 40);
            leftOrange12.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(leftOrange12);
            leftOrange12.BringToFront();
            leftGreenP12 = new Point(leftOrange12.Location.X - 8, leftOrange12.Location.Y - 8);

            leftOrange15.Image = Properties.Resources.orangeCircle;
            leftOrange15.BackColor = Color.Transparent;
            leftOrange15.Location = new Point(140 - 78, 140 + 78);
            leftOrange15.Name = "leftOrange15";
            leftOrange15.Size = new Size(40, 40);
            leftOrange15.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(leftOrange15);
            leftOrange15.BringToFront();
            leftGreenP15 = new Point(leftOrange15.Location.X - 8, leftOrange15.Location.Y - 8);

            leftOrange18.Image = Properties.Resources.orangeCircle;
            leftOrange18.BackColor = Color.Transparent;
            leftOrange18.Location = new Point(140 - 110, 140);
            leftOrange18.Name = "leftOrange18";
            leftOrange18.Size = new Size(40, 40);
            leftOrange18.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(leftOrange18);
            leftOrange18.BringToFront();
            leftGreenP18 = new Point(leftOrange18.Location.X - 8, leftOrange18.Location.Y - 8);

            leftOrange21.Image = Properties.Resources.orangeCircle;
            leftOrange21.BackColor = Color.Transparent;
            leftOrange21.Location = new Point(140 - 78, 140 - 78);
            leftOrange21.Name = "leftOrange21";
            leftOrange21.Size = new Size(40, 40);
            leftOrange21.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(leftOrange21);
            leftOrange21.BringToFront();
            leftGreenP21 = new Point(leftOrange21.Location.X - 8, leftOrange21.Location.Y - 8);

            //rightOrangeCenter.Image = EyeMovement5_Programatically.Properties.Resources.orangeCircle;
            //rightOrangeCenter.BackColor = Color.Transparent;
            //rightOrangeCenter.Location = new Point(400, 140);
            //rightOrangeCenter.Name = "rightOrangeCenter";
            //rightOrangeCenter.Size = new Size(40, 40);
            //rightOrangeCenter.SizeMode = PictureBoxSizeMode.StretchImage;
            //whiteBoxInfinity.Controls.Add(rightOrangeCenter);
            //rightOrangeCenter.BringToFront();

            rightOrange24.Image = Properties.Resources.orangeCircle;
            rightOrange24.BackColor = Color.Transparent;
            rightOrange24.Location = new Point(400, 140 - 110);
            rightOrange24.Name = "rightOrange24";
            rightOrange24.Size = new Size(40, 40);
            rightOrange24.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(rightOrange24);
            rightOrange24.BringToFront();
            rightGreenP24 = new Point(rightOrange24.Location.X - 8, rightOrange24.Location.Y - 8);

            rightOrange3.Image = Properties.Resources.orangeCircle;
            rightOrange3.BackColor = Color.Transparent;
            rightOrange3.Location = new Point(400 + 78, 140 - 78);
            rightOrange3.Name = "rightOrange3";
            rightOrange3.Size = new Size(40, 40);
            rightOrange3.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(rightOrange3);
            rightOrange3.BringToFront();
            rightGreenP3 = new Point(rightOrange3.Location.X - 8, rightOrange3.Location.Y - 8);

            rightOrange6.Image = Properties.Resources.orangeCircle;
            rightOrange6.BackColor = Color.Transparent;
            rightOrange6.Location = new Point(400 + 110, 140);
            rightOrange6.Name = "rightOrange6";
            rightOrange6.Size = new Size(40, 40);
            rightOrange6.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(rightOrange6);
            rightOrange6.BringToFront();
            rightGreenP6 = new Point(rightOrange6.Location.X - 8, rightOrange6.Location.Y - 8);

            rightOrange9.Image = Properties.Resources.orangeCircle;
            rightOrange9.BackColor = Color.Transparent;
            rightOrange9.Location = new Point(400 + 78, 140 + 78);
            rightOrange9.Name = "rightOrange9";
            rightOrange9.Size = new Size(40, 40);
            rightOrange9.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(rightOrange9);
            rightOrange9.BringToFront();
            rightGreenP9 = new Point(rightOrange9.Location.X - 8, rightOrange9.Location.Y - 8);

            rightOrange12.Image = Properties.Resources.orangeCircle;
            rightOrange12.BackColor = Color.Transparent;
            rightOrange12.Location = new Point(400, 140 + 110);
            rightOrange12.Name = "rightOrange12";
            rightOrange12.Size = new Size(40, 40);
            rightOrange12.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(rightOrange12);
            rightOrange12.BringToFront();
            rightGreenP12 = new Point(rightOrange12.Location.X - 8, rightOrange12.Location.Y - 8);

            rightOrange15.Image = Properties.Resources.orangeCircle;
            rightOrange15.BackColor = Color.Transparent;
            rightOrange15.Location = new Point(400 - 78, 140 + 78);
            rightOrange15.Name = "rightOrange15";
            rightOrange15.Size = new Size(40, 40);
            rightOrange15.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(rightOrange15);
            rightOrange15.BringToFront();
            rightGreenP15 = new Point(rightOrange15.Location.X - 8, rightOrange15.Location.Y - 8);

            rightOrange21.Image = Properties.Resources.orangeCircle;
            rightOrange21.BackColor = Color.Transparent;
            rightOrange21.Location = new Point(400 - 78, 140 - 78);
            rightOrange21.Name = "rightOrange21";
            rightOrange21.Size = new Size(40, 40);
            rightOrange21.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBoxInfinity.Controls.Add(rightOrange21);
            rightOrange21.BringToFront();
            rightGreenP21 = new Point(rightOrange21.Location.X - 8, rightOrange21.Location.Y - 8);

        }
        //무한대(누운8) 흰바탕배경 설정
        private void setTwoWhiteBoxes()
        {
            whiteBoxInfinity.Image = Properties.Resources.whiteInfinity;
            whiteBoxInfinity.BackColor = Color.Transparent;
            whiteBoxInfinity.Location = new Point(40, 150);
            whiteBoxInfinity.Size = new Size(580, 320);
            whiteBoxInfinity.SizeMode = PictureBoxSizeMode.StretchImage;
            showForm_infinity.Controls.Add(whiteBoxInfinity);
            whiteBoxInfinity.BringToFront();
        }



        //대각선 라임원 설정
        private void setLimeCircle_RB()
        {
            limeCir.Image = Properties.Resources.greenCircle;
            limeCir.BackColor = Color.Transparent;
            limeCir.Location = new Point(500, 300);
            limeCir.Name = "limeCir";
            limeCir.Size = new Size(80, 80);
            limeCir.SizeMode = PictureBoxSizeMode.StretchImage;
            showForm_RT.Controls.Add(limeCir);
            limeCir.BringToFront();
        }
        //대각선(우측하향) 이동
        private void moveLimeRightBelow(object sender, EventArgs e)
        {
            setText("directionRT=" + directionRT.ToString() + " timesRT= " + timesRT.ToString());

            switch (directionRT)
            {
                case 0: //좌측상단으로 가고있을때
                    if (limeCir.Location.Y == 0) //좌측상단 끝일때, 방향 스위치
                    {
                        limeCir.Location = new Point(limeCir.Location.X + 20, limeCir.Location.Y + 12);
                        directionRT = 1;
                        timesRT++;
                    }
                    else
                        limeCir.Location = new Point(limeCir.Location.X - 20, limeCir.Location.Y - 12);
                    break;
                case 1: //우측하단으로 가고있을때
                    if (limeCir.Location.Y == 300) //우측하단 끝일때, 방향 스위치
                    {
                        limeCir.Location = new Point(limeCir.Location.X - 20, limeCir.Location.Y - 12);
                        directionRT = 0;
                        if (timesRT == 5)
                        {
                            timesRT = 0; //다시 횟수 초기화
                            timer_rightBelow.Stop();
                            showForm_RT.Close();
                        }
                    }
                    else
                        limeCir.Location = new Point(limeCir.Location.X + 20, limeCir.Location.Y + 12);
                    break;
            }
        }
        //대각선(우측상향) 이동
        private void moveLimeRightTop(object sender, EventArgs e)
        {
            setText("directionRT=" + directionRT.ToString() + " timesRT= " + timesRT.ToString());

            switch (directionRT)
            {
                case 0: //좌측하단으로 가고있을때
                    if (limeCir.Location.Y == 300) //좌측하단 끝일때, 방향 스위치
                    {
                        limeCir.Location = new Point(limeCir.Location.X + 20, limeCir.Location.Y - 12);
                        directionRT = 1;
                        timesRT++;
                    }
                    else
                        limeCir.Location = new Point(limeCir.Location.X - 20, limeCir.Location.Y + 12);
                    break;
                case 1: //우측상단으로 가고있을때
                    if (limeCir.Location.Y == 0) //우측상단 끝일때, 방향 스위치
                    {
                        limeCir.Location = new Point(limeCir.Location.X - 20, limeCir.Location.Y + 12);
                        directionRT = 0;
                        if (timesRT == 5)
                        {
                            timesRT = 0; //다시 횟수 초기화
                            timer_rightTop.Stop();
                            showForm_RT.Close();
                        }
                    }
                    else
                        limeCir.Location = new Point(limeCir.Location.X + 20, limeCir.Location.Y - 12);
                    break;
            }
        }
        //대각선 텍스트 설정
        private void setLabelRT()
        {
            labelLogo_RT.Location = new Point(185, 120);
            labelLogo_RT.Name = "labelLogo_LR";
            labelLogo_RT.AutoSize = true;
            labelLogo_RT.Text = "It's time for a break!\nRoll your eyes diagonally.";
            labelLogo_RT.Font = new Font("Arial", 13);
            labelLogo_RT.TextAlign = ContentAlignment.MiddleCenter;
            showForm_RT.Controls.Add(labelLogo_RT);
        }
        //대각선 로고 설정
        private void setLogoRT()
        {
            logoBox_RT.Image = Properties.Resources.tempLogo;
            logoBox_RT.BackColor = Color.Transparent;
            logoBox_RT.Location = new Point(260, 40);
            logoBox_RT.Name = "logoBox_RT";
            logoBox_RT.Size = new Size(60, 60);
            logoBox_RT.SizeMode = PictureBoxSizeMode.StretchImage;
            showForm_RT.Controls.Add(logoBox_RT);
            logoBox_RT.BringToFront();
        }
        //대각선 라임원 설정
        private void setLimeCircle_RT()
        {
            limeCir.Image = Properties.Resources.greenCircle;
            limeCir.BackColor = Color.Transparent;
            limeCir.Location = new Point(500, 0);
            limeCir.Name = "limeCir";
            limeCir.Size = new Size(80, 80);
            limeCir.SizeMode = PictureBoxSizeMode.StretchImage;
            showForm_RT.Controls.Add(limeCir);
            limeCir.BringToFront();
        }



        //좌우 로고 설정
        private void setLogoLR()
        {
            logoBox_LR.Image = Properties.Resources.tempLogo;
            logoBox_LR.BackColor = Color.Transparent;
            logoBox_LR.Location = new Point(100, 30);
            logoBox_LR.Name = "logoBox_LR";
            logoBox_LR.Size = new Size(60, 60);
            logoBox_LR.SizeMode = PictureBoxSizeMode.StretchImage;
            showForm_LR.Controls.Add(logoBox_LR);
            logoBox_LR.BringToFront();
        }
        //좌우 텍스트 설정
        private void setTextLogoLR()
        {
            labelLogo_LR.Location = new Point(200, 40);
            labelLogo_LR.Name = "labelLogo_LR";
            labelLogo_LR.AutoSize = true;
            labelLogo_LR.Text = "It's time for a break!\nRoll your eyes left and right.";
            labelLogo_LR.Font = new Font("Arial", 13);
            labelLogo_LR.TextAlign = ContentAlignment.MiddleCenter;
            showForm_LR.Controls.Add(labelLogo_LR);
            labelLogo_LR.BringToFront();
        }
        //좌우 초록원 이동
        private void moveGreenLeftAndRight(object sender, EventArgs e)
        {
            switch (directionLR)
            {
                case 0: //좌측 방향으로 가고 있을 때
                    if (greenCir.Location.X == 0)
                    {
                        setText("directionLR=" + directionLR.ToString() + " timesLR= " + timesLR.ToString());
                        greenCir.Location = new Point(greenCir.Location.X + 20, greenCir.Location.Y);
                        directionLR = 1;
                        if (timesLR == 5)
                        {
                            timesLR = 0;
                            timer_leftAndRight.Stop();
                            showForm_LR.Close();
                        }
                    }
                    else
                    {
                        greenCir.Location = new Point(greenCir.Location.X - 20, greenCir.Location.Y);
                    }
                    break;
                case 1: //우측 방향으로 가고 있을 때
                    if (greenCir.Location.X == 480)
                    {
                        greenCir.Location = new Point(greenCir.Location.X - 20, greenCir.Location.Y);
                        directionLR = 0;
                        timesLR++;
                    }
                    else
                    {
                        greenCir.Location = new Point(greenCir.Location.X + 20, greenCir.Location.Y);
                    }
                    break;
            }
        }
        //좌우 초록원 설정
        private void setGreenCircle_LR()
        {
            greenCir.Image = Properties.Resources.greenCircle;
            greenCir.BackColor = Color.Transparent;
            greenCir.Location = new Point(0, 130);
            greenCir.Name = "greenCir";
            greenCir.Size = new Size(70, 70);
            greenCir.SizeMode = PictureBoxSizeMode.StretchImage;
            showForm_LR.Controls.Add(greenCir);
            greenCir.BringToFront();
        }




        //반시계방향- 텍스트
        private void setTextLogoAntiClockwise()
        {
            labelLogo_clock.Location = new Point(65, 223);
            labelLogo_clock.Name = "label";
            labelLogo_clock.AutoSize = true;
            labelLogo_clock.Text = "It's time for a break!\nRoll your eyes counterclockwise.";
            labelLogo_clock.Font = new Font("Arial", 13);
            labelLogo_clock.TextAlign = ContentAlignment.MiddleCenter;
            labelLogo_clock.Parent = orange6;
            labelLogo_clock.BackColor = Color.Transparent;
            whiteBox.Controls.Add(labelLogo_clock);
            labelLogo_clock.BringToFront();
        }
        //반시계방향- 초록 원 이동
        private void moveGreenAntiClockwise(object sender, EventArgs e)
        {
            greenCir.BringToFront();

            switch (positionNum)
            {
                case 0:
                    greenCir.Location = greenPoint21;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 1:
                case 9:
                    greenCir.Location = greenPoint18;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 2:
                case 10:
                    greenCir.Location = greenPoint15;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 3:
                case 11:
                    greenCir.Location = greenPoint12;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 4:
                case 12:
                    greenCir.Location = greenPoint9;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 5:
                case 13:
                    greenCir.Location = greenPoint6;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 6:
                case 14:
                    greenCir.Location = greenPoint3;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 7:
                case 15:
                    greenCir.Location = greenPoint24;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 8:
                case 16:
                    greenCir.Location = greenPoint21;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;

            }

            if (positionNum == 16)
            {
                positionNum = 0; //초기화
                setText(positionNum.ToString() + " 끝났다");
                timer_antiClockwise.Stop();
                showForm_clock.Close();
            }
        }
        //시계방향- 초록원 이동 
        private void moveGreenClockwise(object sender, EventArgs e)
        {
            switch (positionNum)
            {
                case 0:
                    greenCir.Location = greenPoint3;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 1:
                case 9:
                    greenCir.Location = greenPoint6;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 2:
                case 10:
                    greenCir.Location = greenPoint9;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 3:
                case 11:
                    greenCir.Location = greenPoint12;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 4:
                case 12:
                    greenCir.Location = greenPoint15;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 5:
                case 13:
                    greenCir.Location = greenPoint18;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 6:
                case 14:
                    greenCir.Location = greenPoint21;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 7:
                case 15:
                    greenCir.Location = greenPoint24;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;
                case 8:
                case 16:
                    greenCir.Location = greenPoint3;
                    positionNum += 1;
                    setText(positionNum.ToString());
                    break;

            }

            if (positionNum == 16)
            {
                positionNum = 0; //초기화
                setText(positionNum.ToString() + " 끝났다");
                timer_clockwise.Stop();
                showForm_clock.Close();
            }
        }
        //시계,반시계방향- 흰색 바탕원 설정
        private void setWhiteBox()
        {
            whiteBox.Image = Properties.Resources.whiteCircle;
            whiteBox.BackColor = Color.Transparent;
            whiteBox.Location = new Point(50, 50);
            whiteBox.Name = "whiteBox";
            whiteBox.Size = new Size(400, 400);
            whiteBox.SizeMode = PictureBoxSizeMode.StretchImage;
            showForm_clock.Controls.Add(whiteBox);
            whiteBox.BringToFront();
        }
        //시계,반시계방향- 오렌지원 바탕원 설정
        private void setOrangeCircles()
        {
            //orangeCenter.Image = EyeMovement5_Programatically.Properties.Resources.orangeCircle;
            //orangeCenter.BackColor = Color.Transparent;
            //orangeCenter.Location = new Point(175, 175);
            //orangeCenter.Name = "orangeCenter";
            //orangeCenter.Size = new Size(50, 50);
            //orangeCenter.SizeMode = PictureBoxSizeMode.StretchImage;
            //whiteBox.Controls.Add(orangeCenter);
            //orangeCenter.BringToFront();

            orange24.Image = Properties.Resources.orangeCircle;
            orange24.BackColor = Color.Transparent;
            orange24.Location = new Point(175, 30);
            orange24.Name = "orange24";
            orange24.Size = new Size(50, 50);
            orange24.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBox.Controls.Add(orange24);
            orange24.BringToFront();
            greenPoint24 = new Point(orange24.Location.X - 10, orange24.Location.Y - 10);

            orange3.Image = Properties.Resources.orangeCircle;
            orange3.BackColor = Color.Transparent;
            orange3.Location = new Point(175 + 103, 30 + 42);
            orange3.Name = "orange3";
            orange3.Size = new Size(50, 50);
            orange3.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBox.Controls.Add(orange3);
            orange3.BringToFront();
            greenPoint3 = new Point(orange3.Location.X - 10, orange3.Location.Y - 10);

            orange6.Image = Properties.Resources.orangeCircle;
            orange6.BackColor = Color.Transparent;
            orange6.Location = new Point(175 + 145, 175);
            orange6.Name = "orange6";
            orange6.Size = new Size(50, 50);
            orange6.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBox.Controls.Add(orange6);
            orange6.BringToFront();
            greenPoint6 = new Point(orange6.Location.X - 10, orange6.Location.Y - 10);

            orange9.Image = Properties.Resources.orangeCircle;
            orange9.BackColor = Color.Transparent;
            orange9.Location = new Point(175 + 103, 175 + 145 - 42);
            orange9.Name = "orange9";
            orange9.Size = new Size(50, 50);
            orange9.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBox.Controls.Add(orange9);
            orange9.BringToFront();
            greenPoint9 = new Point(orange9.Location.X - 10, orange9.Location.Y - 10);

            orange12.Image = Properties.Resources.orangeCircle;
            orange12.BackColor = Color.Transparent;
            orange12.Location = new Point(175, 175 + 145);
            orange12.Name = "orange12";
            orange12.Size = new Size(50, 50);
            orange12.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBox.Controls.Add(orange12);
            orange12.BringToFront();
            greenPoint12 = new Point(orange12.Location.X - 10, orange12.Location.Y - 10);

            orange15.Image = Properties.Resources.orangeCircle;
            orange15.BackColor = Color.Transparent;
            orange15.Location = new Point(175 - 103, 175 + 145 - 42);
            orange15.Name = "orange15";
            orange15.Size = new Size(50, 50);
            orange15.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBox.Controls.Add(orange15);
            orange15.BringToFront();
            greenPoint15 = new Point(orange15.Location.X - 10, orange15.Location.Y - 10);

            orange18.Image = Properties.Resources.orangeCircle;
            orange18.BackColor = Color.Transparent;
            orange18.Location = new Point(175 - 145, 175);
            orange18.Name = "orange18";
            orange18.Size = new Size(50, 50);
            orange18.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBox.Controls.Add(orange18);
            orange18.BringToFront();
            greenPoint18 = new Point(orange18.Location.X - 10, orange18.Location.Y - 10);

            orange21.Image = Properties.Resources.orangeCircle;
            orange21.BackColor = Color.Transparent;
            orange21.Location = new Point(175 - 145 + 42, 175 - 103);
            orange21.Name = "orange21";
            orange21.Size = new Size(50, 50);
            orange21.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBox.Controls.Add(orange21);
            orange21.BringToFront();
            greenPoint21 = new Point(orange21.Location.X - 10, orange21.Location.Y - 10);
        }
        //시계,반시계방향 초록 원 설정
        private void setGreenCircle_clock()
        {
            greenCir.Image = Properties.Resources.greenCircle;
            greenCir.BackColor = Color.Transparent;
            greenCir.Location = new Point(165, 20);
            greenCir.Name = "greenCir";
            greenCir.Size = new Size(70, 70);
            greenCir.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBox.Controls.Add(greenCir);
            greenCir.BringToFront();
            positionNum = 0;
        }
        //시계,반시계방향 로고 설정
        private void setLogoClock()
        {
            logoBox_clock.Image = Properties.Resources.tempLogo;
            logoBox_clock.BackColor = Color.Transparent;
            logoBox_clock.Location = new Point(165, 115);
            logoBox_clock.Name = "logoBox_clock";
            logoBox_clock.Size = new Size(70, 70);
            logoBox_clock.SizeMode = PictureBoxSizeMode.StretchImage;
            whiteBox.Controls.Add(logoBox_clock);
            logoBox_clock.BringToFront();
        }
        //시계방향-텍스트
        private void setTextLogoClockwise()
        {
            labelLogo_clock.Location = new Point(100, 210);
            labelLogo_clock.Name = "label";
            labelLogo_clock.AutoSize = true;
            labelLogo_clock.Text = "It's time for a break!\nRoll your eyes clockwise.";
            labelLogo_clock.Font = new Font("Arial", 13);
            labelLogo_clock.TextAlign = ContentAlignment.MiddleCenter;
            whiteBox.Controls.Add(labelLogo_clock);
            labelLogo_clock.BringToFront();
        }






        //mainForm textBox1 텍스트 수정
        public void setText(String text)
        {
            Form1.mainForm.update(text);
        }

    }
}