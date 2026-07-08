using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lab_1
{
    public partial class Form1 : Form
    {
        private readonly Calculator _calculator;

        private delegate void CalculationHandler();

        private CheckBox myCheckBox;

        public Form1()
        {
            InitializeComponent();
            _calculator = new Calculator();

            myCheckBox = new CheckBox();
            this.Controls.Add(myCheckBox);
            myCheckBox.Text = "123";
            myCheckBox.Click += BtnClear_Click;

            btnCalculate.Click += (s, e) => ExecuteCalculation(PerformMainCalculation);
            btnClear.Click += BtnClear_Click;

            txtMatLength.KeyPress += NumbersOnly;
            txtMatWidth.KeyPress += NumbersOnly;
            txtRoomLength.KeyPress += NumbersOnly;
            txtRoomWidth.KeyPress += NumbersOnly;
            txtWaste.KeyPress += NumbersOnly;
        }

        private void MytextBox_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExecuteCalculation(CalculationHandler action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ввода: {ex.Message}", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PerformMainCalculation()
        {
            double roomW = double.Parse(txtRoomWidth.Text);
            double roomL = double.Parse(txtRoomLength.Text);
            double roomAreaM2 = _calculator.CalculateArea(roomW, roomL);

            double matW = double.Parse(txtMatWidth.Text);
            double matL = double.Parse(txtMatLength.Text);
            double matAreaM2 = _calculator.CalculateArea(matW, matL);

            double waste = string.IsNullOrEmpty(txtWaste.Text) ? 0 : double.Parse(txtWaste.Text);

            double areaFt2 = _calculator.ConvertToSquareFeet(roomAreaM2);
            int quantity = _calculator.CalculateQuantity(roomAreaM2, matAreaM2, waste);

            lblResAreaM2.Text = $"{roomAreaM2:F2} м²";
            lblResArreaFt2.Text = $"{areaFt2:F2} ft²";
            txtTotalQuantity.Text = quantity.ToString() + " шт./рулонов";
        }

        private void NumbersOnly(object sender, KeyPressEventArgs e)
        {
            TextBox owner = sender as TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if(e.KeyChar == ',' && owner.Text.Contains(","))
            {
                e.Handled = true;
            }

            if (e.KeyChar == ',' && owner.Text.Length == 0)
            {
                e.Handled = true;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            foreach(Control c in this.Controls)
            {
                if (c is TextBox tb) tb.Clear();
            }

            txtRoomWidth.Clear();
            txtRoomLength.Clear();
            txtMatWidth.Clear();
            txtMatLength.Clear();
            txtWaste.Clear();

            lblResAreaM2.Text = "0 м²";
            lblResArreaFt2.Text = "0 ft²";
            txtTotalQuantity.Text = "0 шт./рулонов";
        }
    }
}
