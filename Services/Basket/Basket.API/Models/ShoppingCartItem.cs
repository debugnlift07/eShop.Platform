namespace Basket.API.Models
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; } = default!;
        public int Color { get; set; } = default!;
        public int Price { get; set; } = default!;
        public int ProductId { get; set; } = default!;
        public int ProductName { get; set; } = default!;
    }
}
