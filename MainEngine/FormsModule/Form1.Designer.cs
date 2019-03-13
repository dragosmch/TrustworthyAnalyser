namespace FormsModule
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.modeLabel = new System.Windows.Forms.Label();
            this.modeOptionBox = new System.Windows.Forms.GroupBox();
            this.advancedModeButton = new System.Windows.Forms.RadioButton();
            this.mediumModeButton = new System.Windows.Forms.RadioButton();
            this.basicModeButton = new System.Windows.Forms.RadioButton();
            this.instructionsLabel = new System.Windows.Forms.Label();
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.saveReportButton = new System.Windows.Forms.Button();
            this.securitySafetyResultLabel = new System.Windows.Forms.Label();
            this.availabilityResultLabel = new System.Windows.Forms.Label();
            this.resultLabel = new System.Windows.Forms.Label();
            this.analyseButton = new System.Windows.Forms.Button();
            this.resultTitleLabel = new System.Windows.Forms.Label();
            this.fileLocationLabel = new System.Windows.Forms.Label();
            this.fileLocationBox = new System.Windows.Forms.TextBox();
            this.openButton = new System.Windows.Forms.Button();
            this.availabilityExplanation = new System.Windows.Forms.ToolTip(this.components);
            this.securitySafetyExplanation = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.modeOptionBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.modeLabel);
            this.splitContainer1.Panel1.Controls.Add(this.modeOptionBox);
            this.splitContainer1.Panel1.Controls.Add(this.instructionsLabel);
            this.splitContainer1.Panel1.Controls.Add(this.welcomeLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.CausesValidation = false;
            this.splitContainer1.Panel2.Controls.Add(this.saveReportButton);
            this.splitContainer1.Panel2.Controls.Add(this.securitySafetyResultLabel);
            this.splitContainer1.Panel2.Controls.Add(this.availabilityResultLabel);
            this.splitContainer1.Panel2.Controls.Add(this.resultLabel);
            this.splitContainer1.Panel2.Controls.Add(this.analyseButton);
            this.splitContainer1.Panel2.Controls.Add(this.resultTitleLabel);
            this.splitContainer1.Panel2.Controls.Add(this.fileLocationLabel);
            this.splitContainer1.Panel2.Controls.Add(this.fileLocationBox);
            this.splitContainer1.Panel2.Controls.Add(this.openButton);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 0;
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.Location = new System.Drawing.Point(29, 72);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(75, 13);
            this.modeLabel.TabIndex = 5;
            this.modeLabel.Text = "Analysis Mode";
            // 
            // modeOptionBox
            // 
            this.modeOptionBox.Controls.Add(this.advancedModeButton);
            this.modeOptionBox.Controls.Add(this.mediumModeButton);
            this.modeOptionBox.Controls.Add(this.basicModeButton);
            this.modeOptionBox.Location = new System.Drawing.Point(25, 88);
            this.modeOptionBox.Name = "modeOptionBox";
            this.modeOptionBox.Size = new System.Drawing.Size(200, 100);
            this.modeOptionBox.TabIndex = 4;
            this.modeOptionBox.TabStop = false;
            // 
            // advancedModeButton
            // 
            this.advancedModeButton.AutoSize = true;
            this.advancedModeButton.Location = new System.Drawing.Point(7, 65);
            this.advancedModeButton.Name = "advancedModeButton";
            this.advancedModeButton.Size = new System.Drawing.Size(74, 17);
            this.advancedModeButton.TabIndex = 2;
            this.advancedModeButton.TabStop = true;
            this.advancedModeButton.Text = "Advanced";
            this.advancedModeButton.UseVisualStyleBackColor = true;
            // 
            // mediumModeButton
            // 
            this.mediumModeButton.AutoSize = true;
            this.mediumModeButton.Location = new System.Drawing.Point(7, 41);
            this.mediumModeButton.Name = "mediumModeButton";
            this.mediumModeButton.Size = new System.Drawing.Size(62, 17);
            this.mediumModeButton.TabIndex = 1;
            this.mediumModeButton.TabStop = true;
            this.mediumModeButton.Text = "Medium";
            this.mediumModeButton.UseVisualStyleBackColor = true;
            // 
            // basicModeButton
            // 
            this.basicModeButton.AutoSize = true;
            this.basicModeButton.Location = new System.Drawing.Point(7, 17);
            this.basicModeButton.Name = "basicModeButton";
            this.basicModeButton.Size = new System.Drawing.Size(51, 17);
            this.basicModeButton.TabIndex = 0;
            this.basicModeButton.TabStop = true;
            this.basicModeButton.Text = "Basic";
            this.basicModeButton.UseVisualStyleBackColor = true;
            // 
            // instructionsLabel
            // 
            this.instructionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionsLabel.Location = new System.Drawing.Point(22, 210);
            this.instructionsLabel.Name = "instructionsLabel";
            this.instructionsLabel.Size = new System.Drawing.Size(197, 75);
            this.instructionsLabel.TabIndex = 3;
            this.instructionsLabel.Text = "Open an executable file to analyse its trustworthiness.";
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomeLabel.Location = new System.Drawing.Point(9, 27);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(242, 16);
            this.welcomeLabel.TabIndex = 2;
            this.welcomeLabel.Text = "Welcome to Trustworthy Analyser!";
            // 
            // saveReportButton
            // 
            this.saveReportButton.Location = new System.Drawing.Point(418, 415);
            this.saveReportButton.Name = "saveReportButton";
            this.saveReportButton.Size = new System.Drawing.Size(100, 23);
            this.saveReportButton.TabIndex = 8;
            this.saveReportButton.Text = "Save Result File";
            this.saveReportButton.UseVisualStyleBackColor = true;
            this.saveReportButton.Visible = false;
            this.saveReportButton.Click += new System.EventHandler(this.SaveReportButton_Click);
            // 
            // securitySafetyResultLabel
            // 
            this.securitySafetyResultLabel.AutoSize = true;
            this.securitySafetyResultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.securitySafetyResultLabel.Location = new System.Drawing.Point(35, 298);
            this.securitySafetyResultLabel.Name = "securitySafetyResultLabel";
            this.securitySafetyResultLabel.Size = new System.Drawing.Size(0, 17);
            this.securitySafetyResultLabel.TabIndex = 7;
            this.securitySafetyExplanation.SetToolTip(this.securitySafetyResultLabel, resources.GetString("securitySafetyResultLabel.ToolTip"));
            this.securitySafetyResultLabel.Visible = false;
            // 
            // availabilityResultLabel
            // 
            this.availabilityResultLabel.AutoSize = true;
            this.availabilityResultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availabilityResultLabel.Location = new System.Drawing.Point(35, 245);
            this.availabilityResultLabel.Name = "availabilityResultLabel";
            this.availabilityResultLabel.Size = new System.Drawing.Size(0, 17);
            this.availabilityResultLabel.TabIndex = 6;
            this.availabilityExplanation.SetToolTip(this.availabilityResultLabel, "Run the application a specified number of times in order to detect crashes.");
            this.availabilityResultLabel.Visible = false;
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultLabel.Location = new System.Drawing.Point(32, 369);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(0, 17);
            this.resultLabel.TabIndex = 5;
            this.resultLabel.Visible = false;
            // 
            // analyseButton
            // 
            this.analyseButton.Location = new System.Drawing.Point(35, 88);
            this.analyseButton.Name = "analyseButton";
            this.analyseButton.Size = new System.Drawing.Size(75, 23);
            this.analyseButton.TabIndex = 4;
            this.analyseButton.Text = "Analyse";
            this.analyseButton.UseVisualStyleBackColor = true;
            this.analyseButton.Click += new System.EventHandler(this.AnalyseButton_Click);
            // 
            // resultTitleLabel
            // 
            this.resultTitleLabel.AutoSize = true;
            this.resultTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultTitleLabel.Location = new System.Drawing.Point(32, 197);
            this.resultTitleLabel.Name = "resultTitleLabel";
            this.resultTitleLabel.Size = new System.Drawing.Size(59, 17);
            this.resultTitleLabel.TabIndex = 3;
            this.resultTitleLabel.Text = "Results:";
            // 
            // fileLocationLabel
            // 
            this.fileLocationLabel.AutoSize = true;
            this.fileLocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileLocationLabel.Location = new System.Drawing.Point(15, 27);
            this.fileLocationLabel.Name = "fileLocationLabel";
            this.fileLocationLabel.Size = new System.Drawing.Size(88, 17);
            this.fileLocationLabel.TabIndex = 2;
            this.fileLocationLabel.Text = "File Location";
            // 
            // fileLocationBox
            // 
            this.fileLocationBox.Location = new System.Drawing.Point(18, 47);
            this.fileLocationBox.Name = "fileLocationBox";
            this.fileLocationBox.Size = new System.Drawing.Size(428, 20);
            this.fileLocationBox.TabIndex = 1;
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(452, 47);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "TrustworthyAnalyser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.modeOptionBox.ResumeLayout(false);
            this.modeOptionBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label welcomeLabel;
        private System.Windows.Forms.Label instructionsLabel;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Label fileLocationLabel;
        private System.Windows.Forms.TextBox fileLocationBox;
        private System.Windows.Forms.Label resultTitleLabel;
        private System.Windows.Forms.Button analyseButton;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Label availabilityResultLabel;
        private System.Windows.Forms.Label securitySafetyResultLabel;
        private System.Windows.Forms.GroupBox modeOptionBox;
        private System.Windows.Forms.RadioButton mediumModeButton;
        private System.Windows.Forms.RadioButton basicModeButton;
        private System.Windows.Forms.RadioButton advancedModeButton;
        private System.Windows.Forms.Button saveReportButton;
        private System.Windows.Forms.Label modeLabel;
        private System.Windows.Forms.ToolTip availabilityExplanation;
        private System.Windows.Forms.ToolTip securitySafetyExplanation;
    }
}

