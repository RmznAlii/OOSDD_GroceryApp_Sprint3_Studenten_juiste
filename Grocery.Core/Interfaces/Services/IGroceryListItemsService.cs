using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Services
{
    public interface IGroceryListItemsService
    {
        public List<GroceryListItem> GetAll();

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId);

        public GroceryListItem Add(GroceryListItem item);

        public GroceryListItem? Delete(int id);

        public GroceryListItem? Get(int id);

        public GroceryListItem? Update(GroceryListItem item);
    }
}