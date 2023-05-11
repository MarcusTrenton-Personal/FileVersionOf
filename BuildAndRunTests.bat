@echo off

msbuild FileVersionOfTests\FileVersionOfTests.csproj -verbosity:quiet
if NOT %errorlevel% == 0 exit /B %errorlevel%
vstest.console.exe FileVersionOfTests\bin\Debug\net7.0\FileVersionOfTests.dll /Settings:FileVersionOfTests\FullTestCleanup.runsettings
if NOT %errorlevel% == 0 exit /B %errorlevel%
