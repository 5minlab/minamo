package main

import (
	"flag"
	"fmt"
	"io/ioutil"
	"log"
	"os"
	"regexp"
	"strconv"

	"github.com/davecgh/go-spew/spew"
)

var configFilePath string
var logFilePath string
var cmd string
var field string

// -logFile의 인자로는 3가지가 가능하다
// -logFile 없음 : 유니티 기본 로그 파일
// -logFile이 있지만 공백 : stdout, https://answers.unity.com/answers/1139749/view.html
// -logFile이 있음 : 로그 파일 경로
// golang에서는 string에 nil할당이 불가능하고
// 그렇다고 옵션을 뜯어고치긴 귀찮아서 임의의 파일명을 기본값으로 사용
const defaultLogFile = "_DEFAULT_LOG_"

func init() {
	flag.StringVar(&configFilePath, "config", "", "config file path")
	flag.StringVar(&logFilePath, "log", defaultLogFile, "unity log file path")
	flag.StringVar(&cmd, "cmd", "dump", "command")
	flag.StringVar(&field, "field", "", "field to show")
}

func main() {
	flag.Parse()

	config, err := loadConfig(configFilePath, logFilePath)
	if err != nil {
		log.Fatalf("Failed to load config: %s", err)
		return
	}

	switch cmd {
	case "dump":
		cmdDump(&config)
	case "show":
		cmdShow(&config)
	case "build":
		cmdBuild(&config)
	default:
		log.Fatalf("Unknown command: %s", cmd)
	}
}

func cmdDump(c *Config) {
	spew.Dump(c)
	fmt.Println("")
	fmt.Println("BuildPath\t:", c.MakeBuildPath())
	fmt.Println("ConfigPath\t:", c.FilePath)
	fmt.Println("LogFilePath\t:", c.LogFilePath())
	fmt.Println("UnityPath\t:", c.MakeUnityPath())
	fmt.Println("ProjectPath\t:", c.MakeProjectPath())
	fmt.Println("Args\t:", c.Args())
}

func cmdShow(c *Config) {
	fmt.Println(getShowString(c))
}
func getShowString(c *Config) string {
	switch field {
	case "build_path":
		return c.MakeBuildPath()
	case "config_path":
		return c.FilePath
	case "log_file_path":
		return c.LogFilePath()
	case "unity_path":
		return c.MakeUnityPath()
	case "project_path":
		return c.MakeProjectPath()
	case "minamo_version":
		tmpfile, err := ioutil.TempFile("", "minamo")
		if err != nil {
			panic(err)
		}
		fp := tmpfile.Name()
		defer os.Remove(tmpfile.Name())
		tmpfile.Close()

		c.logFilePath = tmpfile.Name()

		method := "Assets.Minamo.Editor.EntryPoint.VersionName"
		_, _, err = c.ExecuteMethod(method)
		if err != nil {
			panic(err)
		}

		data, err := ioutil.ReadFile(fp)
		if err != nil {
			panic(err)
		}

		re := regexp.MustCompile(`MinamoVersion=(\d+.\d+.\d+)`)
		founds := re.FindAllSubmatch(data, -1)
		if len(founds) > 0 {
			f := founds[0][1]
			v := string(f)
			return v
		}

		return ""
	case "version_code":
		code := NewOutputContext(c, c.Now).VersionCode
		return strconv.Itoa(code)
	case "version_name":
		return NewOutputContext(c, c.Now).VersionName
	case "package_name":
		return NewOutputContext(c, c.Now).PackageName
	default:
		log.Fatalf("unknown field: %s", field)
		return ""
	}
}

func cmdBuild(c *Config) {
	output, buildTime, err := c.Execute()
	if err != nil {
		fmt.Println(output)
		panic(err)
	}

	fmt.Println(output)
	fmt.Println("BuildTime\t:", buildTime)
}
