using Listifyr.ItemTypes;
using SQLite;
using System.Diagnostics;

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
        MovieTitleLabel.Text = Item.ItemName;
        ReleaseDateLabel.Text = "Release date: " + Item.Release_Date;
        OverviewLabel.Text = Item.Description;
        if (!string.IsNullOrEmpty(Item.Poster))
        {
            MoviePoster.Source = new UriImageSource { Uri = new Uri("https://image.tmdb.org/t/p/w500" + Item.Poster) };
        }
        else
        {
            MoviePoster.Source = "No_title"; // Стандартне зображення або повідомлення
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
                Description = this.Items.Description,
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