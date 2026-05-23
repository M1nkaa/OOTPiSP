# Lab 5 — Plugins: File Processing

Built on top of Lab 4. Adds the ability to process shape data **before saving** and **after loading**
using dynamically loaded file-processor plugins.

## Variant 5 — Checksum

The main required plugin (`ChecksumPlugin`) computes a **SHA-256 checksum** of the serialized data
and appends it to the file before saving. On load, the checksum is re-computed and compared — if it
doesn't match, an error is shown and the file is rejected.

---

## Project structure

```
Lab4/                        ← main WinForms application (Lab5 version)
  Shapes/
    IFileProcessorPlugin.cs  ← NEW: interface for file processor plugins
    ShapeSerializer.cs       ← NEW: JSON serializer / deserializer for shapes
    ...                      ← existing shape classes (unchanged)
  Figures.cs                 ← updated: save/load + Settings menu + plugin loader
  Figures.Designer.cs        ← updated: added Save/Load buttons and MenuStrip
  BUILD.bat                  ← updated build script

Lab5_ChecksumPlugin/         ← Plugin 1 (variant 5): SHA-256 checksum
  ChecksumPlugin.cs

Lab5_Base64Plugin/           ← Plugin 2: Base64 encoding
  Base64Plugin.cs

Lab5_EncryptPlugin/          ← Plugin 3: XOR encryption
  XorEncryptPlugin.cs
```

---

## How to build and run

1. Open a command prompt in the `Lab4/` folder
2. Run `BUILD.bat`

Or manually:
```
dotnet build Lab1.csproj -c Debug
dotnet build ..\Lab5_ChecksumPlugin\ChecksumPlugin.csproj -c Debug
dotnet build ..\Lab5_Base64Plugin\Base64Plugin.csproj -c Debug
dotnet build ..\Lab5_EncryptPlugin\XorEncryptPlugin.csproj -c Debug

mkdir bin\Debug\net8.0-windows\fileprocessors
copy ..\Lab5_ChecksumPlugin\bin\Debug\net8.0-windows\ChecksumPlugin.dll bin\Debug\net8.0-windows\fileprocessors\
copy ..\Lab5_Base64Plugin\bin\Debug\net8.0-windows\Base64Plugin.dll bin\Debug\net8.0-windows\fileprocessors\
copy ..\Lab5_EncryptPlugin\bin\Debug\net8.0-windows\XorEncryptPlugin.dll bin\Debug\net8.0-windows\fileprocessors\

bin\Debug\net8.0-windows\Lab1.exe
```

---

## How to use

1. Draw shapes on the canvas as usual
2. Open **Settings** menu → select a file processor plugin (or "No file processing")
3. Click **Save to file** (or File → Save shapes...)  
   The data will be processed by the active plugin before writing
4. Click **Load from file** (or File → Load shapes...)  
   The data will be processed (checksum verified, decoded, etc.) before loading
5. You can also load plugins at runtime: **Settings → Load plugin from file...**

---

## File processor plugins

| Plugin             | ProcessBeforeSave              | ProcessAfterLoad                   |
|--------------------|--------------------------------|------------------------------------|
| ChecksumPlugin     | Appends SHA-256 checksum       | Verifies checksum, raises on mismatch |
| Base64Plugin       | Encodes data as Base64         | Decodes Base64                     |
| XorEncryptPlugin   | XOR-encrypts + Base64 wraps    | Decodes + XOR-decrypts             |

Plugins can be **combined in sequence**: select one at a time per save/load operation.
