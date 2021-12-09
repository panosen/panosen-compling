@echo off

dotnet restore

dotnet build --no-restore -c Release

move /Y Panosen.Compling\bin\Release\Panosen.Compling.*.nupkg D:\LocalSavoryNuget\
move /Y Panosen.Compling.LL1\bin\Release\Panosen.Compling.LL1.*.nupkg D:\LocalSavoryNuget\
move /Y Panosen.Compling.SLR1\bin\Release\Panosen.Compling.SLR1.*.nupkg D:\LocalSavoryNuget\

pause