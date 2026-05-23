# Лабораторная 4 — Плагины (иерархия)

## Структура проекта

```
Lab4/                    ← основной проект (Lab2 + доработки)
  Shapes/
    Shape.cs             ← базовый класс (без изменений)
    IShapeFactory.cs     ← интерфейсы фабрик (без изменений)
    IShapeRenderer.cs    ← интерфейс рендерера (без изменений)
    IPluginShapeRenderer.cs  ← НОВЫЙ: интерфейс для плагинных рендереров
    GdiShapeRenderer.cs  ← дополнен: регистрация плагинных рендереров
    ...
  ShapeList/
    ShapeCollection.cs   ← дополнен: поддержка плагинных фигур
  Figures.cs             ← дополнен: загрузка плагинов при старте
  BUILD.bat              ← скрипт сборки всего

Lab4_StarPlugin/         ← плагин со звездой
  Star.cs                ← класс фигуры Звезда
  StarFactory.cs         ← фабрика для создания звёзды
  StarRenderer.cs        ← рендерер звезды
```

## Как собрать и запустить

1. Открой командную строку в папке `Lab4/`
2. Запусти `BUILD.bat`

Или вручную:
```
cd Lab4
dotnet build Lab1.csproj
cd ../Lab4_StarPlugin
dotnet build StarPlugin.csproj
cd ../Lab4
mkdir bin\Debug\net8.0-windows\plugins
copy ..\Lab4_StarPlugin\bin\Debug\net8.0-windows\StarPlugin.dll bin\Debug\net8.0-windows\plugins\
bin\Debug\net8.0-windows\Lab1.exe
```

## Как добавить новый плагин (без изменения кода!)

1. Создай новый проект `.csproj` (как Lab4_StarPlugin)
2. Создай класс фигуры, наследующий `Lab1.Shapes.Shape`
3. Создай класс фабрики, реализующий `Lab1.Shapes.IShapeFactory`
4. Создай класс рендерера, реализующий `Lab1.Shapes.IPluginShapeRenderer`
   - В рендерере ОБЯЗАТЕЛЬНО добавь метод `public Type GetShapeType() => typeof(ТвояФигура);`
5. Скомпилируй и скопируй `.dll` в папку `plugins/` рядом с `Lab1.exe`
6. Запусти программу — новая фигура появится в ComboBox автоматически!

## Как это работает

При старте `Figures.cs` вызывает `LoadPlugins()`:
- Сканирует папку `plugins/`
- Для каждого `.dll` загружает его через `Assembly.LoadFrom()`
- Ищет классы, реализующие `IShapeFactory` → добавляет в список фабрик
- Ищет классы, реализующие `IPluginShapeRenderer` → регистрирует в рендерере

При рисовании `ShapeCollection.DrawAll()`:
- Для стандартных фигур вызывает `_renderer.Draw((dynamic)shape, g)` — через dynamic dispatch
- Если фигура из плагина — `dynamic` бросит `RuntimeBinderException` (нет нужной перегрузки)
- Ловим исключение и вызываем `_renderer.Draw(shape, g)` → он найдёт плагинный рендерер по типу
