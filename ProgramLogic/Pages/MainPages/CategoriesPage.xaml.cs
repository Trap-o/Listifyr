namespace Listifyr.View;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage()
	{
		InitializeComponent();
	}

    private void OnCategoryTapped(object sender, TappedEventArgs e)
    {
        var frame = sender as Frame;
        var selectedCategory = frame?.BindingContext as Category;

        if (selectedCategory != null)
        {
            // Наприклад, можна виконати навігацію на іншу сторінку або виконати інші дії
            DisplayAlert("Категорія", $"Ви обрали категорію: {selectedCategory.Name}", "OK");
            //DisplayAlert("Аніме", $"Ви обрали категорію: {selectedCategory.Name}", "OK");
            // Можливо, перехід на сторінку з деталями:
            // await Navigation.PushAsync(new CategoryDetailPage(selectedCategory));
        }
    }
}