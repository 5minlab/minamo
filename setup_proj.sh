#!/bin/bash

BASE_PROJ_DIR="$(pwd)/UnityProject"
ANDROID_PROJ_DIR="UnityProject_Android"
IOS_PROJ_DIR="UnityProject_iOS"
STANDALONE_PROJ_DIR="UnityProject_Standalone"

mkdir $ANDROID_PROJ_DIR
ln -s $BASE_PROJ_DIR/Assets $ANDROID_PROJ_DIR/Assets
ln -s $BASE_PROJ_DIR/ProjectSettings $ANDROID_PROJ_DIR/ProjectSettings

mkdir $IOS_PROJ_DIR
ln -s $BASE_PROJ_DIR/Assets $IOS_PROJ_DIR/Assets
ln -s $BASE_PROJ_DIR/ProjectSettings $IOS_PROJ_DIR/ProjectSettings

mkdir $STANDALONE_PROJ_DIR
ln -s $BASE_PROJ_DIR/Assets $STANDALONE_PROJ_DIR/Assets
ln -s $BASE_PROJ_DIR/ProjectSettings $STANDALONE_PROJ_DIR/ProjectSettings
