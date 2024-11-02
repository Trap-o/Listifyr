using Listifyr.ProgramLogic.databases;
using SQLite;
using System.ComponentModel;

namespace Listifyr
{
    public class Categories : IDatabaseItem
    {
        private string? name;
        private string? imagePath;

        [PrimaryKey, AutoIncrement]
        public int CategoryID { get; set; }
        public int Id => CategoryID;

        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string? ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                RaisePropertyChanged("Image");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string value)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value));
        }
    }
}