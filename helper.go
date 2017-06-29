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

	if filepath.IsAbs(fp) || strings.HasPrefix(fp, "/") {
		return filepath.Clean(fp)
	}

	fp = filepath.Join(pwd, fp)
	fp = filepath.Clean(fp)
	return fp
}
