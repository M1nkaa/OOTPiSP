@echo off
echo ================================================
echo  Build Lab6 -- Patterns (Adapter + Strategy + Command)
echo ================================================

echo.
echo [1/8] Building main project (Lab1 with Patterns)...
dotnet build Lab1.csproj -c Debug
if errorlevel 1 (
    echo ERROR: main project build failed!
    pause
    exit /b 1
)

echo.
echo [2/8] Building StarPlugin (original)...
dotnet build ..\Lab4_StarPlugin\StarPlugin.csproj -c Debug
if errorlevel 1 ( echo WARNING: StarPlugin build failed & goto :skip_star )
:skip_star

echo.
echo [3/8] Building HeartPlugin...
dotnet build ..\Lab4_HeartPlugin\HeartPlugin.csproj -c Debug
if errorlevel 1 ( echo WARNING: HeartPlugin build failed & goto :skip_heart )
:skip_heart

echo.
echo [4/8] Building StarAdapterPlugin (Adapter pattern)...
dotnet build ..\StarAdapterPlugin\StarAdapterPlugin.csproj -c Debug
if errorlevel 1 ( echo WARNING: StarAdapterPlugin build failed & goto :skip_adapter )
:skip_adapter

echo.
echo [5/8] Building ChecksumPlugin (Strategy pattern)...
dotnet build ..\Lab5_ChecksumPlugin\ChecksumPlugin.csproj -c Debug
if errorlevel 1 ( echo WARNING: ChecksumPlugin build failed & goto :skip_checksum )
:skip_checksum

echo.
echo [6/8] Building Base64Plugin (Strategy pattern)...
dotnet build ..\Lab5_Base64Plugin\Base64Plugin.csproj -c Debug
if errorlevel 1 ( echo WARNING: Base64Plugin build failed & goto :skip_base64 )
:skip_base64

echo.
echo [7/8] Building XorEncryptPlugin (Strategy pattern)...
dotnet build ..\Lab5_EncryptPlugin\XorEncryptPlugin.csproj -c Debug
if errorlevel 1 ( echo WARNING: XorEncryptPlugin build failed & goto :skip_xor )
:skip_xor

echo.
echo [8/8] Copying plugins to output folders...

REM Shape plugins -> plugins/
mkdir bin\Debug\net8.0-windows\plugins 2>nul
if exist ..\Lab4_StarPlugin\bin\Debug\net8.0-windows\StarPlugin.dll (
    copy ..\Lab4_StarPlugin\bin\Debug\net8.0-windows\StarPlugin.dll bin\Debug\net8.0-windows\plugins\
)
if exist ..\Lab4_HeartPlugin\bin\Debug\net8.0-windows\HeartPlugin.dll (
    copy ..\Lab4_HeartPlugin\bin\Debug\net8.0-windows\HeartPlugin.dll bin\Debug\net8.0-windows\plugins\
)

REM File processor plugins -> fileprocessors/
mkdir bin\Debug\net8.0-windows\fileprocessors 2>nul
if exist ..\Lab5_ChecksumPlugin\bin\Debug\net8.0-windows\ChecksumPlugin.dll (
    copy ..\Lab5_ChecksumPlugin\bin\Debug\net8.0-windows\ChecksumPlugin.dll bin\Debug\net8.0-windows\fileprocessors\
)
if exist ..\Lab5_Base64Plugin\bin\Debug\net8.0-windows\Base64Plugin.dll (
    copy ..\Lab5_Base64Plugin\bin\Debug\net8.0-windows\Base64Plugin.dll bin\Debug\net8.0-windows\fileprocessors\
)
if exist ..\Lab5_EncryptPlugin\bin\Debug\net8.0-windows\XorEncryptPlugin.dll (
    copy ..\Lab5_EncryptPlugin\bin\Debug\net8.0-windows\XorEncryptPlugin.dll bin\Debug\net8.0-windows\fileprocessors\
)

REM =========================================================
REM  ADAPTER PLUGIN SETUP (requires signing from Lab4 signer)
REM =========================================================
echo.
echo --- Adapter plugin setup ---
echo.
echo  StarAdapterPlugin.dll must be SIGNED before it can be loaded.
echo  Steps:
echo    1. Run Lab4_PluginSigner\bin\Debug\net8.0\Lab4_PluginSigner.exe
echo    2. Select: ..\StarAdapterPlugin\bin\Debug\net8.0-windows\StarAdapterPlugin.dll
echo    3. It will create StarAdapterPlugin.dll.sig next to the DLL
echo    4. Copy both StarAdapterPlugin.dll and StarAdapterPlugin.dll.sig
echo       into: bin\Debug\net8.0-windows\plugins\
echo.
echo  Then copy your public_key.xml into bin\Debug\net8.0-windows\
echo  (it should already be there from Lab4/Lab5)
echo.

echo.
echo Done! Run bin\Debug\net8.0-windows\Lab1.exe to start.
pause
