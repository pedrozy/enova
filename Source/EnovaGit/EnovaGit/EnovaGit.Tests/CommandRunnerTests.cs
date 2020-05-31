using NUnit.Framework;
using System.Linq;

namespace EnovaGit.Tests
{
    public class CommandRunnerTests
    {
        [TestCase("cd", Common.Path1, Common.Path1)]
        [TestCase("cd", Common.Path2, Common.Path2)]
        [TestCase("echo", Common.Path1, "ECHO is on.")]
        public void CommandRunner_DoesItReturnStdOutputText(string cmd, string wd, string expOutput)
        {
            var expError = string.Empty;
            var result = new CommandRunner().Run(cmd, wd);

            var output = result.OutputData.TakeWhile(x => x != '\r');
            var error = result.ErrorData.TakeWhile(x => x != '\r');

            Assert.AreEqual(expOutput, output);
            Assert.AreEqual(expError, error);
        }

        [Test]
        public void CommandRunner_DoesItReturnErrorText()
        {
            var command = "hhh";

            var expOutput = string.Empty;
            var expError = $"\'{command}\' is not recognized as an internal or external command,";

            var result = new CommandRunner().Run(command, Common.Path2);

            var output = result.OutputData.TakeWhile(x => x != '\r');
            var error = result.ErrorData.TakeWhile(x => x != '\r');

            Assert.AreEqual(expOutput, output);
            Assert.AreEqual(expError, error);
        }
    }
}
