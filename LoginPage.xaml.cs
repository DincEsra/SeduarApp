namespace SeduarApp;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }
private async void OnLoginClicked(object sender, EventArgs e)
{
    string username = UsernameEntry.Text;
    string password = PasswordEntry.Text;

    if (username == "admin" && password == "admin123")
    {
        await DisplayAlert("Başarılı", "Yönetici paneline hoş geldiniz.", "Tamam");
        // Doğrudan Yönetim Paneline yönlendir
        await Navigation.PushAsync(new AdminDashboardPage());
    }
    else
    {
        await DisplayAlert("Hata", "Kullanıcı adı veya şifre hatalı!", "Tamam");
    }
}

    private async void OnContinueAsCustomerClicked(object sender, EventArgs e)
    {
        // Müşteri olarak normal ana sayfaya yönlendir
        await Navigation.PushAsync(new MainPage());
    }
}