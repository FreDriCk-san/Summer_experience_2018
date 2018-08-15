#!/bin/bash

## Add the dotnet product feed
sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
wget -q https://packages.microsoft.com/config/fedora/27/prod.repo
sudo mv prod.repo /etc/yum.repos.d/microsoft-prod.repo
sudo chown root:root /etc/yum.repos.d/microsoft-prod.repo

## Install .NET SDK
sudo dnf update
sudo dnf install dotnet-sdk-2.1
