using Listifyr.ItemTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listifyr
{
    interface IListsViewModel
    {
        void AddListPanel(List list);
        void RemoveListPanel(List list);
        void AddListItem(int itemId, int listId, int categoryId);
        void RemoveListItem(ListItem item);
    }
}
