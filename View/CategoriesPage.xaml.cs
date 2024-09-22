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
            // ���������, ����� �������� �������� �� ���� ������� ��� �������� ���� 䳿
            DisplayAlert("��������", $"�� ������ ��������: {selectedCategory.Name}", "OK");
            //DisplayAlert("����", $"�� ������ ��������: {selectedCategory.Name}", "OK");
            // �������, ������� �� ������� � ��������:
            // await Navigation.PushAsync(new CategoryDetailPage(selectedCategory));
        }
    }
}