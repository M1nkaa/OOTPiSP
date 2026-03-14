using System;
using System.Windows.Forms;
using Lab3.Models;
using Lab3.Helpers;

namespace Lab3
{
    public partial class Form1 : Form
    {
        // Collection of vehicles
        private VehicleCollection _vehicleCollection;
        // Current file path
        private string _currentFilePath;

        // Constructor
        public Form1()
        {
            InitializeComponent();
            _vehicleCollection = new VehicleCollection();
            _currentFilePath = "vehicles.bson";

            // Setup ListView columns
            SetupListViewColumns();
            UpdateListView();
        }

        // Setup ListView columns
        private void SetupListViewColumns()
        {
            listViewVehicles.Columns.Clear();
            listViewVehicles.Columns.Add("Name", 120);
            listViewVehicles.Columns.Add("Type", 80);
            listViewVehicles.Columns.Add("Year", 50);
            listViewVehicles.Columns.Add("Color", 70);
            listViewVehicles.Columns.Add("Max Speed", 70);
            listViewVehicles.Columns.Add("Details", 250);
        }

        // Update ListView with current data
        private void UpdateListView()
        {
            listViewVehicles.Items.Clear();

            foreach (var vehicle in _vehicleCollection.Vehicles)
            {
                var item = new ListViewItem(vehicle.Name);
                item.SubItems.Add(vehicle.GetVehicleType());
                item.SubItems.Add(vehicle.Year.ToString());
                item.SubItems.Add(vehicle.Color);
                item.SubItems.Add(vehicle.MaxSpeed.ToString());
                item.SubItems.Add(vehicle.GetDetails());

                listViewVehicles.Items.Add(item);
            }

            lblStatus.Text = $"Ready | Objects: {_vehicleCollection.Count}";
        }

        // Add button click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Open form to add new vehicle
            using (var editForm = new Form2())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    _vehicleCollection.Add(editForm.Vehicle);
                    UpdateListView();
                }
            }
        }

        // Edit button click
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listViewVehicles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a vehicle to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = listViewVehicles.SelectedIndices[0];
            var vehicle = _vehicleCollection.Vehicles[index];

            // Open form to edit selected vehicle
            using (var editForm = new Form2(vehicle))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    _vehicleCollection.Vehicles[index] = editForm.Vehicle;
                    UpdateListView();
                }
            }
        }

        // Delete button click
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewVehicles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a vehicle to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete the selected vehicle?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int index = listViewVehicles.SelectedIndices[0];
                _vehicleCollection.RemoveAt(index);
                UpdateListView();
            }
        }

        // Save button click
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_vehicleCollection.Count == 0)
            {
                MessageBox.Show("No vehicles to save.", "Empty List",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog.FileName = "vehicles.bson";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BsonSerializationHelper.Serialize(saveFileDialog.FileName, _vehicleCollection);
                    MessageBox.Show($"Successfully saved {_vehicleCollection.Count} vehicles to:\n{saveFileDialog.FileName}",
                        "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving file:\n{ex.Message}", "Save Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Load button click
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _vehicleCollection = BsonSerializationHelper.Deserialize(openFileDialog.FileName);
                    UpdateListView();
                    MessageBox.Show($"Successfully loaded {_vehicleCollection.Count} vehicles from:\n{openFileDialog.FileName}",
                        "Load Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading file:\n{ex.Message}", "Load Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Clear button click
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_vehicleCollection.Count == 0)
                return;

            var result = MessageBox.Show("Are you sure you want to clear all vehicles?",
                "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _vehicleCollection.Clear();
                UpdateListView();
            }
        }

        // ListView selection changed
        private void listViewVehicles_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hasSelection = listViewVehicles.SelectedItems.Count > 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
        }
    }
}