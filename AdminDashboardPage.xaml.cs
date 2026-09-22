namespace SeduarApp;

public partial class AdminDashboardPage : ContentPage
{
    public AdminDashboardPage()
    {
        InitializeComponent();
    }

    private async void OnGoToAddProductClicked(object sender, EventArgs e)
    {
        // Daha önce yazdığımız ürün ekleme sayfasına yönlendir
        await Navigation.PushAsync(new AddProductPage());
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Giriş ekranına (LoginPage) geri dön ve geçmişi temizle
        await Navigation.PopToRootAsync();
    }
}