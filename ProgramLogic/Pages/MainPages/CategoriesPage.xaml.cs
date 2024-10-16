using Listifyr.databases;
using Listifyr.ProgramLogic.Pages;
using Listifyr.ProgramLogic.Pages.NestedPages;
using Microsoft.Maui.Controls;

namespace Listifyr.View;

public partial class CategoriesPage : ContentPage
{
    public CategoriesPage()
    {
        InitializeComponent();
        OnAppearing();
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
            await DisplayAlert("Помилка", $"Не вдалося завантажити категорії: {ex.Message}", "OK");
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
                string data = selectedCategory.Name;
                //Label categoryTitleLabel = (Label)FindByName("titleLabel");
                //string categoryTitle = categoryTitleLabel.Text;
                await Navigation.PushAsync(new CategoryPage(data));
                //await Shell.Current.GoToAsync($"categoryPage?data={selectedCategory.Name}");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка!", $"Опис помилки: {ex}", "OK");
            }
        }
    }
}