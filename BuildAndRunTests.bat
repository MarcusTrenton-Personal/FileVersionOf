@echo off

REM msbuild ServicesTests\ServicesTests.csproj -verbosity:quiet
REM if NOT %errorlevel% == 0 exit /B %errorlevel%
REM vstest.console.exe ServicesTests\bin\Debug\netcoreapp3.1\ServicesTests.dll /Settings:TestUtils\FullTestCleanup.runsettings
REM if NOT %errorlevel% == 0 exit /B %errorlevel%
