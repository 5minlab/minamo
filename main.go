package main

import (
	"flag"
	"fmt"
	"os"

	"github.com/davecgh/go-spew/spew"
)

var configFilePath string
var logFilePath string
var cmd string
var field string

func init() {
	flag.StringVar(&configFilePath, "config", "", "config file path")
	flag.StringVar(&logFilePath, "log", "", "unity log file path")
	flag.StringVar(&cmd, "cmd", "dump", "command")
	flag.StringVar(&field, "field", "", "field to show")
}

func main() {
	flag.Parse()

	config, err := loadConfig(configFilePath, logFilePath)
	if err != nil {
		panic(err)
	}

	switch cmd {
	case "dump":
		cmdDump(&config)
	case "show":
		cmdShow(&config)
	case "build":
		cmdBuild(&config)
	default:
		fmt.Println("unknown command:", cmd)
		os.Exit(-1)
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
	switch field {
	case "build_path":
		fmt.Print(c.MakeProjectPath())
	case "config_path":
		fmt.Print(c.FilePath)
	case "log_file_path":
		fmt.Print(c.LogFilePath())
	case "unity_path":
		fmt.Print(c.MakeUnityPath())
	case "project_path":
		fmt.Print(c.MakeProjectPath())
	}
}

func cmdBuild(c *Config) {
	output, buildTime, err := c.Execute()
	if err != nil {
		panic(err)
	}

	fmt.Println(output)
	fmt.Println("BuildTime\t:", buildTime)
}
