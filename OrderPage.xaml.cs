using System.Collections.ObjectModel;

namespace SeduarApp;

public partial class OrdersPage : ContentPage
{
    public OrdersPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public ObservableCollection<Order> OrderHistory => MainPage.OrderHistory;

    protected override void OnAppearing()
    {
        base.OnAppearing();
        OrdersCollectionView.ItemsSource = null;
        OrdersCollectionView.ItemsSource = MainPage.OrderHistory;
    }
}