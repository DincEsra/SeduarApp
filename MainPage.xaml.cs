using System.Collections.ObjectModel;

namespace SeduarApp;

public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService;
    private int _cartCount = 0;

    public static ObservableCollection<Product> CartItems { get; set; } = new();
    public ObservableCollection<Product> Products { get; set; } = new();

    public static ObservableCollection<Order> OrderHistory { get; set; } = new();

    public static ObservableCollection<Product> FavoriteItems { get; set; } = new();

    public static ObservableCollection<Product> PurchasedItems { get; set; } = new();

    // Tüm ürünlerin ana listesi
    public static ObservableCollection<Product> AllProducts { get; set; } = new();
    
    // Ekranda gösterilen filtrelenmiş liste
    public ObservableCollection<Product> FilteredProducts { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
        // Örnek ürünleri buraya yüklediğini varsayıyoruz
        LoadInitialProducts();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadProductsAsync();
    }

    private async Task LoadProductsAsync()
    {
        var currentProducts = await _apiService.GetProductsAsync();
        
        Products.Clear();
        if (currentProducts != null)
        {
            foreach (var product in currentProducts)
            {
                Products.Add(product);
            }
        }
    }

private async void OnAddToCartClicked(object sender, EventArgs e)
{
    if (sender is Button button && button.CommandParameter is Product product)
    {
        // Sepette bu ürün daha önce var mı kontrol edelim (İsme göre)
        var existingItem = CartItems.FirstOrDefault(p => p.Name == product.Name);
        if (existingItem != null)
        {
            existingItem.Quantity++; // Varsa adedini artır
        }
        else
        {
            product.Quantity = 1;
            CartItems.Add(product); // Yoksa yeni ekle
        }

        string action = await DisplayActionSheet(
            $"{product.Name} sepete eklendi!", 
            null, 
            null, 
            "Sepete Git", 
            "Alışverişe Devam Et");

        if (action == "Sepete Git")
        {
            await Navigation.PushAsync(new CartPage());
        }
    }
}

    private async void OnCartIconClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CartPage());
    }

    private async void OnGoToAddProductClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddProductPage());
    }

private async void OnFavoriteClicked(object sender, EventArgs e)
{
    if (sender is Button button && button.CommandParameter is Product product)
    {
        if (!MainPage.FavoriteItems.Contains(product))
        {
            MainPage.FavoriteItems.Add(product);
            button.Text = "❤️"; 
            await DisplayAlert("Favoriler", $"{product.Name} favorilerinize eklendi!", "Tamam");
        }
        else
        {
            MainPage.FavoriteItems.Remove(product);
            button.Text = "♡"; 
            await DisplayAlert("Favoriler", $"{product.Name} favorilerinizden çıkarıldı!", "Tamam");
        }
    }
}
private async void OnProductTapped(object sender, TappedEventArgs e)
{
    if (e.Parameter is Product product)
    {
        await Navigation.PushAsync(new ProductDetailPage(product));
    }
}

private void LoadInitialProducts()
    {
        
        if (AllProducts.Count == 0)
        {
            AllProducts.Add(new Product { Name = "Siyah Deri Çanta", Price = 1250, Category = "Çanta", ImageUrl = "bag.jpg" });
            AllProducts.Add(new Product { Name = "Klasik Topuklu", Price = 1890, Category = "Ayakkabı", ImageUrl = "shoe.jpg" });
            
        }

        // Başlangıçta tüm ürünleri filtrelenmiş listeye yansıtıyoruz
        FilteredProducts.Clear();
        foreach (var p in AllProducts) FilteredProducts.Add(p);
    }

    // Arama Çubuğunda Yazı Değiştikçe Çalışır
    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        string keyword = e.NewTextValue?.ToLower() ?? string.Empty;

        var results = string.IsNullOrWhiteSpace(keyword) 
            ? AllProducts 
            : new ObservableCollection<Product>(AllProducts.Where(p => p.Name.ToLower().Contains(keyword)));

        FilteredProducts.Clear();
        foreach (var p in results) FilteredProducts.Add(p);
    }

    // Kategori Butonlarına Tıklandığında Çalışır
   private void OnCategoryFilterClicked(object sender, EventArgs e)
{
    if (sender is Button clickedButton && clickedButton.CommandParameter is string category)
    {
        // 1. Layout içindeki TÜM kategori butonlarını gri (pasif) yap
        foreach (var child in CategoryLayout.Children)
        {
            if (child is Button btn)
            {
                btn.BackgroundColor = Color.FromArgb("#e0e0e0");
                btn.TextColor = Color.FromArgb("#333333");
            }
        }

        // 2. Sadece tıklanan butonu siyah (aktif) yap
        clickedButton.BackgroundColor = Color.FromArgb("#2b2b2b");
        clickedButton.TextColor = Colors.White;

        // 3. Ürünleri filtrele
        FilteredProducts.Clear();

        var results = (category == "Tümü") 
            ? AllProducts 
            : AllProducts.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

        foreach (var p in results) 
        {
            FilteredProducts.Add(p);
        }
    }
}

}