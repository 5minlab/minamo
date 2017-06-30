CURR_DIR=`pwd`
PACKAGE_NAME=minamo.unitypackage
UNITY_PATH="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
PROJ_PATH=$CURR_DIR/UnityProject
EXPORT_DIR=$CURR_DIR/dist
EXPORT_PATH=$EXPORT_DIR/$PACKAGE_NAME
METHOD=Assets.Minamo.Editor.EntryPoint.ExportPackage
LOG_FILE=export.log

$UNITY_PATH -quit -batchmode -nographics -silent-crashes -projectPath $PROJ_PATH -executeMethod $METHOD -logFile $LOG_FILE
echo $LOG_FILE
