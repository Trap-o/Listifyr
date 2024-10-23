using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.Pages.NestedPages;
using System.Collections.ObjectModel;

namespace Listifyr.View;
public partial class CategoryPage : ContentPage
{
    private ObservableCollection<Items> mediaItems;
    public ObservableCollection<Items> MediaItems
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

    private string? _pageTitle;
    public CategoryPage() => InitializeComponent();
    public CategoryPage(string title)
    {
        InitializeComponent();
        _pageTitle = title;
    }
    protected async override void OnAppearing()
    {
        this.Title = _pageTitle;

        try
        {
            var categoryID = await App.Database.GetIDByNameAsync<Categories>("Categories", "Name", _pageTitle);

            var items = await App.Database.LoadTableByIDAsync<Items>((int)categoryID);
            var collectionView = this.FindByName<CollectionView>("ItemsCollectionView");
            collectionView.ItemsSource = items;

            if (items.Count == 0)
                this.LabelForEmptyCategory.Text = $"Add your first {ChangeLabelText()} by pressing the button!";
            else
                this.LabelForEmptyCategory.Text = "";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load catalogues: {ex.Message}", "OK");
        }

        base.OnAppearing();
    }

    public async Task LoadItemsAsync()
    {
        var items = await App.Database.GetAsync<Items>();
        MediaItems = new ObservableCollection<Items>(items);
    }
    //

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
}