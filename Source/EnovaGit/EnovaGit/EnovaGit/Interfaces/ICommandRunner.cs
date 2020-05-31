using EnovaGit.DataTypes;

namespace EnovaGit.Interfaces
{
    public interface ICommandRunner
    {
        CommandOutput Run(string cmd, string workingDirectory);
    }
}
