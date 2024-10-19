using Listifyr.databases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Listifyr.databases.SQLiteDatabase;

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
                OnPropertyChanged("SelectedCategory");
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



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}