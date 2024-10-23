using Listifyr.ProgramLogic.APIs;

namespace Listifyr.ProgramLogic.Pages.NestedPages;

public partial class SearchPage : ContentPage
{
    private string searchEngine;
    private int? categoryId;

    public SearchPage() => InitializeComponent();

    public SearchPage(string data)
    {
        InitializeComponent();
        searchEngine = data;
    }
    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue.Length >= 3)
        {
            if (searchEngine == "Movies")
            {
                TMDB_service tMDB_Service = new TMDB_service();
                var movies = await tMDB_Service.SearchMoviesAsync(e.NewTextValue);
                ItemsListView.ItemsSource = movies;
            }
        }
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        categoryId = await App.Database.GetIDByNameAsync<Categories>("Categories", "Name", searchEngine);
        var selectedItem = e.SelectedItem as ItemTypes.Items;
        if (selectedItem != null)
        {
            await Navigation.PushAsync(new SelectedSearchItem(selectedItem, categoryId));
        }
    }
}