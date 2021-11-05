#!/bin/zsh

dotnet build

echo "Removing old dll and metal files"
rm -rf ../../../MMORPG/Assets/Models_Mir2-V2_WebApi

echo "Adding new dll files"
cp -rf bin/Debug ../../../MMORPG/Assets/Models_Mir2-V2_WebApi

