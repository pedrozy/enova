using EnovaGit.DataTypes;
using EnovaGit.Tests.DataTypes;
using Soneta.Types;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace EnovaGit.Tests
{
    public class Common
    {
        public static string GitRepositoryPath
        {
            get
            {
                var dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return Path.Combine(dirPath, "DirForTests", "GitRepository");
            }
        }

        public static string Username1 { get { return "Jan Kowalski"; } }
        public static string Username2 { get { return "Łukasz Nowak"; } }
        public static string Username3 { get { return "Andrzej Krok"; } }
        public static string Username4 { get { return "Robert Korba"; } }

        public static IEnumerable<TestData> TestConfiguration
        {
            get
            {
                var RawGitLog0 = string.Join("\r\n",
                    $"hashcode8 ; {Common.Username1} ; 2020-05-22 ; Przykładowy opis commita 8",
                    $"hashcode7 ; {Common.Username1} ; 2020-05-22 ; Przykładowy opis commita 7",
                    $"hashcode6 ; {Common.Username1} ; 2020-05-22 ; Przykładowy opis commita 6",
                    $"hashcode5 ; {Common.Username2} ; 2020-05-21 ; Przykładowy opis commita 5",
                    $"hashcode4 ; {Common.Username1} ; 2020-05-21 ; Przykładowy opis commita 4",
                    $"hashcode3 ; {Common.Username2} ; 2020-05-20 ; Przykładowy opis commita 3",
                    $"hashcode2 ; {Common.Username3} ; 2020-05-19 ; Przykładowy opis commita 2",
                    $"hashcode1 ; {Common.Username1} ; 2020-05-18 ; Przykładowy opis commita 1"
                );

                var GitCommitCollection0 = new List<GitCommit>()
                {
                    new GitCommit() { Hashcode = "hashcode8", Username = Common.Username1, Date = Date.Parse("2020-05-22"), Description = "Przykładowy opis commita 8" },
                    new GitCommit() { Hashcode = "hashcode7", Username = Common.Username1, Date = Date.Parse("2020-05-22"), Description = "Przykładowy opis commita 7" },
                    new GitCommit() { Hashcode = "hashcode6", Username = Common.Username1, Date = Date.Parse("2020-05-22"), Description = "Przykładowy opis commita 6" },
                    new GitCommit() { Hashcode = "hashcode5", Username = Common.Username2, Date = Date.Parse("2020-05-21"), Description = "Przykładowy opis commita 5" },
                    new GitCommit() { Hashcode = "hashcode4", Username = Common.Username1, Date = Date.Parse("2020-05-21"), Description = "Przykładowy opis commita 4" },
                    new GitCommit() { Hashcode = "hashcode3", Username = Common.Username2, Date = Date.Parse("2020-05-20"), Description = "Przykładowy opis commita 3" },
                    new GitCommit() { Hashcode = "hashcode2", Username = Common.Username3, Date = Date.Parse("2020-05-19"), Description = "Przykładowy opis commita 2" },
                    new GitCommit() { Hashcode = "hashcode1", Username = Common.Username1, Date = Date.Parse("2020-05-18"), Description = "Przykładowy opis commita 1" }
                };

                var RawGitLog1 = string.Join("\r\n",
                    $"af4c3d2a ; {Common.Username1} ; 2020-05-23 ; Bug fixing",
                    $"54ca3b9f ; {Common.Username1} ; 2020-05-23 ; Adding new functionality",
                    $"3d2ea511 ; {Common.Username4} ; 2020-05-23 ; Initial commit"
                );

                var GitCommitCollection1 = new List<GitCommit>()
                {
                    new GitCommit() { Hashcode = "af4c3d2a", Username = Common.Username1, Date = Date.Parse("2020-05-23"), Description = "Bug fixing" },
                    new GitCommit() { Hashcode = "54ca3b9f", Username = Common.Username1, Date = Date.Parse("2020-05-23"), Description = "Adding new functionality" },
                    new GitCommit() { Hashcode = "3d2ea511", Username = Common.Username4, Date = Date.Parse("2020-05-23"), Description = "Initial commit" }
                };

                return new List<TestData>()
                {
                    new TestData()
                    {
                        RawGitLog = RawGitLog0,
                        GitCommitCollection = GitCommitCollection0,

                        ExpectedUsernameList = new List<string>()
                        {
                            Common.Username3, Common.Username1, Common.Username2
                        },
                        StatisticsTestDataList = new List<StatisticsTestData>()
                        {
                            new StatisticsTestData()
                            {
                                Username = Common.Username1,
                                Date = Date.Empty,
                                ExpectedAverage = 1.66666663f,
                                ExpectedDaily = 5.0f,
                                ExpectedFilteredCommits = new List<GitCommit>()
                                {
                                    GitCommitCollection0[0],
                                    GitCommitCollection0[1],
                                    GitCommitCollection0[2],
                                    GitCommitCollection0[4],
                                    GitCommitCollection0[7]
                                }
                            },
                            new StatisticsTestData()
                            {
                                Username = Common.Username1,
                                Date = Date.Parse("2020-05-21"),
                                ExpectedAverage = 1.66666663f,
                                ExpectedDaily = 1.0f,
                                ExpectedFilteredCommits = new List<GitCommit>()
                                {
                                    GitCommitCollection0[4]
                                }
                            },
                            new StatisticsTestData()
                            {
                                Username = Common.Username2,
                                Date = Date.Empty,
                                ExpectedAverage = 1.0f,
                                ExpectedDaily = 2.0f,
                                ExpectedFilteredCommits = new List<GitCommit>()
                                {
                                    GitCommitCollection0[3],
                                    GitCommitCollection0[5]
                                }
                            },
                            new StatisticsTestData()
                            {
                                Username = string.Empty,
                                Date = Date.Parse("2020-05-21"),
                                ExpectedAverage = 0.0f,
                                ExpectedDaily = 2.0f,
                                ExpectedFilteredCommits = new List<GitCommit>()
                                {
                                    GitCommitCollection0[3],
                                    GitCommitCollection0[4]
                                }
                            }
                        }
                    },
                    new TestData()
                    {
                        RawGitLog = RawGitLog1,
                        GitCommitCollection = GitCommitCollection1,

                        ExpectedUsernameList = new List<string>()
                        {
                            Common.Username1, Common.Username4
                        },
                        StatisticsTestDataList = new List<StatisticsTestData>()
                    }
                };
            }
        }
    }
}
