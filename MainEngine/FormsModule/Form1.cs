using MainEngine;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FormsModule
{
    public sealed partial class Form1 : Form
    {
        private static TrustworthinessResult trustworthyResult;

        public Form1()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            // Wrap the creation of the OpenFileDialog instance in a using statement,
            // rather than manually calling the Dispose method to ensure proper disposal
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open file";
                dlg.InitialDirectory = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles";
                dlg.Filter = "Executables(*.exe)|*.exe|All files(*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    fileLocationBox.Text = dlg.FileName;
                }
            }
        }

        private void AnalyseButton_Click(object sender, EventArgs e)
        {
            HideResults();
            string fileLocation = fileLocationBox.Text;
            if (!string.IsNullOrEmpty(fileLocation))
            {
                analyseButton.Enabled = false;
                int mode = GetModeFromModeButtons();
                trustworthyResult = TrustworthyAnalyzer.ReturnResults(fileLocation, mode);
                OutputResults(trustworthyResult, mode);
            }
            analyseButton.Enabled = true;
            saveReportButton.Visible = true;
        }

        private void OutputResults(TrustworthinessResult trustworthyResult, int mode)
        {
            OutputMainResult(trustworthyResult.TrustworthinessLevel);
            OutputAvailabilityResult(trustworthyResult);
            OutputSecuritySafetyyResult(trustworthyResult, mode);
        }

        private void OutputMainResult(TrustworthyApplicationLevel level)
        {
            if (level == TrustworthyApplicationLevel.Trustworthy)
                ColourTextBoxAndWriteText("Trustworthy", Color.Green);
            else if (level == TrustworthyApplicationLevel.NotTrustworthy)
                ColourTextBoxAndWriteText("Not Trustworthy", Color.Red);
            else
                ColourTextBoxAndWriteText("Inconclusive result", Color.Yellow);
        }

        private void OutputAvailabilityResult(TrustworthinessResult result)
        {
            availabilityResultLabel.Visible = true;
            availabilityResultLabel.Text = result.AvailabilityResult.ToString();
        }

        private void OutputSecuritySafetyyResult(TrustworthinessResult result, int mode)
        {
            securitySafetyResultLabel.Visible = true;
            securitySafetyResultLabel.Text = result.SecuritySafetyResult.ToString();
        }

        private void ColourTextBoxAndWriteText(string text, Color color)
        {
            resultLabel.Text = text;
            resultLabel.BackColor = color;
            resultLabel.Visible = true;
        }

        private int GetModeFromModeButtons()
        {
            if (mediumModeButton.Checked)
                return 1;
            if (advancedModeButton.Checked)
                return 2;
            return 0;
        }

        private void HideResults()
        {
            availabilityResultLabel.Visible = false;
            securitySafetyResultLabel.Visible = false;
            resultLabel.Visible = false;
            saveReportButton.Visible = false;
        }

        private void SaveReportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt";
            saveFileDialog1.Title = "Save detailed result report";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                StreamWriter bw = new StreamWriter(File.Create(path));
                bw.Write(GetReportText());
                bw.Close();
            }
        }

        private string GetReportText()
        {
            return
                $"File: {trustworthyResult.SecuritySafetyResult.winCheckSecResultObject.Path}{Environment.NewLine}"
                + $"Time: {DateTime.Now}{Environment.NewLine}"
                + trustworthyResult.ToString(GetModeFromModeButtons());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
