using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr.ItemTypes
{
    interface IMediaItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public double? Rate { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public int CategoryID { get; set; }
        public int CatalogueID { get; set; }
    }
}
