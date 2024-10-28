using Listifyr.ItemTypes;
using SQLite;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace Listifyr.ProgramLogic.Pages.NestedPages;

public partial class SelectedSearchItem : ContentPage
{
    private Items Items { get; set; }
    private int? categoryID;
    public SelectedSearchItem() => InitializeComponent();
    public SelectedSearchItem(Items Item, int? categoryId)
    {
        InitializeComponent();
        Items = Item;
        categoryID = categoryId;
        ItemTitle.Text = Item.ItemName;
        ReleaseDate.Text = "Release date: " + Item.Release_Date;
        Overview.Text = WebUtility.HtmlDecode(Regex.Replace(Item.Description, "<.*?>", string.Empty));
        if (!string.IsNullOrEmpty(Item.Poster))
        {
            if (categoryId == 1 || categoryId == 2)
                ItemPoster.Source = new UriImageSource { Uri = new Uri("https://image.tmdb.org/t/p/w500" + Item.Poster) };
            else if (categoryId == 4)
                ;
            else if (categoryId == 5)
                ;
            else if (categoryId == 7)
                ;
            else if (categoryId == 3 || categoryId == 6 || categoryId == 8)
                ItemPoster.Source = new UriImageSource { Uri = new Uri(Item.Poster) };
        }
        else
        {
            ItemPoster.Source = "No_title"; // Стандартне зображення або повідомлення
        }
    }

    public async void AddButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            Debug.WriteLine("ІД категорії: " + (int)categoryID);
            var addedItem = new Items
            {
                ID_Category = (int)categoryID,
                ItemName = this.Items.ItemName,
                Release_Date = this.Items.Release_Date,
                Description = WebUtility.HtmlDecode(Regex.Replace(this.Items.Description, "<.*?>", string.Empty)),
                Poster = this.Items.Poster,
                Status = "Planned"
            };
            await App.Database.AddItemAsync<Items>(addedItem);
        }
        catch (SQLiteException sqlEx)
        {
            Debug.WriteLine("SQL Error: " + sqlEx);
            await DisplayAlert("Database Error", $"Помилка при додаванні в базу даних: {sqlEx.Message}", "OK");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Помилка: " + ex);
            await DisplayAlert("Error", $"Помилка з текстом: {ex}", "OK");
        }

        await Navigation.PopAsync();
    }
}