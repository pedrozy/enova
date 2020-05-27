using EnovaGit.DataTypes;
using System.Collections.Generic;

namespace EnovaGit.Interfaces
{
    public interface IGitLogCommand
    {
        List<GitCommit> GetGitCommitList(string repositoryPath);
    }
}
