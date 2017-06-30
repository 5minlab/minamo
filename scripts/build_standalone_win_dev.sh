#!/bin/bash

./minamo -cmd=build -config=./configs/standalone_win_dev.json -prompt=false -log=unity_standalone_win_dev.log
cat unity_standalone_win_dev.log | grep "MinamoLog"
cat unity_standalone_win_dev.log
