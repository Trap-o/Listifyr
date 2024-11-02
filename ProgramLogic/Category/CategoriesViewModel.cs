using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Listifyr
{
    public partial class CategoriesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Categories>? categories;
        private Categories? category;

        public Categories SelectedCategory
        {
            get => category;
            set
            {
                category = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        public ObservableCollection<Categories> Categories
        {
            get => categories;
            set
            {
                categories = value;
                OnPropertyChanged("Categories");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}