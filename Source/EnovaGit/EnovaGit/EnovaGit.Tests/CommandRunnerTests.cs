using NUnit.Framework;
using System.Linq;

namespace EnovaGit.Tests
{
    public class CommandRunnerTests
    {
        private const string Path1 = @"C:\Program Files (x86)\Soneta";
        private const string Path2 = @"C:\Users\Public";

        [TestCase("cd", Path1, Path1)]
        [TestCase("cd", Path2, Path2)]
        [TestCase("echo", Path1, "ECHO is on.")]
        public void CommandRunner_DoesItReturnStdOutputText(string cmd, string wd, string expResult)
        {
            var result = new CommandRunner().Run(cmd, wd).TakeWhile(x => x != '\r');
            Assert.AreEqual(expResult, result);
        }
    }
}
