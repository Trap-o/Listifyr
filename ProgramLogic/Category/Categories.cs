using SQLite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Listifyr
{
    public class Categories
    {
        private string? name;
        private string? imagePath;

        [PrimaryKey, AutoIncrement]
        //[Display(AutoGenerateField = false)]
        public int CategoryID { get; set; }

        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                RaisePropertyChanged("Name");
            }
        }

        //[Display(AutoGenerateField = false)]
        public string ImagePath
        {
            get { return this.imagePath; }
            set
            {
                this.imagePath = value;
                this.RaisePropertyChanged("Image");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}