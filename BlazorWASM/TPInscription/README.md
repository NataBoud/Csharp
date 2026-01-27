```bash
dotnet new sln -n TPInscription
dotnet new blazorwasm -n TPInscription.Client -f net8.0
dotnet sln add TPInscription.Client/TPInscription.Client.csproj
```