using DelDeepFolder.Properties;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DelDeepFolder
{
    public partial class Main : Form
    {
        BackgroundWorker backgroundWorker = null;

        public Main()
        {
            InitializeComponent();
        }

        private void InitializeUI()
        {
            tbFolder.Text = Configure.GetFolderPath();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select the raw files directory";
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.DesktopDirectory;
            folderBrowserDialog.SelectedPath = tbFolder.Text;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbFolder.Text = folderBrowserDialog.SelectedPath;
                Configure.SaveFolderPath(tbFolder.Text);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(DeleteFolder_DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(DeleteFolder_ProgressChanged);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DeleteFolder_Completed);
            tbOutput.Text = "Deleting... \r\n";
            backgroundWorker.RunWorkerAsync();
        }

        private void DeleteFolder_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Helpers.DeleteDeepFolder(tbFolder.Text, backgroundWorker);
        }

        private void DeleteFolder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tbOutput.Text += e.UserState.ToString() + "\r\n";
        }

        private void DeleteFolder_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if((bool)(e.Result))
            {
                tbOutput.Text += "Finished deleting folder.\r\n";
            }
            else
            {
                tbOutput.Text += "Failed to delete folder.\r\n";
            }            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
