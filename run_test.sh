#!/bin/bash

BASE_DIR=`pwd`
UNITY_PATH="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
PROJ_PATH=$BASE_DIR/UnityProject
TEST_RESULT=$BASE_DIR/results.xml
LOG_FILE=test.log

$UNITY_PATH -nographics -batchmode -runTests -projectPath $PROJ_PATH -testResults $TEST_RESULT -testPlatform editmode -logFile $LOG_FILE
