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
var usePrompt bool

func init() {
	flag.StringVar(&configFilePath, "config", "", "config file path")
	flag.BoolVar(&usePrompt, "prompt", true, "use prompt")
}

func main() {
	flag.Parse()

	config, err := loadConfig(configFilePath)
	if err != nil {
		panic(err)
	}
	spew.Dump(config)
	fmt.Println("")
	fmt.Println("BuildPath :", config.MakeBuildPath())
	fmt.Println("ConfigPath:", config.FilePath)

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
	output, err := c.Execute()
	if err != nil {
		panic(err)
	}
	fmt.Println(output)
}
