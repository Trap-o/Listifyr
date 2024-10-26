using Listifyr.ItemTypes;

namespace Listifyr.ProgramLogic.Pages.NestedPages;

public partial class CategoryItemPage : ContentPage
{
    private Items Items { get; set; }
    private int itemID;
	public CategoryItemPage() => InitializeComponent();

	public CategoryItemPage(Items Item)
	{
		InitializeComponent();
		Items = Item;
        BindingContext = Items;
        //itemID = Item.ItemID;
        //MovieTitle.Text = Item.ItemName;
        //MoviePoster.Source = Item.Poster;
        //ReleaseDate.Text = "Release date: " + Item.Release_Date;
        //Overview.Text = Item.Description;
        //Status.Text = Item.Status;
    }

    private async void OnStatus_Tapped(object sender, TappedEventArgs e)
    {
        string newStatus = await DisplayActionSheet("New status:", "Cancel", null, "Planned", "In progress", "On-hold", "Dropped", "Completed");
        if(newStatus != null)
        {
            Items.Status = newStatus;
            await App.Database.UpdateItemAsync("Status", "Items", "ItemID", newStatus, Items.ItemID);
        }
    }

    //protected async override void OnAppearing()
    //{
    //	try
    //	{
    //           base.OnAppearing();
    //           await DisplayAlert("1", $": {id}", "OK");
    //           var itemInfo = await App.Database.LoadTableByIDAsync<Items>(id, "ItemID");
    //        if (itemInfo == null)
    //			{
    //				await DisplayAlert("Error", "Item not found.", "OK");
    //				return;
    //			}
    //		MovieTitle.Text = itemInfo.ItemName;
    //		MoviePoster.Source = itemInfo.Poster;
    //		ReleaseDate.Text = "Release date: " + itemInfo.Release_Date;
    //		Overview.Text = itemInfo.Description;
    //		Status.Text = itemInfo.Status;
    //	}
    //	catch (Exception ex)
    //	{
    //           await DisplayAlert("Error", $"Failed to load info: {ex.Message}", "OK");
    //       }
    //   }

    //   public async void GetItemInfo()
    //{
    //	var itemInfo = await App.Database.GetItemByIDAsync<Items>(id);
    //	MovieTitle.Text = itemInfo.ItemName;
    //	MoviePoster.Source = itemInfo.Poster;
    //	ReleaseDate.Text = "Release date: " + itemInfo.Release_Date;
    //	Overview.Text = itemInfo.Description;
    //	Status.Text = itemInfo.Status;
    //}
}