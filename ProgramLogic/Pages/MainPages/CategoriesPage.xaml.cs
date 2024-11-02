namespace Listifyr.View;

public partial class CategoriesPage : ContentPage
{
    public CategoriesPage()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        try
        {
            var categories = await App.Database.GetAsync<Categories>();
            var collectionView = this.FindByName<CollectionView>("CategoryCollectionView");
            collectionView.ItemsSource = categories;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load categories: {ex.Message}", "OK");
        }
        base.OnAppearing();
    }

    private async void OnCategoryTapped(object sender, TappedEventArgs e)
    {
        var frame = sender as Frame;

        if (frame?.BindingContext is Categories selectedCategory)
        {
            try
            {
                var data = selectedCategory.Name;
                await Navigation.PushAsync(new CategoryPage(data));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error description: {ex}", "OK");
            }
        }
    }
}