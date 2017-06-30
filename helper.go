package main

import (
	"os"
	"path/filepath"
	"strings"
)

func makeAbsFilePath(fp string) string {
	if fp == "" {
		return ""
	}

	pwd, err := os.Getwd()
	if err != nil {
		panic(err)
	}

	isAbs := filepath.IsAbs(fp)
	isAbs = isAbs || strings.HasPrefix(fp, "/")
	isAbs = isAbs || strings.HasPrefix(strings.ToLower(fp), `c:\`)
	isAbs = isAbs || strings.HasPrefix(strings.ToLower(fp), `c:/`)

	if isAbs {
		return filepath.Clean(fp)
	}

	fp = filepath.Join(pwd, fp)
	fp = filepath.Clean(fp)
	return fp
}
