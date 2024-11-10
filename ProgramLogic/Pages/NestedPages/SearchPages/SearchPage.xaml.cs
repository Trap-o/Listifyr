using Listifyr.ProgramLogic.APIs;
using Listifyr.ProgramLogic.APIs.AniList;
using Listifyr.ProgramLogic.APIs.ComicVine;
using Listifyr.ProgramLogic.APIs.GoogleBooks;
using Listifyr.ProgramLogic.APIs.TMDB;
using System;
using static System.Reflection.Metadata.BlobBuilder;

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
                    var(success, movies) = await TmdbMovies_service.SearchMoviesAsync(e.NewTextValue);
                    if (!success)
                    {
                        await DisplayAlert("API Error", "Failed to retrieve data from The Movie Database. Please try again later.", "OK");
                        return;
                    }
                    ItemsListView.ItemsSource = movies;
                }

                else if (searchEngine == "Series")
                {
                    apiLabel.Text = "Powered by The Movie Database (TMDB) API";
                    ItemsListView.ItemsSource = null;
                    var(success, series) = await TmdbSeries_service.SearchSeriesAsync(e.NewTextValue);
                    if (!success)
                    {
                        await DisplayAlert("API Error", "Failed to retrieve data from The Movie Database. Please try again later.", "OK");
                        return;
                    }
                    ItemsListView.ItemsSource = series;
                }

                else if (searchEngine == "Manga")
                {
                    apiLabel.Text = "Powered by AniList API";
                    ItemsListView.ItemsSource = null;
                    var(success, manga) = await AniListManga_service.SearchMangaAsync(e.NewTextValue);
                    if (!success)
                    {
                        await DisplayAlert("API Error", "Failed to retrieve data from AniList. Please try again later.", "OK");
                        return;
                    }
                    ItemsListView.ItemsSource = manga;
                }

                else if (searchEngine == "Comics")
                {
                    apiLabel.Text = "Powered by Comic Vine API";
                    ItemsListView.ItemsSource = null;
                    var(success, comics) = await ComicVine_service.SearchComicsAsync(e.NewTextValue);
                    if (!success)
                    {
                        await DisplayAlert("API Error", "Failed to retrieve data from Comic Vine. Please try again later.", "OK");
                        return;
                    }
                    ItemsListView.ItemsSource = comics;
                }

                else if (searchEngine == "Books")
                {
                    apiLabel.Text = "Powered by Google Books API";
                    ItemsListView.ItemsSource = null;
                    var(success, books) = await GoogleBooks_service.SearchBooksAsync(e.NewTextValue);
                    if (!success)
                    {
                        await DisplayAlert("API Error", "Failed to retrieve data from Google Books. Please try again later.", "OK");
                        return;
                    }
                    ItemsListView.ItemsSource = books;
                }

                else if (searchEngine == "Ranobe")
                {
                    apiLabel.Text = "Powered by AniList API";
                    ItemsListView.ItemsSource = null;
                    var(success, ranobe) = await AniListRanobe_service.SearchRanobeAsync(e.NewTextValue);
                    if (!success)
                    {
                        await DisplayAlert("API Error", "Failed to retrieve data from AniList. Please try again later.", "OK");
                        return;
                    }
                    ItemsListView.ItemsSource = ranobe;
                }

                else if (searchEngine == "Games")
                {
                    apiLabel.Text = "Powered by RAWG Video Games Database API";
                    ItemsListView.ItemsSource = null;
                    var(success, games) = await RAWG_service.SearchGamesAsync(e.NewTextValue);
                    if (!success)
                    {
                        await DisplayAlert("API Error", "Failed to retrieve data from RAWG Video Games Database. Please try again later.", "OK");
                        return;
                    }
                    ItemsListView.ItemsSource = games;
                }

                else if (searchEngine == "Anime")
                {
                    apiLabel.Text = "Powered by AniList API";
                    ItemsListView.ItemsSource = null;
                    var (success, animes) = await AniListAnime_service.SearchAnimeAsync(e.NewTextValue);
                    if (!success)
                    {
                        await DisplayAlert("API Error", "Failed to retrieve data from AniList. Please try again later.", "OK");
                        return;
                    }
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