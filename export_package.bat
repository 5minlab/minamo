@set CURR_DIR=%~dp0
@set PACKAGE_NAME=minamo.unitypackage
@set UNITY_PATH="C:\\Program Files\\Unity-5.6.1p1\\Editor\\Unity.exe"
@set PROJ_PATH=%CURR_DIR%\UnityProject
@set EXPORT_DIR=%CURR_DIR%\dist
@set EXPORT_PATH=%EXPORT_DIR%\%PACKAGE_NAME%
@set METHOD=Assets.Minamo.Editor.EntryPoint.ExportPackage

%UNITY_PATH% -quit -batchmode -nographics -silent-crashes -projectPath %PROJ_PATH% -executeMethod %METHOD% -logFile export.log
