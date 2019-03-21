using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryModule;
using MainEngine;

namespace FormsModule
{
    public sealed partial class UserInterface : Form
    {
        private TrustworthinessResult _trustworthyResult;
        private readonly TrustworthyAnalyzer _trustworthyAnalyzer = new TrustworthyAnalyzer();

        public UserInterface()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
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

        private async void AnalyseButton_Click(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            // Remove stop watch before submission
            var sw = new Stopwatch();
            sw.Start();
            HideResults();
            string fileLocation = fileLocationBox.Text;
            if (!string.IsNullOrEmpty(fileLocation))
            {
                var mode = GetModeFromModeButtons();
                var progress = InitialiseProgressBar(mode);

                analyseButton.Enabled = false;
                await Task.Run(() => 
                    _trustworthyResult = _trustworthyAnalyzer.ReturnResults(progress, fileLocation, mode)).ConfigureAwait(true);
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
            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        private IProgress<int> InitialiseProgressBar(AnalysisMode mode)
        {
            progressBar.Maximum = GetMaximumProgressBarValueFromMode(mode);
            progressBar.Step = 1;

            var progress = new Progress<int>(incrementValue =>
            {
                if (progressBar.Value <= progressBar.Maximum - 2)
                {
                    progressBar.Value += incrementValue + 1;
                    progressBar.Value--;
                }
                else if (progressBar.Value <= progressBar.Maximum)
                {
                    progressBar.Maximum--;
                }
            });
            return progress;
        }

        private static int GetMaximumProgressBarValueFromMode(AnalysisMode mode)
        {
            switch (mode)
            {
                case AnalysisMode.Basic:
                    return 8;
                case AnalysisMode.Medium:
                    return 12;
                case AnalysisMode.Advanced:
                    return 22;
                default:
                    throw new ArgumentException("Invalid analysis mode!");
            }
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
                case TrustworthyApplicationLevel.Inconclusive:
                    ColourTextBoxAndWriteText("Inconclusive result", Color.Yellow);
                    break;
                default:
                    throw new ArgumentException("Unknown trustworthiness level!");
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

        private AnalysisMode GetModeFromModeButtons()
        {
            if (basicModeButton.Checked)
                return AnalysisMode.Basic;
            return mediumModeButton.Checked ? AnalysisMode.Medium : AnalysisMode.Advanced;
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
