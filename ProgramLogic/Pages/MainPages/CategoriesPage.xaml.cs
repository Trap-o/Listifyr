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

        // �������� ������ �������� �� ItemsSource ��� CollectionView
        var collectionView = this.FindByName<CollectionView>("CategoryCollectionView");
        collectionView.ItemsSource = categories;
        }
        catch (Exception ex)
        {
            await DisplayAlert("�������", $"�� ������� ����������� �������: {ex.Message}", "OK");
        }
    }

    private void OnCategoryTapped(object sender, TappedEventArgs e)
    {
        var frame = sender as Frame;
        var selectedCategory = frame?.BindingContext as Category;

        if (selectedCategory != null)
        {
            // ���������, ����� �������� �������� �� ���� ������� ��� �������� ���� 䳿
            try
            {
                //databaseInteraction.SQLite_connection();
                DisplayAlert("������", $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db")}", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("�������!", $"���� �������: {ex}", "OK");
            }

            //DisplayAlert("��������", $"�� ������ ��������: {selectedCategory.Name}", "OK");
            //DisplayAlert("����", $"�� ������ ��������: {selectedCategory.Name}", "OK");
            // �������, ������� �� ������� � ��������:
            // await Navigation.PushAsync(new CategoryDetailPage(selectedCategory));
        }
    }
}