#!/bin/sh

# http://blog.stablekernel.com/continuous-integration-for-unity-5-using-travisci

# view-source:https://store.unity.com/kr/download/thank-you?thank-you=personal&os=osx&nid=325
BASE_URL=http://netstorage.unity3d.com/unity
HASH=a2913c821e27
VERSION=5.6.2f1

download() {
  file=$1
  url="$BASE_URL/$HASH/$package"

  echo "Downloading from $url: "
  curl -o `basename "$package"` "$url"
}

install() {
  package=$1
  download "$package"

  echo "Installing "`basename "$package"`
  sudo installer -dumplog -package `basename "$package"` -target /
}

# See $BASE_URL/$HASH/unity-$VERSION-$PLATFORM.ini for complete list
# of available packages, where PLATFORM is `osx` or `win`

#install "MacEditorInstaller/Unity-$VERSION.pkg"
#install "MacEditorTargetInstaller/UnitySetup-Android-Support-for-Editor-$VERSION.pkg"
#install "MacEditorTargetInstaller/UnitySetup-iOS-Support-for-Editor-$VERSION.pkg"
#install "MacEditorTargetInstaller/UnitySetup-Windows-Support-for-Editor-$VERSION.pkg"
