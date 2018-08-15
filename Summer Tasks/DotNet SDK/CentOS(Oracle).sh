#!/bin/bash

## Add the dotnet product feed
sudo rpm -Uvh https://packages.microsoft.com/config/rhel/7/packages-microsoft-prod.rpm

## Install .NET SDK
sudo yum update
sudo yum install dotnet-sdk-2.1
