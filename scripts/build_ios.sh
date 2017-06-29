#!/bin/bash

./minamo -cmd=build -config=./configs/ios_dev.json -prompt=false -log=unity_ios_dev.log
cat unity_ios_dev.log
