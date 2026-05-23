@echo off
echo ===================================
echo Build Lab5 -- Plugins + File Processing
echo ===================================

echo.
echo [1/7] Building main project (Lab1)...
dotnet build Lab1.csproj -c Debug
if errorlevel 1 (
    echo ERROR: main project build failed!
    pause
    exit /b 1
)

echo.
echo [2/7] Building StarPlugin...
dotnet build ..\Lab4_StarPlugin\StarPlugin.csproj -c Debug
if errorlevel 1 ( echo WARNING: StarPlugin build failed & goto :skip_star )
:skip_star

echo.
echo [3/7] Building HeartPlugin...
dotnet build ..\Lab4_HeartPlugin\HeartPlugin.csproj -c Debug
if errorlevel 1 ( echo WARNING: HeartPlugin build failed & goto :skip_heart )
:skip_heart

echo.
echo [4/7] Building ChecksumPlugin...
dotnet build ..\Lab5_ChecksumPlugin\ChecksumPlugin.csproj -c Debug
if errorlevel 1 ( echo WARNING: ChecksumPlugin build failed & goto :skip_checksum )
:skip_checksum

echo.
echo [5/7] Building Base64Plugin...
dotnet build ..\Lab5_Base64Plugin\Base64Plugin.csproj -c Debug
if errorlevel 1 ( echo WARNING: Base64Plugin build failed & goto :skip_base64 )
:skip_base64

echo.
echo [6/7] Building XorEncryptPlugin...
dotnet build ..\Lab5_EncryptPlugin\XorEncryptPlugin.csproj -c Debug
if errorlevel 1 ( echo WARNING: XorEncryptPlugin build failed & goto :skip_xor )
:skip_xor

echo.
echo [7/7] Copying plugins to output folders...

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

echo.
echo Done! Starting the application...
bin\Debug\net8.0-windows\Lab1.exe

pause
