package main

import (
	"bytes"
	"html/template"
	"time"
)

// reference
// https://github.com/Chaser324/unity-build/wiki/Parameter-Details#basic-settings
type OutputContext struct {
	ConfigName    string
	Revision      string
	ShortRevision string

	// yyyy
	Year string
	// MM
	Month string
	// dd
	Day string
	// hhmmss
	Time string

	// android/ios
	PackageName string
	VersionName string
	VersionCode string

	Platform string
}

func NewOutputContext(c *Config, t time.Time) OutputContext {
	return OutputContext{
		ConfigName:    c.FileName,
		Revision:      c.Revision,
		ShortRevision: c.Revision[:7],

		PackageName: c.Identification.PackageName,
		VersionName: c.Identification.VersionName,
		VersionCode: c.Identification.VersionCode,

		Year:  t.Format("2006"),
		Month: t.Format("01"),
		Day:   t.Format("02"),
		Time:  t.Format("150405"),

		Platform: c.Build.Target,
	}
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
