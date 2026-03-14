namespace Lab1
{
    partial class Figures 
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
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
            ((System.ComponentModel.ISupportInitialize)numericWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericHeight).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(135, 15);
            label1.TabIndex = 0;
            label1.Text = "Select the type of figure:";
            // 
            // comboBoxShapeType
            // 
            comboBoxShapeType.FormattingEnabled = true;
            comboBoxShapeType.Location = new Point(12, 27);
            comboBoxShapeType.Name = "comboBoxShapeType";
            comboBoxShapeType.Size = new Size(132, 23);
            comboBoxShapeType.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 70);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 3;
            label2.Text = "Select color:";
            // 
            // numericWidth
            // 
            numericWidth.Location = new Point(61, 150);
            numericWidth.Name = "numericWidth";
            numericWidth.Size = new Size(83, 23);
            numericWidth.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 132);
            label3.Name = "label3";
            label3.Size = new Size(101, 15);
            label3.TabIndex = 5;
            label3.Text = "Enter dimensions:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 221);
            label4.Name = "label4";
            label4.Size = new Size(131, 15);
            label4.TabIndex = 6;
            label4.Text = "Already created figures:";
            // 
            // listBox
            // 
            listBox.FormattingEnabled = true;
            listBox.ItemHeight = 15;
            listBox.Location = new Point(12, 239);
            listBox.Name = "listBox";
            listBox.Size = new Size(132, 169);
            listBox.TabIndex = 7;
            // 
            // numericHeight
            // 
            numericHeight.Location = new Point(61, 179);
            numericHeight.Name = "numericHeight";
            numericHeight.Size = new Size(83, 23);
            numericHeight.TabIndex = 8;
            // 
            // buttonClear
            // 
            buttonClear.Location = new Point(12, 415);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(132, 23);
            buttonClear.TabIndex = 9;
            buttonClear.Text = "Clear";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // buttonColor
            // 
            buttonColor.Location = new Point(12, 88);
            buttonColor.Name = "buttonColor";
            buttonColor.Size = new Size(132, 23);
            buttonColor.TabIndex = 10;
            buttonColor.UseVisualStyleBackColor = true;
            buttonColor.Click += buttonColor_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(9, 152);
            label5.Name = "label5";
            label5.Size = new Size(42, 15);
            label5.TabIndex = 11;
            label5.Text = "Width:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 181);
            label6.Name = "label6";
            label6.Size = new Size(46, 15);
            label6.TabIndex = 12;
            label6.Text = "Height:";
            // 
            // Figures
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(buttonColor);
            Controls.Add(buttonClear);
            Controls.Add(numericHeight);
            Controls.Add(listBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(numericWidth);
            Controls.Add(label2);
            Controls.Add(comboBoxShapeType);
            Controls.Add(label1);
            Name = "Figures";
            Text = "Form1";
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
    }
}
