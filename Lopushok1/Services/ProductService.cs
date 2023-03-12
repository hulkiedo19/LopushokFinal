using Lopushok1.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lopushok1.Services
{
    public class ProductService
    {
        public List<Product> GetProducts()
        {
            List<Product> Products;

            using(var db = new LopushokDbContext())
            {
                Products = db.Products
                    .Include(pm => pm.ProductMaterials)
                    .ThenInclude(m => m.Material)
                    .Include(pt => pt.ProductType)
                    .ToList();
            }

            return Products;
        }
        public List<string> GetTypes()
        {
            List<string> types;

            using (var db = new LopushokDbContext())
            {
                types = db.ProductTypes
                    .Select(pt => pt.Title)
                    .ToList();
            }

            return types;
        }
        public List<Product> Query(int Index, string Title, string Type)
        {
            return SortProducts(Index, SearchProducts(Title, FilterProducts(Type, GetProducts())));
        }
        private List<Product> SortProducts(int Index, List<Product> Products)
        {
            switch (Index)
            {
                case 1:
                    return Products.OrderBy(p => p.Title).ToList();
                case 2:
                    return Products.OrderByDescending(p => p.Title).ToList();
                case 3:
                    return Products.OrderBy(p => p.ProductionWorkshopNumber).ToList();
                case 4:
                    return Products.OrderByDescending(p => p.ProductionWorkshopNumber).ToList();
                case 5:
                    return Products.OrderBy(p => p.MinCostForAgent).ToList();
                case 6:
                    return Products.OrderByDescending(p => p.MinCostForAgent).ToList();
                default:
                    return Products;
            }
        }
        private List<Product> SearchProducts(string Title, List<Product> Products)
        {
            if (Title == null || Title == "")
                return Products;
            else
                return Products.Where(t => t.Title.ToLower().Contains(Title.ToLower())).ToList();
        }
        private List<Product> FilterProducts(string Type, List<Product> Products)
        {
            if (Type == null || Type == "Без фильтра" || Type == "")
                return Products;
            else
                return Products.Where(p => p.ProductType.Title == Type).ToList();
        }

        public void AddProduct(Product? Item)
        {
            if (Item == null)
                return;

            using (var db = new LopushokDbContext())
            {
                db.Products.Add(Item);
                db.SaveChanges();
            }
        }
        public void SaveProduct(Product? Item)
        {
            if (Item == null)
                return;

            using (var db = new LopushokDbContext())
            {
                Product product = db.Products
                    .Where(p => p.Id == Item.Id)
                    .Single();

                product.Title = Item.Title;
                product.ProductTypeId = Item.ProductTypeId;
                product.ArticleNumber = Item.ArticleNumber;
                product.MinCostForAgent = Item.MinCostForAgent;
                product.Description = Item.Description;
                product.ProductionPersonCount = Item.ProductionPersonCount;
                product.ProductionWorkshopNumber = Item.ProductionWorkshopNumber;
                product.Image = Item.Image;

                db.SaveChanges();
            }
        }
        public void DeleteProduct(List<Product> Products, int Index)
        {
            var product = Products.ElementAt(Index);

            using (var db = new LopushokDbContext())
            {
                var productMaterials = db.ProductMaterials
                    .Where(p => p.ProductId == product.Id)
                    .ToList();

                if (productMaterials.Count > 0)
                {
                    foreach (var pm in productMaterials)
                        db.ProductMaterials.Remove(pm);

                    db.SaveChanges();
                }

                db.Products.Remove(product);
                db.SaveChanges();
            }
        }
    }
}
