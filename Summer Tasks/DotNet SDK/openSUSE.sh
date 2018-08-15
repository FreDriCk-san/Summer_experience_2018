#!/bin/bash

## Add the dotnet product feed
sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
wget -q https://packages.microsoft.com/config/opensuse/42.2/prod.repo
sudo mv prod.repo /etc/zypp/repos.d/microsoft-prod.repo
sudo chown root:root /etc/zypp/repos.d/microsoft-prod.repo

## Install .NET SDK
sudo zypper update
sudo zypper install dotnet-sdk-2.1
