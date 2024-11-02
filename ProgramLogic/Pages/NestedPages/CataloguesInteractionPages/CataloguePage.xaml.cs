using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.Pages.NestedPages;
using System.Collections.ObjectModel;

namespace Listifyr.ProgramLogic.Pages.NestedPages.CataloguesInteractionPages;

public partial class CataloguePage : ContentPage
{
    private ObservableCollection<Items>? mediaItems;
    public ObservableCollection<Items>? MediaItems
    {
        get => mediaItems;
        set
        {
            mediaItems = value;
            OnPropertyChanged("MediaItems");
        }
    }
    private readonly string? _pageTitle;
    private int? catalogueID;

    public CataloguePage() => InitializeComponent();

    public CataloguePage(string title)
    {
        InitializeComponent();
        _pageTitle = title;
    }

    protected async override void OnAppearing()
    {
        Title = _pageTitle;

        try
        {
            catalogueID = await App.Database.GetIDByNameAsync<Catalogues>("CatalogueID", "Catalogues", "Name", _pageTitle);
            var items = await App.Database.LoadTableByIDAsync<Items>((int)catalogueID, "ID_Catalogue");
            var collectionView = this.FindByName<CollectionView>("ItemsCollectionView");
            collectionView.ItemsSource = items;

            if (items.Count == 0)
                LabelForEmptyCatalogue.Text = $"Add your first {_pageTitle}'s media from categories!";
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
}