namespace Listifyr.View;

public partial class ListsPage : ContentPage
{
    public ListsPage()
	{
		InitializeComponent();
        OnAppearing();
    }
    protected async override void OnAppearing()
    {
        try
        {
            var catalogues = (CataloguesViewModel)BindingContext;
            await catalogues.LoadCataloguesAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load catalogues: {ex.Message}", "OK");
        }
        base.OnAppearing();
    }

    private async void OnCatalogueTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Перемога", $"Натиснуто на каталог", "OK");
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        CataloguesViewModel cataloguesViewModel = new CataloguesViewModel();
        await cataloguesViewModel.AddCatalogue();
        OnAppearing();
    }
}