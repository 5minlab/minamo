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
	Development    bool `json:"development"`
	AllowDebugging bool `json:"allowDebugging"`
}

type BuildConfig struct {
	TargetGroup string       `json:"targetGroup"`
	Target      string       `json:"target"`
	Options     BuildOptions `json:"options"`
}

type IdentificationConfig struct {
	PackageName string `json:"packageName"`
	VersionName string `json:"versionName"`
	VersionCode string `json:"versionCode"`
}

type VRDevicesConfig struct {
	Enabled bool   `json:"enabled,string"`
	Devices string `json:"devices"`
}

type KeystoreConfig struct {
	KeystoreName string `json:"keystoreName"`
	KeystorePass string `json:"keystorePass"`
	KeyaliasName string `json:"keyaliasName"`
	KeyaliasPass string `json:"keyaliasPass"`
}

type Config struct {
	// absolute path
	UnityPath string `json:"unityPath"`
	// absolute path
	ProjectPath string `json:"projectPath"`
	// absolute path
	BuildBasePath string `json:"buildBasePath"`
	BuildPath     string `json:"buildPath"`

	Method string `json:"method"`

	Build          BuildConfig          `json:"build"`
	Identification IdentificationConfig `json:"identification"`
	VRDevices      VRDevicesConfig      `json:"vrDevices"`
	AndroidSDK     AndroidSDKConfig     `json:"androidSdk"`
	Keystore       KeystoreConfig       `json:"keystore"`

	// extra field
	FileName string
	FilePath string

	// git revision
	Revision string
	Now      time.Time
}

func loadConfig(fp string) (Config, error) {
	data, err := ioutil.ReadFile(fp)
	if err != nil {
		return Config{}, err
	}

	var s Config
	err = json.Unmarshal(data, &s)
	if err != nil {
		return Config{}, err
	}

	wd, err := os.Getwd()
	if err != nil {
		return Config{}, err
	}
	s.FilePath = filepath.Clean(filepath.Join(wd, fp))

	_, filename := filepath.Split(fp)
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
	ctx := NewOutputContext(c, c.Now)
	bp := ctx.MakeStr("buildPath", c.BuildPath)
	return filepath.Join(c.BuildBasePath, bp)
}

func (c *Config) UnityFilePath() string {
	filename := "Unity.exe"
	switch runtime.GOOS {
	case "windows":
		filename = "Unity.exe"
	default:
		panic("unknown platform:" + runtime.GOOS)
	}

	fp := filepath.Join(c.UnityPath, "Editor", filename)
	return fp
}

func (c *Config) Execute() (string, time.Duration, error) {
	t1 := time.Now()

	cmd := exec.Command(
		c.UnityFilePath(),
		"-quit",
		"-batchmode",
		"-projectPath",
		c.ProjectPath,
		"-executeMethod",
		c.Method,
	)
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
