namespace SeduarApp;

public class Order
{
    public string OrderId { get; set; } = "ORD-" + new Random().Next(1000, 9999);
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public decimal TotalAmount { get; set; }
    public List<Product> PurchasedItems { get; set; } = new();
}