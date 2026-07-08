using lab_4_5.Models;
using lab_4_5.ViewModels;
using lab_4_5.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab_4_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                var cursorStream = Application.GetResourceStream(new Uri("Resources/Cursors/aim.cur", UriKind.Relative)).Stream;
                this.Cursor = new Cursor(cursorStream);
            }
            catch { }
        }

        private void ChangeLang(object sender, RoutedEventArgs e)
        {
            string lang = (sender as Button).Tag.ToString();
            ResourceDictionary res = new ResourceDictionary();
            res.Source = new Uri($"Resources/Lang/{lang}.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries[0] = res;
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string droppedFilePath = files[0];

                var addWin = new Views.AddSkinWindow();
                addWin.Owner = this;

                addWin.SetImagePath(droppedFilePath);

                if (addWin.ShowDialog() == true)
                {
                    var vm = this.DataContext as ViewModels.MainViewModel;
                    vm?.AddNewSkin(addWin.NewSkin);
                }
            }
        }
        private void CloseWin(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeWin(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeWin(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }
        public void ChangeTheme(string themeName)
        {
            string path = $"Resources/Themes/{themeName}Theme.xaml";
            Uri uri = new Uri(path, UriKind.Relative);

            ResourceDictionary newTheme = new ResourceDictionary { Source = uri };

            var oldTheme = Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                d => d.Source != null && d.Source.OriginalString.Contains("Themes/"));

            if (oldTheme != null)
            {
                int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldTheme);
                Application.Current.Resources.MergedDictionaries[index] = newTheme;
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(newTheme);
            }
        }

        private void ThemeChange_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                ChangeTheme(btn.Tag.ToString());
            }
        }
        private void OpenProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileView profileWin = new ProfileView();
            profileWin.Owner = this;
            profileWin.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ResetFilters_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var vm = (ViewModels.MainViewModel)this.DataContext;
            vm.SearchText = "";
            vm.MaxPrice = 1000;
            vm.SelectedCategory = "Cat_All";
        }

        private async void Status_Tunneling(object sender, RoutedEventArgs e)
        {
            //RoutingParent.Background = Brushes.Orange;
            //System.Diagnostics.Debug.WriteLine("1. Tunneling: Родитель поймал сигнал первым");

            await Task.Delay(2000);
            //RoutingParent.Background = Brushes.Transparent;
        }

        private async void Status_Bubbling(object sender, RoutedEventArgs e)
        {
            //RoutingChild.Background = Brushes.Lime;
            //System.Diagnostics.Debug.WriteLine("2. Source: Сигнал сработал в кнопке");

            //await Task.Delay(2000);
            //RoutingChild.Background = Brushes.Transparent;

            //RoutingParent.Background = Brushes.Cyan;
            //System.Diagnostics.Debug.WriteLine("3. Bubbling: Родитель поймал сигнал вторым");

            await Task.Delay(2000);
            //RoutingParent.Background = Brushes.Transparent;
        }
    }
}
