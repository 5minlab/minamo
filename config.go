package main

import (
	"encoding/json"
	"io/ioutil"
	"os"
	"os/exec"
	"path/filepath"
	"runtime"
	"strings"
	"time"
)

type AndroidSDKConfig struct {
	Min    int `json:"min"`
	Target int `json:"target"`
}

type BuildOptions struct {
	Development                         bool `json:"development"`
	AllowDebugging                      bool `json:"allowDebugging"`
	AcceptExternalModificationsToPlayer bool `json:"acceptExternalModificationsToPlayer"`
	ConnectWithProfiler                 bool `json:"connectWithProfiler"`
	ShowBuiltPlayer                     bool `json:"showBuiltPlayer"`
	AutoRunPlayer                       bool `json:"autoRunPlayer"`
	SymlinkLibraries                    bool `json:"symlinkLibraries"`
	ForceEnableAssertions               bool `json:"forceEnableAssertions"`
}

type BuildConfig struct {
	TargetGroup string       `json:"targetGroup"`
	Target      string       `json:"target"`
	Options     BuildOptions `json:"options"`
}

type IdentificationConfig struct {
	PackageName string `json:"packageName"`
	VersionName string `json:"versionName"`
	VersionCode int    `json:"versionCode"`
}

type VRDevicesConfig struct {
	Enabled             bool   `json:"enabled"`
	Devices             string `json:"devices"`
	StereoRenderingPath string `json:"stereoRenderingPath"`
}

type KeystoreConfig struct {
	KeystoreName string `json:"keystoreName"`
	KeystorePass string `json:"keystorePass"`
	KeyaliasName string `json:"keyaliasName"`
	KeyaliasPass string `json:"keyaliasPass"`
}

type PublishingConfig struct {
	UseApkExpansion bool `json:"useApkExpansion"`
}

type Config struct {
	UnityPath     string `json:"unityPath"`
	ProjectPath   string `json:"projectPath"`
	BuildBasePath string `json:"buildBasePath"`
	BuildPath     string `json:"buildPath"`

	Method string `json:"method"`

	Build          BuildConfig          `json:"build"`
	Identification IdentificationConfig `json:"identification"`
	VRDevices      VRDevicesConfig      `json:"vrDevices"`
	AndroidSDK     AndroidSDKConfig     `json:"androidSdk"`
	Keystore       KeystoreConfig       `json:"keystore"`
	Publishing     PublishingConfig     `json:"publishing"`

	// extra field
	FileName string
	FilePath string

	// git revision
	Revision string
	Now      time.Time

	logFilePath string
}

func loadConfig(configFp, logFp string) (Config, error) {
	data, err := ioutil.ReadFile(configFp)
	if err != nil {
		return Config{}, err
	}

	var s Config
	err = json.Unmarshal(data, &s)
	if err != nil {
		return Config{}, err
	}

	s.logFilePath = logFp
	s.FilePath = makeAbsFilePath(configFp)

	_, filename := filepath.Split(configFp)
	s.FileName = strings.Split(filename, ".")[0]
	s.Now = time.Now()

	cmd := exec.Command("git", "rev-parse", "HEAD")
	cmd.Dir = s.ProjectPath
	out, err := cmd.Output()
	if err != nil {
		return s, err
	}
	s.Revision = strings.Trim(string(out), "\n\r")

	return s, nil
}

func (c *Config) MakeBuildPath() string {
	bp := makeAbsFilePath(c.BuildBasePath)
	ctx := NewOutputContext(c, c.Now)
	fp := ctx.MakeStr("buildPath", c.BuildPath)
	return filepath.Join(bp, fp)
}

func (c *Config) MakeUnityPath() string {
	// https://github.com/golang/go/blob/master/src/go/build/syslist.go
	switch runtime.GOOS {
	case "windows":
		dir := makeAbsFilePath(c.UnityPath)
		return filepath.Join(dir, "Editor", "Unity.exe")

	case "darwin":
		// /Applications/Unity/Unity.app/Contents/MacOS/Unity
		// /Applications/Unity/Unity.app + Contents/MacOS/Unity
		dir := makeAbsFilePath(c.UnityPath)
		return filepath.Join(dir, "Contents", "MacOS", "Unity")

	default:
		panic("unknown platform:" + runtime.GOOS)
	}
}

func (c *Config) MakeProjectPath() string {
	return makeAbsFilePath(c.ProjectPath)
}

func (c Config) Args() []string {
	// Command line arguments document
	// https://docs.unity3d.com/Manual/CommandLineArguments.html
	// other doucment
	// http://blog.stablekernel.com/continuous-integration-for-unity-5-using-travisci
	args := []string{
		"-quit",
		"-batchmode",
		"-nographics",
		"-silent-crashes",
		"-projectPath",
		c.MakeProjectPath(),
		//"-executeMethod",
		//c.Method,
	}
	if c.logFilePath != "" {
		args = append(args, "-logFile", c.LogFilePath())
	}
	return args
}

func (c *Config) Execute() (string, time.Duration, error) {
	return c.ExecuteMethod(c.Method)
}

func (c *Config) ExecuteMethod(method string) (string, time.Duration, error) {
	args := c.Args()
	args = append(args, "-executeMethod", method)
	return c.executeCli(args)
}

func (c *Config) executeCli(args []string) (string, time.Duration, error) {
	t1 := time.Now()
	cmd := exec.Command(c.MakeUnityPath(), args...)
	cmd.Dir = c.ProjectPath

	cmd.Env = os.Environ()
	cmd.Env = append(cmd.Env, "CONFIG_PATH="+c.FilePath)
	cmd.Env = append(cmd.Env, "OUTPUT_PATH="+c.MakeBuildPath())

	stdoutStderr, err := cmd.CombinedOutput()
	t2 := time.Now()
	dt := t2.Sub(t1)

	if err != nil {
		return "", dt, err
	}
	return string(stdoutStderr), dt, nil
}

// "unity -logFile" needs absolute path
func (c *Config) LogFilePath() string {
	return makeAbsFilePath(c.logFilePath)
}
