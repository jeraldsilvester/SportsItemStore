using SportsItemsStore.Domain.Abstract;
using SportsItemsStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SportsItemsStore.WebUI.Models
{
    public class ItemLoaderManager
    {
        private IProductsRepository repository;

        public ItemLoaderManager(IProductsRepository productRepository)
        {
            this.repository = productRepository;
        }

        /// <summary>
        /// Returns one block of book items
        /// </summary>
        /// <param name="BlockNumber">Starting from 1</param>
        /// <param name="BlockSize">Items count in a block</param>
        /// <returns></returns>
        public ProductsListViewModel ProductsList(int categoryId, int BlockNumber, int BlockSize, string searchTerm,
            int sizeId, int colorId, int start, int end, int manufacturerId)
        {
            int startIndex = (BlockNumber - 1) * BlockSize;

            var products = SelectProduct(categoryId, startIndex, BlockSize, searchTerm, sizeId, colorId, start, end, manufacturerId).ToList();
            string category = repository.Categories.Where(c => c.CategoryId == categoryId).Select(c => c.CategoryName).FirstOrDefault();
            return SetProductsList(products);
        }

        private IQueryable<Product> SelectProduct(int categoryId, int startIndex, int BlockSize, string searchTerm,
            int sizeId, int colorId, int start, int end, int manufacturerId)
        {
            var query = repository.Products;
            if (searchTerm != null && searchTerm != "")
            {
                query = query.Where(p => (p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm)));
            }
            if (categoryId != 0)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            if (sizeId != 0 && colorId != 0 && manufacturerId != 0)
            {
                query = query.Where(p => p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && 
                    p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId) && 
                    p.ProductManufacturers.Select(pm => pm.Manufacturer.ManufacturerID).Contains(manufacturerId));
            }

            if (sizeId != 0 && manufacturerId != 0 && colorId == 0)
            {
                query = query.Where(p => p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && 
                    p.ProductManufacturers.Select(pm => pm.Manufacturer.ManufacturerID).Contains(manufacturerId));
            }

            if (colorId != 0 && manufacturerId != 0 && sizeId == 0)
            {
                query = query.Where(p => p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId) &&
                    p.ProductManufacturers.Select(pm => pm.Manufacturer.ManufacturerID).Contains(manufacturerId));
            }

            if (colorId != 0 && sizeId != 0 && manufacturerId == 0)
            {
                query = query.Where(p => p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId) && 
                    p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId));
            }

            if (sizeId != 0 && manufacturerId == 0 && colorId == 0)
            {
                query = query.Where(p => p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId));
            }

            if (colorId != 0 && manufacturerId == 0 && sizeId == 0)
            {
                query = query.Where(p => p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId));
            }

            if (manufacturerId != 0 && colorId == 0 && sizeId == 0)
            {
                query = query.Where(p => p.ProductManufacturers.Select(pm => pm.Manufacturer.ManufacturerID).Contains(manufacturerId));
            }
                      
            if (start > 0 || end > 0)
            {
                query = query.Where(p => p.Price >= start && p.Price <= end);
            }

            return query.OrderBy(p => p.ProductID).Skip(startIndex).Take(BlockSize); ;
        }

        private ProductsListViewModel SetProductsList(List<Product> products)
        {
            return new ProductsListViewModel
            {
                Products = products,

                //PagingInfo = new PagingInfo
                //{
                //    CurrentPage = startIndex,
                //    ItemsPerPage = BlockSize,
                //    TotalItems = 0,
                //    SizeId=sizeId,
                //    ColorId=colorId,
                //    StartPrice=start,
                //    EndPrice=end
                //},

                //CurrentCategory = category,
                //SearchTerm=searchTerm
            };
        }
    }
}