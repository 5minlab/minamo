package main

import (
	"bufio"
	"flag"
	"fmt"
	"os"

	"strings"

	"github.com/davecgh/go-spew/spew"
)

var configFilePath string
var logFilePath string
var usePrompt bool

func init() {
	flag.StringVar(&configFilePath, "config", "", "config file path")
	flag.StringVar(&logFilePath, "log", "", "unity log file path")
	flag.BoolVar(&usePrompt, "prompt", true, "use prompt")
}

func main() {
	flag.Parse()

	config, err := loadConfig(configFilePath, logFilePath)
	if err != nil {
		panic(err)
	}

	spew.Dump(config)
	fmt.Println("")
	fmt.Println("BuildPath\t:", config.MakeBuildPath())
	fmt.Println("ConfigPath\t:", config.FilePath)
	fmt.Println("LogFilePath\t:", config.LogFilePath())
	fmt.Println("Args\t:", config.Args())

	if usePrompt {
		reader := bufio.NewReader(os.Stdin)
		fmt.Print("Continue build? (Y/other): ")
		text, _ := reader.ReadString('\n')
		if strings.TrimRight(text, "\n\r") != "Y" {
			return
		}
	}

	runBuild(&config)
}

func runBuild(c *Config) {
	output, buildTime, err := c.Execute()
	if err != nil {
		panic(err)
	}

	fmt.Println(output)
	fmt.Println("BuildTime\t:", buildTime)
}
