package main

import "testing"
import "path/filepath"

func Test_loadConfig(t *testing.T) {
	cases := []struct {
		filename string
	}{
		{"android_daydream_dev.json"},
		{"android_gearvr_dev.json"},
		{"android_gearvr_release.json"},
		{"ios_dev.json"},
		{"standalone_win_dev.json"},
		{"vr_win32_oculus_dev.json"},
		{"vr_win32_steamvr_dev.json"},
		{"win32_custom.json"},
	}
	for _, c := range cases {
		fp := filepath.Join("configs", c.filename)
		_, err := loadConfig(fp, "")
		if err != nil {
			t.Fatalf("loadConfig[%s]: %v, ", c, err)
		}
	}
}
