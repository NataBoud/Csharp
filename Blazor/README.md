``` bash
dotnet new globaljson --sdk-version 8.0.0 --roll-forward latestFeature

dotnet new sln -n BlazorDemo

dotnet new blazorwasm -o BlazorDemo.Client -f net8.0 --no-https
dotnet new webapi -o BlazorDemo.Server -f net8.0 --no-openapi --no-https
dotnet new classlib -o BlazorDemo.Shared -f net8.0

dotnet sln add BlazorDemo.Client
dotnet sln add BlazorDemo.Server
dotnet sln add BlazorDemo.Shared

dotnet add BlazorDemo.Client reference BlazorDemo.Shared
dotnet add BlazorDemo.Server reference BlazorDemo.Shared
dotnet add BlazorDemo.Server reference BlazorDemo.Client


<ItemGroup>
  <PackageReference Include="System.ComponentModel.Annotations" Version="5.0.0" />
</ItemGroup>

 <PackageReference Include="Microsoft.Extensions.Http" Version="8.0.0" />

```


