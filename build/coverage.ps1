## WARNING: to run opencover with .NET Core 2.0 you need some requirements
## - the project need 'DebugType' to 'full'
## - the opencover need arguments '-register user -oldstyle'

## Init
$dotnet = "c:\Program Files\dotnet\dotnet.exe"
$packagesFolder = "packages"
$projectTest = "..\src\CPort.Tests\CPort.Tests.csproj"

$output = "coverage"
$outputResult = "$output\coverage.xml"
$history = "$output\history"

# Restore the packages in a specific folder to get the tools for coverage
& dotnet restore $projectTest --packages $packagesFolder

# Search the opencover
$opencover = (Get-ChildItem -Path $packagesFolder -Filter OpenCover.Console.exe -Recurse).FullName
# Search the reportgenerator
$reportGenerator = (Get-ChildItem -Path $packagesFolder -Filter ReportGenerator.exe -Recurse).FullName

# Create output folders if requires
If(!(test-path $output))
{
    New-Item -ItemType Directory -Force -Path $output
}
If(!(test-path $history))
{
    New-Item -ItemType Directory -Force -Path $history
}

# Run the coverage
& "$opencover" -target:"$dotnet" -targetargs:"test ""$projectTest"" -c Debug " -register:user -oldstyle -output:"$outputResult" -filter:"+[CPort*]* -[xunit*]*"
& "$reportGenerator" -reports:"$outputResult" -targetdir:"$output" -reporttypes:"Html;HtmlChart;Badges" -sourcedirs:"..\src\CPort" -historydir:"$history"
