using Lopushok1.Commands;
using Lopushok1.Entities;
using Lopushok1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lopushok1.ViewModels
{
    public class EditWindowViewModel : ViewModelBase
    {
        #region locals and props

        ProductService _productService;
        private string _title;
        private string _productTypeId;
        private string _articleNumber;
        private string _description;
        private string _image;
        private string _personCount;
        private string _workshopNumber;
        private string _minCost;

        public string Title
        {
            get => _title;
            set => Set(ref _title, value, nameof(Title));
        }
        public string ProductTypeId
        {
            get => _productTypeId;
            set => Set(ref _productTypeId, value, nameof(ProductTypeId));
        }
        public string ArticleNumber
        {
            get => _articleNumber;
            set => Set(ref _articleNumber, value, nameof(ArticleNumber));
        }
        public string Description
        {
            get => _description;
            set => Set(ref _description, value, nameof(Description));
        }
        public string Image
        {
            get => _image;
            set => Set(ref _image, value, nameof(Image));
        }
        public string PersonCount
        {
            get => _personCount;
            set => Set(ref _personCount, value, nameof(PersonCount));
        }
        public string WorkshopNumber
        {
            get => _workshopNumber;
            set => Set(ref _workshopNumber, value, nameof(WorkshopNumber));
        }
        public string MinCost
        {
            get => _minCost;
            set => Set(ref _minCost, value, nameof(MinCost));
        }

        #endregion

        #region commands

        public ICommand AddProduct => new Command(p => _productService.AddProduct(GetProduct()));
        public ICommand SaveProduct => new Command(p => _productService.SaveProduct(GetProduct()));

        #endregion

        public EditWindowViewModel()
        {
            _productService = new ProductService();
        }

        private Product? GetProduct()
        {
            Product product = new Product();

            product.Title = Title;
            product.ProductTypeId = Convert.ToInt32(ProductTypeId);
            product.ArticleNumber = ArticleNumber;
            product.MinCostForAgent = Convert.ToDecimal(MinCost);
            product.Description = (Description == "") ? null : Description;
            product.ProductionPersonCount = (PersonCount == "") ? null : Convert.ToInt32(PersonCount);
            product.ProductionWorkshopNumber = (WorkshopNumber == "") ? null : Convert.ToInt32(WorkshopNumber);
            product.Image = (Image == "") ? null : Image;

            return product;
        }

        private void SetProduct(Product Item)
        {
            Title= Item.Title;
            ProductTypeId = Convert.ToString(Item.ProductTypeId);
            Description = Item.Description;
            ArticleNumber = Item.ArticleNumber;
            Image = Item.Image;
            PersonCount = Convert.ToString(Item.ProductionPersonCount);
            WorkshopNumber = Convert.ToString(Item.ProductionWorkshopNumber);
            MinCost = Convert.ToString(Item.MinCostForAgent);
        }
    }
}
