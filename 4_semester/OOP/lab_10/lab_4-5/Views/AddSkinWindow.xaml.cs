using lab_4_5.Models;
using Microsoft.Win32;
using System;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace lab_4_5.Views
{
    public partial class AddSkinWindow : Window
    {
        public Skin NewSkin { get; private set; }

        public AddSkinWindow()
        {
            try
            {
                var cursorStream = Application.GetResourceStream(new Uri("Resources/Cursors/aim.cur", UriKind.Relative)).Stream;
                this.Cursor = new Cursor(cursorStream);
            }
            catch { }

            InitializeComponent();
        }

        private byte[] ImageBytes;

        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                lblImagePath.Text = dlg.FileName;
                ImageBytes = System.IO.File.ReadAllBytes(dlg.FileName);
                SetImagePath(dlg.FileName);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool isNameOk = !string.IsNullOrWhiteSpace(txtName.Text);
            bool isPriceOk = decimal.TryParse(txtPrice.Text, out decimal price);
            bool isCategoryOk = cmbCategory.SelectedItem != null;

            if (!isNameOk || !isPriceOk || !isCategoryOk)
            {
                MessageBox.Show("Заполните все данные корректно!");
                return;
            }

            int catId = GetCategoryId(cmbCategory.Text);

            NewSkin = new Skin
            {
                ShortName = txtName.Text,
                Price = price,
                Quantity = int.TryParse(txtQuantity.Text, out int q) ? q : 0,
                CategoryId = catId,
                Rarity = cmbRarity.Text,
                ImagePath = lblImagePath.Text,
                ImageBytes = this.ImageBytes 
            };

            this.DialogResult = true;
        }

        private int GetCategoryId(string categoryText)
        {
            if (categoryText.Contains("Оружие") || categoryText.Contains("Weapon")) return 1;
            if (categoryText.Contains("Одежда") || categoryText.Contains("Clothing")) return 2;
            if (categoryText.Contains("Транспорт") || categoryText.Contains("Vehicle")) return 3;
            return 1;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) => this.DialogResult = false;

        public void SetImagePath(string path)
        {
            lblImagePath.Text = path;

            try
            {
                if(!string.IsNullOrEmpty(path) && System.IO.File.Exists(path))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(path);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad; 
                    bitmap.EndInit();

                    imgPreview.Source = bitmap;
                    lblImagePath.Text = path;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void LoadSkinData(Skin skin)
        {
            txtHeader.Text = "РЕДАКТИРОВАНИЕ";
            txtName.Text = skin.ShortName;
            txtPrice.Text = skin.Price.ToString();
            txtQuantity.Text = skin.Quantity.ToString();
            lblImagePath.Text = skin.ImagePath;

            string key = "Cat_Weapon";

            if(skin.Category != null)
            {
                key = ConvertRuToKey(skin.Category.Name);
            }
            cmbCategory.SelectedValue = skin.CategoryId;
            cmbCategory.SelectedItem = key;

            if (!string.IsNullOrEmpty(skin.ImagePath)) SetImagePath(skin.ImagePath);
        }

        private string ConvertRuToKey(string ruName)
        {
            switch (ruName)
            {
                case "Оружие": return "Cat_Weapon";
                case "Одежда": return "Cat_Clothing";
                case "Транспорт": return "Cat_Vehicle";
                default: return "Cat_Weapon";
            }
        }
    }
}