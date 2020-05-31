using EnovaGit.DataTypes;
using EnovaGit.Interfaces;

namespace EnovaGit.Tests.Stubs
{
    public class CommandRunnerStub : ICommandRunner
    {
        public string ReturnedOutputData { get; set; }
        public string ReturnedErrorData { get; set; } = string.Empty;

        public CommandOutput Run(string cmd, string workingDirectory)
        {
            return new CommandOutput()
            {
                OutputData = ReturnedOutputData,
                ErrorData = ReturnedErrorData
            };
        }
    }
}
