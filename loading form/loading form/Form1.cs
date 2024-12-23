using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace loading_form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
           backgroundWorker1.WorkerReportsProgress = true;
           backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int sum = 0;
            for (int i = 0; i <= 100; i++) 
            {
                Thread.Sleep(100); 

                sum += 1;
                backgroundWorker1.ReportProgress(i); 

                if (backgroundWorker1.CancellationPending) 
                {
                    e.Cancel = true;
                    backgroundWorker1.ReportProgress(0); 
                    return;
                }
            }

            e.Result = sum; 
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage; 
            label1.Text = $"Progress: {e.ProgressPercentage}%"; 
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                label1.Text = "Operation cancelled.";
            }
            else if (e.Error != null)
            {
                label1.Text = $"Error: {e.Error.Message}";
            }
            else
            {
                label1.Text = $"Completed! Sum: {e.Result}";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                progressBar1.Value = 0; 
                backgroundWorker1.RunWorkerAsync(); 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync(); 
            }
        }
    }
}
