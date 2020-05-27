using EnovaGit.DataTypes;
using EnovaGit.Interfaces;
using System;
using System.Collections.Generic;

namespace EnovaGit.Tests.Stubs
{
    public class GitLogCommandStub : IGitLogCommand
    {
        public List<GitCommit> ReturnedValue { get; set; }
        public bool ThrowException { get; set; }

        public List<GitCommit> GetGitCommitList(string repositoryPath)
        {
            if (ThrowException) throw new Exception();
            return ReturnedValue;
        }
    }
}
