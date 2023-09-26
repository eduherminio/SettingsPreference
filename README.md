# Repro for inner non-null values override issue

https://github.com/dotnet/runtime/issues/92638

Steps:

```bash
$ dotnet run -c Release

Value1: 10
SubSection1: [1, 2]
SubSection2: [1000, 2000]
```

```bash
$ dotnet publish -c Release /p:Optimized=true --runtime win-x64 -o bin
$ bin/SettingsPreference.exe

Value1: 10
SubSection1: [1, 2]
SubSection2: [1000, 2000]
```

Expected values in both cases:

```bash
Value1: 10
SubSection1: [100, 200]
SubSection2: [1000, 2000]
```
