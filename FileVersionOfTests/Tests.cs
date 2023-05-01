/*Copyright(C) 2022 Marcus Trenton, marcus.trenton@gmail.com

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <https://www.gnu.org/licenses/>.*/

using System.Diagnostics;
using System.Text;

namespace FileVersionOfTests
{
    [TestClass]
    public class Tests
    {
        const int ERROR_CODE_FILE_NOT_FOUND = 2;

        private static Process RunFileVersionOfFromCommandLine(string args)
        {
            StringBuilder outputText = new StringBuilder();
            StringBuilder errorText = new StringBuilder();

            Process process = new();
            ProcessStartInfo startInfo = new()
            {
                FileName = "cmd.exe",
                Arguments = "/C FileVersionOf " + args,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();


            return process;
        }

        [TestMethod]
        public void EmptyParametersFails()
        {
            Process process = RunFileVersionOfFromCommandLine(string.Empty);

            Assert.AreEqual(ERROR_CODE_FILE_NOT_FOUND, process.ExitCode, "Wrong exit code");
            string outputText = process.StandardOutput.ReadToEnd();
            Assert.AreEqual(0, outputText.Length, "Incorrectly had output text for an error case");
            string errorText = process.StandardError.ReadToEnd();
            Assert.IsTrue(errorText.Length > 0, "Expected error text not found");
        }

        [TestMethod]
        public void FileNotFoundFails()
        {
            Process process = RunFileVersionOfFromCommandLine("DoesNotExist.exe");

            Assert.AreEqual(ERROR_CODE_FILE_NOT_FOUND, process.ExitCode, "Wrong exit code");
        }

        //FileIsNotCorrectType

        //Read Exe Versions
        //Read DLL Version
    }
}