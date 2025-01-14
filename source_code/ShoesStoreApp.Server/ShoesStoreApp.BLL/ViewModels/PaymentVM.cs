namespace ShoesStoreApp.BLL.ViewModels;

public class PaymentVM
{
    public Guid PaymentId { get; set; }
    public string PaymentMethod { get; set; }
    public string Description { get; set; }
}