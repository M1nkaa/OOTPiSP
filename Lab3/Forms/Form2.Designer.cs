namespace Lab3
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            cmbVehicleType = new ComboBox();
            txtName = new TextBox();
            txtYear = new TextBox();
            txtMaxSpeed = new TextBox();
            txtColor = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            gbCar = new GroupBox();
            nudDoors = new NumericUpDown();
            cmbBodyType = new ComboBox();
            chkAC = new CheckBox();
            label7 = new Label();
            label8 = new Label();
            gbMotorcycle = new GroupBox();
            chkSidecar = new CheckBox();
            cmbMotoType = new ComboBox();
            label9 = new Label();
            gbBoat = new GroupBox();
            nudCapacity = new NumericUpDown();
            chkCabin = new CheckBox();
            label10 = new Label();
            gbShip = new GroupBox();
            nudContainerCapacity = new NumericUpDown();
            cmbShipType = new ComboBox();
            label11 = new Label();
            label12 = new Label();
            gbAirplane = new GroupBox();
            nudPassengerCapacity = new NumericUpDown();
            nudFlightRange = new NumericUpDown();
            label13 = new Label();
            label14 = new Label();
            gbHelicopter = new GroupBox();
            nudRotorBlades = new NumericUpDown();
            nudMaxAltitude = new NumericUpDown();
            label15 = new Label();
            label16 = new Label();
            gbCar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDoors).BeginInit();
            gbMotorcycle.SuspendLayout();
            gbBoat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCapacity).BeginInit();
            gbShip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudContainerCapacity).BeginInit();
            gbAirplane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPassengerCapacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudFlightRange).BeginInit();
            gbHelicopter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudRotorBlades).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMaxAltitude).BeginInit();
            SuspendLayout();
            // 
            // cmbVehicleType
            // 
            cmbVehicleType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVehicleType.FormattingEnabled = true;
            cmbVehicleType.Items.AddRange(new object[] { "Car", "Motorcycle", "Boat", "Ship", "Airplane", "Helicopter" });
            cmbVehicleType.Location = new Point(105, 19);
            cmbVehicleType.Name = "cmbVehicleType";
            cmbVehicleType.Size = new Size(219, 23);
            cmbVehicleType.TabIndex = 0;
            cmbVehicleType.SelectedIndexChanged += cmbVehicleType_SelectedIndexChanged;
            // 
            // txtName
            // 
            txtName.Location = new Point(105, 56);
            txtName.Name = "txtName";
            txtName.Size = new Size(219, 23);
            txtName.TabIndex = 2;
            txtName.Text = "New Vehicle";
            // 
            // txtYear
            // 
            txtYear.Location = new Point(105, 94);
            txtYear.Name = "txtYear";
            txtYear.Size = new Size(219, 23);
            txtYear.TabIndex = 4;
            txtYear.Text = "2023";
            // 
            // txtMaxSpeed
            // 
            txtMaxSpeed.Location = new Point(105, 131);
            txtMaxSpeed.Name = "txtMaxSpeed";
            txtMaxSpeed.Size = new Size(219, 23);
            txtMaxSpeed.TabIndex = 6;
            txtMaxSpeed.Text = "180";
            // 
            // txtColor
            // 
            txtColor.Location = new Point(105, 169);
            txtColor.Name = "txtColor";
            txtColor.Size = new Size(219, 23);
            txtColor.TabIndex = 8;
            txtColor.Text = "Red";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(60, 347);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 28);
            btnSave.TabIndex = 16;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(204, 347);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 28);
            btnCancel.TabIndex = 17;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 22);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 1;
            label1.Text = "Type:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 59);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 3;
            label2.Text = "Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 97);
            label3.Name = "label3";
            label3.Size = new Size(32, 15);
            label3.TabIndex = 5;
            label3.Text = "Year:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 134);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 7;
            label4.Text = "Max Speed:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(26, 172);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 9;
            label5.Text = "Color:";
            // 
            // label6
            // 
            label6.BorderStyle = BorderStyle.Fixed3D;
            label6.Location = new Point(18, 197);
            label6.Name = "label6";
            label6.Size = new Size(315, 2);
            label6.TabIndex = 18;
            // 
            // gbCar
            // 
            gbCar.Controls.Add(nudDoors);
            gbCar.Controls.Add(cmbBodyType);
            gbCar.Controls.Add(chkAC);
            gbCar.Controls.Add(label7);
            gbCar.Controls.Add(label8);
            gbCar.Location = new Point(18, 206);
            gbCar.Name = "gbCar";
            gbCar.Size = new Size(315, 122);
            gbCar.TabIndex = 10;
            gbCar.TabStop = false;
            gbCar.Text = "Car Properties";
            gbCar.Visible = false;
            // 
            // nudDoors
            // 
            nudDoors.Location = new Point(105, 28);
            nudDoors.Maximum = new decimal(new int[] { 7, 0, 0, 0 });
            nudDoors.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudDoors.Name = "nudDoors";
            nudDoors.Size = new Size(192, 23);
            nudDoors.TabIndex = 0;
            nudDoors.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // cmbBodyType
            // 
            cmbBodyType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBodyType.FormattingEnabled = true;
            cmbBodyType.Items.AddRange(new object[] { "Sedan", "Hatchback", "SUV", "Coupe", "Convertible" });
            cmbBodyType.Location = new Point(105, 56);
            cmbBodyType.Name = "cmbBodyType";
            cmbBodyType.Size = new Size(193, 23);
            cmbBodyType.TabIndex = 2;
            // 
            // chkAC
            // 
            chkAC.AutoSize = true;
            chkAC.Location = new Point(87, 97);
            chkAC.Name = "chkAC";
            chkAC.Size = new Size(137, 19);
            chkAC.TabIndex = 4;
            chkAC.Text = "Has Air Conditioning";
            chkAC.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(18, 30);
            label7.Name = "label7";
            label7.Size = new Size(41, 15);
            label7.TabIndex = 1;
            label7.Text = "Doors:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(18, 59);
            label8.Name = "label8";
            label8.Size = new Size(65, 15);
            label8.TabIndex = 3;
            label8.Text = "Body Type:";
            // 
            // gbMotorcycle
            // 
            gbMotorcycle.Controls.Add(chkSidecar);
            gbMotorcycle.Controls.Add(cmbMotoType);
            gbMotorcycle.Controls.Add(label9);
            gbMotorcycle.Location = new Point(18, 206);
            gbMotorcycle.Name = "gbMotorcycle";
            gbMotorcycle.Size = new Size(315, 94);
            gbMotorcycle.TabIndex = 11;
            gbMotorcycle.TabStop = false;
            gbMotorcycle.Text = "Motorcycle Properties";
            gbMotorcycle.Visible = false;
            // 
            // chkSidecar
            // 
            chkSidecar.AutoSize = true;
            chkSidecar.Location = new Point(105, 28);
            chkSidecar.Name = "chkSidecar";
            chkSidecar.Size = new Size(87, 19);
            chkSidecar.TabIndex = 0;
            chkSidecar.Text = "Has Sidecar";
            chkSidecar.UseVisualStyleBackColor = true;
            // 
            // cmbMotoType
            // 
            cmbMotoType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMotoType.FormattingEnabled = true;
            cmbMotoType.Items.AddRange(new object[] { "Sport", "Cruiser", "Touring", "Dirt" });
            cmbMotoType.Location = new Point(105, 56);
            cmbMotoType.Name = "cmbMotoType";
            cmbMotoType.Size = new Size(193, 23);
            cmbMotoType.TabIndex = 1;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(18, 59);
            label9.Name = "label9";
            label9.Size = new Size(35, 15);
            label9.TabIndex = 2;
            label9.Text = "Type:";
            // 
            // gbBoat
            // 
            gbBoat.Controls.Add(nudCapacity);
            gbBoat.Controls.Add(chkCabin);
            gbBoat.Controls.Add(label10);
            gbBoat.Location = new Point(18, 206);
            gbBoat.Name = "gbBoat";
            gbBoat.Size = new Size(315, 94);
            gbBoat.TabIndex = 12;
            gbBoat.TabStop = false;
            gbBoat.Text = "Boat Properties";
            gbBoat.Visible = false;
            // 
            // nudCapacity
            // 
            nudCapacity.Location = new Point(105, 28);
            nudCapacity.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nudCapacity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCapacity.Name = "nudCapacity";
            nudCapacity.Size = new Size(192, 23);
            nudCapacity.TabIndex = 0;
            nudCapacity.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // chkCabin
            // 
            chkCabin.AutoSize = true;
            chkCabin.Location = new Point(105, 56);
            chkCabin.Name = "chkCabin";
            chkCabin.Size = new Size(80, 19);
            chkCabin.TabIndex = 2;
            chkCabin.Text = "Has Cabin";
            chkCabin.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(18, 30);
            label10.Name = "label10";
            label10.Size = new Size(56, 15);
            label10.TabIndex = 1;
            label10.Text = "Capacity:";
            // 
            // gbShip
            // 
            gbShip.Controls.Add(nudContainerCapacity);
            gbShip.Controls.Add(cmbShipType);
            gbShip.Controls.Add(label11);
            gbShip.Controls.Add(label12);
            gbShip.Location = new Point(18, 206);
            gbShip.Name = "gbShip";
            gbShip.Size = new Size(315, 94);
            gbShip.TabIndex = 13;
            gbShip.TabStop = false;
            gbShip.Text = "Ship Properties";
            gbShip.Visible = false;
            // 
            // nudContainerCapacity
            // 
            nudContainerCapacity.Location = new Point(131, 28);
            nudContainerCapacity.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            nudContainerCapacity.Name = "nudContainerCapacity";
            nudContainerCapacity.Size = new Size(166, 23);
            nudContainerCapacity.TabIndex = 0;
            nudContainerCapacity.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // cmbShipType
            // 
            cmbShipType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbShipType.FormattingEnabled = true;
            cmbShipType.Items.AddRange(new object[] { "Cargo", "Tanker", "Passenger", "Container" });
            cmbShipType.Location = new Point(105, 56);
            cmbShipType.Name = "cmbShipType";
            cmbShipType.Size = new Size(193, 23);
            cmbShipType.TabIndex = 2;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(18, 30);
            label11.Name = "label11";
            label11.Size = new Size(111, 15);
            label11.TabIndex = 1;
            label11.Text = "Container Capacity:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(18, 59);
            label12.Name = "label12";
            label12.Size = new Size(61, 15);
            label12.TabIndex = 3;
            label12.Text = "Ship Type:";
            // 
            // gbAirplane
            // 
            gbAirplane.Controls.Add(nudPassengerCapacity);
            gbAirplane.Controls.Add(nudFlightRange);
            gbAirplane.Controls.Add(label13);
            gbAirplane.Controls.Add(label14);
            gbAirplane.Location = new Point(18, 206);
            gbAirplane.Name = "gbAirplane";
            gbAirplane.Size = new Size(315, 94);
            gbAirplane.TabIndex = 14;
            gbAirplane.TabStop = false;
            gbAirplane.Text = "Airplane Properties";
            gbAirplane.Visible = false;
            // 
            // nudPassengerCapacity
            // 
            nudPassengerCapacity.Location = new Point(131, 28);
            nudPassengerCapacity.Maximum = new decimal(new int[] { 800, 0, 0, 0 });
            nudPassengerCapacity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudPassengerCapacity.Name = "nudPassengerCapacity";
            nudPassengerCapacity.Size = new Size(166, 23);
            nudPassengerCapacity.TabIndex = 0;
            nudPassengerCapacity.Value = new decimal(new int[] { 150, 0, 0, 0 });
            // 
            // nudFlightRange
            // 
            nudFlightRange.Location = new Point(131, 56);
            nudFlightRange.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            nudFlightRange.Name = "nudFlightRange";
            nudFlightRange.Size = new Size(166, 23);
            nudFlightRange.TabIndex = 2;
            nudFlightRange.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(18, 30);
            label13.Name = "label13";
            label13.Size = new Size(112, 15);
            label13.TabIndex = 1;
            label13.Text = "Passenger Capacity:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(18, 58);
            label14.Name = "label14";
            label14.Size = new Size(76, 15);
            label14.TabIndex = 3;
            label14.Text = "Flight Range:";
            // 
            // gbHelicopter
            // 
            gbHelicopter.Controls.Add(nudRotorBlades);
            gbHelicopter.Controls.Add(nudMaxAltitude);
            gbHelicopter.Controls.Add(label15);
            gbHelicopter.Controls.Add(label16);
            gbHelicopter.Location = new Point(18, 206);
            gbHelicopter.Name = "gbHelicopter";
            gbHelicopter.Size = new Size(315, 94);
            gbHelicopter.TabIndex = 15;
            gbHelicopter.TabStop = false;
            gbHelicopter.Text = "Helicopter Properties";
            gbHelicopter.Visible = false;
            // 
            // nudRotorBlades
            // 
            nudRotorBlades.Location = new Point(105, 28);
            nudRotorBlades.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            nudRotorBlades.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            nudRotorBlades.Name = "nudRotorBlades";
            nudRotorBlades.Size = new Size(192, 23);
            nudRotorBlades.TabIndex = 0;
            nudRotorBlades.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // nudMaxAltitude
            // 
            nudMaxAltitude.Location = new Point(105, 56);
            nudMaxAltitude.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudMaxAltitude.Name = "nudMaxAltitude";
            nudMaxAltitude.Size = new Size(192, 23);
            nudMaxAltitude.TabIndex = 2;
            nudMaxAltitude.Value = new decimal(new int[] { 3000, 0, 0, 0 });
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(18, 30);
            label15.Name = "label15";
            label15.Size = new Size(76, 15);
            label15.TabIndex = 1;
            label15.Text = "Rotor Blades:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(18, 58);
            label16.Name = "label16";
            label16.Size = new Size(77, 15);
            label16.TabIndex = 3;
            label16.Text = "Max Altitude:";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 394);
            Controls.Add(label6);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(gbHelicopter);
            Controls.Add(gbAirplane);
            Controls.Add(gbShip);
            Controls.Add(gbBoat);
            Controls.Add(gbMotorcycle);
            Controls.Add(gbCar);
            Controls.Add(label5);
            Controls.Add(txtColor);
            Controls.Add(label4);
            Controls.Add(txtMaxSpeed);
            Controls.Add(label3);
            Controls.Add(txtYear);
            Controls.Add(label2);
            Controls.Add(txtName);
            Controls.Add(label1);
            Controls.Add(cmbVehicleType);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Vehicle";
            gbCar.ResumeLayout(false);
            gbCar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudDoors).EndInit();
            gbMotorcycle.ResumeLayout(false);
            gbMotorcycle.PerformLayout();
            gbBoat.ResumeLayout(false);
            gbBoat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCapacity).EndInit();
            gbShip.ResumeLayout(false);
            gbShip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudContainerCapacity).EndInit();
            gbAirplane.ResumeLayout(false);
            gbAirplane.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPassengerCapacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudFlightRange).EndInit();
            gbHelicopter.ResumeLayout(false);
            gbHelicopter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudRotorBlades).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMaxAltitude).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ComboBox cmbVehicleType;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtMaxSpeed;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;

        // Car controls
        private System.Windows.Forms.GroupBox gbCar;
        private System.Windows.Forms.NumericUpDown nudDoors;
        private System.Windows.Forms.ComboBox cmbBodyType;
        private System.Windows.Forms.CheckBox chkAC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;

        // Motorcycle controls
        private System.Windows.Forms.GroupBox gbMotorcycle;
        private System.Windows.Forms.CheckBox chkSidecar;
        private System.Windows.Forms.ComboBox cmbMotoType;
        private System.Windows.Forms.Label label9;

        // Boat controls
        private System.Windows.Forms.GroupBox gbBoat;
        private System.Windows.Forms.NumericUpDown nudCapacity;
        private System.Windows.Forms.CheckBox chkCabin;
        private System.Windows.Forms.Label label10;

        // Ship controls
        private System.Windows.Forms.GroupBox gbShip;
        private System.Windows.Forms.NumericUpDown nudContainerCapacity;
        private System.Windows.Forms.ComboBox cmbShipType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;

        // Airplane controls
        private System.Windows.Forms.GroupBox gbAirplane;
        private System.Windows.Forms.NumericUpDown nudPassengerCapacity;
        private System.Windows.Forms.NumericUpDown nudFlightRange;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;

        // Helicopter controls
        private System.Windows.Forms.GroupBox gbHelicopter;
        private System.Windows.Forms.NumericUpDown nudRotorBlades;
        private System.Windows.Forms.NumericUpDown nudMaxAltitude;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
    }
}