using EnovaGit.Interfaces;
using System;
using System.Windows.Forms;

namespace EnovaGit
{
    public class Dialog : IDialog
    {
        public string SelectedPath { get; private set; }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool ShowFolderBrowserDialog()
        {
            using (var browseDialog = new FolderBrowserDialog())
            {
                browseDialog.SelectedPath = SelectedPath;
                browseDialog.ShowNewFolderButton = false;
                browseDialog.RootFolder = Environment.SpecialFolder.MyComputer;

                if (browseDialog.ShowDialog() == DialogResult.OK)
                {
                    SelectedPath = browseDialog.SelectedPath;
                    return true;
                }

                return false;
            }
        }
    }
}
