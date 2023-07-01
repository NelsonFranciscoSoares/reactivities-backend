Write-Host "About to Create the directory" -ForegroundColor Green

mkdir "01 - Reactivities"
cd "01 - Reactivities"

Write-Host "About to create the solution and projects" -ForegroundColor Green
dotnet new sln
mkdir src
cd src
dotnet new webapi -n API
dotnet new classlib -n Application
dotnet new classlib -n Domain
dotnet new classlib -n Persistence

Write-Host "Adding projects to the solution" -ForegroundColor Green
cd ..
dotnet sln add ./src/API/API.csproj
dotnet sln add ./src/Application/Application.csproj
dotnet sln add ./src/Domain/Domain.csproj
dotnet sln add ./src/Persistence/Persistence.csproj

Write-Host "Adding project references" -ForegroundColor Green
cd ./src/API
dotnet add reference ../Application/Application.csproj
cd ../Application
dotnet add reference ../Domain/Domain.csproj
dotnet add reference ../Persistence/Persistence.csproj
cd ../Persistence
dotnet add reference ../Domain/Domain.csproj
cd ../..

Write-Host "Executing dotnet restore" -ForegroundColor Green
dotnet restore

Write-Host "Finished!" -ForegroundColor Green
