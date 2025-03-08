namespace ConvertTagWF
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
            richTextBox1 = new RichTextBox();
            richTextBox2 = new RichTextBox();
            button1 = new Button();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            button2 = new Button();
            textBox1 = new TextBox();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            textBox2 = new TextBox();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            richTextBox3 = new RichTextBox();
            checkBox4 = new CheckBox();
            button10 = new Button();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 55);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(380, 559);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(398, 55);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(380, 559);
            richTextBox2.TabIndex = 1;
            richTextBox2.Text = "";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(170, 37);
            button1.TabIndex = 2;
            button1.Text = " >>>";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(1037, 528);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(122, 19);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Re-map addresses";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(1036, 562);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(158, 19);
            checkBox2.TabIndex = 4;
            checkBox2.Text = "Send output to clipboard";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(1037, 55);
            button2.Name = "button2";
            button2.Size = new Size(142, 36);
            button2.TabIndex = 5;
            button2.Text = "Output -> Delta HMI";
            button2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(1037, 24);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(142, 23);
            textBox1.TabIndex = 6;
            textBox1.Text = "{EtherLink1}1@W4-";
            // 
            // button3
            // 
            button3.Location = new Point(952, 122);
            button3.Name = "button3";
            button3.Size = new Size(227, 36);
            button3.TabIndex = 7;
            button3.Text = "Convert -> Siemens HMI (Modbus)";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ControlLightLight;
            button4.FlatAppearance.BorderColor = Color.Black;
            button4.FlatAppearance.BorderSize = 2;
            button4.Location = new Point(952, 266);
            button4.Name = "button4";
            button4.Size = new Size(227, 36);
            button4.TabIndex = 8;
            button4.Text = "Convert -> Siemens HMI (OPC UA)";
            button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.Location = new Point(952, 308);
            button5.Name = "button5";
            button5.Size = new Size(227, 36);
            button5.TabIndex = 9;
            button5.Text = "Convert -> Siemens PLC DB";
            button5.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(851, 237);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(328, 23);
            textBox2.TabIndex = 10;
            textBox2.Text = "ns=urn:Schneider:M262:customprovider;s=Application.GVL.";
            // 
            // button6
            // 
            button6.Location = new Point(952, 350);
            button6.Name = "button6";
            button6.Size = new Size(227, 36);
            button6.TabIndex = 11;
            button6.Text = "Schneider -> Siemens PLC DB";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(952, 392);
            button7.Name = "button7";
            button7.Size = new Size(227, 36);
            button7.TabIndex = 12;
            button7.Text = "Schneider -> Siemens HMI";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.Location = new Point(952, 434);
            button8.Name = "button8";
            button8.Size = new Size(227, 36);
            button8.TabIndex = 13;
            button8.Text = "Schneider STL -> Siemens SCL";
            button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Location = new Point(927, 476);
            button9.Name = "button9";
            button9.Size = new Size(252, 36);
            button9.TabIndex = 14;
            button9.Text = "Delta HMI TextBank -> Siemens HMI TextList";
            button9.UseVisualStyleBackColor = true;
            // 
            // richTextBox3
            // 
            richTextBox3.Location = new Point(12, 620);
            richTextBox3.Name = "richTextBox3";
            richTextBox3.Size = new Size(766, 81);
            richTextBox3.TabIndex = 16;
            richTextBox3.Text = "";
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Checked = true;
            checkBox4.CheckState = CheckState.Checked;
            checkBox4.Location = new Point(1036, 595);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(137, 19);
            checkBox4.TabIndex = 17;
            checkBox4.Text = "Show results window";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            button10.Location = new Point(1037, 630);
            button10.Name = "button10";
            button10.Size = new Size(136, 32);
            button10.TabIndex = 18;
            button10.Text = "File as input...";
            button10.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1206, 713);
            Controls.Add(button10);
            Controls.Add(checkBox4);
            Controls.Add(richTextBox3);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(textBox2);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(button1);
            Controls.Add(richTextBox2);
            Controls.Add(richTextBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private Button button1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Button button2;
        private TextBox textBox1;
        private Button button3;
        private Button button4;
        private Button button5;
        private TextBox textBox2;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private RichTextBox richTextBox3;
        private CheckBox checkBox4;
        private Button button10;
    }
}
