using System;
using System.Collections.Generic;

namespace lab_4_5.Models
{
    public class Skin
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; } 
        public string Category { get; set; }  
        public double Rating { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Rarity { get; set; }   
        public double Discount { get; set; }
        public string Production { get; set; }
        public int SoldCount { get; set; }
        public bool IsAvailable => Quantity > 0;
    }
}