using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab_4_5.Models
{
    public class Skin : INotifyPropertyChanged
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; } 
        public string Category { get; set; }  
        public double Rating { get; set; }
        public decimal Price { get; set; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        public string Rarity { get; set; }    
        public double Discount { get; set; }
        public string Production { get; set; } 
        public int SoldCount { get; set; }
        public bool IsAvailable => Quantity > 0;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}