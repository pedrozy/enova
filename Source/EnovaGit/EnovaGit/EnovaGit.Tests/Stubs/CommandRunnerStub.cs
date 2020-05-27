using EnovaGit.Interfaces;

namespace EnovaGit.Tests.Stubs
{
    public class CommandRunnerStub : ICommandRunner
    {
        public string ReturnedValue { get; set; }

        public string Run(string cmd, string workingDirectory)
        {
            return ReturnedValue;
        }
    }
}
