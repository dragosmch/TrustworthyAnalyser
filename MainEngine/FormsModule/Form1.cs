using MainEngine;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormsModule
{
    public sealed partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AllowDrop = true;
            DragEnter += new DragEventHandler(Form1_DragEnter);
            DragDrop += new DragEventHandler(Form1_DragDrop);
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

        private void analyseButton_Click(object sender, EventArgs e)
        {
            string fileLocation = fileLocationBox.Text;
            if (!string.IsNullOrEmpty(fileLocation))
            {
                analyseButton.Enabled = false;
                OutputResults(TrustworthyAnalyzer.ReturnResults(fileLocation));
            }
            analyseButton.Enabled = true;
        }

        private void OutputResults(TrustworthinessResult trustworthyResult)
        {
            OutputMainResult(trustworthyResult.TrustworthinessLevel);
            OutputAvailabilityResult(trustworthyResult);
            OutputSecuritySafetyyResult(trustworthyResult);
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
            availabilityResultLabel.Text = 
                $"Sucessful runs: {result.AvailabilityNoOfSuccessfulRuns}/{result.AvailabilityNoOfRuns}";
        }

        private void OutputSecuritySafetyyResult(TrustworthinessResult result)
        {
            securitySafetyResultLabel.Visible = true;
            securitySafetyResultLabel.Text = 
                $"Security and Safety protection score: {result.SafetyAndSecurityPercentage}/{result.SafetyAndSecurityPercentageBase}";
        }

        private void ColourTextBoxAndWriteText(string text, Color color)
        {
            resultLabel.Text = text;
            resultLabel.BackColor = color;
            resultLabel.Visible = true;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Link;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1)
            {
                fileLocationBox.Text = files[0];
            }
            foreach (string file in files) Console.WriteLine(file);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
