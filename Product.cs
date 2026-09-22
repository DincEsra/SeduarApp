using System.Text.Json.Serialization;

namespace SeduarApp;

public class Product
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    // Python'daki "image_url" verisini C#'taki ImageUrl özelliğine bağlar
    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; }
    
    public string Category { get; set; }
    
    // Python'daki "is_new_arrival" verisini C#'taki IsNewArrival özelliğine bağlar
    [JsonPropertyName("is_new_arrival")]
    public bool IsNewArrival { get; set; }
}