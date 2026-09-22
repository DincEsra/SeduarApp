using System.Collections.ObjectModel;

namespace SeduarApp
{
    public class HomeViewModel
    {
        private readonly ApiService _apiService;

        // Arayüzde göstereceğimiz dinamik ürün listesi
        public ObservableCollection<Product> Products { get; set; } = new();

        public HomeViewModel()
        {
            _apiService = new ApiService();
            _ = LoadProductsAsync();
        }

        public async Task LoadProductsAsync()
        {
            var serverProducts = await _apiService.GetProductsAsync();

            Products.Clear();
            foreach (var product in serverProducts)
            {
                Products.Add(product);
            }
        }
    }
}