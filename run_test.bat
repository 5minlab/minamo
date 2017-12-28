@set BASE_DIR=%~dp0
@set UNITY_PATH="C:\\Program Files\\Unity 2017.3.0f3\\Editor\\Unity.exe"
@set PROJ_PATH=%BASE_DIR%\UnityProject_Minamo
@set TEST_RESULT=%BASE_DIR%\results.xml

%UNITY_PATH% -nographics -batchmode -runTests -projectPath %PROJ_PATH% -testResults %TEST_RESULT% -testPlatform editmode -logFile test.log
