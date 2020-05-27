using Soneta.Types;
using System;

namespace EnovaGit.DataTypes
{
    public class GitCommit : IEquatable<GitCommit>
    {
        public string Hashcode { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public Date Date { get; set; }

        public bool Equals(GitCommit other)
        {
            return Hashcode.Equals(other.Hashcode) &&
                Username.Equals(other.Username) &&
                Description.Equals(other.Description) &&
                Date.Equals(other.Date);
        }
    }
}
