namespace EyeExercise_ScreenColor
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.clockwise = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.counterclockwise = new System.Windows.Forms.Button();
            this.leftAndRight = new System.Windows.Forms.Button();
            this.diagonal_RightTop = new System.Windows.Forms.Button();
            this.diagonal_RightBelow = new System.Windows.Forms.Button();
            this.infinity = new System.Windows.Forms.Button();
            this.random = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ShowImage = new System.Windows.Forms.Button();
            this.AlrimchangBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clockwise
            // 
            this.clockwise.Location = new System.Drawing.Point(0, 0);
            this.clockwise.Name = "clockwise";
            this.clockwise.Size = new System.Drawing.Size(203, 50);
            this.clockwise.TabIndex = 0;
            this.clockwise.Text = "clockwise";
            this.clockwise.UseVisualStyleBackColor = true;
            this.clockwise.Click += new System.EventHandler(this.clockwise_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 204);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(218, 28);
            this.textBox1.TabIndex = 2;
            // 
            // counterclockwise
            // 
            this.counterclockwise.Location = new System.Drawing.Point(0, 66);
            this.counterclockwise.Name = "counterclockwise";
            this.counterclockwise.Size = new System.Drawing.Size(203, 50);
            this.counterclockwise.TabIndex = 3;
            this.counterclockwise.Text = "counterclockwise";
            this.counterclockwise.UseVisualStyleBackColor = true;
            this.counterclockwise.Click += new System.EventHandler(this.counterclockwise_Click);
            // 
            // leftAndRight
            // 
            this.leftAndRight.Location = new System.Drawing.Point(0, 137);
            this.leftAndRight.Name = "leftAndRight";
            this.leftAndRight.Size = new System.Drawing.Size(203, 50);
            this.leftAndRight.TabIndex = 4;
            this.leftAndRight.Text = "leftAndRight";
            this.leftAndRight.UseVisualStyleBackColor = true;
            this.leftAndRight.Click += new System.EventHandler(this.leftAndRight_Click);
            // 
            // diagonal_RightTop
            // 
            this.diagonal_RightTop.Location = new System.Drawing.Point(0, 238);
            this.diagonal_RightTop.Name = "diagonal_RightTop";
            this.diagonal_RightTop.Size = new System.Drawing.Size(203, 50);
            this.diagonal_RightTop.TabIndex = 5;
            this.diagonal_RightTop.Text = "diagonal_RightTop";
            this.diagonal_RightTop.UseVisualStyleBackColor = true;
            this.diagonal_RightTop.Click += new System.EventHandler(this.diagonal_RightTop_Click);
            // 
            // diagonal_RightBelow
            // 
            this.diagonal_RightBelow.Location = new System.Drawing.Point(0, 311);
            this.diagonal_RightBelow.Name = "diagonal_RightBelow";
            this.diagonal_RightBelow.Size = new System.Drawing.Size(203, 50);
            this.diagonal_RightBelow.TabIndex = 6;
            this.diagonal_RightBelow.Text = "diagonal_RightBelow";
            this.diagonal_RightBelow.UseVisualStyleBackColor = true;
            this.diagonal_RightBelow.Click += new System.EventHandler(this.diagonal_RightBelow_Click);
            // 
            // infinity
            // 
            this.infinity.Location = new System.Drawing.Point(0, 388);
            this.infinity.Name = "infinity";
            this.infinity.Size = new System.Drawing.Size(203, 50);
            this.infinity.TabIndex = 7;
            this.infinity.Text = "infinity";
            this.infinity.UseVisualStyleBackColor = true;
            this.infinity.Click += new System.EventHandler(this.infinity_Click);
            // 
            // random
            // 
            this.random.Location = new System.Drawing.Point(0, 463);
            this.random.Name = "random";
            this.random.Size = new System.Drawing.Size(203, 50);
            this.random.TabIndex = 8;
            this.random.Text = "Random";
            this.random.UseVisualStyleBackColor = true;
            this.random.Click += new System.EventHandler(this.random_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Items.AddRange(new object[] {
            "1200k: Ember",
            "1900k: Candle",
            "2300k: Dim Incandescent",
            "2700k: Incandescent",
            "3400k: Halogen",
            "4200k: Fluorescent",
            "5000k: Sunlight",
            "White"});
            this.listBox1.Location = new System.Drawing.Point(253, 92);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(291, 346);
            this.listBox1.TabIndex = 9;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // ShowImage
            // 
            this.ShowImage.Location = new System.Drawing.Point(262, 444);
            this.ShowImage.Name = "ShowImage";
            this.ShowImage.Size = new System.Drawing.Size(116, 50);
            this.ShowImage.TabIndex = 10;
            this.ShowImage.Text = "ShowImage";
            this.ShowImage.UseVisualStyleBackColor = true;
            this.ShowImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ShowImage_MouseClick);
            // 
            // AlrimchangBtn
            // 
            this.AlrimchangBtn.Location = new System.Drawing.Point(419, 444);
            this.AlrimchangBtn.Name = "AlrimchangBtn";
            this.AlrimchangBtn.Size = new System.Drawing.Size(116, 50);
            this.AlrimchangBtn.TabIndex = 11;
            this.AlrimchangBtn.Text = "Alrimchang";
            this.AlrimchangBtn.UseVisualStyleBackColor = true;
            this.AlrimchangBtn.Click += new System.EventHandler(this.AlrimchangBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(419, 500);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(116, 50);
            this.CancelBtn.TabIndex = 12;
            this.CancelBtn.Text = "CancelBtn";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(262, 500);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 50);
            this.button1.TabIndex = 13;
            this.button1.Text = "closeImage";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.closeImage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 546);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.AlrimchangBtn);
            this.Controls.Add(this.ShowImage);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.random);
            this.Controls.Add(this.infinity);
            this.Controls.Add(this.diagonal_RightBelow);
            this.Controls.Add(this.diagonal_RightTop);
            this.Controls.Add(this.leftAndRight);
            this.Controls.Add(this.counterclockwise);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.clockwise);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button clockwise;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button counterclockwise;
        private System.Windows.Forms.Button leftAndRight;
        private System.Windows.Forms.Button diagonal_RightTop;
        private System.Windows.Forms.Button diagonal_RightBelow;
        private System.Windows.Forms.Button infinity;
        private System.Windows.Forms.Button random;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button ShowImage;
        private System.Windows.Forms.Button AlrimchangBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button button1;
    }
}

