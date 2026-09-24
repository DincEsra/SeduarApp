namespace SeduarApp;

public partial class ProductDetailPage : ContentPage
{
    private Product _product;
    private string _selectedSize = null;

    public ProductDetailPage(Product product)
    {
        InitializeComponent();
        _product = product;
        BindingContext = _product;

        // Beden listesini CollectionView'a yüklüyoruz
        SizeCollectionView.ItemsSource = _product.AvailableSizes;
    }

    private void OnSizeSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is string selected)
        {
            _selectedSize = selected;
        }
    }

    private async void OnAddToCartClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_selectedSize))
        {
            await DisplayAlert("Uyarı", "Lütfen bir beden seçiniz.", "Tamam");
            return;
        }

        var existingItem = MainPage.CartItems.FirstOrDefault(p => p.Name == _product.Name && p.SelectedSize == _selectedSize);
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            var newItem = new Product
            {
                Name = _product.Name,
                Price = _product.Price,
                ImageUrl = _product.ImageUrl,
                Category = _product.Category,
                Description = _product.Description,
                Quantity = 1,
                SelectedSize = _selectedSize,
                AvailableSizes = _product.AvailableSizes
            };
            MainPage.CartItems.Add(newItem);
        }

        await DisplayAlert("Başarılı", $"{_product.Name} ({_selectedSize} Beden) sepete eklendi!", "Tamam");
        await Navigation.PopAsync();
    }

private async void OnAddReviewClicked(object sender, EventArgs e)
{
    // Kullanıcı bu ürünü daha önce satın aldı mı kontrol edelim
    bool hasPurchased = MainPage.PurchasedItems.Any(p => p.Name == _product.Name);

    if (!hasPurchased)
    {
        await DisplayAlert("İşlem Başarısız", "Bu ürüne yorum yapabilmek için ürünü satın almış olmanız gerekmektedir.", "Tamam");
        return;
    }

    if (!string.IsNullOrWhiteSpace(CommentEntry.Text))
    {
        _product.Reviews.Add(new Review
        {
            UserName = "Sen (Doğrulanmış Alıcı)",
            CommentText = CommentEntry.Text,
            Rating = 5,
            Date = DateTime.Now.ToShortDateString()
        });

        CommentEntry.Text = string.Empty;
        await DisplayAlert("Başarılı", "Yorumunuz eklendi, teşekkür ederiz!", "Tamam");
    }
    else
    {
        await DisplayAlert("Uyarı", "Lütfen boş bir yorum göndermeyin.", "Tamam");
    }
}
}
