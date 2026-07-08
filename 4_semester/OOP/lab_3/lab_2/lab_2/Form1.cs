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
using System.ComponentModel.DataAnnotations;

namespace lab_2
{
    public partial class Form1 : Form
    {
        private List<Apartament> _apartaments = new List<Apartament>();
        private Stack<List<Apartament>> _undoStack = new Stack<List<Apartament>>();
        private Stack<List<Apartament>> _redoStack = new Stack<List<Apartament>>();

        private bool isDragging = false;

        public Form1()
        {
            InitializeComponent();
            SetupUI();

            Timer timer = new Timer { Interval = 1000 };
            timer.Tick += (s, e) => {
                lblTime.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToShortDateString();
            };
            timer.Start();

            tsBtnSearch.Click += (s, e) =>
            {
                ButtonSearch();
            };
            tsSortArea.Click += (s, e) => SortByArea();
            tsBtnClear.Click += BtnClear_Click;

            toolStrip1.MouseDown += ToolStrip1_MouseDown;
            toolStrip1.MouseMove += ToolStrip1_MouseMove;
            toolStrip1.MouseUp += ToolStrip1_MouseUp;

            tsBtnBack.Click += (s, e) => Undo();
            tsBtnForward.Click += (s, e) => Redo();

            tsBtnLock.Click += (s, e) => tsBtnLock_Click(s, e);
            BtnInfo.Click += (s, e) => AboutToolStripMenuItem_Click(s, e);

            tsBtnDelete.Click += (s, e) => {
                if (listOutput.SelectedIndex != -1)
                {
                    SaveState();
                    _apartaments.RemoveAt(listOutput.SelectedIndex);
                    UpdateDisplay(_apartaments);
                    UpdateStatus("Объект удален");
                }
            };

            hideToolstripToolStripMenuItem.Click += (s,e) => ToggleToolbarVisibility(s, e);
            BtnToggleView.Click += (s, e) => ToggleToolbarVisibility(s, e);
        }

        private Point dragOffset;

        private void ToolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            if (toolStrip1.Dock == DockStyle.None && e.Button == MouseButtons.Left)
            {
                isDragging = true;

                Point mousePos = toolStrip1.PointToClient(Control.MousePosition);
                dragOffset = new Point(mousePos.X, mousePos.Y);
            }
        }

        private void ToolStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && e.Button == MouseButtons.Left)
            {
                Point formMousePos = this.PointToClient(Control.MousePosition);

                int newX = formMousePos.X - dragOffset.X;
                int newY = formMousePos.Y - dragOffset.Y;

                toolStrip1.Location = new Point(newX, newY);
            }
        }

        private void ToolStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void ButtonSearch()
        {
            using (SearchForm searchWindow = new SearchForm())
            {
                if (searchWindow.ShowDialog() == DialogResult.OK)
                {
                    string pattern = searchWindow.CityPattern;
                    int minArea = searchWindow.MinArea;       

                    var filteredResults = _apartaments.Where(a =>
                        System.Text.RegularExpressions.Regex.IsMatch(a.ApartamentAddress.City, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase) &&
                        a.Area >= minArea
                    ).ToList();

                    UpdateDisplay(filteredResults);
                    UpdateStatus($"Найдено совпадений: {filteredResults.Count}");

                    if (filteredResults.Any())
                    {
                        string searchJson = JsonSerializer.Serialize(filteredResults, new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText("search_results.json", searchJson);
                        MessageBox.Show("Результаты поиска сохранены в search_results.json");
                    }
                    else
                    {
                        MessageBox.Show("По вашему запросу ничего не найдено.");
                    }
                }
            }
        }

        private void SaveState()
        {
            _undoStack.Push(new List<Apartament>(_apartaments));
            _redoStack.Clear();
        }

        private void Undo()
        {
            if (_undoStack.Count > 0)
            {
                _redoStack.Push(new List<Apartament>(_apartaments));

                _apartaments = _undoStack.Pop();

                UpdateDisplay(_apartaments);
                UpdateStatus("Действие отменено (Назад)");
            }
            else
            {
                MessageBox.Show("История пуста (некуда возвращаться)");
            }
        }

        private void Redo()
        {
            if (_redoStack.Count > 0)
            {
                _undoStack.Push(new List<Apartament>(_apartaments));

                _apartaments = _redoStack.Pop();

                UpdateDisplay(_apartaments);
                UpdateStatus("Действие возвращено (Вперед)");
            }
            else
            {
                MessageBox.Show("Нет действий для возврата");
            }
        }

        private void UpdateStatus(string action)
        {
            lblStatusCount.Text = $"Объектов: {_apartaments.Count}";
            lblLastAction.Text = $"Действие: {action}";
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
                HasBasement = chkBasement.Checked,
                ApartamentAddress = new Address
                {
                    Street = txtStreet.Text
                }
            };

            double result = tempApt.CalculateCost();
            
            listOutput.Items.Clear();
            listOutput.Items.Add($"{tempApt.ApartamentAddress.Street}, {tempApt.Area}м2 - {result}$");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid()) return;

            SaveState();

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

            apt.ApartamentAddress.City = txtCity.Text;
            apt.ApartamentAddress.Street = txtStreet.Text;
            apt.ApartmentDeveloper.Name = txtDevName.Text;
            apt.ApartmentDeveloper.INN = txtTIN.Text;

            if(!ValidateObject(apt))
            {
                return;
            }


            apt.CalculatedCost = apt.CalculateCost();
            _apartaments.Add(apt);

            string json = JsonSerializer.Serialize(_apartaments, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("apartments.json", json);

            UpdateStatus("Объект сохранен");
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
        private void tsBtnLock_Click(object sender, EventArgs e)
        {
            if (toolStrip1.Dock == DockStyle.Top)
            {
                toolStrip1.Dock = DockStyle.None;
                toolStrip1.GripStyle = ToolStripGripStyle.Visible;
                toolStrip1.Location = new Point(50, 50);
                UpdateStatus("Панель откреплена: тяните за край слева");
            }
            else
            {
                toolStrip1.Dock = DockStyle.Top;
                toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
                UpdateStatus("Панель закреплена");
            }
        }
        private void ToggleToolbarVisibility(object sender, EventArgs e)
        {
            toolStrip1.Visible = !toolStrip1.Visible;

            string state = toolStrip1.Visible ? "показана" : "скрыта";
            UpdateStatus($"Панель инструментов {state}");
        }
        private bool ValidateObject(object obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(obj, context, results, true))
            {
                string errors = string.Join("\n", results.Select(r => r.ErrorMessage));
                MessageBox.Show(errors, "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void SortByArea()
        {
            SaveState();
            var sorted = _apartaments.OrderBy(a => a.Area).ToList();
            UpdateDisplay(sorted);
            UpdateStatus("Сортировка по площади");
        }
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Версия 1.0\nРазработчик: Сикорский Артём Александрович",
                            "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnToggleToolbar_Click(object sender, EventArgs e)
        {
            if (toolStrip1.Dock == DockStyle.Top)
            {
                toolStrip1.Dock = DockStyle.None;
                toolStrip1.Visible = false;
            }
            else
            {
                toolStrip1.Dock = DockStyle.Top;
                toolStrip1.Visible = true;
            }
        }
        private void UpdateDisplay(List<Apartament> list)
        {
            listOutput.Items.Clear();
            if (list != null)
            {
                foreach (var a in list)
                {
                    listOutput.Items.Add($"{a.ApartamentAddress.Street}, {a.Area}м2 - {a.CalculatedCost}$");
                }
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearAllInputs(this);
            UpdateStatus("Все поля очищены");
        }

        private void ClearAllInputs(Control container)
        {
            foreach (Control c in container.Controls)
            {
                if (c is TextBox tb) tb.Clear();
                if (c is NumericUpDown nud) nud.Value = nud.Minimum;
                if (c is ComboBox cb) cb.SelectedIndex = -1;
                if (c is CheckBox chk) chk.Checked = false;
                if (c is TrackBar trk) trk.Value = trk.Minimum;

                if (c.HasChildren) ClearAllInputs(c);
            }
        }

    }
}
