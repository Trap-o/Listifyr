using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.Pages.NestedPages;
using System.Collections.ObjectModel;

namespace Listifyr.View;
public partial class CategoryPage : ContentPage
{
    private ObservableCollection<Items>? mediaItems;
    public ObservableCollection<Items>? MediaItems
    {
        get
        {
            return mediaItems;
        }
        set
        {
            mediaItems = value;
            OnPropertyChanged("MediaItems");
        }
    }

    private readonly string? _pageTitle;
    public CategoryPage() => InitializeComponent();
    public CategoryPage(string title)
    {
        InitializeComponent();
        _pageTitle = title;
    }
    protected async override void OnAppearing()
    {
        Title = _pageTitle;

        try
        {
            var categoryID = await App.Database.GetIDByNameAsync<Categories>("CategoryID", "Categories", "Name", _pageTitle);
            var items = await App.Database.LoadTableByIDAsync<Items>((int)categoryID, "ID_Category");
            var collectionView = this.FindByName<CollectionView>("ItemsCollectionView");
            collectionView.ItemsSource = items;

            if (items.Count == 0)
                LabelForEmptyCategory.Text = $"Add your first {ChangeLabelText()} by pressing the button!";
            else
                LabelForEmptyCategory.Text = "";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load categories: {ex.Message}", "OK");
        }

        base.OnAppearing();
    }

    private string ChangeLabelText()
    {
        string emptyElementName = "";
        switch (Title)
        {
            case "Movies":
                emptyElementName = "movie";
                break;

            case "Series":
                emptyElementName = "series";
                break;

            case "Manga":
                emptyElementName = "manga";
                break;

            case "Comics":
                emptyElementName = "comics";
                break;

            case "Books":
                emptyElementName = "book";
                break;

            case "Ranobe":
                emptyElementName = "ranobe";
                break;

            case "Games":
                emptyElementName = "game";
                break;

            case "Anime":
                emptyElementName = "anime";
                break;
        }
        return emptyElementName;
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        string data = Title;
        await Navigation.PushAsync(new SearchPage(data));
    }

    private async void OnCategory_Tapped(object sender, TappedEventArgs e)
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