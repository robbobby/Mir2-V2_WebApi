#!/bin/zsh

dotnet build

echo "Removing old dll and metal files"
rm -rf ../../../MMORPG/Assets/Models_Mir2-V2_WebApi
mkdir ../../../MMORPG/Assets/Models_Mir2-V2_WebApi

echo "Adding new dll files"
cp bin/Debug/SharedModels_Mir2_V2.dll ../../../MMORPG/Assets/Models_Mir2-V2_WebApi/

