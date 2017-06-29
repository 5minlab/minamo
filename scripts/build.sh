#!/bin/bash

# http://blog.stablekernel.com/continuous-integration-for-unity-5-using-travisci

echo "Attempting to build - win32_steamvr_dev"
./minamo -config=config_travis-ci/win32_steamvr_dev.json -prompt=false -log=./unity.log

echo 'Logs from build'
cat ./unity.log

echo 'build list'
ls -al ./output

# echo "Attempting to build $project for Windows"
# /Applications/Unity/Unity.app/Contents/MacOS/Unity \
#   -batchmode \
#   -nographics \
#   -silent-crashes \
#   -logFile $(pwd)/unity.log \
#   -projectPath $(pwd) \
#   -buildWindowsPlayer "$(pwd)/Build/windows/$project.exe" \
#   -quit

# echo "Attempting to build $project for OS X"
# /Applications/Unity/Unity.app/Contents/MacOS/Unity \
#   -batchmode \
#   -nographics \
#   -silent-crashes \
#   -logFile $(pwd)/unity.log \
#   -projectPath $(pwd) \
#   -buildOSXUniversalPlayer "$(pwd)/Build/osx/$project.app" \
#   -quit

# echo "Attempting to build $project for Linux"
# /Applications/Unity/Unity.app/Contents/MacOS/Unity \
#   -batchmode \
#   -nographics \
#   -silent-crashes \
#   -logFile $(pwd)/unity.log \
#   -projectPath $(pwd) \
#   -buildLinuxUniversalPlayer "$(pwd)/Build/linux/$project.exe" \
#   -quit

# echo 'Logs from build'
# cat $(pwd)/unity.log


# echo 'Attempting to zip builds'
# zip -r $(pwd)/Build/linux.zip $(pwd)/Build/linux/
#zip -r $(pwd)/Build/mac.zip $(pwd)/Build/osx/
#zip -r $(pwd)/Build/windows.zip $(pwd)/Build/windows/
