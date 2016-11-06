using System;
using System.Windows.Forms;
using DADSTORM.PuppetMaster.Services;

namespace DADSTORM.MoPForm {
    public partial class Dadform : Form {
        OpenFileDialog configDialog = new OpenFileDialog();
        OpenFileDialog scriptDialog = new OpenFileDialog();

        public Dadform() {
            InitializeComponent();

        }


        #region - Config - 
        private void ConfigBrowseButton_Click(object sender, EventArgs e) {
            configDialog.InitialDirectory = "c:\\";
            configDialog.Filter = "Text files (*.txt)|*.txt";
            //configDialog.FilterIndex = 1;
            configDialog.RestoreDirectory = true;

            if (configDialog.ShowDialog() == DialogResult.OK) {
                ConfigFileTextBox.Text = configDialog.FileName;
            }
        }

        private void LoadConfigButton_Click(object sender, EventArgs e) {

            if (!String.IsNullOrEmpty(configDialog.FileName)) {
                PuppetMaster.PuppetMaster.Instance.init(configDialog.FileName);
                ConfigGroup.Enabled = false;
            }
        }
        #endregion

        #region - Command Script -
        private void ScriptBrowseButton_Click(object sender, EventArgs e) {
            scriptDialog.InitialDirectory = "c:\\";
            scriptDialog.Filter = "Text files (*.txt)|*.txt";
            //scriptDialog.FilterIndex = 1;
            scriptDialog.RestoreDirectory = false;

            scriptDialog.ShowDialog();
            ScriptFileTextBox.Text = scriptDialog.FileName;
        }

        private void LoadScriptButton_Click(object sender, EventArgs e) {
            if (!String.IsNullOrEmpty(scriptDialog.FileName)) {
                //TODO
                //probably implement service in puppet master for load
            }
        }

        private void RunNextButton_Click(object sender, EventArgs e) {
            //TODO
        }

        private void RunAllButton_Click(object sender, EventArgs e) {
            //TODO
        }
        #endregion

        private void StartButton_Click(object sender, EventArgs e) {
            if (!String.IsNullOrEmpty(StartOpIdTextBox.Text)) {
                StartService service = new StartService(StartOpIdTextBox.Text);
                service.assyncexecute();
            }
                
        }

        private void IntervalButton_Click(object sender, EventArgs e) {
            if (!String.IsNullOrEmpty(IntervalOpIdTextBox.Text) && !String.IsNullOrEmpty(IntervalTimeTextBox.Text)) {
                IntervalService service = new IntervalService(StartOpIdTextBox.Text, Int32.Parse(IntervalTimeTextBox.Text));
                service.assyncexecute();
            }
        }

        private void WaitButton_Click(object sender, EventArgs e) {
            if (!String.IsNullOrEmpty(WaitTimeTextBox.Text)) {
                WaitService service = new WaitService(Int32.Parse(WaitTimeTextBox.Text));
                service.assyncexecute();
            }
        }

        private void StatusButton_Click(object sender, EventArgs e) {
            StatusService service = new StatusService();
            service.assyncexecute();
        }

        private void CrashButton_Click(object sender, EventArgs e) {
            //TODO
        }

        private void FreezeButton_Click(object sender, EventArgs e) {
            //TODO
        }

        private void UnfreezeButton_Click(object sender, EventArgs e) {
            //TODO
        }

        public void PrintToHistory(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(PrintToHistory), new object[] { value });
                return;
            }
            HistoryTextBox.Text += value;
        }
    }
}
