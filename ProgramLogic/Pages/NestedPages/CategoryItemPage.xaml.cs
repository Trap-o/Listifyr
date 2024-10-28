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
    }

    private async void OnStatus_Tapped(object sender, TappedEventArgs e)
    {
        string newStatus = await DisplayActionSheet("New status:", "Cancel", null, "Planned", "In progress", "On-hold", "Dropped", "Completed");
        if(newStatus != null && newStatus != "Cancel")
        {
            Items.Status = newStatus;
            await App.Database.UpdateItemAsync("Status", "Items", "ItemID", newStatus, Items.ItemID);
        }
    }
}