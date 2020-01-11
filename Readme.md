## Installation

This program is using .NET Core 3.1. You must have .NET Core installed first.

Just Build the program with Visual Studio or from CMD (dotnet build) then run it from your CMD in Windows.

## Parameters

- Path :
```
--path "yourpath"
```

**Note:** You can provide multiple paths using '**;**' as separator between paths.

```
--path "yourfirstpath;yoursecondpath"
```

- Include Sub-Folders :

You can specify if you want to iterate over all subfolders or not.

```
--includeSubFolders true
or
--includeSubFolders false
```

- File Extensions :

You can specify which type of files you want to check by specifying one or multiple extensions.

```
--fileExtensions cs
or
--fileExtensions cs;js
```

## Usage

Navigate to the .exe file and run it from your the Windows CMD like described bellow :

```
HowManyLinesOfCode.exe --path "C:\sources\MLSandbox" --includeSubFolders true --fileExtensions cs;js
```