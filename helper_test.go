package main

import (
	"os"
	"path/filepath"
	"testing"
)

func Test_makeAbsFilePath(t *testing.T) {
	// filename only
	pwd, err := os.Getwd()
	if err != nil {
		t.Fatal("os.Getwd:", err)
	}

	cases := []struct {
		input    string
		expected string
	}{
		// empty
		{"", ""},

		// relative path
		{"unity.log", filepath.Join(pwd, "unity.log")},
		{"../unity.log", filepath.Clean(filepath.Join(pwd, "../unity.log"))},

		// absolute path - unix
		{"/unity.log", filepath.Join("/", "unity.log")},
		{"/tmp/unity.log", filepath.Join("/", "tmp", "unity.log")},
		{"/tmp/../../../../../unity.log", filepath.Join("/", "unity.log")},

		// absolute path - windows
		//{`c:\unity.log`, filepath.Join("c:/", "unity.log")},
		{`c:/unity.log`, filepath.Join("c:/", "unity.log")},
	}
	for _, c := range cases {
		v := makeAbsFilePath(c.input)
		if v != c.expected {
			t.Error("Expected", c.expected, ", got", v, "input=", c.input)
		}
	}
}
