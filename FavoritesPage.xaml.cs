using System.Collections.ObjectModel;

namespace SeduarApp;

public partial class FavoritesPage : ContentPage
{
    public FavoritesPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public ObservableCollection<Product> FavoriteItems => MainPage.FavoriteItems;

    protected override void OnAppearing()
    {
        base.OnAppearing();
        FavoritesCollectionView.ItemsSource = null;
        FavoritesCollectionView.ItemsSource = MainPage.FavoriteItems;
    }

   private async void OnAddToCartFromFavoritesClicked(object sender, EventArgs e)
{
    if (sender is Button button && button.CommandParameter is Product product)
    {
        // 1. Sepete Ekle veya Adedini Artır
        var existingItem = MainPage.CartItems.FirstOrDefault(p => p.Name == product.Name);
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            product.Quantity = 1;
            MainPage.CartItems.Add(product);
        }

        // 2. Favorilerden Kaldır
        MainPage.FavoriteItems.Remove(product);
        FavoritesCollectionView.ItemsSource = null;
        FavoritesCollectionView.ItemsSource = MainPage.FavoriteItems;

        await DisplayAlert("Başarılı", $"{product.Name} sepete eklendi ve favorilerinizden kaldırıldı!", "Tamam");
    }
}

    private void OnRemoveFavoriteClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Product product)
        {
            MainPage.FavoriteItems.Remove(product);
            FavoritesCollectionView.ItemsSource = null;
            FavoritesCollectionView.ItemsSource = MainPage.FavoriteItems;
        }
    }
}