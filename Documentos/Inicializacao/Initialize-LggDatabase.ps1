param(
    [string]$ContainerName = 'lgg-postgres',
    [string]$Database = 'postgres',
    [string]$User = 'postgres'
)

$ErrorActionPreference = 'Stop'

$schemaExists = docker exec $ContainerName psql -U $User -d $Database -tAc "SELECT 1 FROM information_schema.schemata WHERE schema_name = 'dbggclt';"
if ($schemaExists -match '1') {
    throw "O schema dbggclt ja existe no banco $Database. Nenhuma alteracao foi feita."
}

$sqlDirectory = Join-Path $PSScriptRoot '..\Sql'
$sqlFiles = @(
    'campeonato_mensal.sql',
    'usuario.sql',
    'video.sql',
    'video_ciclo.sql',
    'video_balde.sql',
    'video_estatistica.sql',
    'video_top_dez.sql',
    'email_balde.sql'
)

$sql = "CREATE SCHEMA dbggclt;`n" + (($sqlFiles | ForEach-Object { Get-Content -Raw (Join-Path $sqlDirectory $_) }) -join "`n")
$sql | docker exec -i $ContainerName psql -v ON_ERROR_STOP=1 -U $User -d $Database

if ($LASTEXITCODE -ne 0) {
    throw 'A inicializacao do banco falhou.'
}

Write-Host 'Schema dbggclt e tabelas LGG criados com sucesso.'
