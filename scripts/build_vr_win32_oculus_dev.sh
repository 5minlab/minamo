#!/bin/bash

./minamo -cmd=build -config=./configs/vr_win32_oculus_dev.json -prompt=false -log=unity_vr_win32_oculus_dev.log
cat unity_vr_win32_oculus_dev.log
