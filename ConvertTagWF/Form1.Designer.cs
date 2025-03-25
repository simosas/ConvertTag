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
            DeltaToSiemensOpc = new Button();
            SchneiderToSiemensOPC = new Button();
            SiemensOpcPrefixTextBox = new TextBox();
            DeltaBankToSiemensList = new Button();
            SchneiderStlToScl = new Button();
            ResultsTextBox = new RichTextBox();
            ShowResultsCheckbox = new CheckBox();
            UnmapButton = new Button();
            SiemensModbusConnection = new TextBox();
            OpenFileButton = new Button();
            SaveInputTxtButton = new Button();
            SaveOutputTxtButton = new Button();
            DeltaSiemensPlcDB = new Button();
            SchneiderSiemensPlcDB = new Button();
            HelpButton = new Button();
            SuspendLayout();
            // 
            // InputTextBox
            // 
            InputTextBox.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InputTextBox.Location = new Point(12, 55);
            InputTextBox.Name = "InputTextBox";
            InputTextBox.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            InputTextBox.Size = new Size(517, 559);
            InputTextBox.TabIndex = 0;
            InputTextBox.Text = "";
            InputTextBox.WordWrap = false;
            // 
            // OutputTextBox
            // 
            OutputTextBox.BackColor = SystemColors.Window;
            OutputTextBox.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            OutputTextBox.Location = new Point(535, 55);
            OutputTextBox.Name = "OutputTextBox";
            OutputTextBox.ReadOnly = true;
            OutputTextBox.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            OutputTextBox.Size = new Size(540, 559);
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
            RemapCheckBox.Checked = true;
            RemapCheckBox.CheckState = CheckState.Checked;
            RemapCheckBox.Location = new Point(188, 22);
            RemapCheckBox.Name = "RemapCheckBox";
            RemapCheckBox.Size = new Size(122, 19);
            RemapCheckBox.TabIndex = 3;
            RemapCheckBox.Text = "Re-map addresses";
            RemapCheckBox.UseVisualStyleBackColor = true;
            // 
            // SendToClipCheckbox
            // 
            SendToClipCheckbox.AutoSize = true;
            SendToClipCheckbox.Location = new Point(316, 22);
            SendToClipCheckbox.Name = "SendToClipCheckbox";
            SendToClipCheckbox.Size = new Size(158, 19);
            SendToClipCheckbox.TabIndex = 4;
            SendToClipCheckbox.Text = "Send output to clipboard";
            SendToClipCheckbox.UseVisualStyleBackColor = true;
            // 
            // OutToDeltaHmi
            // 
            OutToDeltaHmi.Location = new Point(1266, 53);
            OutToDeltaHmi.Name = "OutToDeltaHmi";
            OutToDeltaHmi.Size = new Size(142, 36);
            OutToDeltaHmi.TabIndex = 5;
            OutToDeltaHmi.Text = "Output -> Delta HMI";
            OutToDeltaHmi.UseVisualStyleBackColor = true;
            OutToDeltaHmi.Click += OutToDeltaHmi_Click;
            // 
            // DeltaEtherlinkTextBox
            // 
            DeltaEtherlinkTextBox.Location = new Point(1266, 22);
            DeltaEtherlinkTextBox.Name = "DeltaEtherlinkTextBox";
            DeltaEtherlinkTextBox.Size = new Size(142, 23);
            DeltaEtherlinkTextBox.TabIndex = 6;
            DeltaEtherlinkTextBox.Text = "{EtherLink1}1@W4-";
            // 
            // OutToSiemensModbus
            // 
            OutToSiemensModbus.Location = new Point(1209, 143);
            OutToSiemensModbus.Name = "OutToSiemensModbus";
            OutToSiemensModbus.Size = new Size(199, 36);
            OutToSiemensModbus.TabIndex = 7;
            OutToSiemensModbus.Text = "Output -> Siemens HMI (Modbus)";
            OutToSiemensModbus.UseVisualStyleBackColor = true;
            OutToSiemensModbus.Click += OutToSiemensModbus_Click;
            // 
            // DeltaToSiemensOpc
            // 
            DeltaToSiemensOpc.BackColor = SystemColors.ControlLightLight;
            DeltaToSiemensOpc.FlatAppearance.BorderColor = Color.Black;
            DeltaToSiemensOpc.FlatAppearance.BorderSize = 2;
            DeltaToSiemensOpc.Location = new Point(1171, 252);
            DeltaToSiemensOpc.Name = "DeltaToSiemensOpc";
            DeltaToSiemensOpc.Size = new Size(238, 37);
            DeltaToSiemensOpc.TabIndex = 8;
            DeltaToSiemensOpc.Text = "Delta -> Siemens HMI (OPC UA)";
            DeltaToSiemensOpc.UseVisualStyleBackColor = false;
            DeltaToSiemensOpc.Click += DeltaToSiemensOpc_Click;
            // 
            // SchneiderToSiemensOPC
            // 
            SchneiderToSiemensOPC.Location = new Point(1171, 295);
            SchneiderToSiemensOPC.Name = "SchneiderToSiemensOPC";
            SchneiderToSiemensOPC.Size = new Size(237, 34);
            SchneiderToSiemensOPC.TabIndex = 9;
            SchneiderToSiemensOPC.Text = "Schneider  -> Siemens HMI (OPC UA)";
            SchneiderToSiemensOPC.UseVisualStyleBackColor = true;
            SchneiderToSiemensOPC.Click += SchneiderToSiemensOPC_Click;
            // 
            // SiemensOpcPrefixTextBox
            // 
            SiemensOpcPrefixTextBox.Location = new Point(1093, 223);
            SiemensOpcPrefixTextBox.Name = "SiemensOpcPrefixTextBox";
            SiemensOpcPrefixTextBox.Size = new Size(316, 23);
            SiemensOpcPrefixTextBox.TabIndex = 10;
            SiemensOpcPrefixTextBox.Text = "ns=urn:Schneider:M262:customprovider;s=Application.GVL.";
            // 
            // DeltaBankToSiemensList
            // 
            DeltaBankToSiemensList.Location = new Point(1209, 434);
            DeltaBankToSiemensList.Name = "DeltaBankToSiemensList";
            DeltaBankToSiemensList.Size = new Size(199, 23);
            DeltaBankToSiemensList.TabIndex = 12;
            DeltaBankToSiemensList.Text = "Delta TextBank -> Siemens TextList";
            DeltaBankToSiemensList.UseVisualStyleBackColor = true;
            DeltaBankToSiemensList.Visible = false;
            DeltaBankToSiemensList.Click += DeltaBankToSiemensList_Click;
            // 
            // SchneiderStlToScl
            // 
            SchneiderStlToScl.Location = new Point(1210, 463);
            SchneiderStlToScl.Name = "SchneiderStlToScl";
            SchneiderStlToScl.Size = new Size(199, 25);
            SchneiderStlToScl.TabIndex = 13;
            SchneiderStlToScl.Text = "Schneider STL -> Siemens SCL";
            SchneiderStlToScl.UseVisualStyleBackColor = true;
            SchneiderStlToScl.Click += SchneiderStlToScl_Click;
            // 
            // ResultsTextBox
            // 
            ResultsTextBox.Location = new Point(12, 620);
            ResultsTextBox.Name = "ResultsTextBox";
            ResultsTextBox.Size = new Size(1063, 81);
            ResultsTextBox.TabIndex = 16;
            ResultsTextBox.Text = "";
            // 
            // ShowResultsCheckbox
            // 
            ShowResultsCheckbox.AutoSize = true;
            ShowResultsCheckbox.Checked = true;
            ShowResultsCheckbox.CheckState = CheckState.Checked;
            ShowResultsCheckbox.Location = new Point(1081, 639);
            ShowResultsCheckbox.Name = "ShowResultsCheckbox";
            ShowResultsCheckbox.Size = new Size(137, 19);
            ShowResultsCheckbox.TabIndex = 17;
            ShowResultsCheckbox.Text = "Show results window";
            ShowResultsCheckbox.UseVisualStyleBackColor = true;
            ShowResultsCheckbox.CheckedChanged += ShowResultsCheckbox_CheckedChanged;
            // 
            // UnmapButton
            // 
            UnmapButton.Location = new Point(480, 18);
            UnmapButton.Name = "UnmapButton";
            UnmapButton.Size = new Size(86, 26);
            UnmapButton.TabIndex = 19;
            UnmapButton.Text = "Unmap";
            UnmapButton.UseVisualStyleBackColor = true;
            UnmapButton.Click += UnmapButton_Click;
            // 
            // SiemensModbusConnection
            // 
            SiemensModbusConnection.Location = new Point(1266, 114);
            SiemensModbusConnection.Name = "SiemensModbusConnection";
            SiemensModbusConnection.Size = new Size(142, 23);
            SiemensModbusConnection.TabIndex = 20;
            SiemensModbusConnection.Text = "Connection_1";
            // 
            // OpenFileButton
            // 
            OpenFileButton.Location = new Point(572, 18);
            OpenFileButton.Name = "OpenFileButton";
            OpenFileButton.Size = new Size(86, 26);
            OpenFileButton.TabIndex = 21;
            OpenFileButton.Text = "Open";
            OpenFileButton.UseVisualStyleBackColor = true;
            OpenFileButton.Click += OpenFileButton_Click;
            // 
            // SaveInputTxtButton
            // 
            SaveInputTxtButton.Location = new Point(664, 18);
            SaveInputTxtButton.Name = "SaveInputTxtButton";
            SaveInputTxtButton.Size = new Size(115, 26);
            SaveInputTxtButton.TabIndex = 22;
            SaveInputTxtButton.Text = "Save input as .txt";
            SaveInputTxtButton.UseVisualStyleBackColor = true;
            SaveInputTxtButton.Click += SaveInputTxtButton_Click;
            // 
            // SaveOutputTxtButton
            // 
            SaveOutputTxtButton.Location = new Point(785, 18);
            SaveOutputTxtButton.Name = "SaveOutputTxtButton";
            SaveOutputTxtButton.Size = new Size(115, 26);
            SaveOutputTxtButton.TabIndex = 23;
            SaveOutputTxtButton.Text = "Save output as .txt";
            SaveOutputTxtButton.UseVisualStyleBackColor = true;
            SaveOutputTxtButton.Click += SaveOutputTxtButton_Click;
            // 
            // DeltaSiemensPlcDB
            // 
            DeltaSiemensPlcDB.Location = new Point(1210, 494);
            DeltaSiemensPlcDB.Name = "DeltaSiemensPlcDB";
            DeltaSiemensPlcDB.Size = new Size(199, 23);
            DeltaSiemensPlcDB.TabIndex = 24;
            DeltaSiemensPlcDB.Text = "Delta  -> Siemens PLC DB";
            DeltaSiemensPlcDB.UseVisualStyleBackColor = true;
            DeltaSiemensPlcDB.Click += DeltaSiemensPlcDB_Click;
            // 
            // SchneiderSiemensPlcDB
            // 
            SchneiderSiemensPlcDB.Location = new Point(1210, 523);
            SchneiderSiemensPlcDB.Name = "SchneiderSiemensPlcDB";
            SchneiderSiemensPlcDB.Size = new Size(199, 23);
            SchneiderSiemensPlcDB.TabIndex = 25;
            SchneiderSiemensPlcDB.Text = "Schneider -> Siemens PLC DB";
            SchneiderSiemensPlcDB.UseVisualStyleBackColor = true;
            SchneiderSiemensPlcDB.Click += SchneiderSiemensPlcDB_Click;
            // 
            // HelpButton
            // 
            HelpButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            HelpButton.Location = new Point(1325, 625);
            HelpButton.Name = "HelpButton";
            HelpButton.Size = new Size(83, 39);
            HelpButton.TabIndex = 26;
            HelpButton.Text = "Help";
            HelpButton.UseVisualStyleBackColor = true;
            HelpButton.Click += HelpButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(1421, 713);
            Controls.Add(HelpButton);
            Controls.Add(SchneiderSiemensPlcDB);
            Controls.Add(DeltaSiemensPlcDB);
            Controls.Add(SaveOutputTxtButton);
            Controls.Add(SaveInputTxtButton);
            Controls.Add(OpenFileButton);
            Controls.Add(SiemensModbusConnection);
            Controls.Add(UnmapButton);
            Controls.Add(ShowResultsCheckbox);
            Controls.Add(ResultsTextBox);
            Controls.Add(SchneiderStlToScl);
            Controls.Add(DeltaBankToSiemensList);
            Controls.Add(SiemensOpcPrefixTextBox);
            Controls.Add(SchneiderToSiemensOPC);
            Controls.Add(DeltaToSiemensOpc);
            Controls.Add(OutToSiemensModbus);
            Controls.Add(DeltaEtherlinkTextBox);
            Controls.Add(OutToDeltaHmi);
            Controls.Add(SendToClipCheckbox);
            Controls.Add(RemapCheckBox);
            Controls.Add(InputToOutputButton);
            Controls.Add(OutputTextBox);
            Controls.Add(InputTextBox);
            Name = "Form1";
            Text = "Mapper";
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
        private Button DeltaToSiemensOpc;
        private Button SchneiderToSiemensOPC;
        private TextBox SiemensOpcPrefixTextBox;
        private Button DeltaBankToSiemensList;
        private Button SchneiderStlToScl;
        private RichTextBox ResultsTextBox;
        private CheckBox ShowResultsCheckbox;
        private Button UnmapButton;
        private TextBox SiemensModbusConnection;
        private Button OpenFileButton;
        private Button SaveInputTxtButton;
        private Button SaveOutputTxtButton;
        private Button DeltaSiemensPlcDB;
        private Button SchneiderSiemensPlcDB;
        private Button HelpButton;
    }
}
