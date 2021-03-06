# escape=`

# This Dockerfile will build the Sitecore solution and save the build artifacts for use in
# other images, such as 'cm' and 'rendering'. It does not produce a runnable image itself.

ARG BUILD_IMAGE

# In a separate image (as to not affect layer cache), gather all NuGet-related solution assets, so that
# we have what we need to run a cached NuGet restore in the next layer:
# https://stackoverflow.com/questions/51372791/is-there-a-more-elegant-way-to-copy-specific-files-using-docker-copy-to-the-work/61332002#61332002
# This technique is described here:
# https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-3.1#the-dockerfile-1
FROM ${BUILD_IMAGE} AS nuget-prep
COPY *.sln nuget.config Directory.build.props Directory.build.targets /nuget/
COPY src/ /temp/
RUN Invoke-Expression 'robocopy C:/temp C:/nuget/src /s /ndl /njh /njs *.csproj *.scproj packages.config'

FROM ${BUILD_IMAGE} AS builder
ARG BUILD_CONFIGURATION

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]
WORKDIR /build

RUN Invoke-WebRequest -OutFile nodejs.zip -UseBasicParsing "https://nodejs.org/dist/v14.16.0/node-v14.16.0-win-x64.zip"; `
    Expand-Archive nodejs.zip -DestinationPath C:\; `
    Rename-Item "C:\\node-v14.16.0-win-x64" c:\nodejs
RUN SETX /M PATH $($Env:PATH + ';C:\nodejs')
RUN npm config set registry https://registry.npmjs.org/
# Copy prepped NuGet artifacts, and restore as distinct layer to take advantage of caching.
COPY --from=nuget-prep ./nuget ./

# Restore NuGet packages
RUN nuget restore -Verbosity quiet

# Copy remaining source code
COPY src/ ./src/

# Ensure deploy folder exist to prevent errors on initial build
RUN mkdir ./docker/deploy/platform

# Build the Sitecore main platform artifacts
RUN msbuild .\src\feature\workbox\code\Feature.Workbox.csproj /p:Configuration=$env:BUILD_CONFIGURATION /t:Restore,Build /p:DeployOnBuild=true /p:PublishUrl=C:\build\docker\deploy\platform /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem

WORKDIR c:\build\src\client
RUN npm install
RUN npm run build
RUN npm run toaspx

# Save the artifacts for copying into other images (see 'cm' and 'rendering' Dockerfiles).
FROM mcr.microsoft.com/windows/nanoserver:1809
WORKDIR /artifacts
COPY --from=builder /build/docker/deploy/ ./
COPY --from=builder /build/src/client/build ./platform/sitecore/shell/client/Applications/advancedworkbox/