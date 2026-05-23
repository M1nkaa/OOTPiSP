namespace Lab1
{
    partial class Figures 
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            // Controls
            label1 = new Label();
            comboBoxShapeType = new ComboBox();
            label2 = new Label();
            numericWidth = new NumericUpDown();
            label3 = new Label();
            label4 = new Label();
            listBox = new ListBox();
            numericHeight = new NumericUpDown();
            buttonClear = new Button();
            colorDialog = new ColorDialog();
            buttonColor = new Button();
            label5 = new Label();
            label6 = new Label();
            buttonSave = new Button();
            buttonLoad = new Button();

            // MenuStrip
            menuStrip = new MenuStrip();
            menuFile = new ToolStripMenuItem("File");
            menuItemSave = new ToolStripMenuItem("Save shapes...");
            menuItemLoad = new ToolStripMenuItem("Load shapes...");
            menuSettings = new ToolStripMenuItem("Settings");

            ((System.ComponentModel.ISupportInitialize)numericWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericHeight).BeginInit();
            SuspendLayout();

            // ── menuStrip ──
            menuStrip.Items.AddRange(new ToolStripItem[] { menuFile, menuSettings });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 24);
            menuStrip.TabIndex = 20;

            // menuFile
            menuItemSave.Click += OnSave;
            menuItemLoad.Click += OnLoad;
            menuFile.DropDownItems.AddRange(new ToolStripItem[] { menuItemSave, menuItemLoad });

            // ── label1 ──
            label1.AutoSize = true;
            label1.Location = new Point(12, 33);
            label1.Name = "label1";
            label1.Text = "Select the type of figure:";

            // ── comboBoxShapeType ──
            comboBoxShapeType.FormattingEnabled = true;
            comboBoxShapeType.Location = new Point(12, 51);
            comboBoxShapeType.Name = "comboBoxShapeType";
            comboBoxShapeType.Size = new Size(132, 23);
            comboBoxShapeType.TabIndex = 1;

            // ── label2 ──
            label2.AutoSize = true;
            label2.Location = new Point(12, 94);
            label2.Text = "Select color:";

            // ── buttonColor ──
            buttonColor.Location = new Point(12, 112);
            buttonColor.Name = "buttonColor";
            buttonColor.Size = new Size(132, 23);
            buttonColor.TabIndex = 10;
            buttonColor.UseVisualStyleBackColor = true;
            buttonColor.Click += buttonColor_Click;

            // ── label3 ──
            label3.AutoSize = true;
            label3.Location = new Point(12, 150);
            label3.Text = "Enter dimensions:";

            // ── label5 ──
            label5.AutoSize = true;
            label5.Location = new Point(9, 176);
            label5.Text = "Width:";

            // ── numericWidth ──
            numericWidth.Location = new Point(61, 174);
            numericWidth.Name = "numericWidth";
            numericWidth.Size = new Size(83, 23);
            numericWidth.Value = 50;
            numericWidth.Maximum = 1000;
            numericWidth.Minimum = 1;

            // ── label6 ──
            label6.AutoSize = true;
            label6.Location = new Point(9, 205);
            label6.Text = "Height:";

            // ── numericHeight ──
            numericHeight.Location = new Point(61, 203);
            numericHeight.Name = "numericHeight";
            numericHeight.Size = new Size(83, 23);
            numericHeight.Value = 50;
            numericHeight.Maximum = 1000;
            numericHeight.Minimum = 1;

            // ── label4 ──
            label4.AutoSize = true;
            label4.Location = new Point(12, 240);
            label4.Text = "Already created figures:";

            // ── listBox ──
            listBox.FormattingEnabled = true;
            listBox.ItemHeight = 15;
            listBox.Location = new Point(12, 258);
            listBox.Name = "listBox";
            listBox.Size = new Size(132, 154);
            listBox.TabIndex = 7;

            // ── buttonClear ──
            buttonClear.Location = new Point(12, 420);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(132, 23);
            buttonClear.Text = "Clear";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;

            // ── buttonSave ──
            buttonSave.Location = new Point(12, 450);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(132, 23);
            buttonSave.Text = "Save to file";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += OnSave;

            // ── buttonLoad ──
            buttonLoad.Location = new Point(12, 480);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(132, 23);
            buttonLoad.Text = "Load from file";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += OnLoad;

            // ── Figures form ──
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 520);
            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(buttonColor);
            Controls.Add(buttonClear);
            Controls.Add(buttonSave);
            Controls.Add(buttonLoad);
            Controls.Add(numericHeight);
            Controls.Add(listBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(numericWidth);
            Controls.Add(label2);
            Controls.Add(comboBoxShapeType);
            Controls.Add(label1);
            Name = "Figures";
            Text = "Graphics Editor - Lab5";

            ((System.ComponentModel.ISupportInitialize)numericWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericHeight).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboBoxShapeType;
        private Label label2;
        private NumericUpDown numericWidth;
        private Label label3;
        private Label label4;
        private ListBox listBox;
        private NumericUpDown numericHeight;
        private Button buttonClear;
        private ColorDialog colorDialog;
        private Button buttonColor;
        private Label label5;
        private Label label6;
        private Button buttonSave;
        private Button buttonLoad;
        private MenuStrip menuStrip;
        private ToolStripMenuItem menuFile;
        private ToolStripMenuItem menuItemSave;
        private ToolStripMenuItem menuItemLoad;
        private ToolStripMenuItem menuSettings;
    }
}
