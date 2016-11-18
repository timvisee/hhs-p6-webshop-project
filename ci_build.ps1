# Log a fancy message
Function fancyLog($msg)
{
echo "`n----------------------------------------------------------------"
echo "  $msg"
echo "----------------------------------------------------------------`n"
}


# Header
fancyLog -msg "Starting MonoOnWinCI..."
fancyLog -msg "Tool for building Mono projects on Windows CI"
fancyLog -msg "Developed by Tim Visee, timvisee.com.

# Log the current runtimes
fancyLog -msg "Current runtimes:"
dnvm list

# Install required runtimes
fancyLog -msg "Installing required runtimes..."
dnvm install 1.0.0-rc1-update1 -r coreclr -a x86
dnvm install 1.0.0-rc1-update1 -r clr -a x86
dnvm install 1.0.0-rc1-update1 -r coreclr -a x64
dnvm install 1.0.0-rc1-update1 -r clr -a x64

# Log the current runtimes
fancyLog -msg "Current runtimes:"
dnvm list

# Select a runtime to use by default
fancyLog -msg "Selecting default runtime..."
dnvm use 1.0.0-rc1-update1 -a x64 -r coreclr

# Restore the project
dnu feeds list
dnu restore

# Report mono and xbuild versions
fancyLog -msg "Fetching Mono/XBuild versions..."
# & "C:\Program Files (x86)\Mono\bin\mono" /version
& "C:\Program Files (x86)\Mono\bin\xbuild" /version

# Restore NuGet configuration
fancyLog -msg "Restoring NuGet dependencies..."
nuget restore hhs-p6-webshop-project.sln

# Build the actual project using xbuild
fancyLog -msg "Building project (Debug)..."
& "C:\Program Files (x86)\Mono\bin\xbuild" /p:Configuration=Debug hhs-p6-webshop-project.sln
fancyLog -msg "Building project (Release)..."
& "C:\Program Files (x86)\Mono\bin\xbuild" /p:Configuration=Release hhs-p6-webshop-project.sln

# Run the build
# & "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" hhs-p6-webshop-project.sln

## Tests
#dnvm use 1.0.0-rc1-update1 -a x64 -r coreclr
#dnx -p tests\hhs-p6-webshop-project.Tests test -xml xunit-results.xml

## Upload results to AppVeyor
#$wc = New-Object 'System.Net.WebClient'
#$wc.UploadFile("https://ci.appveyor.com/api/testresults/xunit/$($env:APPVEYOR_JOB_ID)", (Resolve-Path .\xunit-results.xml))