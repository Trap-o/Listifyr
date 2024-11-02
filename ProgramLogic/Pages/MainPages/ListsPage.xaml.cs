using Listifyr.ProgramLogic.Pages.NestedPages.CataloguesInteractionPages;

namespace Listifyr.View;

public partial class ListsPage : ContentPage
{
    public ListsPage()
	{
		InitializeComponent();
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
        var frame = sender as Frame;

        if (frame?.BindingContext is Catalogues selectedCatalogue)
        {
            try
            {
                var data = selectedCatalogue.Name;
                await Navigation.PushAsync(new CataloguePage(data));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error description: {ex}", "OK");
            }
        }
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        CataloguesViewModel cataloguesViewModel = new();
        await cataloguesViewModel.AddCatalogue();
        OnAppearing();
    }
}