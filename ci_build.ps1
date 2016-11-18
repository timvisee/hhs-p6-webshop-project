# Log the runtimes
dnvm list
dnvm install 1.0.0-rc1-update1 -r coreclr -a x86
dnvm install 1.0.0-rc1-update1 -r clr -a x86
dnvm install 1.0.0-rc1-update1 -r coreclr -a x64
dnvm install 1.0.0-rc1-update1 -r clr -a x64
dnvm list
dnvm use 1.0.0-rc1-update1 -a x64 -r coreclr
dnu feeds list
dnu restore

# Report mono and xbuild versions
& "C:\Program Files (x86)\Mono\bin\mono" /version
& "C:\Program Files (x86)\Mono\bin\xbuild" /version

# Restore NuGet configuration
nuget restore hhs-p6-webshop-project.sln

# Build the actual project using xbuild
& "C:\Program Files (x86)\Mono\bin\xbuild" /p:Configuration=Release hhs-p6-webshop-project.sln

# Run the build
& "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" hhs-p6-webshop-project.sln

## Tests
#dnvm use 1.0.0-rc1-update1 -a x64 -r coreclr
#dnx -p tests\hhs-p6-webshop-project.Tests test -xml xunit-results.xml

## Upload results to AppVeyor
#$wc = New-Object 'System.Net.WebClient'
#$wc.UploadFile("https://ci.appveyor.com/api/testresults/xunit/$($env:APPVEYOR_JOB_ID)", (Resolve-Path .\xunit-results.xml))