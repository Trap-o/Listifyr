using Listifyr.ItemTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr
{
    interface ICatalogueViewModel
    {
        void AddListPanel(Catalogue list);
        void RemoveListPanel(Catalogue list);
        void AddListItem(int itemId, int listId, int categoryId);
        void RemoveListItem(MediaItem item);
    }
}
