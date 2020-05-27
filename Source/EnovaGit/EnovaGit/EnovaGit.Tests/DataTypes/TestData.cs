using EnovaGit.DataTypes;
using System.Collections.Generic;

namespace EnovaGit.Tests.DataTypes
{
    public class TestData
    {
        public string RawGitLog { get; set; }
        public IEnumerable<GitCommit> GitCommitCollection { get; set; }
        public IEnumerable<string> ExpectedUsernameList { get; set; }
        public IEnumerable<StatisticsTestData> StatisticsTestDataList { get; set; }
    }
}
