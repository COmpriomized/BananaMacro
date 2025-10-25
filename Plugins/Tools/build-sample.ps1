param(
  [string]$ProjectPath = "Plugins/Samples/SamplePlugin",
  [string]$OutputInstall = "Plugins/Installed"
)

dotnet build $ProjectPath -c Release
$bin = Join-Path $ProjectPath "bin/Release/net6.0"
$dll = Join-Path $bin "SamplePlugin.dll"
if (!(Test-Path $dll)) { Write-Error "Built DLL not found: $dll"; exit 1 }

New-Item -ItemType Directory -Force -Path $OutputInstall | Out-Null
Copy-Item -Path $dll -Destination $OutputInstall -Force
Write-Host "Copied $dll to $OutputInstall"