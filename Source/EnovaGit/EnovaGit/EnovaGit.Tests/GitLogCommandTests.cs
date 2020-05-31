using EnovaGit.Commands;
using EnovaGit.Tests.Stubs;
using NUnit.Framework;
using System;
using System.IO;

namespace EnovaGit.Tests
{
    public class GitLogCommandTests
    {
        private CommandRunnerStub cmdRunnerStub;
        private GitLogCommand target;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            cmdRunnerStub = new CommandRunnerStub();
            target = new GitLogCommand(cmdRunnerStub);

            Directory.CreateDirectory(Common.GitRepositoryPath);
            Directory.CreateDirectory(Path.Combine(Common.GitRepositoryPath, ".git"));
        }

        [Test]
        public void GitLogCommand_ReturnsCommitCollection()
        {
            foreach (var configuration in Common.TestConfiguration)
            {
                cmdRunnerStub.ReturnedOutputData = configuration.RawGitLog;
                Assert.AreEqual(configuration.GitCommitCollection, target.GetGitCommitList(Common.GitRepositoryPath));
            }
        }

        [Test]
        public void GitLogCommand_ThrowsWhenDirectoryDoesNotContainGitRepository()
        {
            var errorMsg = $"W lokalizacji {Common.Path1} nie znaleziono repozytorium Gita.";

            var exception = Assert.Throws<Exception>(() => target.GetGitCommitList(Common.Path1));
            Assert.AreEqual(errorMsg, exception.Message);
        }

        [Test]
        public void GitLogCommand_ThrowsWhenGitLogFails()
        {
            var errorMsg = "Fatal Error Test";
            cmdRunnerStub.ReturnedErrorData = errorMsg + "\r\n";

            var exception = Assert.Throws<Exception>(() => target.GetGitCommitList(Common.GitRepositoryPath));
            Assert.AreEqual(errorMsg, exception.Message);
        }
    }
}
