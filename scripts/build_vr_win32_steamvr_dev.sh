#!/bin/bash

./minamo -cmd=build -config=./configs/vr_win32_steamvr_dev.json -prompt=false -log=unity_vr_win32_steamvr_dev.log
cat unity_vr_win32_steamvr_dev.log
cat unity_vr_win32_steamvr_dev.log | grep "MinamoLog"

