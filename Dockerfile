# syntax=docker/dockerfile:1

# https://docs.docker.com/go/dockerfile-reference/
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
# https://github.com/dotnet/dotnet-docker/blob/main/samples/enable-globalization.md
################################################################################
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

COPY ./GoGoSumo.Server/ /source/GoGoSumo.Server/
COPY ./GoGoSumo.DTOs/ /source/GoGoSumo.DTOs/

# Setup https folder with perms
RUN mkdir /https
RUN chown $APP_UID:$APP_UID /https

WORKDIR /source

ARG TARGETARCH

# Build the application.
# Leverage a cache mount to /root/.nuget/packages so that subsequent builds don't have to re-download packages.
# If TARGETARCH is "amd64", replace it with "x64" - "x64" is .NET's canonical name for this and "amd64" doesn't
#   work in .NET 6.0.
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish GoGoSumo.Server/GoGoSumo.Server.csproj -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

################################################################################
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

COPY --from=build /app .

# Set logging on postgresql config file
#RUN echo "log_statement = 'all'" >> /var/lib/postgresql/data/postgresql.conf

# See https://docs.docker.com/go/dockerfile-user-best-practices/
# and https://github.com/dotnet/dotnet-docker/discussions/4764
USER $APP_UID

ENTRYPOINT ["dotnet", "GoGoSumo.Server.dll"]
