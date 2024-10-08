using Listifyr.databases;
using Microsoft.Maui.Controls;

namespace Listifyr.View;

public partial class CategoriesPage : ContentPage
{
    public CategoriesPage()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        DisplayAlert("Шлях до бази даних", Database.DatabasePath, "OK");
        try
        {
            var categories = await App.Database.GetCategoriesAsync();

            var collectionView = this.FindByName<CollectionView>("CategoryCollectionView");
            collectionView.ItemsSource = categories;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка", $"Не вдалося завантажити категорії: {ex.Message}", "OK");
        }
    }

    private void OnCategoryTapped(object sender, TappedEventArgs e)
    {
        var frame = sender as Frame;
        var selectedCategory = frame?.BindingContext as Categories;

        if (selectedCategory != null)
        {
            try
            {
                DisplayAlert("Успішно", $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db")}", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Помилка!", $"Опис помилки: {ex}", "OK");
            }
        }
    }
}