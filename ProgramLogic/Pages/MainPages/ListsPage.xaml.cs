namespace Listifyr.View;
using static Listifyr.databases.SQLiteDatabase;

public partial class ListsPage : ContentPage
{
    public ListsPage()
	{
		InitializeComponent();
        OnAppearing();
    }

    protected async override void OnAppearing()
    {
        try
        {
            var catalogues = (CataloguesViewModel)BindingContext;
            await catalogues.LoadCataloguesAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("�������", $"�� ������� ����������� ��������: {ex.Message}", "OK");
        }
        base.OnAppearing();
    }

    private async void OnCatalogueTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("��������", $"��������� �� �������", "OK");
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        //string newCatalogueName = await DisplayPromptAsync("����� ������", "������ ����� ������");
        //var addedCatalogue = new Catalogues { Name = newCatalogueName };
        //await App.Database.AddItemAsync<Catalogues>(addedCatalogue);
        CataloguesViewModel cataloguesViewModel = new CataloguesViewModel();
        cataloguesViewModel.AddCatalogue();
        OnAppearing();
    }
}