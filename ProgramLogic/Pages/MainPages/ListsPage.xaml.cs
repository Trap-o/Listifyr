namespace Listifyr.View;
using static Listifyr.databases.SQLiteDatabase;

public partial class ListsPage : ContentPage
{
    public Command LongPressCommand { get; set; }
    public ListsPage()
	{
		InitializeComponent();
        OnAppearing();
    }

    protected async override void OnAppearing()
    {
        try
        {
            var catalogues = await App.Database.GetAsync<Catalogues>();

            var collectionView = this.FindByName<CollectionView>("CatalogueCollectionView");
            collectionView.ItemsSource = catalogues;

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
        string newCatalogueName = await DisplayPromptAsync("����� ������", "������ ����� ������");
        var addedCatalogue = new Catalogues { Name = newCatalogueName };
        await App.Database.AddItemAsync<Catalogues>(addedCatalogue);
        OnAppearing();
    }
}