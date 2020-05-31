using EnovaGit.DataTypes;
using EnovaGit.Interfaces;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnovaGit.Commands
{
    public class GitLogCommand : IGitLogCommand
    {
        private const string command = "git log --pretty=format:\"%h ;;; %an ;;; %ad ;;; %s\" --date=short";

        private ICommandRunner CmdRunner { get; set; }

        public GitLogCommand()
        {
            CmdRunner = new CommandRunner();
        }

        public GitLogCommand(ICommandRunner cmdRunner)
        {
            CmdRunner = cmdRunner;
        }

        public List<GitCommit> GetGitCommitList(string repositoryPath)
        {
            if (!Directory.Exists(Path.Combine(repositoryPath, ".git")))
            {
                throw new Exception($"W lokalizacji {repositoryPath} nie znaleziono repozytorium Gita.");
            }

            var result = CmdRunner.Run(command, repositoryPath);
            var errorMsgLines = result.ErrorData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (errorMsgLines.Length != 0)
            {
                throw new Exception(string.Join("\n", errorMsgLines));
            }

            return CreateGitCommitList(result.OutputData);
        }

        private List<GitCommit> CreateGitCommitList(string gitLog)
        {
            var list = new List<GitCommit>();

            foreach (var line in gitLog.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var splitLine = line.Split(new string[] { ";;;" }, StringSplitOptions.None).Select(x => x.Trim()).ToList();

                list.Add(new GitCommit()
                {
                    Hashcode = splitLine[0],
                    Username = splitLine[1],
                    Date = Date.Parse(splitLine[2]),
                    Description = splitLine[3]
                });
            }

            return list;
        }
    }
}
