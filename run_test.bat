@set BASE_DIR=%~dp0
@set UNITY_PATH="C:\\Program Files\\Unity-5.6.1p1\\Editor\\Unity.exe"
@set PROJ_PATH=%BASE_DIR%\UnityProject
@set TEST_RESULT=%BASE_DIR%\results.xml

%UNITY_PATH% -nographics -batchmode -runTests -projectPath %PROJ_PATH% -testResults %TEST_RESULT% -testPlatform editmode -logFile test.log
