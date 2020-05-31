﻿using EnovaGit.Commands;
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
                cmdRunnerStub.ReturnedValue = configuration.RawGitLog;
                Assert.AreEqual(configuration.GitCommitCollection, target.GetGitCommitList(Common.GitRepositoryPath));
            }
        }

        [Test]
        public void GitLogCommand_ThrowsWhenDirectoryDoesNotContainGitRepository()
        {
            Assert.Throws<Exception>(() => target.GetGitCommitList(""));
        }
    }
}
