# Ambiente local

## Dependencias

- .NET SDK 8 (o SDK mais novo com runtime .NET 8 instalado)
- Docker Desktop

## Subir as dependencias

Na raiz da solucao, execute:

```powershell
docker compose -f Documentos/Inicializacao/docker-compose.yml up -d
docker compose -f Documentos/Inicializacao/docker-compose.yml ps
```

Esse comando sobe somente o RabbitMQ, pois o ambiente atual ja possui o PostgreSQL `lgg-postgres` em `localhost:5432`.

Para criar o schema e as tabelas no PostgreSQL existente, execute uma vez:

```powershell
.\Documentos\Inicializacao\Initialize-LggDatabase.ps1
```

O script usa o container `lgg-postgres`, banco `postgres` e cria o schema `dbggclt` com todas as tabelas de `Documentos/Sql`. Ele para caso o schema ja exista, evitando sobrescrever uma base local existente.

Em uma maquina que ainda nao tenha PostgreSQL, suba tambem o perfil de banco (`docker compose -f Documentos/Inicializacao/docker-compose.yml --profile database up -d`). Nesse caso, o container cria o schema e as tabelas apenas na primeira inicializacao do volume.

O RabbitMQ fica em `localhost:5672`; o painel esta em `http://localhost:15672` (usuario e senha: `guest`).

Para usar portas ou senha diferentes, copie `Documentos/Inicializacao/.env.example` para `Documentos/Inicializacao/.env`, ajuste os valores e tambem sobrescreva a connection string da aplicacao por variavel de ambiente.

## Executar a API

```powershell
dotnet run --project src/LGG.Api --launch-profile https
```

A API abre em `https://localhost:7182`; a documentacao interativa fica em `https://localhost:7182/swagger`. Em outro terminal, a verificacao inicial pode ser feita com:

```powershell
Invoke-WebRequest https://localhost:7182/Teste/teste_publico -SkipCertificateCheck
```

## Executar os workers

Execute cada processo em um terminal proprio, conforme a necessidade do teste:

```powershell
dotnet run --project src/LGG.Email.Worker
dotnet run --project src/LGG.VideoCiclo.Worker
dotnet run --project src/LGG.VideoMetricas.Worker
```

Os workers fazem polling a cada 15 segundos. O de e-mail envia pendencias da tabela `email_balde`; os outros dois dependem de um campeonato vigente e o de metricas tambem consulta a API do YouTube.

## Parar

```powershell
docker compose -f Documentos/Inicializacao/docker-compose.yml down
```

Esse comando preserva os dados locais. Para recriar o banco do zero, use `docker compose down -v` (isso apaga o volume local do PostgreSQL).
