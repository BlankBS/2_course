using lab_4_5.Models;
using lab_4_5.ViewModels;
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
    }
}
