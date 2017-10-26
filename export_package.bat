@set CURR_DIR=%~dp0
@set UNITY_PATH="C:\\Program Files\\Unity 2017.2.0f3\\Editor\\Unity.exe"
@set PROJ_PATH=%CURR_DIR%\UnityProject_Minamo
@set EXPORT_DIR=%CURR_DIR%\dist
@set METHOD=Assets.Minamo.Editor.EntryPoint.ExportPackage
@set CONFIG_PATH=%CURR_DIR%\configs_dev\local.json

@minamo.exe -cmd=show -config=%CONFIG_PATH% -field=minamo_version > temp.txt
@set /p VERSION=<temp.txt
del temp.txt
@set PACKAGE_NAME=minamo-%VERSION%.unitypackage
@set EXPORT_PATH=%EXPORT_DIR%\%PACKAGE_NAME%

%UNITY_PATH% -quit -batchmode -nographics -silent-crashes -projectPath %PROJ_PATH% -executeMethod %METHOD% -logFile export.log
