using SportsItemsStore.Domain.Abstract;
using SportsItemsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;

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
        public ProductsListViewModel ProductsList(int categoryId, int BlockNumber, int BlockSize, string searchTerm ,
            int sizeId, int colorId, int start, int end)
        {
            int startIndex = (BlockNumber - 1) * BlockSize;
           
            var products = SelectProduct(categoryId, startIndex, BlockSize, searchTerm, sizeId, colorId, start, end).ToList();
            string category = repository.Categories.Where(c=>c.CategoryId==categoryId).Select(c=>c.CategoryName).FirstOrDefault();
            return SetProductsList(products,category, startIndex, BlockSize, searchTerm, sizeId, colorId, start, end);
        }

       
       
        private IQueryable<Product> SelectProduct(int categoryId, int startIndex, int BlockSize, string searchTerm,
            int sizeId, int colorId, int start, int end)
        {
            
            var query  = repository.Products;
            if (searchTerm != null && searchTerm != "")
            {
                query = query.Where(p=>p.Name.Contains(searchTerm));
                
            }
            if (categoryId != 0)
            {
               
                query = query.Where(p=>p.CategoryId == categoryId);
            }

            if (sizeId != 0 && colorId != 0)
            {
               query = query.Where(p=>p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId) && p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId));
            }

            if (sizeId != 0 && colorId == 0)
            {
                query = query.Where(p=>p.ProductSizes.Select(ps => ps.Size.SizeID).Contains(sizeId));
            
            }

            if (colorId != 0 && sizeId == 0)
            {
               query = query.Where(p=>p.ProductColors.Select(pc => pc.Color.ColorID).Contains(colorId));
            }

            if (start > 0 || end > 0)
            {
               query = query.Where(p=>p.Price >= start && p.Price <= end);
               
            }

            return query.OrderBy(p => p.ProductID).Skip(startIndex).Take(BlockSize);;
        }

        private ProductsListViewModel SetProductsList(List<Product> products, string category, int startIndex, int BlockSize, string searchTerm,
            int sizeId, int colorId, int start, int end)
        {
            return new  ProductsListViewModel
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