namespace SeduarApp;

public partial class CheckoutPage : ContentPage
{
    public CheckoutPage()
    {
        InitializeComponent();
    }

    private async void OnCompleteOrderClicked(object sender, EventArgs e)
    {
        // 1. Alanların doluluk kontrolü
        if (string.IsNullOrWhiteSpace(NameEntry.Text) || 
            string.IsNullOrWhiteSpace(PhoneEntry.Text) || 
            string.IsNullOrWhiteSpace(AddressEditor.Text) || 
            string.IsNullOrWhiteSpace(CardNumberEntry.Text) ||
            string.IsNullOrWhiteSpace(ExpiryEntry.Text) ||
            string.IsNullOrWhiteSpace(CvvEntry.Text))
        {
            await DisplayAlert("Eksik Bilgi", "Lütfen tüm adres ve ödeme alanlarını doldurun.", "Tamam");
            return;
        }

        // 2. Ödeme Onay SMS Simülasyonu
        string smsCode = await DisplayPromptAsync(
            "SMS Doğrulaması", 
            $"{PhoneEntry.Text} numaralı telefonunuza gönderilen 4 haneli onay kodunu giriniz:\n(Test kodu: 1234)", 
            "Onayla", 
            "İptal", 
            placeholder: "Örn: 1234", 
            maxLength: 4, 
            keyboard: Keyboard.Numeric);

        // Kullanıcı iptal ederse
        if (smsCode == null) return;

       if (smsCode == "1234")
{
    // Yeni sipariş nesnesi oluşturuyoruz
    var newOrder = new Order
    {
        FullName = NameEntry.Text,
        Phone = PhoneEntry.Text,
        Address = AddressEditor.Text,
        TotalAmount = MainPage.CartItems.Sum(x => x.Price * x.Quantity),
        PurchasedItems = new List<Product>(MainPage.CartItems)
    };

    // Küresel listeye ekliyoruz
    MainPage.OrderHistory.Add(newOrder);

    // Sepeti temizliyoruz
    MainPage.CartItems.Clear();

   await DisplayAlert("Tebrikler!", "Ödemeniz onaylandı ve siparişiniz alındı! Siparişlerim sayfasına yönlendiriliyorsunuz.", "Tamam");

    // 1. Önce uygulamayı ana kök durumuna (AppShell) getiriyoruz
    Application.Current.MainPage = new AppShell();

    // 2. Ardından alt menüdeki "Siparişlerim" sekmesine (route adına göre) geçiş yapıyoruz
    // AppShell.xaml içinde OrdersPage rotasını tanımlamıştık
    await Shell.Current.GoToAsync("//OrdersPage");
}
    }
}