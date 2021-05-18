FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CodeAssignmentPeerIslands.Application/CodeAssignmentPeerIslands.Application.csproj", "CodeAssignmentPeerIslands.Application/"]
COPY ["CodeAssignmentPeerIslands.Domain/CodeAssignmentPeerIslands.Domain.csproj", "CodeAssignmentPeerIslands.Domain/"]
COPY ["CodeAssignmentPeerIslands.Infra/CodeAssignmentPeerIslands.Infra.csproj", "CodeAssignmentPeerIslands.Infra/"]
COPY ["CodeAssignmentPeerIslands.Service/CodeAssignmentPeerIslands.Service.csproj", "CodeAssignmentPeerIslands.Service/"]
RUN dotnet restore "CodeAssignmentPeerIslands.Application/CodeAssignmentPeerIslands.Application.csproj"
COPY . .
WORKDIR "/src/CodeAssignmentPeerIslands.Application"
RUN dotnet build "CodeAssignmentPeerIslands.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeAssignmentPeerIslands.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeAssignmentPeerIslands.Application.dll"]