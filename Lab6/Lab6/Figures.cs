using Lab1.ShapeList;
using Lab1.Shapes;
using Lab1.Patterns;          // Lab6: Strategy + Command pattern types
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Figures : Form
    {
        private ShapeCollection shapeCollection;
        private GdiShapeRenderer renderer;
        private List<IShapeFactory> factories = new List<IShapeFactory>();
        private Color currentColor = Color.Black;
        private Point? firstClick = null;

        // List of loaded file processor plugins (for save/load processing)
        private List<IFileProcessorPlugin> fileProcessorPlugins = new List<IFileProcessorPlugin>();

        // STRATEGY PATTERN (Lab6):
        // Context object that owns the file-processing strategy currently
        // selected in the Settings menu. It replaces the old single
        // "activeFileProcessor" field and the inline null-checks.
        private readonly FileProcessingContext fileContext = new FileProcessingContext();

        // Loaded plugin assemblies (needed for deserializing plugin shapes)
        private List<Assembly> pluginAssemblies = new List<Assembly>();

        // COMMAND PATTERN (Lab6):
        // History of executed commands. Each canvas-changing action is
        // pushed here so the Undo button can reverse it (LIFO order).
        private readonly Stack<ICommand> undoStack = new Stack<ICommand>();

        public Figures()
        {
            InitializeComponent();

            this.Paint += Figures_Paint;
            this.MouseClick += Figures_MouseClick;

            renderer = new GdiShapeRenderer();
            shapeCollection = new ShapeCollection(renderer);

            // Register built-in shape factories
            factories.Add(new RectangleFactory());
            factories.Add(new EllipseFactory());
            factories.Add(new LineFactory());
            factories.Add(new SquareFactory());
            factories.Add(new CircleFactory());
            factories.Add(new TriangleFactory());

            // Auto-load shape plugins from plugins/ folder
            // (this is also how the StarAdapterPlugin is discovered)
            LoadShapePlugins();

            // Auto-load file processor plugins from fileprocessors/ folder
            LoadFileProcessorPlugins();

            // Populate shape type combo box
            foreach (var factory in factories)
                comboBoxShapeType.Items.Add(factory.ShapeName);
            comboBoxShapeType.SelectedIndex = 0;

            buttonColor.BackColor = currentColor;
            buttonColor.Text = "";

            // Build the Settings menu dynamically based on loaded plugins
            RebuildSettingsMenu();
        }

        // ─── Shape plugin loading (existing from Lab4) ─────────────────────────

        /// <summary>
        /// Auto-load shape plugins (IShapeFactory / IPluginShapeRenderer) from plugins/ folder.
        /// </summary>
        private void LoadShapePlugins()
        {
            string pluginsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");
            if (!Directory.Exists(pluginsDir))
            {
                Directory.CreateDirectory(pluginsDir);
                return;
            }

            foreach (string dllPath in Directory.GetFiles(pluginsDir, "*.dll"))
                TryLoadShapePlugin(dllPath);
        }

        /// <summary>
        /// Load a single shape plugin DLL after verifying its signature.
        /// </summary>
        private void TryLoadShapePlugin(string dllPath)
        {
            string error;
            if (!PluginSignatureVerifier.VerifyPlugin(dllPath, out error))
            {
                MessageBox.Show($"Plugin {Path.GetFileName(dllPath)} rejected:\n{error}",
                    "Signature check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Assembly assembly = Assembly.LoadFrom(dllPath);
                pluginAssemblies.Add(assembly);

                foreach (Type type in assembly.GetTypes())
                {
                    // Register shape factories
                    if (typeof(IShapeFactory).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        IShapeFactory factory = (IShapeFactory)Activator.CreateInstance(type)!;
                        factories.Add(factory);
                    }

                    // Register plugin renderers
                    if (typeof(IPluginShapeRenderer).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        IPluginShapeRenderer pluginRenderer = (IPluginShapeRenderer)Activator.CreateInstance(type)!;
                        MethodInfo? getShapeType = type.GetMethod("GetShapeType");
                        if (getShapeType != null)
                        {
                            Type shapeType = (Type)getShapeType.Invoke(pluginRenderer, null)!;
                            renderer.RegisterPluginRenderer(shapeType, pluginRenderer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading plugin {Path.GetFileName(dllPath)}:\n{ex.Message}",
                    "Plugin error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ─── File processor plugin loading (new in Lab5) ────────────────────────

        /// <summary>
        /// Auto-load file processor plugins (IFileProcessorPlugin) from fileprocessors/ folder.
        /// </summary>
        private void LoadFileProcessorPlugins()
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fileprocessors");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                return;
            }

            foreach (string dllPath in Directory.GetFiles(dir, "*.dll"))
                TryLoadFileProcessorPlugin(dllPath);
        }

        /// <summary>
        /// Load a single file processor plugin DLL. No signature check needed for these.
        /// </summary>
        private bool TryLoadFileProcessorPlugin(string dllPath)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(dllPath);
                bool found = false;

                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IFileProcessorPlugin).IsAssignableFrom(type)
                        && !type.IsInterface && !type.IsAbstract)
                    {
                        IFileProcessorPlugin plugin = (IFileProcessorPlugin)Activator.CreateInstance(type)!;
                        fileProcessorPlugins.Add(plugin);
                        found = true;
                    }
                }

                return found;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading processor plugin {Path.GetFileName(dllPath)}:\n{ex.Message}",
                    "Plugin error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        // ─── Settings menu ──────────────────────────────────────────────────────

        /// <summary>
        /// Build (or rebuild) the Settings menu based on currently loaded file processor plugins.
        /// Called after initial load and after manually loading a new plugin.
        /// </summary>
        private void RebuildSettingsMenu()
        {
            menuSettings.DropDownItems.Clear();

            // "Load plugin from file..." item
            var loadPluginItem = new ToolStripMenuItem("Load plugin from file...");
            loadPluginItem.Click += OnLoadPluginFromFile;
            menuSettings.DropDownItems.Add(loadPluginItem);

            menuSettings.DropDownItems.Add(new ToolStripSeparator());

            // "No processing" option (always present)
            var noneItem = new ToolStripMenuItem("No file processing");
            noneItem.Tag = null;
            // STRATEGY PATTERN: this option corresponds to "no strategy".
            noneItem.Checked = (fileContext.CurrentStrategy == null);
            noneItem.Click += OnSelectFileProcessor;
            menuSettings.DropDownItems.Add(noneItem);

            // One menu item per loaded plugin (= one concrete strategy)
            foreach (var plugin in fileProcessorPlugins)
            {
                var item = new ToolStripMenuItem(plugin.PluginName);
                item.Tag = plugin;
                item.Checked = (fileContext.CurrentStrategy == plugin);
                item.Click += OnSelectFileProcessor;
                menuSettings.DropDownItems.Add(item);
            }

            menuSettings.DropDownItems.Add(new ToolStripSeparator());

            // Show which strategy is currently active
            var infoItem = new ToolStripMenuItem($"Active: {fileContext.StrategyName}");
            infoItem.Enabled = false;
            menuSettings.DropDownItems.Add(infoItem);
        }

        /// <summary>
        /// User clicked a file processor option in the Settings menu.
        /// </summary>
        private void OnSelectFileProcessor(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                // STRATEGY PATTERN: swap the active strategy at run time.
                fileContext.SetStrategy(item.Tag as IFileProcessorPlugin);
                RebuildSettingsMenu();
            }
        }

        /// <summary>
        /// User chose "Load plugin from file..." — opens a file dialog.
        /// </summary>
        private void OnLoadPluginFromFile(object? sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Title = "Select file processor plugin DLL",
                Filter = "DLL files (*.dll)|*.dll",
                Multiselect = false
            };

            if (dlg.ShowDialog() != DialogResult.OK) return;

            bool loaded = TryLoadFileProcessorPlugin(dlg.FileName);
            if (loaded)
            {
                MessageBox.Show($"Plugin loaded successfully from:\n{dlg.FileName}",
                    "Plugin loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RebuildSettingsMenu();
            }
            else
            {
                MessageBox.Show("No IFileProcessorPlugin implementations found in the selected DLL.",
                    "Plugin not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ─── Save / Load ─────────────────────────────────────────────────────────

        /// <summary>
        /// Save all shapes to a JSON file.
        /// The active file-processing strategy (if any) transforms the data before writing.
        /// </summary>
        private void OnSave(object? sender, EventArgs e)
        {
            using var dlg = new SaveFileDialog
            {
                Title = "Save shapes",
                Filter = "Shape files (*.shapes)|*.shapes|All files (*.*)|*.*",
                DefaultExt = "shapes"
            };

            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                // Serialize shapes to JSON
                string data = ShapeSerializer.Serialize(shapeCollection.Shapes);

                // STRATEGY PATTERN: the context applies whatever algorithm
                // is currently selected (checksum / encode / encrypt / none).
                data = fileContext.ApplyOnSave(data);

                File.WriteAllText(dlg.FileName, data, System.Text.Encoding.UTF8);

                string pluginInfo = fileContext.CurrentStrategy != null
                    ? $" (processed with: {fileContext.StrategyName})"
                    : "";
                MessageBox.Show($"Saved {shapeCollection.Count} shapes{pluginInfo}.",
                    "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file:\n{ex.Message}",
                    "Save error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load shapes from a JSON file.
        /// The active file-processing strategy (if any) transforms the data before deserializing.
        /// </summary>
        private void OnLoad(object? sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Title = "Load shapes",
                Filter = "Shape files (*.shapes)|*.shapes|All files (*.*)|*.*"
            };

            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                string data = File.ReadAllText(dlg.FileName, System.Text.Encoding.UTF8);

                // STRATEGY PATTERN: reverse the transformation (verify / decode / decrypt).
                data = fileContext.ApplyOnLoad(data);

                var shapes = ShapeSerializer.Deserialize(data, pluginAssemblies);

                // Replace current canvas content
                shapeCollection.Clear();
                listBox.Items.Clear();

                // COMMAND PATTERN: a freshly loaded file is a new baseline,
                // so the previous undo history no longer applies.
                undoStack.Clear();

                foreach (var shape in shapes)
                {
                    shapeCollection.AddShape(shape);
                    listBox.Items.Add($"{shape.GetType().Name} - {shape.Color.Name}");
                }

                Invalidate();

                string pluginInfo = fileContext.CurrentStrategy != null
                    ? $" (verified with: {fileContext.StrategyName})"
                    : "";
                MessageBox.Show($"Loaded {shapes.Count} shapes{pluginInfo}.",
                    "Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidDataException ex)
            {
                // Checksum mismatch or decode error — show specific message
                MessageBox.Show($"File processing error:\n{ex.Message}",
                    "Integrity check failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading file:\n{ex.Message}",
                    "Load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Event handlers ──────────────────────────────────────────────────────

        private void Figures_Paint(object sender, PaintEventArgs e)
        {
            if (shapeCollection != null)
            {
                e.Graphics.Clear(Color.White);
                shapeCollection.DrawAll(e.Graphics);
            }
        }

        private void Figures_MouseClick(object sender, MouseEventArgs e)
        {
            if (comboBoxShapeType.SelectedIndex < 0) return;

            IShapeFactory factory = factories[comboBoxShapeType.SelectedIndex];
            Shape? newShape = null;

            if (factory is LineFactory)
            {
                if (firstClick == null)
                {
                    firstClick = e.Location;
                    Text = "Click for the end point of the line";
                    return;
                }
                else
                {
                    newShape = factory.CreateShape(firstClick.Value, e.Location, currentColor);
                    firstClick = null;
                    Text = "Graphics Editor";
                }
            }
            else if (factory is TriangleFactory)
            {
                int width = (int)numericWidth.Value;
                int height = (int)numericHeight.Value;
                Point p1 = e.Location;
                Point p2 = new Point(e.X + width / 2, e.Y + height);
                Point p3 = new Point(e.X - width / 2, e.Y + height);
                newShape = new Triangle(currentColor, p1, p2, p3);
            }
            else
            {
                int width = (int)numericWidth.Value;
                int height = (int)numericHeight.Value;

                if (factory is CircleFactory || factory is SquareFactory)
                    newShape = factory.CreateShape(e.Location, width, currentColor);
                else if (factory is RectangleFactory || factory is EllipseFactory)
                    newShape = factory.CreateShape(e.Location, width, height, currentColor);
                else
                {
                    // Plugin shape (including the StarAdapterPlugin) —
                    // try different parameter combinations.
                    try { newShape = factory.CreateShape(e.Location, width, height, currentColor); }
                    catch
                    {
                        try { newShape = factory.CreateShape(e.Location, width, currentColor); }
                        catch { newShape = factory.CreateShape(e.Location, currentColor); }
                    }
                }
            }

            if (newShape != null)
            {
                // COMMAND PATTERN: wrap the "add shape" action in a command
                // object, execute it, and push it onto the undo history.
                var command = new AddShapeCommand(
                    shapeCollection, listBox, newShape,
                    $"{newShape.GetType().Name} - {currentColor.Name}");
                command.Execute();
                undoStack.Push(command);
                Invalidate();
            }
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = currentColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
                buttonColor.BackColor = currentColor;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // COMMAND PATTERN: clearing the canvas is a reversible command,
            // so it is also routed through the command/undo mechanism.
            var command = new ClearCommand(shapeCollection, listBox);
            command.Execute();
            undoStack.Push(command);
            Invalidate();
        }

        /// <summary>
        /// COMMAND PATTERN: undo the most recently executed command.
        /// </summary>
        private void buttonUndo_Click(object sender, EventArgs e)
        {
            if (undoStack.Count == 0)
            {
                MessageBox.Show("Nothing to undo.", "Undo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Pop the last command and reverse it.
            ICommand command = undoStack.Pop();
            command.Undo();
            Invalidate();
        }
    }
}
