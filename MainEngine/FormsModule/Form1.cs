using MainEngine;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormsModule
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AllowDrop = true;
            DragEnter += new DragEventHandler(Form1_DragEnter);
            DragDrop += new DragEventHandler(Form1_DragDrop);
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Link;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            string fileLocation = fileLocationBox.Text;
            if (fileLocation != null && fileLocation != "")
            {
                button2.Enabled = false;
                OutputResults(TrustworthyAnalyzer.ReturnResults(fileLocation));
            }
            button2.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void OutputResults(TrustworthyApplicationLevel level)
        {
            if (level == TrustworthyApplicationLevel.Trustworthy)
                ColourTextBoxAndWriteText("Trustworthy", Color.Green);
            else if (level == TrustworthyApplicationLevel.NotTrustworthy)
                ColourTextBoxAndWriteText("Not Trustworthy", Color.Red);
            else
                ColourTextBoxAndWriteText("Inconclusive result", Color.Yellow);
        }

        private void ColourTextBoxAndWriteText(string text, Color color)
        {
            label5.Text = text;
            label5.BackColor = color;
            label5.Visible = true;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
