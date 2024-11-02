using Listifyr.ItemTypes;
using Listifyr.ProgramLogic.PrivateData;
using SQLite;
using System.Net;
using System.Text.RegularExpressions;

namespace Listifyr.ProgramLogic.Pages.NestedPages;

public partial class SelectedSearchItem : ContentPage
{
    private Items? Items { get; set; }
    private readonly int categoryID;

    public SelectedSearchItem() => InitializeComponent();
    public SelectedSearchItem(Items Item, int categoryId)
    {
        InitializeComponent();
        Items = Item;
        categoryID = categoryId;
        ItemTitle.Text = Item.ItemName;
        ReleaseDate.Text = "Release date: " + Item.Release_Date;
        Overview.Text = WebUtility.HtmlDecode(Regex.Replace(Item.Description, "<.*?>", string.Empty));
        Console.WriteLine("Poster: " + Item.Poster);
        if (!string.IsNullOrEmpty(Item.Poster))
        {
            if (categoryId is 1 or 2)
                ItemPoster.Source = new UriImageSource { Uri = new Uri("https://image.tmdb.org/t/p/w500" + Item.Poster) };
            else if (categoryId is 3 or 4 or 5 or 6 or 7 or 8)
                ItemPoster.Source = new UriImageSource { Uri = new Uri(Item.Poster) };
        }
        else
            ItemPoster.Source = Data.noImageIcon;
    }

    public async void AddButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var addedItem = new Items
            {
                ID_Category = categoryID,
                ItemName = Items?.ItemName,
                Release_Date = Items?.Release_Date,
                Description = WebUtility.HtmlDecode(Regex.Replace(Items?.Description, "<.*?>", string.Empty)),
                Poster = Items.Poster,
                Status = "Planned",
            };
            await App.Database.AddItemAsync<Items>(addedItem);
        }
        catch (SQLiteException sqlEx)
        {
            await DisplayAlert("Database Error", $"Error with adding item in DB: {sqlEx.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error with text: {ex}", "OK");
        }

        await Navigation.PopAsync();
    }
}