using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace lab_4_5.Views
{
    public partial class ProfileView : Window
    {
        public ProfileView()
        {
            InitializeComponent();
            LoadCurrentSettings();

            txtUserName.Text = "PUBG_Master";
            txtEmail.Text = "master@erangel.com";
        }

        private void LoadCurrentSettings()
        {
            var currentThemeDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Themes/"));

            if(currentThemeDict != null)
            {
                string source = currentThemeDict.Source.OriginalString;

                foreach(ComboBoxItem item in cmbThemes.Items)
                {
                    if(source.Contains(item.Tag.ToString()))
                    {
                        cmbThemes.SelectedItem = item;
                        break;
                    }
                }
            }

            var currentLangDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Lang/"));

            if(currentLangDict!= null)
            {
                if(currentLangDict.Source.OriginalString.Contains("ru"))
                {
                    rbRu.IsChecked = true;
                }
                else
                {
                    rbEn.IsChecked = true;
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e) => this.Close();

        private void CmbThemes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded) return;

            if (cmbThemes.SelectedItem is ComboBoxItem item)
            {
                string themeName = item.Tag.ToString();
                var uri = new Uri($"Resources/Themes/{themeName}Theme.xaml", UriKind.Relative);
                ResourceDictionary newDict = new ResourceDictionary { Source = uri };

                var oldTheme = Application.Current.Resources.MergedDictionaries
                    .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Themes/"));

                if (oldTheme != null)
                {
                    int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldTheme);
                    Application.Current.Resources.MergedDictionaries[index] = newDict;
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(newDict);
                }
            }
        }

        private void Lang_Click(object sender, RoutedEventArgs e)
        {
            string lang = (sender as RadioButton).Tag.ToString();
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri($"Resources/Lang/{lang}.xaml", UriKind.Relative);

            var oldLang = Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source.OriginalString.Contains("Lang/"));
            if (oldLang != null)
            {
                int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldLang);
                Application.Current.Resources.MergedDictionaries[index] = dict;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Данные профиля {txtUserName.Text} сохранены!", "Успех");
            this.Close();
        }
    }
}
