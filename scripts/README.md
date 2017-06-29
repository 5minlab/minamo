## development note

travis-ci 에서 ios, android 빌드 제외.
설치후 빌드하려고 하면 아래와 같은 문제로 최종 빌드가 나오질 않는다.

```
Building a player for 'iPhone' (9) target is not supported in this Unity build.
```

```bash
cd scripts; ./install_ios.sh; cd -
./scripts/build_ios.sh
```
