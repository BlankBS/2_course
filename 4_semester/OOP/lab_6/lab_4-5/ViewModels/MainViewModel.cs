using lab_4_5.Models;
using lab_4_5.ViewModels;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace lab_4_5.ViewModels
{
    public class MainViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        private ObservableCollection<Skin> _allSkins;
        public ObservableCollection<Skin> FilteredSkins { get; set; } = new ObservableCollection<Skin>();
        public List<string> Categories { get; set; } = new List<string> { "Cat_All", "Cat_Weapon", "Cat_Clothing", "Cat_Vehicle" };

        private string _selectedCategory = "Cat_All";
        public string SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; ApplyFilters(); OnPropertyChanged(); }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                ApplyFilters();
            }
        }
        private decimal _maxPrice = 1000;
        public decimal MaxPrice
        {
            get => _maxPrice;
            set
            {
                _maxPrice = value;
                OnPropertyChanged();
                ApplyFilters();

                if (_maxPrice == 1000)
                {
                    LogAction("Пользователь установил фильтр на максимум (1000$)");
                }
            }
        }

        public ObservableCollection<Skin> CartItems { get; set; } = new ObservableCollection<Skin>();
        private decimal _cartTotal;
        public decimal CartTotal
        {
            get => _cartTotal;
            set
            {
                _cartTotal = value;
                OnPropertyChanged();
            }
        }

        private Stack<string> _undoStack = new Stack<string>();
        private Stack<string> _redoStack = new Stack<string>();
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand BuyCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand AddToCartCommand { get; }
        public ICommand RemoveFromCartCommand { get; }
        public ICommand CheckoutCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        public MainViewModel()
        {
            LoadData();
            ApplyFilters();

            AddCommand = new RelayCommand(obj =>
            {
                var addWin = new Views.AddSkinWindow();

                if (addWin.ShowDialog() == true)
                {
                    SaveState();
                    _allSkins.Add(addWin.NewSkin);
                    SaveData();
                    ApplyFilters();
                }

            }, (obj) => IsAdmin);

            DeleteCommand = new RelayCommand(obj =>
            {
                if (obj is Skin s)
                {
                    SaveState();
                    _allSkins.Remove(s);
                    SaveData();
                    ApplyFilters();
                }
            });

            BuyCommand = new RelayCommand(obj =>
            {
                if (obj is Skin skin)
                {
                    BuySkin(skin);
                }
            });

            EditCommand = new RelayCommand(obj =>
            {
                if (obj is Skin skinToEdit)
                {
                    var editWin = new Views.AddSkinWindow();

                    editWin.LoadSkinData(skinToEdit);

                    if (editWin.ShowDialog() == true)
                    {
                        skinToEdit.ShortName = editWin.NewSkin.ShortName;
                        skinToEdit.Price = editWin.NewSkin.Price;
                        skinToEdit.Quantity = editWin.NewSkin.Quantity;
                        skinToEdit.Category = editWin.NewSkin.Category;
                        skinToEdit.Rarity = editWin.NewSkin.Rarity;
                        skinToEdit.ImagePath = editWin.NewSkin.ImagePath;

                        SaveData();
                        ApplyFilters();
                    }
                }
            });

            AddToCartCommand = new RelayCommand(obj =>
            {
                if (obj is Skin skin)
                {
                    int countInCart = CartItems.Count(x => x.Id == skin.Id);
                    if (countInCart < skin.Quantity)
                    {
                        CartItems.Add(skin);
                        UpdateCartTotal();
                        CommandManager.InvalidateRequerySuggested();

                        if (skin.Quantity - countInCart == 1)
                        {
                            MessageBox.Show($"Внимание! Это последний экземпляр {skin.ShortName} на складе!",
                                            "Оповещение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }

                }
            });

            RemoveFromCartCommand = new RelayCommand(obj =>
            {
                if (obj is Skin s)
                {
                    CartItems.Remove(s);
                    UpdateCartTotal();
                }
            });

            CheckoutCommand = new RelayCommand(obj =>
            {
                if (CartItems.Count == 0) return;

                var groupedItems = CartItems.GroupBy(x => x.Id);

                foreach (var group in groupedItems)
                {
                    var skinInStock = _allSkins.FirstOrDefault(s => s.Id == group.Key);
                    if (skinInStock != null)
                    {
                        int amountToBuy = group.Count();

                        if (skinInStock.Quantity >= amountToBuy)
                        {
                            skinInStock.Quantity -= amountToBuy;
                            skinInStock.SoldCount += amountToBuy;
                        }
                    }
                }

                CartItems.Clear();
                UpdateCartTotal();
                SaveData();
                ApplyFilters();
                MessageBox.Show("Покупка успешно оформлена!", "Market");
            }, (obj) => CartItems.Count > 0);

            UndoCommand = new RelayCommand(obj => Undo(), obj => _undoStack.Count > 0);
            RedoCommand = new RelayCommand(obj => Redo(), obj => _redoStack.Count > 0);
        }

        private void LogAction(string message)
        {
            try
            {
                string logPath = "actions_log.txt";
                string logEntry = $"[{DateTime.Now}] {message}{Environment.NewLine}";
                File.AppendAllText(logPath, logEntry);
            }
            catch { }
        }

        private void SaveState()
        {
            string snapshot = JsonConvert.SerializeObject(_allSkins);
            _undoStack.Push(snapshot);
            _redoStack.Clear();

            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
        }

        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                _redoStack.Push(JsonConvert.SerializeObject(_allSkins));

                string lastState = _undoStack.Pop();
                var items = JsonConvert.DeserializeObject<List<Skin>>(lastState);

                _allSkins.Clear();
                foreach (var item in items) _allSkins.Add(item);

                SaveData();
                ApplyFilters();
            }
        }

        private void Redo()
        {
            if (_redoStack.Count > 0)
            {
                _undoStack.Push(JsonConvert.SerializeObject(_allSkins));

                string nextState = _redoStack.Pop();
                var items = JsonConvert.DeserializeObject<List<Skin>>(nextState);

                _allSkins.Clear();
                foreach (var item in items) _allSkins.Add(item);

                SaveData();
                ApplyFilters();
            }
        }

        private void UpdateCartTotal()
        {
            CartTotal = CartItems.Sum(x => x.Price);
        }

        private void BuySkin(Skin skin)
        {
            if (skin.Quantity > 0)
            {
                skin.Quantity--;
                skin.SoldCount++;

                SaveData();
                ApplyFilters();

                MessageBox.Show($"Поздравляем! Вы купили {skin.ShortName}.\nОсталось в наличии: {skin.Quantity}",
                        "Успешная покупка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("К сожалению, этого скина нет в наличии!",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void AddNewSkin(Skin newSkin)
        {
            if (newSkin == null)
            {
                return;
            }

            SaveState();
            _allSkins.Add(newSkin);
            SaveData();
            ApplyFilters();
        }

        public void ApplyFilters()
        {
            var filtered = _allSkins.Where(s =>
                           (string.IsNullOrEmpty(SearchText) || s.ShortName.ToLower().Contains(SearchText.ToLower())) &&
                           (s.Price <= MaxPrice) &&
                           (SelectedCategory == "Cat_All" || s.Category == GetCategoryName(SelectedCategory)))
                           .ToList();

            FilteredSkins.Clear();
            foreach (var item in filtered) FilteredSkins.Add(item);
        }

        private string GetCategoryName(string key)
        {
            switch (key)
            {
                case "Cat_Weapon": return "Оружие";
                case "Cat_Clothing": return "Одежда";
                case "Cat_Vehicle": return "Транспорт";
                default: return "Все";
            }
        }

        private void LoadData()
        {
            if (File.Exists("data.json"))
                _allSkins = new ObservableCollection<Skin>(JsonConvert.DeserializeObject<List<Skin>>(File.ReadAllText("data.json")));
            else
                _allSkins = new ObservableCollection<Skin>();
        }
        private void SaveData()
        {
            string json = JsonConvert.SerializeObject(_allSkins, Formatting.Indented);
            File.WriteAllText("data.json", json);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}