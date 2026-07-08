using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab_4_5.Models
{
    [Table("Skins")]
    public class Skin : INotifyPropertyChanged
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string ShortName { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [NotMapped]
        public string FullName { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [NotMapped]
        public string ImagePath { get; set; }
        [NotMapped]
        public double Rating { get; set; }
        public decimal Price { get; set; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        public string Rarity { get; set; }    
        [NotMapped]
        public double Discount { get; set; }
        [NotMapped]
        public string Production { get; set; }
        public int SoldCount { get; set; }
        [NotMapped]
        public bool IsAvailable => Quantity > 0;
        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get => _imageBytes;
            set { _imageBytes = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}