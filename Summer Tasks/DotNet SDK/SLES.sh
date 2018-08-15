#!/bin/bash

## Add the dotnet product feed
sudo rpm -Uvh https://packages.microsoft.com/config/sles/12/packages-microsoft-prod.rpm

## Install .NET SDK
sudo zypper update
sudo zypper install dotnet-sdk-2.1
