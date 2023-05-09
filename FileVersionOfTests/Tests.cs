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
using ExeVersion;

namespace FileVersionOfTests
{
    [TestClass]
    public class Tests
    {
        private static Process RunFileVersionOfFromCommandLine(string args)
        {
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
            string outputText = process.StandardOutput.ReadToEnd();
            string errorText = process.StandardError.ReadToEnd();

            Assert.AreEqual(FileVersionOf.ERROR_CODE_FILE_NOT_FOUND, process.ExitCode, "Wrong exit code");
            Assert.AreEqual(0, outputText.Length, "Incorrectly had output text for an error case");
            Assert.IsTrue(errorText.Length > 0, "Expected error text not found");
        }

        [TestMethod]
        public void FileNotFoundFails()
        {
            Process process = RunFileVersionOfFromCommandLine("DoesNotExist.exe");
            string outputText = process.StandardOutput.ReadToEnd();
            string errorText = process.StandardError.ReadToEnd();

            Assert.AreEqual(FileVersionOf.ERROR_CODE_FILE_NOT_FOUND, process.ExitCode, "Wrong exit code");
            Assert.AreEqual(0, outputText.Length, "Incorrectly had output text for an error case");
            Assert.IsTrue(errorText.Length > 0, "Expected error text not found");
        }

        [TestMethod]
        public void FileIsNotCorrectType()
        {
            Process process = RunFileVersionOfFromCommandLine("../../../TestInputs/WrongFileType.txt");
            string outputText = process.StandardOutput.ReadToEnd();
            string errorText = process.StandardError.ReadToEnd();

            Assert.AreEqual(FileVersionOf.ERROR_INVALID_DATA, process.ExitCode, "Wrong exit code");
            Assert.AreEqual(0, outputText.Length, "Incorrectly had output text for an error case");
            Assert.IsTrue(errorText.Length > 0, "Expected error text not found");
        }

        [DataTestMethod]
        [DataRow("TestAllZeros.exe", "0.0.0.0")]
        [DataRow("TestBigNumbers.exe", "65000.65000.65000.65000")]
        [DataRow("TestDefaultFileVersion.exe", "1.0.0.0")]
        [DataRow("TestFullFileVersion.exe", "1.23.3120.216")]
        [DataRow("TestDefaultFileVersion.dll", "1.0.0.0")]
        public void ReadFileVersion(string fileName, string expectedResult)
        {
            string path = Path.Combine("../../../TestInputs/", fileName);
            Process process = RunFileVersionOfFromCommandLine(path);
            string outputText = process.StandardOutput.ReadToEnd();
            string errorText = process.StandardError.ReadToEnd();

            Assert.AreEqual(FileVersionOf.ERROR_CODE_SUCCESS, process.ExitCode, "Wrong exit code");
            Assert.AreEqual(expectedResult, outputText, "Incorrectly had output text for an error case");
            Assert.AreEqual(0, errorText.Length, "Incorrectly found error text");
        }
    }
}