#Requires -Version 5

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$env:TODOLISTDB = "Data Source=$(Join-Path $PSScriptRoot 'data\todolist.sqlite')"

Push-Location (Join-Path $PSScriptRoot 'src\Todo.WebApp')
