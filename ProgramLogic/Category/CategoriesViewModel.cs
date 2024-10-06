using Listifyr.databases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr
{
    public class CategoriesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Category> categories;
        private Category category;

        public Category SelectedCategory
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public ObservableCollection<Category> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                categories = value;
                OnPropertyChanged("Categories");
            }
        }

        public CategoriesViewModel()
        {
            GenerateCategories();
        }

        private void GenerateCategories()
        {
            Categories = new ObservableCollection<Category>();
            PopulateDB();
        }

        private async void PopulateDB()
        {
            foreach (Category category in Categories)
            {
                var item = await App.Database.GetCategoryAsync(category);
                if (item == null)
                {
                    await App.Database.AddCategoryAsync(category);
                }
            }
        }
        private async void OnAddNewItem()
        {
            await App.Database.AddCategoryAsync(SelectedCategory);
            Categories.Add(SelectedCategory);
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
