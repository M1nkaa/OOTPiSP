# Лабораторная работа №6 — «Паттерны»

Работа выполнена на базе ЛР №5 (графический редактор с плагинами).
Реализованы **три паттерна проектирования**: Adapter, Strategy, Command.

---

## 1. Структура поставки

```
Lab6/
├── Lab1_modified/                 ← заменить эти файлы в основном проекте Lab1
│   ├── Figures.cs                 (изменён: подключены Strategy и Command)
│   ├── Figures.Designer.cs        (изменён: добавлена кнопка Undo)
│   └── Patterns/                  ← НОВАЯ папка, добавить в проект Lab1
│       ├── ICommand.cs
│       ├── AddShapeCommand.cs
│       ├── ClearCommand.cs
│       └── FileProcessingContext.cs
│
├── StarAdapterPlugin/             ← НОВЫЙ проект (плагин с адаптером)
│   ├── StarAdapterPlugin.csproj
│   ├── IShapeFactoryPlugin.cs     (контракт товарища — НЕ менять)
│   ├── PartnerStar.cs             (код товарища — адаптируемый класс)
│   ├── PartnerStarFactory.cs      (код товарища — адаптируемая фабрика)
│   ├── AdaptedStar.cs             (Адаптер, часть 1)
│   ├── StarFactoryAdapter.cs      (Адаптер, часть 2 — основной)
│   └── AdaptedStarRenderer.cs     (Адаптер, часть 3)
│
├── .gitignore
└── LAB6_README.md
```

---

## 2. Паттерн Adapter (обязательный — плагин товарища)

**Зачем.** Товарищ передал свой плагин «Звезда». Его фабрика реализует
интерфейс `PluginInterface.IShapeFactoryPlugin`, а наша программа умеет
работать только с `Lab1.Shapes.IShapeFactory`. Интерфейсы несовместимы:

| | Наш `IShapeFactory` | Товарищ `IShapeFactoryPlugin` |
|---|---|---|
| Создание | один вызов `CreateShape(point, params)` | `AddClick` → `IsReady` → `CreateShape()` |
| Результат | `Shape` | `object` |
| Параметры | передаются аргументами | спрашиваются через `InputBox` |

Менять ни наш код, ни код товарища нельзя — это и есть классическая
ситуация для **Адаптера**.

**Как реализовано (object adapter, через композицию):**

- `StarFactoryAdapter : IShapeFactory` — **Адаптер**. Внутри держит
  `IShapeFactoryPlugin` (Adaptee) и в методе `CreateShape` переводит
  один вызов в протокол товарища: `Reset → AddClick → IsReady → CreateShape`.
- `AdaptedStar : Shape` — оборачивает `StarPlugin.Star` товарища
  (который НЕ наследует `Shape`) в наш базовый класс `Shape`.
- `AdaptedStarRenderer : IPluginShapeRenderer` — рисует адаптированную
  звезду, переиспользуя метод `GetPoints()` товарища.

Адаптер собран в отдельный DLL-плагин и подгружается штатным механизмом
загрузки плагинов — то есть «новые функции товарища появляются через
плагин с адаптером», ничей исходный код при этом не меняется.

---

## 3. Паттерн Strategy (файловые плагины)

**Зачем.** Способ преобразования файла при сохранении/загрузке
(Base64-кодирование, контрольная сумма, XOR-шифрование или ничего) —
это алгоритм, который меняется независимо от самого процесса
сохранения. Strategy убирает ветвления `if/else` и позволяет менять
алгоритм в рантайме через меню Settings.

**Роли:**

- *Strategy* — интерфейс `IFileProcessorPlugin`;
- *Concrete strategies* — плагины Base64 / Checksum / XOR;
- *Context* — класс `FileProcessingContext` (новый): хранит выбранную
  стратегию и применяет её методами `ApplyOnSave` / `ApplyOnLoad`;
- *Client* — форма `Figures` (вызывает контекст вместо ручных проверок).

Чтобы добавить новый алгоритм обработки файла, достаточно написать
новый класс-стратегию — форму менять не нужно.

---

## 4. Паттерн Command (отмена действий — Undo)

**Зачем.** В графическом редакторе пользователь должен иметь
возможность отменить действие. Command превращает каждое действие
над холстом в объект с методами `Execute()` и `Undo()`, что позволяет
вести историю и откатывать изменения.

**Роли:**

- *Command* — интерфейс `ICommand` (`Execute` / `Undo` / `Description`);
- *Concrete commands* — `AddShapeCommand` (добавление фигуры) и
  `ClearCommand` (очистка холста; хранит снимок для восстановления);
- *Invoker* — форма `Figures` со стеком `undoStack` и кнопкой **Undo**;
- *Receiver* — `ShapeCollection` и `ListBox`.

Создание фигуры и очистка холста теперь проходят через команды;
кнопка **Undo** снимает последнюю команду со стека и вызывает `Undo()`.

---

## 5. Сборка и развёртывание

1. **Основной проект `Lab1`:**
   - заменить `Figures.cs` и `Figures.Designer.cs` файлами из `Lab1_modified/`;
   - добавить в проект папку `Patterns/` с четырьмя файлами.

2. **Плагин-адаптер:**
   - добавить проект `StarAdapterPlugin` в решение
     (`Add → Existing Project` или `dotnet sln add`);
   - в `StarAdapterPlugin.csproj` указать правильный путь к `Lab1.csproj`
     в теге `<ProjectReference>`;
   - собрать проект → получится `StarAdapterPlugin.dll`.

3. **Подпись плагина** (механизм из ЛР №4):
   - подписать `StarAdapterPlugin.dll` своим инструментом-подписывальщиком
     (создастся `StarAdapterPlugin.dll.sig`);
   - убедиться, что `public_key.xml` лежит рядом с программой или в `plugins/`.

4. **Установка:**
   - скопировать `StarAdapterPlugin.dll` и `StarAdapterPlugin.dll.sig`
     в папку `plugins/` рядом с `.exe` программы.

5. Запустить программу — в списке типов фигур появится
   **«Star (Partner Adapter)»**. Выбрать её и кликнуть по холсту.

> Примечание: интерфейс товарища `IShapeFactoryPlugin` включён в проект
> исходным кодом (а не ссылкой на `PluginInterface.dll`), чтобы плагин
> был единым самодостаточным DLL — так его проще разворачивать и
> подписывать. Контракт побайтово сверен с присланным `PluginInterface.dll`.

---

## 6. Работа с системой контроля версий (git)

```bash
git init
git add .gitignore
git commit -m "Add .gitignore for .NET solution"

# Паттерн 1
git add StarAdapterPlugin/
git commit -m "Lab6: integrate partner's plugin via Adapter pattern"

# Паттерн 2
git add Lab1/Patterns/FileProcessingContext.cs Lab1/Figures.cs
git commit -m "Lab6: extract file processing into Strategy pattern"

# Паттерн 3
git add Lab1/Patterns/ICommand.cs Lab1/Patterns/AddShapeCommand.cs \
        Lab1/Patterns/ClearCommand.cs Lab1/Figures.cs Lab1/Figures.Designer.cs
git commit -m "Lab6: add undo support via Command pattern"
```

Каждый паттерн — в отдельном коммите с осмысленным сообщением.
