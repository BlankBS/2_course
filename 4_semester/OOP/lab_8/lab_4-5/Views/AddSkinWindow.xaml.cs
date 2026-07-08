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

        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                SetImagePath(dlg.FileName);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || !decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Ошибка данных!");
                return;
            }

            string selectedKey = cmbCategory.SelectedItem.ToString();

            NewSkin = new Skin
            {
                ShortName = txtName.Text,
                Price = price,
                Quantity = int.Parse(txtQuantity.Text),
                Category = ConvertKeyToRu(selectedKey),
                Rarity = cmbRarity.Text,
                ImagePath = lblImagePath.Text
            };

            this.DialogResult = true;
        }

        private string ConvertKeyToRu(string key)
        {
            switch (key)
            {
                case "Cat_Weapon": return "Оружие";
                case "Cat_Clothing": return "Одежда";
                case "Cat_Vehicle": return "Транспорт";
                default: return "Оружие";
            }
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

            string key = ConvertRuToKey(skin.Category);
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