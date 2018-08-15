#!/bin/bash
#### Start client
cd Client/Client
## Build a project
dotnet build
cd bin/Debug/netcoreapp2.1
## Execute project
dotnet Client.dll
