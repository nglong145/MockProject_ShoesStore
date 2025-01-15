namespace ShoesStoreApp.BLL.ViewModels;

public class AddCartItem
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}