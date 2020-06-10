using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsImporterWinform
{
    public partial class Form1 : Form
    {
        string csvLocation;
        DateTime startDate;
        DateTime endDate;
        RunningForm runningForm = new RunningForm();
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            runningForm.Close();
            Process.Start(System.IO.Directory.GetParent(csvLocation).ToString());
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                JsImporterLibrary.ImportBuilder importBuilder = new JsImporterLibrary.ImportBuilder(startDate, endDate, csvLocation);
                importBuilder.BuildImport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong\nPlease contact Systems", "Error");
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "csv|*.csv";
            
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                this.csvLocation = ofd.FileName;
                this.startDate = startDatePicker.Value;
                this.endDate = endDatePicker.Value;
                if(endDate > startDate)
                {
                    runningForm = new RunningForm();
                    runningForm.Show();
                    backgroundWorker1.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("End Date must be later than Start Date", "Error");
                }
            }
        }
    }
}
