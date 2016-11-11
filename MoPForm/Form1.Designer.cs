namespace DADSTORM.MoPForm {
    partial class Dadform {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.ConfigBrowseButton = new System.Windows.Forms.Button();
            this.ConfigFileTextBox = new System.Windows.Forms.TextBox();
            this.ConfigGroup = new System.Windows.Forms.GroupBox();
            this.LoadConfigButton = new System.Windows.Forms.Button();
            this.ScriptGroup = new System.Windows.Forms.GroupBox();
            this.RunAllButton = new System.Windows.Forms.Button();
            this.RunNextButton = new System.Windows.Forms.Button();
            this.LoadScriptButton = new System.Windows.Forms.Button();
            this.ScriptFileTextBox = new System.Windows.Forms.TextBox();
            this.ScriptBrowseButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.CommandsGroup = new System.Windows.Forms.GroupBox();
            this.UnfreezeReplTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.UnfreezeOpIdTextBox = new System.Windows.Forms.TextBox();
            this.FreezeReplTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.FreezeOpIdTextBox = new System.Windows.Forms.TextBox();
            this.CrashReplTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.WaitTimeTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.WaitButton = new System.Windows.Forms.Button();
            this.UnfreezeButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.FreezeButton = new System.Windows.Forms.Button();
            this.CrashOpIdTextBox = new System.Windows.Forms.TextBox();
            this.CrashButton = new System.Windows.Forms.Button();
            this.StatusButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.IntervalTimeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.IntervalOpIdTextBox = new System.Windows.Forms.TextBox();
            this.IntervalButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.StartOpIdTextBox = new System.Windows.Forms.TextBox();
            this.HistoryTextBox = new System.Windows.Forms.RichTextBox();
            this.ConfigGroup.SuspendLayout();
            this.ScriptGroup.SuspendLayout();
            this.CommandsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConfigBrowseButton
            // 
            this.ConfigBrowseButton.Location = new System.Drawing.Point(377, 31);
            this.ConfigBrowseButton.Name = "ConfigBrowseButton";
            this.ConfigBrowseButton.Size = new System.Drawing.Size(65, 23);
            this.ConfigBrowseButton.TabIndex = 0;
            this.ConfigBrowseButton.Text = "Browse...";
            this.ConfigBrowseButton.UseVisualStyleBackColor = true;
            this.ConfigBrowseButton.Click += new System.EventHandler(this.ConfigBrowseButton_Click);
            // 
            // ConfigFileTextBox
            // 
            this.ConfigFileTextBox.Location = new System.Drawing.Point(71, 33);
            this.ConfigFileTextBox.Name = "ConfigFileTextBox";
            this.ConfigFileTextBox.Size = new System.Drawing.Size(300, 20);
            this.ConfigFileTextBox.TabIndex = 1;
            // 
            // ConfigGroup
            // 
            this.ConfigGroup.Controls.Add(this.LoadConfigButton);
            this.ConfigGroup.Controls.Add(this.ConfigFileTextBox);
            this.ConfigGroup.Controls.Add(this.ConfigBrowseButton);
            this.ConfigGroup.Location = new System.Drawing.Point(29, 21);
            this.ConfigGroup.Name = "ConfigGroup";
            this.ConfigGroup.Size = new System.Drawing.Size(530, 79);
            this.ConfigGroup.TabIndex = 5;
            this.ConfigGroup.TabStop = false;
            this.ConfigGroup.Text = "Configuration File";
            // 
            // LoadConfigButton
            // 
            this.LoadConfigButton.Location = new System.Drawing.Point(448, 31);
            this.LoadConfigButton.Name = "LoadConfigButton";
            this.LoadConfigButton.Size = new System.Drawing.Size(65, 23);
            this.LoadConfigButton.TabIndex = 2;
            this.LoadConfigButton.Text = "Load";
            this.LoadConfigButton.UseVisualStyleBackColor = true;
            this.LoadConfigButton.Click += new System.EventHandler(this.LoadConfigButton_Click);
            // 
            // ScriptGroup
            // 
            this.ScriptGroup.Controls.Add(this.RunAllButton);
            this.ScriptGroup.Controls.Add(this.RunNextButton);
            this.ScriptGroup.Controls.Add(this.LoadScriptButton);
            this.ScriptGroup.Controls.Add(this.ScriptFileTextBox);
            this.ScriptGroup.Controls.Add(this.ScriptBrowseButton);
            this.ScriptGroup.Enabled = false;
            this.ScriptGroup.Location = new System.Drawing.Point(29, 117);
            this.ScriptGroup.Name = "ScriptGroup";
            this.ScriptGroup.Size = new System.Drawing.Size(530, 115);
            this.ScriptGroup.TabIndex = 6;
            this.ScriptGroup.TabStop = false;
            this.ScriptGroup.Text = "Script File";
            // 
            // RunAllButton
            // 
            this.RunAllButton.Location = new System.Drawing.Point(142, 59);
            this.RunAllButton.Name = "RunAllButton";
            this.RunAllButton.Size = new System.Drawing.Size(65, 23);
            this.RunAllButton.TabIndex = 5;
            this.RunAllButton.Text = "Run All";
            this.RunAllButton.UseVisualStyleBackColor = true;
            this.RunAllButton.Click += new System.EventHandler(this.RunAllButton_Click);
            // 
            // RunNextButton
            // 
            this.RunNextButton.Location = new System.Drawing.Point(71, 59);
            this.RunNextButton.Name = "RunNextButton";
            this.RunNextButton.Size = new System.Drawing.Size(65, 23);
            this.RunNextButton.TabIndex = 4;
            this.RunNextButton.Text = "Run Next";
            this.RunNextButton.UseVisualStyleBackColor = true;
            this.RunNextButton.Click += new System.EventHandler(this.RunNextButton_Click);
            // 
            // LoadScriptButton
            // 
            this.LoadScriptButton.Location = new System.Drawing.Point(448, 31);
            this.LoadScriptButton.Name = "LoadScriptButton";
            this.LoadScriptButton.Size = new System.Drawing.Size(65, 23);
            this.LoadScriptButton.TabIndex = 3;
            this.LoadScriptButton.Text = "Load";
            this.LoadScriptButton.UseVisualStyleBackColor = true;
            this.LoadScriptButton.Click += new System.EventHandler(this.LoadScriptButton_Click);
            // 
            // ScriptFileTextBox
            // 
            this.ScriptFileTextBox.Location = new System.Drawing.Point(71, 33);
            this.ScriptFileTextBox.Name = "ScriptFileTextBox";
            this.ScriptFileTextBox.Size = new System.Drawing.Size(300, 20);
            this.ScriptFileTextBox.TabIndex = 1;
            // 
            // ScriptBrowseButton
            // 
            this.ScriptBrowseButton.Location = new System.Drawing.Point(377, 31);
            this.ScriptBrowseButton.Name = "ScriptBrowseButton";
            this.ScriptBrowseButton.Size = new System.Drawing.Size(65, 23);
            this.ScriptBrowseButton.TabIndex = 0;
            this.ScriptBrowseButton.Text = "Browse...";
            this.ScriptBrowseButton.UseVisualStyleBackColor = true;
            this.ScriptBrowseButton.Click += new System.EventHandler(this.ScriptBrowseButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(126, 29);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(65, 23);
            this.StartButton.TabIndex = 7;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // CommandsGroup
            // 
            this.CommandsGroup.Controls.Add(this.UnfreezeReplTextBox);
            this.CommandsGroup.Controls.Add(this.label5);
            this.CommandsGroup.Controls.Add(this.label6);
            this.CommandsGroup.Controls.Add(this.UnfreezeOpIdTextBox);
            this.CommandsGroup.Controls.Add(this.FreezeReplTextBox);
            this.CommandsGroup.Controls.Add(this.label9);
            this.CommandsGroup.Controls.Add(this.label10);
            this.CommandsGroup.Controls.Add(this.FreezeOpIdTextBox);
            this.CommandsGroup.Controls.Add(this.CrashReplTextBox);
            this.CommandsGroup.Controls.Add(this.label8);
            this.CommandsGroup.Controls.Add(this.WaitTimeTextBox);
            this.CommandsGroup.Controls.Add(this.label7);
            this.CommandsGroup.Controls.Add(this.WaitButton);
            this.CommandsGroup.Controls.Add(this.UnfreezeButton);
            this.CommandsGroup.Controls.Add(this.label4);
            this.CommandsGroup.Controls.Add(this.FreezeButton);
            this.CommandsGroup.Controls.Add(this.CrashOpIdTextBox);
            this.CommandsGroup.Controls.Add(this.CrashButton);
            this.CommandsGroup.Controls.Add(this.StatusButton);
            this.CommandsGroup.Controls.Add(this.label3);
            this.CommandsGroup.Controls.Add(this.IntervalTimeTextBox);
            this.CommandsGroup.Controls.Add(this.label2);
            this.CommandsGroup.Controls.Add(this.IntervalOpIdTextBox);
            this.CommandsGroup.Controls.Add(this.IntervalButton);
            this.CommandsGroup.Controls.Add(this.label1);
            this.CommandsGroup.Controls.Add(this.StartOpIdTextBox);
            this.CommandsGroup.Controls.Add(this.StartButton);
            this.CommandsGroup.Enabled = false;
            this.CommandsGroup.Location = new System.Drawing.Point(29, 250);
            this.CommandsGroup.Name = "CommandsGroup";
            this.CommandsGroup.Size = new System.Drawing.Size(530, 179);
            this.CommandsGroup.TabIndex = 8;
            this.CommandsGroup.TabStop = false;
            this.CommandsGroup.Text = "Commands";
            // 
            // UnfreezeReplTextBox
            // 
            this.UnfreezeReplTextBox.Location = new System.Drawing.Point(393, 95);
            this.UnfreezeReplTextBox.Name = "UnfreezeReplTextBox";
            this.UnfreezeReplTextBox.Size = new System.Drawing.Size(49, 20);
            this.UnfreezeReplTextBox.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(352, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Rep #";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(256, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Op ID";
            // 
            // UnfreezeOpIdTextBox
            // 
            this.UnfreezeOpIdTextBox.Location = new System.Drawing.Point(297, 95);
            this.UnfreezeOpIdTextBox.Name = "UnfreezeOpIdTextBox";
            this.UnfreezeOpIdTextBox.Size = new System.Drawing.Size(49, 20);
            this.UnfreezeOpIdTextBox.TabIndex = 34;
            // 
            // FreezeReplTextBox
            // 
            this.FreezeReplTextBox.Location = new System.Drawing.Point(393, 64);
            this.FreezeReplTextBox.Name = "FreezeReplTextBox";
            this.FreezeReplTextBox.Size = new System.Drawing.Size(49, 20);
            this.FreezeReplTextBox.TabIndex = 33;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(352, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Rep #";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(256, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "Op ID";
            // 
            // FreezeOpIdTextBox
            // 
            this.FreezeOpIdTextBox.Location = new System.Drawing.Point(297, 64);
            this.FreezeOpIdTextBox.Name = "FreezeOpIdTextBox";
            this.FreezeOpIdTextBox.Size = new System.Drawing.Size(49, 20);
            this.FreezeOpIdTextBox.TabIndex = 30;
            // 
            // CrashReplTextBox
            // 
            this.CrashReplTextBox.Location = new System.Drawing.Point(393, 32);
            this.CrashReplTextBox.Name = "CrashReplTextBox";
            this.CrashReplTextBox.Size = new System.Drawing.Size(49, 20);
            this.CrashReplTextBox.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(352, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Rep #";
            // 
            // WaitTimeTextBox
            // 
            this.WaitTimeTextBox.Location = new System.Drawing.Point(71, 135);
            this.WaitTimeTextBox.Name = "WaitTimeTextBox";
            this.WaitTimeTextBox.Size = new System.Drawing.Size(49, 20);
            this.WaitTimeTextBox.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Time (ms)";
            // 
            // WaitButton
            // 
            this.WaitButton.Location = new System.Drawing.Point(126, 133);
            this.WaitButton.Name = "WaitButton";
            this.WaitButton.Size = new System.Drawing.Size(65, 23);
            this.WaitButton.TabIndex = 25;
            this.WaitButton.Text = "Wait";
            this.WaitButton.UseVisualStyleBackColor = true;
            this.WaitButton.Click += new System.EventHandler(this.WaitButton_Click);
            // 
            // UnfreezeButton
            // 
            this.UnfreezeButton.Location = new System.Drawing.Point(448, 93);
            this.UnfreezeButton.Name = "UnfreezeButton";
            this.UnfreezeButton.Size = new System.Drawing.Size(65, 23);
            this.UnfreezeButton.TabIndex = 22;
            this.UnfreezeButton.Text = "Unfreeze";
            this.UnfreezeButton.UseVisualStyleBackColor = true;
            this.UnfreezeButton.Click += new System.EventHandler(this.UnfreezeButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(256, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Op ID";
            // 
            // FreezeButton
            // 
            this.FreezeButton.Location = new System.Drawing.Point(448, 61);
            this.FreezeButton.Name = "FreezeButton";
            this.FreezeButton.Size = new System.Drawing.Size(65, 23);
            this.FreezeButton.TabIndex = 19;
            this.FreezeButton.Text = "Freeze";
            this.FreezeButton.UseVisualStyleBackColor = true;
            this.FreezeButton.Click += new System.EventHandler(this.FreezeButton_Click);
            // 
            // CrashOpIdTextBox
            // 
            this.CrashOpIdTextBox.Location = new System.Drawing.Point(297, 32);
            this.CrashOpIdTextBox.Name = "CrashOpIdTextBox";
            this.CrashOpIdTextBox.Size = new System.Drawing.Size(49, 20);
            this.CrashOpIdTextBox.TabIndex = 17;
            // 
            // CrashButton
            // 
            this.CrashButton.Location = new System.Drawing.Point(448, 31);
            this.CrashButton.Name = "CrashButton";
            this.CrashButton.Size = new System.Drawing.Size(65, 23);
            this.CrashButton.TabIndex = 16;
            this.CrashButton.Text = "Crash";
            this.CrashButton.UseVisualStyleBackColor = true;
            this.CrashButton.Click += new System.EventHandler(this.CrashButton_Click);
            // 
            // StatusButton
            // 
            this.StatusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusButton.Location = new System.Drawing.Point(448, 131);
            this.StatusButton.Name = "StatusButton";
            this.StatusButton.Size = new System.Drawing.Size(65, 24);
            this.StatusButton.TabIndex = 15;
            this.StatusButton.Text = "Status";
            this.StatusButton.UseVisualStyleBackColor = true;
            this.StatusButton.Click += new System.EventHandler(this.StatusButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Op ID";
            // 
            // IntervalTimeTextBox
            // 
            this.IntervalTimeTextBox.Location = new System.Drawing.Point(71, 95);
            this.IntervalTimeTextBox.Name = "IntervalTimeTextBox";
            this.IntervalTimeTextBox.Size = new System.Drawing.Size(49, 20);
            this.IntervalTimeTextBox.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Time (ms)";
            // 
            // IntervalOpIdTextBox
            // 
            this.IntervalOpIdTextBox.Location = new System.Drawing.Point(71, 71);
            this.IntervalOpIdTextBox.Name = "IntervalOpIdTextBox";
            this.IntervalOpIdTextBox.Size = new System.Drawing.Size(49, 20);
            this.IntervalOpIdTextBox.TabIndex = 11;
            // 
            // IntervalButton
            // 
            this.IntervalButton.Location = new System.Drawing.Point(126, 93);
            this.IntervalButton.Name = "IntervalButton";
            this.IntervalButton.Size = new System.Drawing.Size(65, 23);
            this.IntervalButton.TabIndex = 10;
            this.IntervalButton.Text = "Interval";
            this.IntervalButton.UseVisualStyleBackColor = true;
            this.IntervalButton.Click += new System.EventHandler(this.IntervalButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Op ID";
            // 
            // StartOpIdTextBox
            // 
            this.StartOpIdTextBox.Location = new System.Drawing.Point(71, 31);
            this.StartOpIdTextBox.Name = "StartOpIdTextBox";
            this.StartOpIdTextBox.Size = new System.Drawing.Size(49, 20);
            this.StartOpIdTextBox.TabIndex = 8;
            // 
            // HistoryTextBox
            // 
            this.HistoryTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.HistoryTextBox.Location = new System.Drawing.Point(29, 450);
            this.HistoryTextBox.Name = "HistoryTextBox";
            this.HistoryTextBox.ReadOnly = true;
            this.HistoryTextBox.Size = new System.Drawing.Size(530, 279);
            this.HistoryTextBox.TabIndex = 9;
            this.HistoryTextBox.Text = "";
            // 
            // Dadform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 754);
            this.Controls.Add(this.HistoryTextBox);
            this.Controls.Add(this.CommandsGroup);
            this.Controls.Add(this.ScriptGroup);
            this.Controls.Add(this.ConfigGroup);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(605, 793);
            this.MinimumSize = new System.Drawing.Size(605, 793);
            this.Name = "Dadform";
            this.Text = "DADSTORM";
            this.ConfigGroup.ResumeLayout(false);
            this.ConfigGroup.PerformLayout();
            this.ScriptGroup.ResumeLayout(false);
            this.ScriptGroup.PerformLayout();
            this.CommandsGroup.ResumeLayout(false);
            this.CommandsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ConfigBrowseButton;
        private System.Windows.Forms.TextBox ConfigFileTextBox;
        private System.Windows.Forms.GroupBox ConfigGroup;
        private System.Windows.Forms.Button LoadConfigButton;
        private System.Windows.Forms.GroupBox ScriptGroup;
        private System.Windows.Forms.Button LoadScriptButton;
        private System.Windows.Forms.TextBox ScriptFileTextBox;
        private System.Windows.Forms.Button ScriptBrowseButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.GroupBox CommandsGroup;
        private System.Windows.Forms.Button FreezeButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CrashOpIdTextBox;
        private System.Windows.Forms.Button CrashButton;
        private System.Windows.Forms.Button StatusButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox IntervalTimeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IntervalOpIdTextBox;
        private System.Windows.Forms.Button IntervalButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox StartOpIdTextBox;
        private System.Windows.Forms.Button UnfreezeButton;
        private System.Windows.Forms.TextBox WaitTimeTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button WaitButton;
        private System.Windows.Forms.RichTextBox HistoryTextBox;
        private System.Windows.Forms.TextBox UnfreezeReplTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox UnfreezeOpIdTextBox;
        private System.Windows.Forms.TextBox FreezeReplTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox FreezeOpIdTextBox;
        private System.Windows.Forms.TextBox CrashReplTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button RunAllButton;
        private System.Windows.Forms.Button RunNextButton;
    }
}

