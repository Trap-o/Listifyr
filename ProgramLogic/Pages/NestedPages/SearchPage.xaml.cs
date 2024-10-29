using Listifyr.ProgramLogic.APIs;
using Listifyr.ProgramLogic.APIs.AniList;
using Listifyr.ProgramLogic.APIs.ComicVine;
using Listifyr.ProgramLogic.APIs.GoogleBooks;
using Listifyr.ProgramLogic.APIs.TMDB;

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
        if (e.NewTextValue.Length >= 2)
        {
            if (searchEngine == "Movies")
            {
                ItemsListView.ItemsSource = null;
                TmdbMovies_service tMDB_Service = new TmdbMovies_service();
                var movies = await tMDB_Service.SearchMoviesAsync(e.NewTextValue);
                ItemsListView.ItemsSource = movies;
            }
            else if (searchEngine == "Series")
            {
                ItemsListView.ItemsSource = null;
                TmdbSeries_service tMDB_Service = new TmdbSeries_service();
                var series = await tMDB_Service.SearchSeriesAsync(e.NewTextValue);
                ItemsListView.ItemsSource = series;
            }
            else if (searchEngine == "Manga")
            {
                ItemsListView.ItemsSource = null;
                AniListManga_service aniListManga_Service = new AniListManga_service();
                var manga = await aniListManga_Service.SearchMangaAsync(e.NewTextValue);
                Console.WriteLine($"Found manga: {manga?.Count()}");
                ItemsListView.ItemsSource = manga;
            }
            else if (searchEngine == "Comics")
            {
                ItemsListView.ItemsSource = null;
                ComicVine_service comicVine_service = new ComicVine_service();
                var comics = await comicVine_service.SearchComicsAsync(e.NewTextValue);
                Console.WriteLine($"Found comics: {comics?.Count()}");
                ItemsListView.ItemsSource = comics;
            }
            else if (searchEngine == "Books")
            {
                ItemsListView.ItemsSource = null;
                GoogleBooks_service googleBooks_service = new GoogleBooks_service();
                var books = await googleBooks_service.SearchBooksAsync(e.NewTextValue);
                Console.WriteLine($"Found books: {books?.Count()}");
                ItemsListView.ItemsSource = books;
            }
            else if (searchEngine == "Ranobe")
            {
                ItemsListView.ItemsSource = null;
                AniListRanobe_service aniListRanobe_Service = new AniListRanobe_service();
                var ranobe = await aniListRanobe_Service.SearchRanobeAsync(e.NewTextValue);
                Console.WriteLine($"Found ranobe: {ranobe?.Count()}");
                ItemsListView.ItemsSource = ranobe;
            }
            else if (searchEngine == "Games")
            {
                ItemsListView.ItemsSource = null;
                RAWG_service rawg_service = new RAWG_service();
                var games = await rawg_service.SearchGamesAsync(e.NewTextValue);
                Console.WriteLine($"Found games: {games?.Count()}");
                ItemsListView.ItemsSource = games;
            }
            else if (searchEngine == "Anime")
            {
                ItemsListView.ItemsSource = null;
                AniListAnime_service aniListAnime_Service = new AniListAnime_service();
                var animes = await aniListAnime_Service.SearchAnimeAsync(e.NewTextValue);
                Console.WriteLine($"Found animes: {animes?.Count()}");
                ItemsListView.ItemsSource = animes;
            }
        }
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        categoryId = await App.Database.GetIDByNameAsync<Categories>("CategoryID", "Categories", "Name", searchEngine);
        var selectedItem = e.SelectedItem as ItemTypes.Items;
        if (selectedItem != null)
        {
            await Navigation.PushAsync(new SelectedSearchItem(selectedItem, categoryId));
        }
    }
}