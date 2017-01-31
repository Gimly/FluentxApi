# All file took (with modifications from http://andrewlock.net/publishing-your-first-nuget-package-with-appveyor-and-myget/)

<#
.SYNOPSIS
    This method will test if psbuild is installed by checking that
    the command Invoke-MSBuild is available.
    If it doesn't exist, it will call Install-Module psbuild to install it
#>
function Assert-PsBuildInstalled {
    if (-not (Get-Command "Invoke-MSBuild" -ErrorAction SilentlyContinue)) {
        Write-Verbose "Installing PsBuild"
        Install-Module psbuild
    } else{
        Write-Verbose "psbuild already installed, yay!"
    }

    if (-not (Get-Command "Invoke-MSBuild" -ErrorAction SilentlyContinue)) {
        throw ('Something went wrong with the psbuild install.')
    }
}

# Taken from psake https://github.com/psake/psake

<#  
.SYNOPSIS
  This is a helper function that runs a scriptblock and checks the PS variable $lastexitcode
  to see if an error occcured. If an error is detected then an exception is thrown.
  This function allows you to run command-line programs without having to
  explicitly check the $lastexitcode variable.
.EXAMPLE
  exec { svn info $repository_trunk } "Error executing SVN. Please verify SVN command-line client is installed"
#>
function Exec  
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

if (Test-Path .\artifacts) {
    Remove-Item .\artifacts -Force -Recurse
}

Assert-PsBuildInstalled

exec {& dotnet restore .\src\Mos.xApi}

Invoke-MSBuild

$revision = @{ $true = $env:APPVEYOR_BUILD_NUMBER; $false = 1 }[$env:APPVEYOR_BUILD_NUMBER -ne $NULL];
$revision = "{0:D4}" -f [convert]::ToInt32($revision, 10)

# exec { & dotnet test .\test\Mos.xApi.Test -c Release }

exec { & dotnet pack .\src\Mos.xApi -c Release -o .\artifacts --version-suffix=$revision }