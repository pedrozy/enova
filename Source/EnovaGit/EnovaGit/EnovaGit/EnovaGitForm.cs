using EnovaGit;
using EnovaGit.Commands;
using EnovaGit.DataTypes;
using EnovaGit.Interfaces;
using Soneta.Business;
using Soneta.Business.App;
using Soneta.Business.Licence;
using Soneta.Business.UI;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: FolderView("Git",
    Priority = 0,
    Description = "Moduł obsługi repozytorium Git",
    BrickColor = FolderViewAttribute.BlackBrick,
    Contexts = new object[] { LicencjeModułu.All }
    )]


[assembly: FolderView("Git/Statystyki",
    Priority = 1,
    Description = "Statystyki commitów",
    BrickColor = FolderViewAttribute.BlackBrick,
    ObjectType = typeof(EnovaGitForm),
    ObjectPage = "EnovaGitForm.Statystyki.pageform.xml",
    ReadOnlySession = false,
    ConfigSession = false
    )]

// Sposób w jaki należy zarejestrować extender, który później zostanie użyty w interfejsie.
[assembly: Worker(typeof(EnovaGitForm))]

namespace EnovaGit
{
    public class EnovaGitForm
    {
        private IEnumerable<GitCommit> GitCommitList;

        private IGitLogCommand GitLogCmd;
        private IDialog DialogController;

        public EnovaGitForm()
        {
            GitCommitList = new List<GitCommit>();
            GitLogCmd = new GitLogCommand();
            DialogController = new Dialog();
        }

        public EnovaGitForm(IGitLogCommand gitLogCmd, IDialog dialogController)
        {
            GitCommitList = new List<GitCommit>();
            GitLogCmd = gitLogCmd;
            DialogController = dialogController;
        }

        [Context]
        public Session Session { get; set; }

        [Context]
        public Login Login { get; set; }

        public string User { get; set; } = string.Empty;
        public string RepositoryPath { get; set; }
        public Date Date { get; set; }

        public float Average
        {
            get
            {
                var commitsByUser = GitCommitList.Where(x => x.Username == User).ToList();
                var dates = commitsByUser.Select(x => x.Date).Distinct().ToList();

                if (dates.Count == 0) return 0;

                return (float) commitsByUser.Count / (float) dates.Count;
            }
        }

        public float Daily
        {
            get { return GitCommitListFiltered.ToList().Count; }
        }

        public IEnumerable<GitCommit> GitCommitListFiltered
        {
            get
            {
                var list = GitCommitList;

                if (!string.IsNullOrEmpty(User))
                {
                    list = list.Where(x => x.Username == User).ToList();
                }

                if (Date != Date.Empty)
                {
                    list = list.Where(x => x.Date == Date).ToList();
                }

                return list;
            }
        }

        public List<string> GetListUser()
        {
            var list = GitCommitList.Select(x => x.Username).Distinct().ToList();
            list.Sort();

            return list;
        }

        public void PrzegladajAction()
        {
            if (DialogController.ShowFolderBrowserDialog())
            {
                try
                {
                    RepositoryPath = DialogController.SelectedPath;
                    GitCommitList = GitLogCmd.GetGitCommitList(RepositoryPath);
                }
                catch (Exception e)
                {
                    DialogController.ShowErrorMessage(e.Message);
                    GitCommitList = new List<GitCommit>();
                }

                User = string.Empty;                
                Session?.InvokeChanged();
            }
        }
    }
}
