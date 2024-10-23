using Listifyr.ProgramLogic.databases;
using SQLite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Listifyr
{
    public class Categories : IDatabaseItem
    {
        private string? name;
        private string? imagePath;

        [PrimaryKey, AutoIncrement]
        //[Display(AutoGenerateField = false)]
        public int CategoryID { get; set; }
        public int Id => CategoryID;

        public string? Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                RaisePropertyChanged("Name");
            }
        }

        //[Display(AutoGenerateField = false)]
        public string? ImagePath
        {
            get { return this.imagePath; }
            set
            {
                this.imagePath = value;
                this.RaisePropertyChanged("Image");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String value)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value));
        }
    }
}