using System.Text.Json.Serialization;
using System.Collections.ObjectModel;

namespace SeduarApp;

public class Product
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; } = "Bu ürün için henüz detaylı açıklama girilmedi. Yüksek kaliteli malzemeden üretilmiştir.";
    
    public decimal Price { get; set; }
    
    // Python'daki "image_url" verisini C#'taki ImageUrl özelliğine bağlar
    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; }
    
    public string Category { get; set; }
    
    // Python'daki "is_new_arrival" verisini C#'taki IsNewArrival özelliğine bağlar
    [JsonPropertyName("is_new_arrival")]
    public bool IsNewArrival { get; set; }

     public int Quantity { get;
     set; } = 1;


    
    public string SelectedSize { get; set; } = null;
    
    public List<string> AvailableSizes { get; set; } = new() { "Xs","S", "M", "L", "XL", "XXL", "3XL", "4XL", "70A", "75B", "75C", "75D", "80A", "80B","80C","80D", "85A", "85B", "85C","85D", "90A", "90B", "90C", "90D", "95A", "95B", "95C", "95D", "100A", "100B", "100C", "100D" }; 

public ObservableCollection<Review> Reviews { get; set; } = new()
{
    new Review { UserName = "Zeynep K.", CommentText = "Ürün bayıldııım, kalitesi harika!", Rating = 5, Date = "22.09.2026" },
    new Review { UserName = "Ahmet Y.", CommentText = "Kumaşı beklediğimden biraz ince ama yine de güzel.", Rating = 4, Date = "23.09.2026" }
};
}