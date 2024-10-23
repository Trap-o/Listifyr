using Listifyr.ProgramLogic.databases;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr.ItemTypes
{
    public class Items : IDatabaseItem//IMediaItem
    {
        private string? itemname;
        private string? status;
        private double? rate;
        private string? description;
        private string? type;
        private string? poster;
        private string? release_date;
        public int ID_Category { get; set; }
        public int? ID_Catalogue { get; set; }

        [PrimaryKey, AutoIncrement]
        //[Display(AutoGenerateField = false)]
        public int ItemID { get; set; }
        public int Id => ItemID;

        public string? ItemName
        {
            get { return this.itemname; }
            set
            {
                this.itemname = value;
                RaisePropertyChanged("ItemName");
            }
        }
        public string? Status
        {
            get { return this.status; }
            set
            {
                this.status = value;
                RaisePropertyChanged("Status");
            }
        }
        public double? Rate
        {
            get { return this.rate; }
            set
            {
                this.rate = value;
                RaisePropertyChanged("Rate");
            }
        }
        public string? Description
        {
            get { return this.description; }
            set
            {
                this.description = value;
                RaisePropertyChanged("Description");
            }
        }
        public string? Type
        {
            get { return this.type; }
            set
            {
                this.type = value;
                RaisePropertyChanged("Type");
            }
        }
        public string? Poster
        {
            get { return this.poster; }
            set
            {
                this.poster = value;
                RaisePropertyChanged("Poster");
            }
        }
        public string? Release_Date
        {
            get { return this.release_date; }
            set
            {
                this.release_date = value;
                RaisePropertyChanged("Release_Date");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string value)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(value));
        }

    }
}
