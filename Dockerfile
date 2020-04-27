FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.sln .
COPY ["./Radicitus.Health/Radicitus.Health.csproj", "app/Radicitus.Health/"]
COPY ["./Radicitus.Health.Data/Radicitus.Health.Data.csproj", "app/Radicitus.Health.Data/"]
COPY ["./Radicitus.Health.Dto/Radicitus.Health.Dto.csproj", "app/Radicitus.Health.Dto/"]
RUN dotnet restore "app/Radicitus.Health/Radicitus.Health.csproj"
COPY . .
RUN apt-get update -yq 
RUN apt-get install curl gnupg -yq 
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install -y nodejs
RUN dotnet tool install --global dotnet-ef
RUN dotnet ef migrations script -o migrations.sql
RUN dotnet build "/src/Radicitus.Health/Radicitus.Health.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "/src/Radicitus.Health/Radicitus.Health.csproj" -c Release -o /app/publish

FROM base AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Radicitus.Health.dll"]
