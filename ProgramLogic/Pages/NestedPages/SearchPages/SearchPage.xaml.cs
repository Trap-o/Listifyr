using Listifyr.ProgramLogic.APIs;
using Listifyr.ProgramLogic.APIs.AniList;
using Listifyr.ProgramLogic.APIs.ComicVine;
using Listifyr.ProgramLogic.APIs.GoogleBooks;
using Listifyr.ProgramLogic.APIs.TMDB;

namespace Listifyr.ProgramLogic.Pages.NestedPages;

public partial class SearchPage : ContentPage
{
    private readonly string searchEngine;
    private int? categoryId;

    public SearchPage() => InitializeComponent();

    public SearchPage(string data)
    {
        InitializeComponent();
        CheckInternetConnection();
        searchEngine = data;
    }

    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (InternetConnectionAvailable())
        {
            if (e.NewTextValue.Length >= 2)
            {
                if (searchEngine == "Movies")
                {
                    apiLabel.Text = "Powered by The Movie Database (TMDB) API";
                    ItemsListView.ItemsSource = null;
                    var movies = await TmdbMovies_service.SearchMoviesAsync(e.NewTextValue);
                    ItemsListView.ItemsSource = movies;
                }
                else if (searchEngine == "Series")
                {
                    apiLabel.Text = "Powered by The Movie Database (TMDB) API";
                    ItemsListView.ItemsSource = null;
                    var series = await TmdbSeries_service.SearchSeriesAsync(e.NewTextValue);
                    ItemsListView.ItemsSource = series;
                }
                else if (searchEngine == "Manga")
                {
                    apiLabel.Text = "Powered by AniList API";
                    ItemsListView.ItemsSource = null;
                    var manga = await AniListManga_service.SearchMangaAsync(e.NewTextValue);
                    ItemsListView.ItemsSource = manga;
                }
                else if (searchEngine == "Comics")
                {
                    apiLabel.Text = "Powered by Comic Vine API";
                    ItemsListView.ItemsSource = null;
                    var comics = await ComicVine_service.SearchComicsAsync(e.NewTextValue);
                    ItemsListView.ItemsSource = comics;
                }
                else if (searchEngine == "Books")
                {
                    apiLabel.Text = "Powered by Google Books API";
                    ItemsListView.ItemsSource = null;
                    var books = await GoogleBooks_service.SearchBooksAsync(e.NewTextValue);
                    ItemsListView.ItemsSource = books;
                }
                else if (searchEngine == "Ranobe")
                {
                    apiLabel.Text = "Powered by AniList API";
                    ItemsListView.ItemsSource = null;
                    var ranobe = await AniListRanobe_service.SearchRanobeAsync(e.NewTextValue);
                    ItemsListView.ItemsSource = ranobe;
                }
                else if (searchEngine == "Games")
                {
                    apiLabel.Text = "Powered by RAWG Video Games Database API";
                    ItemsListView.ItemsSource = null;
                    var games = await RAWG_service.SearchGamesAsync(e.NewTextValue);
                    ItemsListView.ItemsSource = games;
                }
                else if (searchEngine == "Anime")
                {
                    apiLabel.Text = "Powered by AniList API";
                    ItemsListView.ItemsSource = null;
                    var animes = await AniListAnime_service.SearchAnimeAsync(e.NewTextValue);
                    ItemsListView.ItemsSource = animes;
                }
            }
        }
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        categoryId = await App.Database.GetIDByNameAsync<Categories>("CategoryID", "Categories", "Name", searchEngine);
        if (e.SelectedItem is ItemTypes.Items selectedItem)
        {
            await Navigation.PushAsync(new SelectedSearchItem(selectedItem, (int)categoryId));
        }
    }

    private static bool InternetConnectionAvailable()
    {
        return Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
    }

    private async void CheckInternetConnection()
    {
        if (!InternetConnectionAvailable())
            await DisplayAlert("Check the Internet connection", "An Internet connection is required for searching", "OK");
    }
}