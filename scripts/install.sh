#!/bin/sh

# view-source:https://store.unity.com/kr/download/thank-you?thank-you=personal&os=osx&nid=325
BASE_URL=http://netstorage.unity3d.com/unity
HASH=892c0f8d8f8a
VERSION=2017.1.0p4
CACHE_DIR=unity-installers

download() {
  package=$1
  file=$1
  url="$BASE_URL/$HASH/$package"

  echo "Downloading from $url: "
  curl -o $CACHE_DIR/`basename "$package"` "$url"
}

install() {
  package=$1
  echo "Installing "`basename "$package"`
  sudo installer -dumplog -package $CACHE_DIR/`basename "$package"` -target /
}

main() {
  package=$1

  if [ ! -f $CACHE_DIR/`basename "$package"` ]; then
    download "$package"
  fi

  install "$package"
}


case "$1" in
	"editor")
    main "MacEditorInstaller/Unity-$VERSION.pkg"
    ;;
  "windows")
    main "MacEditorTargetInstaller/UnitySetup-Windows-Support-for-Editor-$VERSION.pkg"
    ;;
  "mac")
    main "MacEditorTargetInstaller/UnitySetup-Mac-Support-for-Editor-$VERSION.pkg"
    ;;
  "linux")
    main "MacEditorTargetInstaller/UnitySetup-Linux-Support-for-Editor-$VERSION.pkg"
    ;;
  "android")
    main "MacEditorTargetInstaller/UnitySetup-Android-Support-for-Editor-$VERSION.pkg"
    ;;
  "ios")
    main "MacEditorTargetInstaller/UnitySetup-iOS-Support-for-Editor-$VERSION.pkg"
    ;;
  *)
    echo "unknown unity installer : $1"
esac
