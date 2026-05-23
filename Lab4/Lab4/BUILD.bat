@echo off
echo ===================================
echo Сборка Lab4 — Иерархия с плагинами
echo ===================================

echo.
echo [1/3] Собираем основной проект (Lab1)...
dotnet build Lab1.csproj -c Debug
if errorlevel 1 (
    echo ОШИБКА: сборка основного проекта провалилась!
    pause
    exit /b 1
)

echo.
echo [2/3] Собираем плагин (StarPlugin)...
dotnet build ..\Lab4_StarPlugin\StarPlugin.csproj -c Debug
if errorlevel 1 (
    echo ОШИБКА: сборка плагина провалилась!
    pause
    exit /b 1
)

echo.
echo [3/3] Копируем плагин в папку plugins/...
mkdir bin\Debug\net8.0-windows\plugins 2>nul
copy ..\Lab4_StarPlugin\bin\Debug\net8.0-windows\StarPlugin.dll bin\Debug\net8.0-windows\plugins\
echo Готово!

echo.
echo Запускаем программу...
bin\Debug\net8.0-windows\Lab1.exe

pause
