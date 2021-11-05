#!/bin/zsh

dotnet build
cp -rf DatabaseProvider.cs bin/Debug/net5.0 ../../../MMORPG/Assets/Models_Mir2-V2_WebApi