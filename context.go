package main

import (
	"bytes"
	"html/template"
	"time"
)

// reference
// https://github.com/Chaser324/unity-build/wiki/Parameter-Details#basic-settings
type OutputContext struct {
	ConfigName string
	Revision   string
	now        time.Time

	// android/ios
	PackageName string
	VersionName string
	VersionCode string

	Platform string
}

func NewOutputContext(c *Config, t time.Time) OutputContext {
	return OutputContext{
		ConfigName: c.FileName,
		Revision:   c.Revision,
		now:        t,

		PackageName: c.Identification.PackageName,
		VersionName: c.Identification.VersionName,
		VersionCode: c.Identification.VersionCode,

		Platform: c.Build.Target,
	}
}

func (c *OutputContext) ShortRevision() string {
	return c.Revision[:7]
}

// yyyy
func (c *OutputContext) Year() string {
	return c.now.Format("2006")
}

// MM
func (c *OutputContext) Month() string {
	return c.now.Format("01")
}

// dd
func (c *OutputContext) Day() string {
	return c.now.Format("02")
}

// hhmmss
func (c *OutputContext) Time() string {
	return c.now.Format("150405")
}

func (c *OutputContext) MakeStr(name, text string) string {
	tmpl, err := template.New(name).Parse(text)
	if err != nil {
		panic(err)
	}

	w := new(bytes.Buffer)
	err = tmpl.Execute(w, c)
	if err != nil {
		panic(err)
	}

	return w.String()
}
