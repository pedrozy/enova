using EnovaGit.Tests.Stubs;
using NUnit.Framework;
using Soneta.Types;
using System;
using System.Linq;

namespace EnovaGit.Tests
{
    public class EnovaGitFormTests
    {
        private GitLogCommandStub gitLogCmdStub;
        private DialogStub dialogControllerStub;
        private EnovaGitForm target;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            gitLogCmdStub = new GitLogCommandStub();
            dialogControllerStub = new DialogStub();

            target = new EnovaGitForm(gitLogCmdStub, dialogControllerStub);
        }

        [SetUp]
        public void SetUp()
        {
            target.User = string.Empty;
            target.Date = Date.Empty;
            gitLogCmdStub.ThrowException = false;
        }

        [Test]
        public void EnovaGitFormTests_GetListUser()
        {
            foreach (var configuration in Common.TestConfiguration)
            {
                gitLogCmdStub.ReturnedValue = configuration.GitCommitCollection.ToList();

                target.PrzegladajAction();
                Assert.AreEqual(configuration.ExpectedUsernameList, target.GetListUser());
            }

            Assert.NotZero(target.GetListUser().Count);

            gitLogCmdStub.ThrowException = true;
            target.PrzegladajAction();

            Assert.Zero(target.GetListUser().Count);
        }

        [Test]
        public void EnovaGitFormTests_Statistics()
        {
            foreach (var configuration in Common.TestConfiguration)
            {
                gitLogCmdStub.ReturnedValue = configuration.GitCommitCollection.ToList();

                target.PrzegladajAction();

                foreach (var stats in configuration.StatisticsTestDataList)
                {
                    target.User = stats.Username;
                    target.Date = stats.Date;

                    Assert.AreEqual(stats.ExpectedAverage, target.Average);
                    Assert.AreEqual(stats.ExpectedDaily, target.Daily);
                    Assert.AreEqual(stats.ExpectedFilteredCommits, target.GitCommitListFiltered);
                }
            }
        }
    }
}
