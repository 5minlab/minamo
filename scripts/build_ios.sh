#!/bin/bash

./minamo -cmd=build -config=./configs/ios_dev.json -log=unity_ios_dev.log
cat unity_ios_dev.log | grep "MinamoLog"
cat unity_ios_dev.log
