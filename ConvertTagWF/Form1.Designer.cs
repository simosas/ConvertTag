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
            InputTextBox = new RichTextBox();
            OutputTextBox = new RichTextBox();
            InputToOutputButton = new Button();
            RemapCheckBox = new CheckBox();
            SendToClipCheckbox = new CheckBox();
            OutToDeltaHmi = new Button();
            DeltaEtherlinkTextBox = new TextBox();
            OutToSiemensModbus = new Button();
            OutToSiemensOpc = new Button();
            OutToSiemensDB = new Button();
            SiemensOpcPrefixTextBox = new TextBox();
            OutToSiemensPLC = new Button();
            OutToSiemensHmi = new Button();
            SchneiderStlToScl = new Button();
            ResultsTextBox = new RichTextBox();
            ShowResultsCheckbox = new CheckBox();
            button10 = new Button();
            SuspendLayout();
            // 
            // InputTextBox
            // 
            InputTextBox.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InputTextBox.Location = new Point(12, 55);
            InputTextBox.Name = "InputTextBox";
            InputTextBox.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            InputTextBox.Size = new Size(380, 559);
            InputTextBox.TabIndex = 0;
            InputTextBox.Text = "";
            InputTextBox.WordWrap = false;
            // 
            // OutputTextBox
            // 
            OutputTextBox.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            OutputTextBox.Location = new Point(398, 55);
            OutputTextBox.Name = "OutputTextBox";
            OutputTextBox.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            OutputTextBox.Size = new Size(380, 559);
            OutputTextBox.TabIndex = 1;
            OutputTextBox.Text = "";
            OutputTextBox.WordWrap = false;
            // 
            // InputToOutputButton
            // 
            InputToOutputButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            InputToOutputButton.Location = new Point(12, 12);
            InputToOutputButton.Name = "InputToOutputButton";
            InputToOutputButton.Size = new Size(170, 37);
            InputToOutputButton.TabIndex = 2;
            InputToOutputButton.Text = " >>>";
            InputToOutputButton.UseVisualStyleBackColor = true;
            InputToOutputButton.Click += InputToOutputButton_Click;
            // 
            // RemapCheckBox
            // 
            RemapCheckBox.AutoSize = true;
            RemapCheckBox.Location = new Point(1037, 528);
            RemapCheckBox.Name = "RemapCheckBox";
            RemapCheckBox.Size = new Size(122, 19);
            RemapCheckBox.TabIndex = 3;
            RemapCheckBox.Text = "Re-map addresses";
            RemapCheckBox.UseVisualStyleBackColor = true;
            // 
            // SendToClipCheckbox
            // 
            SendToClipCheckbox.AutoSize = true;
            SendToClipCheckbox.Location = new Point(1036, 562);
            SendToClipCheckbox.Name = "SendToClipCheckbox";
            SendToClipCheckbox.Size = new Size(158, 19);
            SendToClipCheckbox.TabIndex = 4;
            SendToClipCheckbox.Text = "Send output to clipboard";
            SendToClipCheckbox.UseVisualStyleBackColor = true;
            // 
            // OutToDeltaHmi
            // 
            OutToDeltaHmi.Location = new Point(1037, 55);
            OutToDeltaHmi.Name = "OutToDeltaHmi";
            OutToDeltaHmi.Size = new Size(142, 36);
            OutToDeltaHmi.TabIndex = 5;
            OutToDeltaHmi.Text = "Output -> Delta HMI";
            OutToDeltaHmi.UseVisualStyleBackColor = true;
            // 
            // DeltaEtherlinkTextBox
            // 
            DeltaEtherlinkTextBox.Location = new Point(1037, 24);
            DeltaEtherlinkTextBox.Name = "DeltaEtherlinkTextBox";
            DeltaEtherlinkTextBox.Size = new Size(142, 23);
            DeltaEtherlinkTextBox.TabIndex = 6;
            DeltaEtherlinkTextBox.Text = "{EtherLink1}1@W4-";
            // 
            // OutToSiemensModbus
            // 
            OutToSiemensModbus.Location = new Point(952, 97);
            OutToSiemensModbus.Name = "OutToSiemensModbus";
            OutToSiemensModbus.Size = new Size(227, 36);
            OutToSiemensModbus.TabIndex = 7;
            OutToSiemensModbus.Text = "Output -> Siemens HMI (Modbus)";
            OutToSiemensModbus.UseVisualStyleBackColor = true;
            OutToSiemensModbus.Click += OutToSiemensModbus_Click;
            // 
            // OutToSiemensOpc
            // 
            OutToSiemensOpc.BackColor = SystemColors.ControlLightLight;
            OutToSiemensOpc.FlatAppearance.BorderColor = Color.Black;
            OutToSiemensOpc.FlatAppearance.BorderSize = 2;
            OutToSiemensOpc.Location = new Point(952, 266);
            OutToSiemensOpc.Name = "OutToSiemensOpc";
            OutToSiemensOpc.Size = new Size(227, 36);
            OutToSiemensOpc.TabIndex = 8;
            OutToSiemensOpc.Text = "Output -> Siemens HMI (OPC UA)";
            OutToSiemensOpc.UseVisualStyleBackColor = false;
            OutToSiemensOpc.Visible = false;
            // 
            // OutToSiemensDB
            // 
            OutToSiemensDB.Location = new Point(952, 308);
            OutToSiemensDB.Name = "OutToSiemensDB";
            OutToSiemensDB.Size = new Size(227, 36);
            OutToSiemensDB.TabIndex = 9;
            OutToSiemensDB.Text = "Output -> Siemens PLC DB";
            OutToSiemensDB.UseVisualStyleBackColor = true;
            OutToSiemensDB.Visible = false;
            // 
            // SiemensOpcPrefixTextBox
            // 
            SiemensOpcPrefixTextBox.Location = new Point(851, 237);
            SiemensOpcPrefixTextBox.Name = "SiemensOpcPrefixTextBox";
            SiemensOpcPrefixTextBox.Size = new Size(328, 23);
            SiemensOpcPrefixTextBox.TabIndex = 10;
            SiemensOpcPrefixTextBox.Text = "ns=urn:Schneider:M262:customprovider;s=Application.GVL.";
            SiemensOpcPrefixTextBox.Visible = false;
            // 
            // OutToSiemensPLC
            // 
            OutToSiemensPLC.Location = new Point(952, 350);
            OutToSiemensPLC.Name = "OutToSiemensPLC";
            OutToSiemensPLC.Size = new Size(227, 36);
            OutToSiemensPLC.TabIndex = 11;
            OutToSiemensPLC.Text = "Output -> Siemens PLC";
            OutToSiemensPLC.UseVisualStyleBackColor = true;
            OutToSiemensPLC.Visible = false;
            // 
            // OutToSiemensHmi
            // 
            OutToSiemensHmi.Location = new Point(952, 392);
            OutToSiemensHmi.Name = "OutToSiemensHmi";
            OutToSiemensHmi.Size = new Size(227, 36);
            OutToSiemensHmi.TabIndex = 12;
            OutToSiemensHmi.Text = "Output -> Siemens HMI";
            OutToSiemensHmi.UseVisualStyleBackColor = true;
            OutToSiemensHmi.Visible = false;
            // 
            // SchneiderStlToScl
            // 
            SchneiderStlToScl.Location = new Point(952, 450);
            SchneiderStlToScl.Name = "SchneiderStlToScl";
            SchneiderStlToScl.Size = new Size(227, 36);
            SchneiderStlToScl.TabIndex = 13;
            SchneiderStlToScl.Text = "Schneider STL -> Siemens SCL";
            SchneiderStlToScl.UseVisualStyleBackColor = true;
            SchneiderStlToScl.Visible = false;
            // 
            // ResultsTextBox
            // 
            ResultsTextBox.Location = new Point(12, 620);
            ResultsTextBox.Name = "ResultsTextBox";
            ResultsTextBox.Size = new Size(766, 81);
            ResultsTextBox.TabIndex = 16;
            ResultsTextBox.Text = "";
            // 
            // ShowResultsCheckbox
            // 
            ShowResultsCheckbox.AutoSize = true;
            ShowResultsCheckbox.Checked = true;
            ShowResultsCheckbox.CheckState = CheckState.Checked;
            ShowResultsCheckbox.Location = new Point(1036, 595);
            ShowResultsCheckbox.Name = "ShowResultsCheckbox";
            ShowResultsCheckbox.Size = new Size(137, 19);
            ShowResultsCheckbox.TabIndex = 17;
            ShowResultsCheckbox.Text = "Show results window";
            ShowResultsCheckbox.UseVisualStyleBackColor = true;
            ShowResultsCheckbox.CheckedChanged += ShowResultsCheckbox_CheckedChanged;
            // 
            // button10
            // 
            button10.Location = new Point(1037, 630);
            button10.Name = "button10";
            button10.Size = new Size(136, 32);
            button10.TabIndex = 18;
            button10.Text = "File as input...";
            button10.UseVisualStyleBackColor = true;
            button10.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1206, 713);
            Controls.Add(button10);
            Controls.Add(ShowResultsCheckbox);
            Controls.Add(ResultsTextBox);
            Controls.Add(SchneiderStlToScl);
            Controls.Add(OutToSiemensHmi);
            Controls.Add(OutToSiemensPLC);
            Controls.Add(SiemensOpcPrefixTextBox);
            Controls.Add(OutToSiemensDB);
            Controls.Add(OutToSiemensOpc);
            Controls.Add(OutToSiemensModbus);
            Controls.Add(DeltaEtherlinkTextBox);
            Controls.Add(OutToDeltaHmi);
            Controls.Add(SendToClipCheckbox);
            Controls.Add(RemapCheckBox);
            Controls.Add(InputToOutputButton);
            Controls.Add(OutputTextBox);
            Controls.Add(InputTextBox);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox InputTextBox;
        private RichTextBox OutputTextBox;
        private Button InputToOutputButton;
        private CheckBox RemapCheckBox;
        private CheckBox SendToClipCheckbox;
        private Button OutToDeltaHmi;
        private TextBox DeltaEtherlinkTextBox;
        private Button OutToSiemensModbus;
        private Button OutToSiemensOpc;
        private Button OutToSiemensDB;
        private TextBox SiemensOpcPrefixTextBox;
        private Button OutToSiemensPLC;
        private Button OutToSiemensHmi;
        private Button SchneiderStlToScl;
        private RichTextBox ResultsTextBox;
        private CheckBox ShowResultsCheckbox;
        private Button button10;
    }
}
