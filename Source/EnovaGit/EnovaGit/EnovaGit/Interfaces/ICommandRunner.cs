namespace EnovaGit.Interfaces
{
    public interface ICommandRunner
    {
        string Run(string cmd, string workingDirectory);
    }
}
