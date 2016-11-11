using System;
using System.Windows.Forms;
using DADSTORM.PuppetMaster.Services;
using DADSTORM.CommonTypes.Parsing;
using System.Collections.Generic;
using System.Threading;
using DADSTORM.CommonTypes;

namespace DADSTORM.MoPForm {
    public partial class Dadform : Form {
        OpenFileDialog configDialog = new OpenFileDialog();
        OpenFileDialog scriptDialog = new OpenFileDialog();
        private ScriptFileParser parser;

        public Dadform() {
            InitializeComponent();

        }


        #region - Config - 
        private void ConfigBrowseButton_Click(object sender, EventArgs e) {
            configDialog.InitialDirectory = "c:\\";
            configDialog.Filter = "Configuration files (*.config)|*.config|Text files (*.txt)|*.txt";
            configDialog.FilterIndex = 1;
            configDialog.RestoreDirectory = true;

            if (configDialog.ShowDialog() == DialogResult.OK) {
                ConfigFileTextBox.Text = configDialog.FileName;
            }
        }

        private void LoadConfigButton_Click(object sender, EventArgs e) {

            if (!String.IsNullOrEmpty(configDialog.FileName)) {
                PuppetMaster.PuppetMaster.Instance.init(configDialog.FileName);
                PuppetMaster.PuppetMaster.Instance.setLoggerOutputDestination(new PuppetMaster.DelegateUpdateInfo(PrintToHistory));
                ConfigGroup.Enabled = false;
                CommandsGroup.Enabled = true;
                ScriptGroup.Enabled = true;
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
                parser = new ScriptFileParser(scriptDialog.FileName);
                parser.parse();
                RunNextButton.Enabled = true;
                RunAllButton.Enabled = true;
            }
        }

        private void RunNextButton_Click(object sender, EventArgs e) {
            KeyValuePair<string, string[]> command = parser.nextCommand();
            runCommand(command.Key, command.Value);
        }

        private void RunAllButton_Click(object sender, EventArgs e) {
            CommandsGroup.Enabled = false;
            RunNextButton.Enabled = false;
            Thread thread = new Thread(runAllCommands);
            thread.Start();
            thread.Join();
            CommandsGroup.Enabled = true;
            RunNextButton.Enabled = true;
        }
        private void runAllCommands() {
            KeyValuePair<string, string[]> command;
            while(parser.hasNextCommand()) {
                command = parser.nextCommand();
                if (command.Key.Equals(Command.WAIT)) {
                    Thread.Sleep(Int32.Parse(command.Value[0]));
                    continue;
                }
                runCommand(command.Key, command.Value);
            }

        }
        #endregion

        private void runCommand(string name, string[] param) {
            PuppetMasterService service;
            switch (name) {
                case Command.START:
                    service = new StartService(param[0]);
                    break;
                case Command.INTERVAL:
                    service = new IntervalService(param[0], Int32.Parse(param[1]));
                    break;
                case Command.STATUS:
                    service = new StatusService();
                    break;
                case Command.CRASH:
                    service = new CrashService(param[0], Int32.Parse(param[1]));
                    break;
                case Command.FREEZE:
                    service = new FreezeService(param[0], Int32.Parse(param[1]));
                    break;
                case Command.UNFREEZE:
                    service = new UnfreezeService(param[0], Int32.Parse(param[1]));
                    break;
                default:
                    PrintToHistory("Script Parser: command '" + name + "' not recognised. Skipping command.");
                    return;
            }
            service.assyncexecute();
        }

        private void StartButton_Click(object sender, EventArgs e) {
            if (!String.IsNullOrEmpty(StartOpIdTextBox.Text)) {
                runCommand(Command.START, new string[] { StartOpIdTextBox.Text });
            }
                
        }

        private void IntervalButton_Click(object sender, EventArgs e) {
            if (!String.IsNullOrEmpty(IntervalOpIdTextBox.Text) && !String.IsNullOrEmpty(IntervalTimeTextBox.Text)) {
                runCommand(Command.INTERVAL, new string[] { IntervalOpIdTextBox.Text, IntervalTimeTextBox.Text });
            }
        }

        private void StatusButton_Click(object sender, EventArgs e) {
            runCommand(Command.STATUS, null);
        }

        private void CrashButton_Click(object sender, EventArgs e) {
            if (!String.IsNullOrEmpty(CrashOpIdTextBox.Text) && !String.IsNullOrEmpty(CrashReplTextBox.Text)) {
                runCommand(Command.CRASH, new string[] { CrashOpIdTextBox.Text, CrashReplTextBox.Text });
            }
        }

        private void FreezeButton_Click(object sender, EventArgs e) {
            if (!String.IsNullOrEmpty(FreezeOpIdTextBox.Text) && !String.IsNullOrEmpty(FreezeReplTextBox.Text)) {
                runCommand(Command.FREEZE, new string[] { FreezeOpIdTextBox.Text, FreezeReplTextBox.Text });
            }
        }

        private void UnfreezeButton_Click(object sender, EventArgs e) {
            if (!String.IsNullOrEmpty(UnfreezeOpIdTextBox.Text) && !String.IsNullOrEmpty(UnfreezeReplTextBox.Text)) {
                runCommand(Command.UNFREEZE, new string[] { UnfreezeOpIdTextBox.Text, UnfreezeReplTextBox.Text });
            }
        }

        public void PrintToHistory(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(PrintToHistory), new object[] { value });
                return;
            }
            HistoryTextBox.Text += value + "\n";
        }
    }
}
