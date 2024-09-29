namespace Tool.CompressImage
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
            groupBox2 = new GroupBox();
            btnStart = new Button();
            btn_set_reset = new Button();
            groupBox1 = new GroupBox();
            label7 = new Label();
            label8 = new Label();
            label1 = new Label();
            input_quality = new TextBox();
            input_resize_height = new TextBox();
            label3 = new Label();
            label6 = new Label();
            radioButton1 = new RadioButton();
            input_resize_width = new TextBox();
            label4 = new Label();
            label5 = new Label();
            radioButton2 = new RadioButton();
            progressBar_compress = new ProgressBar();
            label2 = new Label();
            txtResultInfo = new TextBox();
            tbSelectedPath = new TextBox();
            button1 = new Button();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnStart);
            groupBox2.Location = new Point(14, 214);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(384, 394);
            groupBox2.TabIndex = 27;
            groupBox2.TabStop = false;
            groupBox2.Text = "压缩：点击图片文件夹，然后点击开始处理";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(110, 91);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(153, 52);
            btnStart.TabIndex = 4;
            btnStart.Text = "开始处理";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btn_set_reset
            // 
            btn_set_reset.Location = new Point(147, 137);
            btn_set_reset.Name = "btn_set_reset";
            btn_set_reset.Size = new Size(75, 34);
            btn_set_reset.TabIndex = 18;
            btn_set_reset.Text = "恢复默认";
            btn_set_reset.UseVisualStyleBackColor = true;
            btn_set_reset.Click += btn_set_reset_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_set_reset);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(input_quality);
            groupBox1.Controls.Add(input_resize_height);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(input_resize_width);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Location = new Point(14, 11);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(384, 185);
            groupBox1.TabIndex = 25;
            groupBox1.TabStop = false;
            groupBox1.Text = "设置";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.DarkCyan;
            label7.Location = new Point(198, 63);
            label7.Name = "label7";
            label7.Size = new Size(176, 17);
            label7.TabIndex = 16;
            label7.Text = "超出宽或高，缩放到设置的宽高";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.DarkCyan;
            label8.Location = new Point(199, 24);
            label8.Name = "label8";
            label8.Size = new Size(165, 17);
            label8.TabIndex = 17;
            label8.Text = "质量请输入：1 ~ 100 的整数";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 24);
            label1.Name = "label1";
            label1.Size = new Size(0, 17);
            label1.TabIndex = 0;
            // 
            // input_quality
            // 
            input_quality.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            input_quality.Location = new Point(67, 20);
            input_quality.Name = "input_quality";
            input_quality.Size = new Size(93, 23);
            input_quality.TabIndex = 7;
            input_quality.Text = "75";
            input_quality.TextAlign = HorizontalAlignment.Center;
            // 
            // input_resize_height
            // 
            input_resize_height.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            input_resize_height.Location = new Point(218, 100);
            input_resize_height.Name = "input_resize_height";
            input_resize_height.Size = new Size(64, 23);
            input_resize_height.TabIndex = 15;
            input_resize_height.Text = "1080";
            input_resize_height.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(2, 23);
            label3.Name = "label3";
            label3.Size = new Size(59, 17);
            label3.TabIndex = 8;
            label3.Text = "图片质量:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(189, 103);
            label6.Name = "label6";
            label6.Size = new Size(23, 17);
            label6.TabIndex = 14;
            label6.Text = "高:";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(70, 63);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(38, 21);
            radioButton1.TabIndex = 9;
            radioButton1.TabStop = true;
            radioButton1.Text = "是";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // input_resize_width
            // 
            input_resize_width.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            input_resize_width.Location = new Point(100, 100);
            input_resize_width.Name = "input_resize_width";
            input_resize_width.Size = new Size(60, 23);
            input_resize_width.TabIndex = 13;
            input_resize_width.Text = "1920";
            input_resize_width.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 64);
            label4.Name = "label4";
            label4.Size = new Size(59, 17);
            label4.TabIndex = 10;
            label4.Text = "比例缩放:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(71, 103);
            label5.Name = "label5";
            label5.Size = new Size(23, 17);
            label5.TabIndex = 12;
            label5.Text = "宽:";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(122, 63);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(38, 21);
            radioButton2.TabIndex = 11;
            radioButton2.Text = "否";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // progressBar_compress
            // 
            progressBar_compress.Location = new Point(65, 374);
            progressBar_compress.Name = "progressBar_compress";
            progressBar_compress.Size = new Size(323, 23);
            progressBar_compress.Step = 1;
            progressBar_compress.TabIndex = 26;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 377);
            label2.Name = "label2";
            label2.Size = new Size(35, 17);
            label2.TabIndex = 24;
            label2.Text = "进度:";
            // 
            // txtResultInfo
            // 
            txtResultInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtResultInfo.CausesValidation = false;
            txtResultInfo.ImeMode = ImeMode.NoControl;
            txtResultInfo.Location = new Point(24, 408);
            txtResultInfo.MaxLength = 0;
            txtResultInfo.Multiline = true;
            txtResultInfo.Name = "txtResultInfo";
            txtResultInfo.ReadOnly = true;
            txtResultInfo.ScrollBars = ScrollBars.Vertical;
            txtResultInfo.Size = new Size(364, 197);
            txtResultInfo.TabIndex = 23;
            // 
            // tbSelectedPath
            // 
            tbSelectedPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbSelectedPath.Location = new Point(26, 241);
            tbSelectedPath.Multiline = true;
            tbSelectedPath.Name = "tbSelectedPath";
            tbSelectedPath.ReadOnly = true;
            tbSelectedPath.Size = new Size(260, 50);
            tbSelectedPath.TabIndex = 22;
            // 
            // button1
            // 
            button1.Location = new Point(288, 240);
            button1.Name = "button1";
            button1.Size = new Size(100, 52);
            button1.TabIndex = 21;
            button1.Text = "图片文件夹";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(412, 619);
            Controls.Add(progressBar_compress);
            Controls.Add(label2);
            Controls.Add(txtResultInfo);
            Controls.Add(tbSelectedPath);
            Controls.Add(button1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            MaximizeBox = false;
            MaximumSize = new Size(420, 650);
            MinimumSize = new Size(420, 650);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "图片压缩工具";
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox2;
        private Button btnStart;
        private Button btn_set_reset;
        private GroupBox groupBox1;
        private Label label7;
        private Label label8;
        private Label label1;
        private TextBox input_quality;
        private TextBox input_resize_height;
        private Label label3;
        private Label label6;
        private RadioButton radioButton1;
        private TextBox input_resize_width;
        private Label label4;
        private Label label5;
        private RadioButton radioButton2;
        private ProgressBar progressBar_compress;
        private Label label2;
        private TextBox txtResultInfo;
        private TextBox tbSelectedPath;
        private Button button1;
    }
}
