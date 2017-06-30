# Minamo

Command-line based unity project build script

[![Build Status](https://travis-ci.org/5minlab/minamo.svg?branch=master)](https://travis-ci.org/5minlab/minamo)

## Features
* config file based build

## Install
`go get github.com/5minlab/minamo`

## Usage

see wiki.

```bash
minamo.exe -cmd=build -config=./configs_dev/local.json -log=./unity.log
minamo.exe -cmd=dump -config=./configs_dev/local.json
minamo.exe -cmd=show -config=./configs_dev/local.json -field=build_path
```
