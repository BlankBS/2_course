using lab_4_5.Models;
using lab_4_5.ViewModels;   
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        public MainViewModel()
        {
            LoadData();
            ApplyFilters();

            AddCommand = new RelayCommand(obj => {
                var addWin = new Views.AddSkinWindow();

                if(addWin.ShowDialog() == true)
                {
                    _allSkins.Add(addWin.NewSkin);
                    SaveData();
                    ApplyFilters();
                }

            });

            DeleteCommand = new RelayCommand(obj => {
                if (obj is Skin s)
                {
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

                    if(editWin.ShowDialog() == true)
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
                if (obj is Skin skin && skin.Quantity > 0)
                {
                    int countInCart = CartItems.Count(x => x.Id == skin.Id);
                    if (countInCart < skin.Quantity)
                    {
                        CartItems.Add(skin);
                        UpdateCartTotal();
                    }
                    else
                    {
                        MessageBox.Show($"Извини, на складе всего {skin.Quantity} шт. этого товара. Ты уже добавил их все в корзину!",
                            "Лимит достигнут", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            });

            RemoveFromCartCommand = new RelayCommand(obj => {
                if (obj is Skin s)
                {
                    CartItems.Remove(s);
                    UpdateCartTotal();
                }
            });

            CheckoutCommand = new RelayCommand(obj => {
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
        }

        private void SaveSnapshot()
        {
            _undoStack.Push(JsonConvert.SerializeObject(_allSkins));
        }
        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                _redoStack.Push(JsonConvert.SerializeObject(_allSkins));
                var previous = JsonConvert.DeserializeObject<ObservableCollection<Skin>>(_undoStack.Pop());
                _allSkins.Clear();
                foreach (var s in previous) _allSkins.Add(s);
                ApplyFilters();
            }
        }

        private void UpdateCartTotal()
        {
            CartTotal = CartItems.Sum(x => x.Price);
        }

        private void BuySkin(Skin skin)
        {
            if(skin.Quantity > 0)
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
            if(newSkin == null)
            {
                return;
            }

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