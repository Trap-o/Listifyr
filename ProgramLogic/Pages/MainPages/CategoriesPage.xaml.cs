//using Listifyr.databases;

using Microsoft.Maui.Controls;

namespace Listifyr.View;

public partial class CategoriesPage : ContentPage
{
    //private static readonly DatabaseInteraction databaseInteraction = new();
    public CategoriesPage()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            var categories = await App.Database.GetCategoriesAsync();

        // Присвоїти список категорій як ItemsSource для CollectionView
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
        var selectedCategory = frame?.BindingContext as Category;

        if (selectedCategory != null)
        {
            // Наприклад, можна виконати навігацію на іншу сторінку або виконати інші дії
            try
            {
                //databaseInteraction.SQLite_connection();
                DisplayAlert("Успішно", $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db")}", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Помилка!", $"Опис помилки: {ex}", "OK");
            }

            //DisplayAlert("Категорія", $"Ви обрали категорію: {selectedCategory.Name}", "OK");
            //DisplayAlert("Аніме", $"Ви обрали категорію: {selectedCategory.Name}", "OK");
            // Можливо, перехід на сторінку з деталями:
            // await Navigation.PushAsync(new CategoryDetailPage(selectedCategory));
        }
    }
}