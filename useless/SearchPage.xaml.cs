//using Listifyr.useless.APIs;
//using System.Diagnostics;

//namespace Listifyr.ProgramLogic.Pages.NestedPages;

//public partial class SearchPage : ContentPage
//{
//    private string searchEngine;

//    public SearchPage()
//	{
//		InitializeComponent();
//	}

//    public SearchPage(string data)
//    {
//        InitializeComponent();
//        searchEngine = data;
//    }
//    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
//    {
//        if (e.NewTextValue.Length >= 3)
//        {
//            if(searchEngine == "Фільми")
//            {
//                TMDB_service tMDB_Service = new TMDB_service();
//                var movies = await tMDB_Service.SearchMoviesAsync(e.NewTextValue);
//                ItemsListView.ItemsSource = movies;
//                Debug.WriteLine("Інфа про фільм: " + movies);
//            }
//        }
//    }

//    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
//    {
//        var selectedItem = e.SelectedItem as ItemTypes.MediaItems;
//        if (selectedItem != null)
//        {
//            // Відкриття нової сторінки з детальною інформацією про фільм
//            await Navigation.PushAsync(new SelectedSearchItem(selectedItem));
//        }
//    }
//}