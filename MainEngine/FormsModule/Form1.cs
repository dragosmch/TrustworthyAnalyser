using MainEngine;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FormsModule
{
    public sealed partial class Form1 : Form
    {
        private static TrustworthinessResult _trustworthyResult;

        public Form1()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            // Wrap the creation of the OpenFileDialog instance in a using statement,
            // rather than manually calling the Dispose method to ensure proper disposal
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = @"Open file";
                openFileDialog.InitialDirectory = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser\TestFiles";
                openFileDialog.Filter = @"Executables(*.exe)|*.exe|All files(*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileLocationBox.Text = openFileDialog.FileName;
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
                _trustworthyResult = TrustworthyAnalyzer.ReturnResults(fileLocation, mode);
                if (_trustworthyResult == null)
                {
                    ShowFailureMessage("Please select an executable file.", "Cannot analyse this file");
                    analyseButton.Enabled = true;
                    return;
                }
                OutputResults(_trustworthyResult);
            }
            analyseButton.Enabled = true;
            saveReportButton.Visible = true;
        }

        private static void ShowFailureMessage(string bodyText, string title)
        {
            MessageBox.Show(bodyText, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void OutputResults(TrustworthinessResult trustworthyResult)
        {
            OutputMainResult(trustworthyResult.TrustworthinessLevel);
            OutputAvailabilityResult(trustworthyResult);
            OutputSecuritySafetyResult(trustworthyResult);
        }

        private void OutputMainResult(TrustworthyApplicationLevel level)
        {
            switch (level)
            {
                case TrustworthyApplicationLevel.Trustworthy:
                    ColourTextBoxAndWriteText("Trustworthy", Color.Green);
                    break;
                case TrustworthyApplicationLevel.NotTrustworthy:
                    ColourTextBoxAndWriteText("Not Trustworthy", Color.Red);
                    break;
                default:
                    ColourTextBoxAndWriteText("Inconclusive result", Color.Yellow);
                    break;
            }
        }

        private void OutputAvailabilityResult(TrustworthinessResult result)
        {
            availabilityResultLabel.Visible = true;
            availabilityResultLabel.Text = result.AvailabilityResult.ToString();
        }

        private void OutputSecuritySafetyResult(TrustworthinessResult result)
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
            // basic mode
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
            var saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = @"C:\Users\Dragos\Documents\GitHub\TrustworthyAnalyser",
                Filter = @"Text files(*.txt)|*.txt",
                Title = @"Save detailed result report"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            string path = saveFileDialog.FileName;
            try
            {
                var streamWriter = new StreamWriter(File.Create(path));
                streamWriter.Write(GetReportText());
                streamWriter.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                ShowFailureMessage("Please try again.", "Cannot save file in the chosen folder");

            }
        }

        private string GetReportText()
        {
            return
                $"File: {_trustworthyResult.SecuritySafetyResult.WinCheckSecResultObject.Path}{Environment.NewLine}"
                + $"Time: {DateTime.Now}{Environment.NewLine}"
                + _trustworthyResult.ToString(GetModeFromModeButtons());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
