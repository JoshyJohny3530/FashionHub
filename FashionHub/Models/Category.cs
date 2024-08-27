﻿namespace FashionHub.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Item> Items { get; set; } 
    }
}