#!/bin/bash
#### Start server
cd Server/Server
## Build a project
dotnet build
cd bin/Debug/netcoreapp2.1
## Execute project
dotnet Server.dll
