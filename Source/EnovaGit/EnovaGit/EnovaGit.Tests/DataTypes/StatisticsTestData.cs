using EnovaGit.DataTypes;
using Soneta.Types;
using System.Collections.Generic;

namespace EnovaGit.Tests.DataTypes
{
    public class StatisticsTestData
    {
        public string Username;
        public Date Date;
        public float ExpectedAverage;
        public float ExpectedDaily;
        public IEnumerable<GitCommit> ExpectedFilteredCommits;
    }
}
