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
        private ObservableCollection<Categories> categories;
        private Categories category;

        public Categories SelectedCategory
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

        public ObservableCollection<Categories> Categories
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

        private async void GenerateCategories()
        {
            Categories = new ObservableCollection<Categories>();
            await PopulateDB();
        }

        private async Task PopulateDB()
        {
            foreach (Categories category in Categories)
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