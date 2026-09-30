# SGI-JMC

Sistema de Gerenciamento Interno do projeto SGI-JMC, voltado à administração de rotinas administrativas de secretarias escolares

## Registro de Software

O software **SGI-JMC — Sistema de Gerenciamento Interno** encontra-se registrado junto ao Instituto Nacional da Propriedade Industrial (INPI), sob o seguinte processo:

**Processo Nº: BR512024000816-0**

## Funcionalidades

- Cadastro e consulta de alunos, matrículas e transferências.
- Gestão de frequência, horários, advertências e comunicados.
- Emissão de declarações, ofícios, contratos e documentos escolares.
- Autenticação e controle de usuários e perfis.
- Envio de e-mails para os fluxos que dependem de comunicação.

## Tecnologias

- ASP.NET Core MVC com Razor Pages.
- .NET Core 3.1.
- Entity Framework Core com SQL Server.
- ASP.NET Core Identity.
- MailKit para envio de e-mail e bibliotecas iText/PdfSharp para geração de PDFs.

> O projeto usa .NET Core 3.1, que está fora de suporte. Essa versão não recebe atualizações de segurança. A execução local requer o SDK e o runtime compatíveis; planeje a atualização do framework antes de expor a aplicação à internet.

## Pré-requisitos

- .NET Core SDK 3.1.
- SQL Server acessível pela máquina que executa a aplicação.
- Visual Studio ou VS Code (opcional).

## Configuração local

O projeto precisa de uma connection string chamada `DefaultConnection`. Configure-a com User Secrets para desenvolvimento, sem colocar senhas no controle de versão:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<connection-string-do-sql-server>" --project SGI-JMC/SGI-JMC.csproj
```

Para habilitar o envio de e-mail, configure também estes valores de User Secrets:

```powershell
dotnet user-secrets set "GmailSettings:NomeRemetente" "<nome-do-remetente>" --project SGI-JMC/SGI-JMC.csproj
dotnet user-secrets set "GmailSettings:EmailRemetente" "<email>" --project SGI-JMC/SGI-JMC.csproj
dotnet user-secrets set "GmailSettings:Senha" "<senha-ou-senha-de-aplicativo>" --project SGI-JMC/SGI-JMC.csproj
dotnet user-secrets set "GmailSettings:EnderecoServidor" "smtp.gmail.com" --project SGI-JMC/SGI-JMC.csproj
dotnet user-secrets set "GmailSettings:PortaServidor" "465" --project SGI-JMC/SGI-JMC.csproj
dotnet user-secrets set "GmailSettings:UsarSsl" "true" --project SGI-JMC/SGI-JMC.csproj
```

Use credenciais próprias para o ambiente. Não publique senhas, tokens ou connection strings com credenciais em arquivos versionados.

## Executar

Na raiz do repositório, restaure e compile a solução:

```powershell
dotnet restore SGI-JMC.sln
dotnet build SGI-JMC.sln
```

Inicie a aplicação no ambiente de desenvolvimento:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run --project SGI-JMC/SGI-JMC.csproj
```

O endereço local será informado pelo `dotnet run`. A rota inicial abre a consulta de matrículas atuais. A aplicação precisa que o SQL Server configurado esteja disponível.

## Estrutura

- `SGI-JMC/Controllers`: endpoints MVC das funcionalidades.
- `SGI-JMC/Models` e `SGI-JMC/ViewModels`: modelos de domínio e de apresentação.
- `SGI-JMC/Views`: páginas Razor.
- `SGI-JMC/Data` e `SGI-JMC/Migrations`: acesso a dados e migrações do Entity Framework Core.
- `SGI-JMC/Services`: serviços da aplicação, incluindo envio de e-mail.
- `SGI-JMC/wwwroot`: arquivos estáticos.
