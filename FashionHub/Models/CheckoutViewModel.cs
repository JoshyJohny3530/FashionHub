using System.ComponentModel.DataAnnotations;

namespace FashionHub.ViewModels
{
    public class CheckoutViewModel
    {
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }

        // Shipping Information
        [Required(ErrorMessage = "Full Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid postal code format.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        // Payment Information
        [Required(ErrorMessage = "Card Number is required.")]
        [CreditCard(ErrorMessage = "Invalid card number.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Name on Card is required.")]
        public string CardName { get; set; }

        [Required(ErrorMessage = "Expiry Date is required.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Invalid expiry date format (MM/YY).")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVV is required.")]
        [Range(100, 999, ErrorMessage = "CVV must be a 3-digit number.")]
        public string CVV { get; set; }
    }

    public class OrderItem
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
