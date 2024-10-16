using Listifyr.ItemTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr
{
    interface ICataloguesViewModel
    {
        void AddListPanel(Catalogues list);
        void RemoveListPanel(Catalogues list);
        void AddListItem(int itemId, int listId, int categoryId);
        void RemoveListItem(MediaItems item);
    }
}
