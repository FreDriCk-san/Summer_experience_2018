#!/bin/bash
#### Start program
cd Notebook/Notebook
## Build a project
dotnet build
cd bin/Debug/netcoreapp2.1
## Execute project
dotnet Notebook.dll
