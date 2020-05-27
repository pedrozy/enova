using EnovaGit.Interfaces;

namespace EnovaGit.Tests.Stubs
{
    public class DialogStub : IDialog
    {
        public string SelectedPath { get; private set; }

        public void ShowErrorMessage(string message)
        {
            
        }

        public bool ShowFolderBrowserDialog()
        {
            SelectedPath = Common.GitRepositoryPath;
            return true;
        }
    }
}
