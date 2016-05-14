# Drive Locker [![Build status](https://ci.appveyor.com/api/projects/status/mvomhxj2s5pdphh7?svg=true)](https://ci.appveyor.com/project/Metalnem/drive-locker) [![license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat)](https://raw.githubusercontent.com/metalnem/drive-locker/master/LICENSE)
Console application that enables/disables eject button on your optical drive.

## Usage

```
> DriveLocker.exe [-l|--lock] <drive_letter>
> DriveLocker.exe [-u|--unlock] <drive_letter>
```

## Options

```
-l --lock    Lock the drive.
-u --unlock  Unlock the drive.
```

## Examples

```
> DriveLocker.exe -l D:
> DriveLocker.exe --unlock D
```
