using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

namespace lab_2
{
    public partial class Form1 : Form
    {
        private List<Apartament> _apartaments = new List<Apartament>();

        public Form1()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            trackArea.Minimum = 10;
            trackArea.Maximum = 300;
            trackArea.ValueChanged += (s, e) => lblAreaValue.Text = $"{trackArea.Value} м²";

            cmbMaterial.Items.AddRange(new string[] { "Кирпич", "Панель", "Блок", "Монолит" });
            cmbDevType.Items.AddRange(new string[] { "ООО", "ИП", "ОАО", "ЗАО" });
        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(txtStreet.Text)) return Error("Введите улицу");
            if (string.IsNullOrWhiteSpace(txtDevName.Text)) return Error("Введите застройщика");
            if (txtTIN.Text.Length != 10 && txtTIN.Text.Length != 12) return Error("ИНН должен быть 10 или 12 цифр");
            if (cmbMaterial.SelectedIndex == -1) return Error("Выберите материал");
            return true;
        }

        private bool Error(string msg)
        {
            MessageBox.Show(msg, "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            if (!IsValid()) return;

            var tempApt = new Apartament
            {
                Area = trackArea.Value,
                RoomsCount = (int)numRooms.Value,
                Floor = (int)numFloor.Value,
                YearBuilt = dtpYearBuilt.Value.Year,
                MaterialType = cmbMaterial.SelectedItem?.ToString() ?? "Блок",
                HasBalcony = chkBalcony.Checked,
                HasBasement = chkBasement.Checked
            };

            double result = tempApt.CalculateCost();
            
            listOutput.Items.Clear();
            listOutput.Items.Add($"{tempApt.ApartamentAddress.Street}, {tempApt.Area}м2 - {tempApt.CalculatedCost}$");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid()) return;

            var apt = new Apartament
            {
                Area = trackArea.Value,
                RoomsCount = (int)numRooms.Value,
                Floor = (int)numFloor.Value,
                YearBuilt = dtpYearBuilt.Value.Year,
                MaterialType = cmbMaterial.SelectedItem.ToString(),
                HasBalcony = chkBalcony.Checked,
                HasBasement = chkBasement.Checked,
                Condition = rbNoRepair.Checked ? "Новая" : "Вторичка",

                ApartamentAddress = new Address
                {
                    Country = txtCountry.Text,
                    City = txtCity.Text,
                    Street = txtStreet.Text
                },
                ApartmentDeveloper = new Developer
                {
                    Name = txtDevName.Text,
                    INN = txtTIN.Text,
                    CompanyType = cmbDevType.Text
                }
            };

            apt.CalculatedCost = apt.CalculateCost();
            _apartaments.Add(apt);

            string json = JsonSerializer.Serialize(_apartaments, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("apartments.json", json);

            MessageBox.Show($"Сохранено! Рассчитанная стоимость: {apt.CalculatedCost}$");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (File.Exists("apartments.json"))
            {
                string json = File.ReadAllText("apartments.json");
                _apartaments = JsonSerializer.Deserialize<List<Apartament>>(json);

                listOutput.Items.Clear();
                foreach (var a in _apartaments)
                {
                    listOutput.Items.Add($"{a.ApartamentAddress.Street}, {a.Area}м2 - {a.CalculatedCost}$");
                }
            }
        }
    }
}
