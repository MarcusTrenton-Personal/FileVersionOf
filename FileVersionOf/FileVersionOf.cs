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

using System;
using System.Diagnostics;
using System.IO;

namespace ExeVersion
{
    public class FileVersionOf
    {
        //Following the conventions laid out at https://learn.microsoft.com/en-us/windows/win32/debug/system-error-codes--0-499-
        public const int ERROR_CODE_SUCCESS = 0;
        public const int ERROR_CODE_FILE_NOT_FOUND = 2;
        public const int ERROR_INVALID_DATA = 13;

        public static void Main(string[] args)
        {
            if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
            {
                Console.Error.WriteLine("Missing parameter for exe path");
                PrintUsage();
                Environment.Exit(ERROR_CODE_FILE_NOT_FOUND);
            }

            try
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(args[0]);
                bool foundFileVersion = versionInfo.FileVersion != null;
                if (foundFileVersion) 
                {
                    Console.Out.Write(versionInfo.FileVersion);
                }
                else 
                {
                    ExitWithError("File " + args[0] + " does not have file version.", ERROR_INVALID_DATA);
                }
            }
            catch(FileNotFoundException)
            {
                ExitWithError("File " + args[0] + " not found.", ERROR_CODE_FILE_NOT_FOUND);
            }
        }

        private static void ExitWithError(string message, int errorCode) 
        {
            Console.Error.WriteLine(message);
            PrintUsage();
            Environment.Exit(errorCode);
        }

        private static void PrintUsage()
        {
            Console.Error.WriteLine("Usage: ExeVersion <path>");
            Console.Error.WriteLine("Example: ExeVersion C:\\Windows\\System32\\notepad.exe");
        }
    }
}
