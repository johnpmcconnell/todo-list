#Requires -Version 5

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$envVars = @{
    "TODOLISTDB" = "Data Source=$(Join-Path $PSScriptRoot 'data\todolist.sqlite')";
}

foreach ($ev in $envVars.GetEnumerator()) {
    $envName = 'env:{0}' -f $ev.Name
    Write-Host ('Setting $' + $envName)
    Set-Item $envName -Value $ev.Value
}

$envFileContent = ($envVars.GetEnumerator() | ForEach-Object { $_.Name + "=" + $_.Value }) -join "`n"

$utf8NoBOM = New-Object System.Text.UTF8Encoding $False
Set-Content (Join-Path $PSScriptRoot '.env') $utf8NoBOM.GetBytes($envFileContent) -Encoding Byte

Push-Location (Join-Path $PSScriptRoot 'src\Todo.WebApp')
