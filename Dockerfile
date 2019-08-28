FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
COPY ["EmployeePortal.Web/EmployeePortal.Web.csproj", "EmployeePortal.Web/"]
COPY ["EmployeePortal.SkillManagement/EmployeePortal.SkillManagement.csproj", "EmployeePortal.SkillManagement/"]
COPY ["EmployeePortal.Infrastructure/EmployeePortal.Infrastructure.csproj", "EmployeePortal.Infrastructure/"]
COPY ["EmployeePortal.TimeRegistration/EmployeePortal.TimeRegistration.csproj", "EmployeePortal.TimeRegistration/"]
RUN dotnet restore "EmployeePortal.Web/EmployeePortal.Web.csproj"
COPY . ./
WORKDIR "EmployeePortal.Web"
RUN dotnet build "EmployeePortal.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EmployeePortal.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EmployeePortal.Web.dll"]