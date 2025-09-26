using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class GroceryListItemsService : IGroceryListItemsService
    {
        private readonly IGroceryListItemsRepository _groceriesRepository;
        private readonly IProductRepository _productRepository;

        public GroceryListItemsService(IGroceryListItemsRepository groceriesRepository, IProductRepository productRepository)
        {
            _groceriesRepository = groceriesRepository;
            _productRepository = productRepository;
        }

        public List<GroceryListItem> GetAll()
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId)
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll().Where(g => g.GroceryListId == groceryListId).ToList();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public GroceryListItem Add(GroceryListItem item)
        {
            return _groceriesRepository.Add(item);
        }

        public GroceryListItem? Delete(int id)
        {
            // 1) haal item op
            var item = _groceriesRepository.Get(id);
            if (item == null) return null;

            // 2) verwijder item
            var deleted = _groceriesRepository.Delete(id);
            if (deleted == null) return null;

            // 3) verhoog voorraad product
            var product = _productRepository.Get(item.ProductId);
            if (product != null)
            {
                product.Stock++;
                _productRepository.Update(product);
            }

            // 4) vul productreferentie voor retourwaarde
            deleted.Product = product ?? new(0, "", 0);
            return deleted;
        }

        public GroceryListItem? Get(int id)
        {
            var item = _groceriesRepository.Get(id);
            if (item != null)
            {
                item.Product = _productRepository.Get(item.ProductId) ?? new(0, "", 0);
            }
            return item;
        }

        public GroceryListItem? Update(GroceryListItem item)
        {
            var updated = _groceriesRepository.Update(item);
            if (updated != null)
            {
                updated.Product = _productRepository.Get(updated.ProductId) ?? new(0, "", 0);
            }
            return updated;
        }

        private void FillService(List<GroceryListItem> groceryListItems)
        {
            foreach (GroceryListItem g in groceryListItems)
            {
                g.Product = _productRepository.Get(g.ProductId) ?? new(0, "", 0);
            }
        }
    }
}