using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryModule;
using MainEngine;

namespace FormsModule
{
    /// <inheritdoc />
    public sealed partial class UserInterface : Form
    {
        private TrustworthinessResult _trustworthyResult;
        private readonly TrustworthyAnalyser _trustworthyAnalyser = new TrustworthyAnalyser();

        /// <inheritdoc />
        public UserInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open dialogue to select file for analysis. 
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
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

        /// <summary>
        /// Start analysis following user interaction.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private async void AnalyseButton_Click(object sender, EventArgs e)
        {
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
                    _trustworthyResult = _trustworthyAnalyser.ReturnResults(progress, fileLocation, mode)).ConfigureAwait(true);
                if (_trustworthyResult == null)
                {
                    ShowFailureMessage("Please select an executable file.", "Cannot analyse this file");
                    analyseButton.Enabled = true;
                    return;
                }
                DisplayAllResults(_trustworthyResult);
            }
            analyseButton.Enabled = true;
            saveReportButton.Visible = true;
            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// Create a report file with details of the current analysis
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
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
            catch
            {
                ShowFailureMessage("Please try again.", "Cannot save file in the chosen folder");
            }
        }

        /// <summary>
        /// Initialise the analysis progress bar and set routine for incrementing the value displayed. 
        /// </summary>
        /// <param name="mode">Basic, Medium or Advanced.</param>
        /// <returns>A progress object with the required bounds.</returns>
        private IProgress<int> InitialiseProgressBar(AnalysisMode mode)
        {
            progressBar.Value = 0;
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

        /// <summary>
        /// Compute maximum progress value following the algorithm:
        /// 2 * (number of availability runs + 1)
        /// where 1 is the security safety step
        /// </summary>
        /// <param name="mode">Basic, Medium or Advanced.</param>
        /// <returns>Upper bound of the progress bar value</returns>
        private static int GetMaximumProgressBarValueFromMode(AnalysisMode mode)
        {
            int progressStepsFromAvailability = AnalysisModeMapping.GetAvailabilityMaxRuns(mode);
            return 2 * (progressStepsFromAvailability + 1);
        }

        /// <summary>
        /// Display a failure message following an error.
        /// </summary>
        /// <param name="bodyText">Message for user.</param>
        /// <param name="title">Title of the pop-up windows.</param>
        private static void ShowFailureMessage(string bodyText, string title)
        {
            MessageBox.Show(bodyText, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Display all results of the analysis
        /// </summary>
        /// <param name="trustworthyResult">Overall result</param>
        private void DisplayAllResults(TrustworthinessResult trustworthyResult)
        {
            DisplayTrustworthinessResult(trustworthyResult.TrustworthinessLevel);
            DisplayAvailabilityResult(trustworthyResult);
            DisplaySecuritySafetyResult(trustworthyResult);
        }

        /// <summary>
        /// Show analysis result to user
        /// </summary>
        /// <param name="level">Final trustworthiness level</param>
        private void DisplayTrustworthinessResult(TrustworthyApplicationLevel level)
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

        /// <summary>
        /// Output details about the availability result
        /// </summary>
        /// <param name="result">Overall trustworthiness result</param>
        private void DisplayAvailabilityResult(TrustworthinessResult result)
        {
            availabilityResultLabel.Visible = true;
            availabilityResultLabel.Text = result.AvailabilityResult.ToString();
        }

        /// <summary>
        /// Output details about the security and safety result
        /// </summary>
        /// <param name="result">Overall trustworthiness result</param>
        private void DisplaySecuritySafetyResult(TrustworthinessResult result)
        {
            securitySafetyResultLabel.Visible = true;
            securitySafetyResultLabel.Text = result.SecuritySafetyResult.ToString();
        }

        /// <summary>
        /// Colour the result label
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// <param name="color">Background colour.</param>
        private void ColourTextBoxAndWriteText(string text, Color color)
        {
            resultLabel.Text = text;
            resultLabel.BackColor = color;
            resultLabel.Visible = true;
        }

        /// <summary>
        /// Get the analysis mode from radio button user selection.
        /// </summary>
        /// <returns>An analysis mode object.</returns>
        private AnalysisMode GetModeFromModeButtons()
        {
            if (basicModeButton.Checked)
                return AnalysisMode.Basic;
            return mediumModeButton.Checked ? AnalysisMode.Medium : AnalysisMode.Advanced;
        }

        /// <summary>
        /// Hide previous results and corresponding labels.
        /// </summary>
        private void HideResults()
        {
            availabilityResultLabel.Visible = false;
            securitySafetyResultLabel.Visible = false;
            resultLabel.Visible = false;
            saveReportButton.Visible = false;
        }

        /// <summary>
        /// Get analysis' scores and other details.
        /// </summary>
        /// <returns>String representation of the report.</returns>
        private string GetReportText()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"File: {_trustworthyResult.SecuritySafetyResult.WinCheckSecResultObject.Path}");
            stringBuilder.AppendLine($"Time: {DateTime.Now}");
            stringBuilder.Append(_trustworthyResult.ToString(GetModeFromModeButtons()));
            return stringBuilder.ToString();
        }
    }
}
