package main

import "testing"

func Test_loadConfig(t *testing.T) {
	cases := []struct {
		filepath string
	}{
		{"configs/android_daydream_dev.json"},
		{"configs/android_gearvr_dev.json"},
		{"configs/android_gearvr_release.json"},
		{"configs/ios_dev.json"},
		{"configs/standalone_win_dev.json"},
		{"configs/vr_win32_oculus_dev.json"},
		{"configs/vr_win32_steamvr_dev.json"},
		{"configs/win32_custom.json"},

		{"configs_dev/local.json"},
	}
	for _, c := range cases {
		_, err := loadConfig(c.filepath, "")
		if err != nil {
			t.Fatalf("loadConfig[%s]: %v, ", c, err)
		}
	}
}
