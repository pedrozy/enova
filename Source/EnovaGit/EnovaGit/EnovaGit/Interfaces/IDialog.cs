namespace EnovaGit.Interfaces
{
    public interface IDialog
    {
        string SelectedPath { get; }

        void ShowErrorMessage(string message);
        bool ShowFolderBrowserDialog();
    }
}
