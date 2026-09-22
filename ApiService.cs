using System.Text.Json;

namespace SeduarApp;

public class ApiService
{
    // iOS Simülatörü, Mac'in yerel sunucusuna 127.0.0.1 üzerinden erişebilir
    private const string BaseUrl = "http://127.0.0.1:8000";
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        try
        {
            // Python sunucusuna GET isteği atıyoruz
            var response = await _httpClient.GetAsync($"{BaseUrl}/products/");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                
                // Gelen JSON metnini C# listesine çeviriyoruz (büyük/küçük harf duyarlılığını kapatarak)
                var products = JsonSerializer.Deserialize<List<Product>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                return products ?? new List<Product>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Sunucuya bağlanılamadı: {ex.Message}");
        }
        
        return new List<Product>(); // Hata olursa uygulamanın çökmemesi için boş liste dön
    }

    public async Task<bool> AddProductAsync(Product product)
{
    try
    {
        var json = JsonSerializer.Serialize(product);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync($"{BaseUrl}/products/", content);
        return response.IsSuccessStatusCode;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ürün eklenemedi: {ex.Message}");
        return false;
    }
}
}