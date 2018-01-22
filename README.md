# Minamo

Command-line based unity project build script

[![Build Status](https://travis-ci.org/5minlab/minamo.svg?branch=master)](https://travis-ci.org/5minlab/minamo)

## Features
* config file based build

## Install
`go get -u github.com/5minlab/minamo`

## Usage

### Build

build unity project

```
minamo -cmd=build -config=./configs_dev/local.json -log=./unity.log
```

parameters

* cmd : `build`, required
* config : config file path, required
* log : unity log file path. if log value is `''`, log output is stdout, required

### Show

show specific field. for scripting.

```
minamo -cmd=show -config=./configs_dev/local.json -field=build_path
```

sample output

```
$ minamo -cmd=show -config=./configs/win32_steamvr_dev.json -field=build_path
C:\Users\haruna\go\src\github.com\5minlab\minamo\UnityProject_Standalone
```

parameters

* cmd : `show`, required
* config : config file path, required
* field : specific field

field | description
------|------------
`build_path` | build path
`config_path` | config path
`log_file_path` | log file path
`unity_path` | unity path
`project_path` | project path

### Dump

dump configure file. for debugging.

```bash
minamo -cmd=dump -config=./configs_dev/local.json
```

sample output

```
$ minamo -cmd=dump -config=./configs/win32_steamvr_dev.json
(*main.Config)(0xc0420b0000)({
 UnityPath: (string) (len=30) "C:\\Program Files\\Unity-5.6.1p1",
 ProjectPath: (string) (len=25) "./UnityProject_Standalone",
 ......
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

parameters

* cmd : `dump`, required
* config : config file path, requried

## config file

sample files

* [/configs/](https://github.com/5minlab/minamo/tree/master/configs)
* [/configs_dev/](https://github.com/5minlab/minamo/tree/master/configs_dev)

### root attributes

```json
{
  "unityPath": "C:\\Program Files\\Unity 2017.3.0f3",
  "projectPath": "./UnityProject_Minamo",
  "buildBasePath": "./output",
  "buildPath": "{{.ConfigName}}-{{.Year}}-{{.Month}}-{{.Day}}-{{.Platform}}",
  "method": "Assets.Minamo.Editor.EntryPoint.Build",
}
```

### build


```json
{
  "build": {
    "targetGroup": "wsa",
    "target": "wsa",
    "options": {
      "development": true,
      "allowDebugging": true
    }
  },
}
```

* targetGroup : search `ForBuildTargetGroup()` function
* target : search `ForBuildTarget()` function
* options : search `ForBuildOptions()` function

### defines

see [Modifier_DefineSymbol.cs][Modifier_DefineSymbol.cs]

```json
{
  "defines": [
    "HELLO_WORLD",
    "PLATFORM_UWP"
  ],
}
```

### XR

see [Modifier_XR.cs][Modifier_XR.cs]

```json
{
  "xr": {
    "enabled": true,
    "devices": [
      "WindowsMR"
    ],
    "stereoRenderingPath": "instancing"
  },
}
```

* devices : search `Modifier_XR` class
  * Oculus
  * OpenVR
  * daydream
  * cardboard
  * WindowsMR
  * PlayStationVR
* stereoRenderingPath : search `ForStereoRenderingPath()` function

### identification

see [Modifier_Identification.cs][Modifier_Identification.cs]

```json
{
  "identification": {
    "packageName": "com.fiveminlab.minamo.localdev",
    "versionName": "0.0.1",
    "versionCode": 1
  },
}
```

### android sdk

see [Modifier_AndroidSdkVersion.cs][Modifier_AndroidSdkVersion.cs]

```json
{
  "androidSdk": {
    "min": 24,
    "target": 0
  },
}
```

### keystore

see [Modifier_Keystore.cs][Modifier_Keystore.cs]

```json
{
  "keystore": {
    "keystoreName": "C:/devel/minamo/UnitySecret/user.keystore",
    "keystorePass": "asdf1234",
    "keyaliasName": "dev",
    "keyaliasPass": "zxcvasdf"
  }
}
```

### publishing

see [Modifier_Publishing.cs][Modifier_Publishing.cs]

```json
{
  "publishing": {
    "android_useApkExpansion": true,

    "uwp_capability": [
      "SpatialPerception",
      "WebCam"
    ],

    "ps4_attribExclusiveVR": true,
    "ps4_attribShareSupport": true,
    "ps4_attribMoveSupport": true,
    "ps4_category": "Application",
    "ps4_masterVersion": "0000",
    "ps4_contentID": "0000",
    "ps4_applicationParameter1": 0,
    "ps4_applicationParameter2": 0,
    "ps4_applicationParameter3": 0,
    "ps4_applicationParameter4": 0,
    "ps4_enterButtonAssignment": "CrossButton"
  },
}
```

* uwp_capability : search `ForWSACapability()` function
* ps4_category : search `ForPS4AppCategory()` function
* ps4_enterButtonAssignment : search `ForPS4EnterButtonAssignment()` function

### scripting

see [Modifier_Scripting.cs][Modifier_Scripting.cs]

```json
{
  "scripting": {
    "backend": "Mono2x",
    "scriptingRuntimeVersion": "latest",
    "apiCompatibilityLevel": "NET_4_6"
  },
}
```

* backend : search `ForScriptingImplementation()` function
* scriptingRuntimeVersion : search `ForScriptingRuntimeVersion()` function
* apiCompatibilityLevel : search `ForApiCompatibilityLevel()` function

### editor user build

see [Modifier_EditorUserBuild.cs][Modifier_EditorUserBuild.cs]

```json
{
  "editorUserBuild": {
    "wsaSubtarget": "HoloLens",
    "wsaUWPBuildType": "XAML",
    "wsaUWPSDK": "",
    "wsaBuildAndRunDeployTarget": "LocalMachine",
    "wsaGenerateReferenceProjects": false
  },
}
```

* wsaSubtarget : search `ForWSASubtarget()` function
* wsaUWPBuildType : search `ForWSAUWPBuildType()` function
* wsaBuildAndRunDeployTarget : search `ForWSABuildAndRunDeployTarget()` function

### resolution and presentation

see [Modifier_ResolutionAndPresentation.cs][Modifier_ResolutionAndPresentation.cs]

```json
{
  "resolutionAndPresentation": {
    "runInBackground": false
  }
}
````


[Modifier_AndroidSdkVersion.cs]: https://github.com/5minlab/minamo/blob/master/UnityProject_Minamo/Assets/Minamo/Editor/Modifier_AndroidSdkVersion.cs
[Modifier_DefineSymbol.cs]: https://github.com/5minlab/minamo/blob/master/UnityProject_Minamo/Assets/Minamo/Editor/Modifier_DefineSymbol.cs
[Modifier_EditorUserBuild.cs]: https://github.com/5minlab/minamo/blob/master/UnityProject_Minamo/Assets/Minamo/Editor/Modifier_EditorUserBuild.cs
[Modifier_Identification.cs]: https://github.com/5minlab/minamo/blob/master/UnityProject_Minamo/Assets/Minamo/Editor/Modifier_Identification.cs
[Modifier_Keystore.cs]: https://github.com/5minlab/minamo/blob/master/UnityProject_Minamo/Assets/Minamo/Editor/Modifier_Keystore.cs
[Modifier_Publishing.cs]: https://github.com/5minlab/minamo/blob/master/UnityProject_Minamo/Assets/Minamo/Editor/Modifier_Publishing.cs
[Modifier_ResolutionAndPresentation.cs]: https://github.com/5minlab/minamo/blob/master/UnityProject_Minamo/Assets/Minamo/Editor/Modifier_ResolutionAndPresentation.cs
[Modifier_Scripting.cs]: https://github.com/5minlab/minamo/blob/master/UnityProject_Minamo/Assets/Minamo/Editor/Modifier_Scripting.cs
[Modifier_XR.cs]: https://github.com/5minlab/minamo/blob/master/UnityProject_Minamo/Assets/Minamo/Editor/Modifier_XR.cs


## development note

### run unit test
1. modify `run_test.bat` or `run_test.sh`. write correct unity path
2. modify `configs_dev/local.json`. write correct unity path
3. execute `run_test.bat` or `run_test.sh`
4. read `results.xml`

### export package
1. modify `export_package.bat` or `export_package.sh`. write correct unity path
2. modify `configs_dev/local.json`. write correct unity path
3. execute `export_package.bat` or `export_package.sh`
4. `/dist/minamo-x.y.z.unitypackage` is generated
