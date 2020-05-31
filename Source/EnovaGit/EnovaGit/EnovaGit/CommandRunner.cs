using EnovaGit.DataTypes;
using EnovaGit.Interfaces;
using System.Diagnostics;
using System.Text;

namespace EnovaGit
{
    public class CommandRunner : ICommandRunner
    {
        public CommandOutput Run(string command, string workingDirectory)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo("cmd.exe")
                {
                    WorkingDirectory = workingDirectory,
                    Arguments = $"/C {command}",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            StringBuilder OutputStringBuilder = new StringBuilder();
            StringBuilder ErrorStringBuilder = new StringBuilder();

            process.OutputDataReceived += delegate (object sender, DataReceivedEventArgs e)
            {
                OutputStringBuilder.AppendLine(e.Data);
            };

            process.ErrorDataReceived += delegate (object sender, DataReceivedEventArgs e)
            {
                ErrorStringBuilder.AppendLine(e.Data);
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            return new CommandOutput()
            {
                OutputData = ConvertToUTF8(OutputStringBuilder.ToString()),
                ErrorData = ConvertToUTF8(ErrorStringBuilder.ToString())
            };
        }

        private string ConvertToUTF8(string text)
        {
            byte[] bytes = Encoding.Default.GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
