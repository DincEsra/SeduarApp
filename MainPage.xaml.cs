using System.Collections.ObjectModel;

namespace SeduarApp;

public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService;
    
    // Arayüzdeki (XAML) CollectionView'ın bağlanacağı liste
    public ObservableCollection<Product> Products { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
        
        // Arayüze "Verileri bu sayfadan (this) alacaksın" diyoruz
        BindingContext = this;
    }

    // Sayfa ekranda göründüğü anda tetiklenen metot
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadProductsAsync();
    }

    private async Task LoadProductsAsync()
    {
        // ApiService üzerinden Python backend'ine istek atıyoruz
        var currentProducts = await _apiService.GetProductsAsync();
        
        Products.Clear();
        foreach (var product in currentProducts)
        {
            Products.Add(product);
        }
    }

    private async void OnGoToAddProductClicked(object sender, EventArgs e)
{
    await Navigation.PushAsync(new AddProductPage());
}
}