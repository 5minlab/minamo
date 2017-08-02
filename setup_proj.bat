@set BIN_DIR=%~dp0
@set BASE_DIR=%BIN_DIR%\
@set BASE_PROJ_DIR=%BASE_DIR%\UnityProject_Minamo
@set ANDROID_PROJ_DIR=%BASE_DIR%\UnityProject_Android
@set IOS_PROJ_DIR=%BASE_DIR%\UnityProject_iOS
@set STANDALONE_PROJ_DIR=%BASE_DIR%\UnityProject_Standalone

mkdir %ANDROID_PROJ_DIR%
mklink /j %ANDROID_PROJ_DIR%\Assets %BASE_PROJ_DIR%\Assets
mklink /j %ANDROID_PROJ_DIR%\ProjectSettings %BASE_PROJ_DIR%\ProjectSettings

mkdir %IOS_PROJ_DIR%
mklink /j %IOS_PROJ_DIR%\Assets %BASE_PROJ_DIR%\Assets
mklink /j %IOS_PROJ_DIR%\ProjectSettings %BASE_PROJ_DIR%\ProjectSettings

mkdir %STANDALONE_PROJ_DIR%
mklink /j %STANDALONE_PROJ_DIR%\Assets %BASE_PROJ_DIR%\Assets
mklink /j %STANDALONE_PROJ_DIR%\ProjectSettings %BASE_PROJ_DIR%\ProjectSettings
