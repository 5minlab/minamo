#!/bin/bash

CURR_DIR=`pwd`
UNITY_PATH="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
PROJ_PATH=$CURR_DIR/UnityProject
EXPORT_DIR=$CURR_DIR/dist
METHOD=Assets.Minamo.Editor.EntryPoint.ExportPackage
CONFIG_PATH=$CURR_DIR/configs/standalone_win_dev.json

VERSION=`./minamo -cmd=show -config=$CONFIG_PATH -field=minamo_version`
PACKAGE_NAME=minamo-$VERSION.unitypackage
export EXPORT_PATH=$EXPORT_DIR/$PACKAGE_NAME

LOG_FILE=export.log

$UNITY_PATH -quit -batchmode -nographics -silent-crashes -projectPath $PROJ_PATH -executeMethod $METHOD -logFile $LOG_FILE
