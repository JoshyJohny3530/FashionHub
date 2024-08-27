namespace FashionHub.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ItemImage { get; set; } 
        public int CategoryId { get; set; }
        public Category? Category { get; set; } 
    }
}
