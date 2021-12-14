FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ./Entities/*.csproj ./Entities/
COPY ./DataAccess/*.csproj ./DataAccess/
COPY ./Business/*.csproj ./Business/
COPY ./Business.Tests/*.csproj ./Business.Tests/
COPY ./WebAPI/*.csproj ./WebAPI/
COPY *.sln .

# restore solution
RUN dotnet restore
COPY . .
RUN dotnet test ./Business.Tests/*.csproj
RUN dotnet publish ./WebAPI/*.csproj -c Release -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:5.0 As runtime
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT [ "dotnet","WebAPI.dll" ]



