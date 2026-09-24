namespace SeduarApp;

public partial class AddProductPage : ContentPage
{
   
    private readonly ApiService _apiService;


    public AddProductPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(PriceEntry.Text))
        {
            await DisplayAlert("Hata", "Lütfen ürün adı ve fiyatını boş bırakmayın.", "Tamam");
            return;
        }

        if (!decimal.TryParse(PriceEntry.Text, out decimal price))
        {
            await DisplayAlert("Hata", "Geçerli bir fiyat giriniz.", "Tamam");
            return;
        }

        var newProduct = new Product
        {
            Name = NameEntry.Text,
            Description = DescEditor.Text ?? "",
            Price = price,
            ImageUrl = ImageEntry.Text ?? "https://ornekresim.com/default.jpg",
            Category = CategoryEntry.Text ?? "GENEL",
            IsNewArrival = IsNewCheckBox.IsChecked
        };

        bool success = await _apiService.AddProductAsync(newProduct);

        if (success)
        {
            await DisplayAlert("Başarılı", "Ürün başarıyla veritabanına eklendi!", "Tamam");
            await Navigation.PopAsync(); 
        }
        else
        {
            await DisplayAlert("Hata", "Sunucuya bağlanılamadı, ürün eklenemedi.", "Tamam");
        }
    }
}