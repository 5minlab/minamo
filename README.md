# Minamo

Command-line based unity project build script

[![Build Status](https://travis-ci.org/5minlab/minamo.svg?branch=master)](https://travis-ci.org/5minlab/minamo)

## Features
* config file based build

## Usage

### Dump

dump configure file

```sh
minamo.exe -cmd=dump -config=./configs/win32_steamvr_dev.json
```

```
C:\Users\haruna\go\src\github.com\5minlab\minamo>minamo.exe -cmd=dump -config=./configs/win32_steamvr_dev.json
(*main.Config)(0xc0420b0000)({
 UnityPath: (string) (len=30) "C:\\Program Files\\Unity-5.6.1p1",
 ProjectPath: (string) (len=25) "./UnityProject_Standalone",
 BuildBasePath: (string) (len=8) "./output",
 BuildPath: (string) (len=82) "{{.ConfigName}}-{{.Year}}-{{.Month}}-{{.Day}}-{{.Platform}}/{{.ShortRevision}}.exe",
 Method: (string) (len=34) "Assets.Minamo.Editor.Build.Execute",
 Build: (main.BuildConfig) {
  TargetGroup: (string) (len=10) "standalone",
  Target: (string) (len=7) "windows",
  Options: (main.BuildOptions) {
   Development: (bool) true,
   AllowDebugging: (bool) true
  }
 },
 Identification: (main.IdentificationConfig) {
  PackageName: (string) "",
  VersionName: (string) "",
  VersionCode: (string) ""
 },
 VRDevices: (main.VRDevicesConfig) {
  Enabled: (bool) true,
  Devices: (string) (len=6) "OpenVR"
 },
 AndroidSDK: (main.AndroidSDKConfig) {
  Min: (int) 0,
  Target: (int) 0
 },
 Keystore: (main.KeystoreConfig) {
  KeystoreName: (string) "",
  KeystorePass: (string) "",
  KeyaliasName: (string) "",
  KeyaliasPass: (string) ""
 },
 FileName: (string) (len=17) "win32_steamvr_dev",
 FilePath: (string) (len=79) "C:\\Users\\haruna\\go\\src\\github.com\\5minlab\\minamo\\configs\\win32_steamvr_dev.json",
 Revision: (string) (len=40) "0b7c0843b30e153f87080075afdc84da70571342",
 Now: (time.Time) 2017-06-29 12:22:09.3484599 +0900 KST,
 logFilePath: (string) ""
})

BuildPath       : C:\Users\haruna\go\src\github.com\5minlab\minamo\output\win32_steamvr_dev-2017-06-29-windows\0b7c084.exe
ConfigPath      : C:\Users\haruna\go\src\github.com\5minlab\minamo\configs\win32_steamvr_dev.json
LogFilePath     :
UnityPath       : C:\Program Files\Unity-5.6.1p1\Editor\Unity.exe
ProjectPath     : C:\Users\haruna\go\src\github.com\5minlab\minamo\UnityProject_Standalone
Args    : [-quit -batchmode -nographics -silent-crashes -projectPath C:\Users\haruna\go\src\github.com\5minlab\minamo\UnityProject_Standalone -executeMethod Assets.Minamo.Editor.Build.Execute]
```

* parameters
    * cmd : `dump`, required
    * config : requried

### Show

show specific field. for scripting.

```sh
minamo.exe -cmd=show -config=./configs/win32_steamvr_dev.json -field=build_path
```

```
C:\Users\haruna\go\src\github.com\5minlab\minamo>minamo.exe -cmd=show -config=./configs/win32_steamvr_dev.json -field=build_path
C:\Users\haruna\go\src\github.com\5minlab\minamo\UnityProject_Standalone
```

* parameters
    * cmd : `show`, required
    * config : required
    * field
        * `build_path`
        * `config_path`
	    * `log_file_path`
	    * `unity_path`
	    * `project_path`


### Build

run build

```sh
minamo.exe -cmd=build -config=./configs/win32_steamvr_dev.json -prompt=false -log=./unity.log
```

* parameters
    * cmd : `build`, required
    * config : required
    * prompt : optional
    * log : optional

### Parameters
* cmd : command
* config : config file path
* prompt : ask again
    * `true` or `false` or ignore
    * default value : `true`
* log : unity log file path
* field

## config file
see `configs`, `configs_travis-ci`
