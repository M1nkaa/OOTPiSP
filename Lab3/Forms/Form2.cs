using Lab3.Models;
using System;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form2 : Form
    {
        // Current vehicle
        private Vehicle _vehicle;

        // Vehicle property
        public Vehicle Vehicle => _vehicle;

        // Constructor
        public Form2(Vehicle vehicle = null)
        {
            InitializeComponent();

            if (vehicle == null)
            {
                // Create default car
                _vehicle = new Car
                {
                    Name = "New Car",
                    Year = 2023,
                    MaxSpeed = 180,
                    Color = "Red"
                };
            }
            else
            {
                _vehicle = vehicle;
            }

            // Fill basic fields
            txtName.Text = _vehicle.Name;
            txtYear.Text = _vehicle.Year.ToString();
            txtMaxSpeed.Text = _vehicle.MaxSpeed.ToString();
            txtColor.Text = _vehicle.Color;

            // Set type in combobox
            if (_vehicle is Car) cmbVehicleType.SelectedIndex = 0;
            else if (_vehicle is Motorcycle) cmbVehicleType.SelectedIndex = 1;
            else if (_vehicle is Boat) cmbVehicleType.SelectedIndex = 2;
            else if (_vehicle is Ship) cmbVehicleType.SelectedIndex = 3;
            else if (_vehicle is Airplane) cmbVehicleType.SelectedIndex = 4;
            else if (_vehicle is Helicopter) cmbVehicleType.SelectedIndex = 5;

            // Show correct fields
            ShowCorrectGroupBox();

            // Fill specific fields
            LoadSpecificData();
        }

        // Show correct group box
        private void ShowCorrectGroupBox()
        {
            // Hide all
            gbCar.Visible = false;
            gbMotorcycle.Visible = false;
            gbBoat.Visible = false;
            gbShip.Visible = false;
            gbAirplane.Visible = false;
            gbHelicopter.Visible = false;

            // Show correct one
            if (_vehicle is Car) gbCar.Visible = true;
            else if (_vehicle is Motorcycle) gbMotorcycle.Visible = true;
            else if (_vehicle is Boat) gbBoat.Visible = true;
            else if (_vehicle is Ship) gbShip.Visible = true;
            else if (_vehicle is Airplane) gbAirplane.Visible = true;
            else if (_vehicle is Helicopter) gbHelicopter.Visible = true;
        }

        // Load specific data
        private void LoadSpecificData()
        {
            if (_vehicle is Car car)
            {
                nudDoors.Value = car.DoorsCount;
                cmbBodyType.SelectedItem = car.BodyType;
                chkAC.Checked = car.HasAirConditioning;
            }
            else if (_vehicle is Motorcycle motorcycle)
            {
                chkSidecar.Checked = motorcycle.HasSidecar;
                cmbMotoType.SelectedItem = motorcycle.MotorcycleType;
            }
            else if (_vehicle is Boat boat)
            {
                nudCapacity.Value = boat.Capacity;
                chkCabin.Checked = boat.HasCabin;
            }
            else if (_vehicle is Ship ship)
            {
                nudContainerCapacity.Value = ship.ContainerCapacity;
                cmbShipType.SelectedItem = ship.ShipType;
            }
            else if (_vehicle is Airplane airplane)
            {
                nudPassengerCapacity.Value = airplane.PassengerCapacity;
                nudFlightRange.Value = (decimal)airplane.FlightRange;
            }
            else if (_vehicle is Helicopter helicopter)
            {
                nudRotorBlades.Value = helicopter.RotorBlades;
                nudMaxAltitude.Value = (decimal)helicopter.MaxAltitude;
            }
        }

        // Vehicle type changed
        private void cmbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Create new object of selected type
            switch (cmbVehicleType.SelectedIndex)
            {
                case 0:
                    _vehicle = new Car();
                    break;
                case 1:
                    _vehicle = new Motorcycle();
                    break;
                case 2:
                    _vehicle = new Boat();
                    break;
                case 3:
                    _vehicle = new Ship();
                    break;
                case 4:
                    _vehicle = new Airplane();
                    break;
                case 5:
                    _vehicle = new Helicopter();
                    break;
            }

            // Copy basic properties
            _vehicle.Name = txtName.Text;
            _vehicle.Year = int.TryParse(txtYear.Text, out int y) ? y : 2023;
            _vehicle.MaxSpeed = double.TryParse(txtMaxSpeed.Text, out double s) ? s : 0;
            _vehicle.Color = txtColor.Text;

            // Show correct panel
            ShowCorrectGroupBox();
        }

        // Save button click
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Save basic properties
                _vehicle.Name = txtName.Text;
                _vehicle.Year = int.Parse(txtYear.Text);
                _vehicle.MaxSpeed = double.Parse(txtMaxSpeed.Text);
                _vehicle.Color = txtColor.Text;

                // Save specific properties
                if (_vehicle is Car car)
                {
                    car.DoorsCount = (int)nudDoors.Value;
                    car.BodyType = cmbBodyType.SelectedItem?.ToString() ?? "Sedan";
                    car.HasAirConditioning = chkAC.Checked;
                }
                else if (_vehicle is Motorcycle motorcycle)
                {
                    motorcycle.HasSidecar = chkSidecar.Checked;
                    motorcycle.MotorcycleType = cmbMotoType.SelectedItem?.ToString() ?? "Sport";
                }
                else if (_vehicle is Boat boat)
                {
                    boat.Capacity = (int)nudCapacity.Value;
                    boat.HasCabin = chkCabin.Checked;
                }
                else if (_vehicle is Ship ship)
                {
                    ship.ContainerCapacity = (int)nudContainerCapacity.Value;
                    ship.ShipType = cmbShipType.SelectedItem?.ToString() ?? "Cargo";
                }
                else if (_vehicle is Airplane airplane)
                {
                    airplane.PassengerCapacity = (int)nudPassengerCapacity.Value;
                    airplane.FlightRange = (double)nudFlightRange.Value;
                }
                else if (_vehicle is Helicopter helicopter)
                {
                    helicopter.RotorBlades = (int)nudRotorBlades.Value;
                    helicopter.MaxAltitude = (double)nudMaxAltitude.Value;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}