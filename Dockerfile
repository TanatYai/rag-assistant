# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file and project files
COPY *.sln ./
COPY RagAssistant.Api/*.csproj ./RagAssistant.Api/
COPY RagAssistant.Job/*.csproj ./RagAssistant.Job/
COPY RagAssistant.Share/*.csproj ./RagAssistant.Share/

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . ./

# Build API project
WORKDIR /src/RagAssistant.Api
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 7274

ENTRYPOINT ["dotnet", "RagAssistant.Api.dll"]
