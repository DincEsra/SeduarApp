namespace SeduarApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		// NavigationPage ile sarmalayarak yönlendirmeyi garantiye alıyoruz
        MainPage = new NavigationPage(new LoginPage());
	}
}