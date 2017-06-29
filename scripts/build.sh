#!/bin/bash

# http://blog.stablekernel.com/continuous-integration-for-unity-5-using-travisci

echo 'Builds - standalone'
./minamo -cmd=build -config=./config_travis-ci/standalone_win_dev.json -prompt=false -log=./unity.log

echo 'Builds - VR win32'
./minamo -cmd=build -config=./config_travis-ci/vr_win32_steamvr_dev.json -prompt=false -log=./unity.log
./minamo -cmd=build -config=./config_travis-ci/vr_win32_oculus_dev.json -prompt=false -log=./unity.log

echo 'Builds - VR ios'
./minamo -cmd=build -config=./config_travis-ci/ios_dev.json -prompt=false -log=./unity.log

echo 'Builds - VR mobile'
./minamo -cmd=build -config=./config_travis-ci/android_daydream_dev.json -prompt=false -log=./unity.log
./minamo -cmd=build -config=./config_travis-ci/android_gearvr_dev.json -prompt=false -log=./unity.log
./minamo -cmd=build -config=./config_travis-ci/android_gearvr_release.json -prompt=false -log=./unity.log

echo 'Logs from build'
cat ./unity.log

echo 'build list'
ls -al ./output
