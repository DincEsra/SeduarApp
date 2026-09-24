namespace SeduarApp;

public partial class CartPage : ContentPage
{
    public CartPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshCart();
    }

    private void CalculateTotalPrice()
    {
        decimal total = 0;
        foreach (var item in MainPage.CartItems)
        {
            total += item.Price * item.Quantity;
        }
        TotalPriceLabel.Text = $"₺{total:N2}";
    }

    private void OnIncreaseTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Product product)
        {
            product.Quantity++;
            RefreshCart();
        }
    }

    private void OnDecreaseTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Product product)
        {
            if (product.Quantity > 1)
            {
                product.Quantity--;
            }
            else
            {
                MainPage.CartItems.Remove(product);
            }
            RefreshCart();
        }
    }

    private void OnRemoveTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Product product)
        {
            MainPage.CartItems.Remove(product);
            RefreshCart();
        }
    }

    // Ürünü sepetten çıkarıp favorilere ekleme
    private async void OnMoveToFavoritesTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Product product)
        {
            if (!MainPage.FavoriteItems.Contains(product))
            {
                MainPage.FavoriteItems.Add(product);
            }
            MainPage.CartItems.Remove(product);
            RefreshCart();
            await DisplayAlert("Başarılı", $"{product.Name} favorilerinize taşındı.", "Tamam");
        }
    }

    // Üst sekmeden "FAVORİLER" yazısına tıklandığında Favoriler sayfasına geçiş
    private async void OnTabFavoritesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FavoritesPage());
    }

    private void RefreshCart()
    {
        CartCollectionView.ItemsSource = null;
        CartCollectionView.ItemsSource = MainPage.CartItems;
        CalculateTotalPrice();

        if (MainPage.CartItems.Count > 0)
        {
            CheckoutButton.IsEnabled = true;
            CheckoutButton.BackgroundColor = Color.FromArgb("#00875a"); // Görseldeki yeşil renk
        }
        else
        {
            CheckoutButton.IsEnabled = false;
            CheckoutButton.BackgroundColor = Color.FromArgb("#cccccc");
        }
    }

    private async void OnCheckoutClicked(object sender, EventArgs e)
    {

        // 1. Önceki sepet ürünlerini satın alınanlara ekleyelim:
    foreach (var item in MainPage.CartItems)
    {
        // Daha önce eklenmediyse ekleyelim
        if (!MainPage.PurchasedItems.Any(p => p.Name == item.Name && p.SelectedSize == item.SelectedSize))
        {
            MainPage.PurchasedItems.Add(item);
        }
    }
        await Navigation.PushAsync(new CheckoutPage());
    }
}
