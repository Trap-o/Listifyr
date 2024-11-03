using Listifyr.ItemTypes;

namespace Listifyr.ProgramLogic.Pages.NestedPages.CataloguesInteractionPages;

public partial class CataloguePage : ContentPage
{
    private int? catalogueID;
    private Catalogues? Catalogues { get; set; }

    public CataloguePage() => InitializeComponent();

    public CataloguePage(Catalogues catalogue)
    {
        InitializeComponent();
        Catalogues = catalogue;
        BindingContext = Catalogues;

    }

    protected async override void OnAppearing()
    {
        Title = Catalogues?.Name;
        try
        {
            catalogueID = await App.Database.GetIDByNameAsync<Catalogues>("CatalogueID", "Catalogues", "Name", Catalogues?.Name);
            var items = await App.Database.LoadTableByIDAsync<Items>((int)catalogueID, "ID_Catalogue");
            var collectionView = this.FindByName<CollectionView>("ItemsCollectionView");
            collectionView.ItemsSource = items;

            if (items.Count == 0)
                LabelForEmptyCatalogue.Text = $"Add your first {Catalogues?.Name}'s media from categories!";
            else
                LabelForEmptyCatalogue.Text = "";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load catalogues: {ex.Message}", "OK");
        }

        base.OnAppearing();
    }

    private async void OnCatalogue_Tapped(object sender, TappedEventArgs e)
    {
        var frame = sender as Frame;

        if (frame?.BindingContext is Items selectedItem)
        {
            try
            {
                await Navigation.PushAsync(new CategoryItemPage(selectedItem));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error description: {ex}", "OK");
            }
        }
    }

    private async void RenameButton_Clicked(object sender, EventArgs e)
    {
        string newCatalogueName = await Shell.Current.DisplayPromptAsync("New name", "Enter new catalogue name (max. 15 characters):", maxLength: 15);
        if (!string.IsNullOrWhiteSpace(newCatalogueName))
        {
            try
            {
                Catalogues.Name = newCatalogueName;
                await App.Database.UpdateItemAsync("Name", "Catalogues", "CatalogueID", newCatalogueName, Catalogues.CatalogueID);

            }
            catch(Exception)
            {
                await Shell.Current.DisplayAlert("Error!", "Enter unique name for catalogue!", "OK");
            }
            OnAppearing();
        }
    }
}