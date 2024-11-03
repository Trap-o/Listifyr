using Listifyr.ItemTypes;

namespace Listifyr.ProgramLogic.Pages.NestedPages;

public partial class CategoryItemPage : ContentPage
{
    private Items? Items { get; set; }

    public CategoryItemPage() => InitializeComponent();

	public CategoryItemPage(Items Item)
	{
		InitializeComponent();
		Items = Item;
        BindingContext = Items;
    }

    private async void OnStatus_Tapped(object sender, TappedEventArgs e)
    {
        string newStatus = await DisplayActionSheet("New status:", "Cancel", null, "Planned", "In progress", "Completed", "On-hold", "Dropped");
        if(newStatus != null && newStatus != "Cancel")
        {
            Items.Status = newStatus;
            await App.Database.UpdateItemAsync("Status", "Items", "ItemID", newStatus, Items.ItemID);
        }
    }

    private async void OnCatalogue_Tapped(object sender, TappedEventArgs e)
    {
        var cataloguesNames = await App.Database.GetListOfCataloguesNamesAsync();
        string newCatalogue = await DisplayActionSheet("New catalogue:", "Cancel", null, [.. cataloguesNames]);

        if (newCatalogue != null && newCatalogue != "Cancel")
        {
            var newCatalogueID = await App.Database.GetIDByNameAsync<Catalogues>("CatalogueID", "Catalogues", "Name", newCatalogue);
            Items.ID_Catalogue = newCatalogueID;
            await App.Database.UpdateItemAsync("ID_Catalogue", "Items", "ItemID", newCatalogueID.Value.ToString(), Items.ItemID);
        }
    }

    public async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        await App.Database.DeleteItemAsync("Items", "ItemName", Items.ItemName);
        await Navigation.PopAsync();
    }
}