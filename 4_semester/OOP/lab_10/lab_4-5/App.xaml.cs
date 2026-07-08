using lab_4_5.UnitOfWork;
using lab_4_5.ViewModels;
using System.Windows;

namespace lab_4_5
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Здесь происходит подключение паттернов:
            // 1. Создаём фабрику — она знает, как создавать UnitOfWork
            IUnitOfWorkFactory factory = new UnitOfWorkFactory();

            // 2. Передаём фабрику во ViewModel — он получает зависимость через конструктор
            var viewModel = new MainViewModel(factory);

            // 3. Передаём ViewModel в окно — оно ставит его как DataContext
            var window = new MainWindow(viewModel);
            window.Show();
        }
    }
}
