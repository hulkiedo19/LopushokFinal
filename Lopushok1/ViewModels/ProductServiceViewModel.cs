using Lopushok1.Commands;
using Lopushok1.Entities;
using Lopushok1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Lopushok1.ViewModels
{
    public class ProductServiceViewModel : ViewModelBase
    {
        #region locals and props

        private ProductService _productService;
        private PagesService _pagesService;

        private List<Product> _dbProducts;
        private List<Product> _pageProducts;
        private List<Button> _buttonsList;
        private int _selectedProduct;
        private string _searchValue;
        private int _selectedSort;
        private string _selectedFilter;
        private int _numPages = 1;
        private int _currentPage;
        private const int _itemsOnPage = 20;
        private List<RoutedEventHandler> _methods;

        public List<Product> Products
        {
            get => _pageProducts;
            set => Set(ref _pageProducts, value, nameof(Products));
        }
        public int SelectedProduct
        {
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value, nameof(SelectedProduct));
        }
        public List<string> SortList { get; } = new List<string>()
        {
            "Без сортировки",
            "Наименование >",
            "Наименование <",
            "Номер цеха >",
            "Номер цеха <",
            "Мин стоимость >",
            "Мин стоимость <"
        };
        public List<string> FilterList { get; } = new List<string>()
        {
            "Без фильтра"
        };
        public List<Button> ButtonsList
        {
            get => _buttonsList;
            set => Set(ref _buttonsList, value, nameof(ButtonsList));
        }
        public string SearchValue
        {
            get => _searchValue;
            set
            {
                if (Set(ref _searchValue, value, nameof(SearchValue)))
                    DisplayProducts();
            }
        }
        public int SortIndex
        {
            get => _selectedSort;
            set
            {
                if (Set(ref _selectedSort, value, nameof(SortIndex)))
                    DisplayProducts();
            }
        }
        public string FilterItem
        {
            get => _selectedFilter;
            set
            {
                if (Set(ref _selectedFilter, value, nameof(FilterItem)))
                    DisplayProducts();
            }
        }

        #endregion

        #region commands
        public ICommand AddProduct => new Command(ap => MessageBox.Show("Add Product"));
        public ICommand EditProduct => new Command(ap => MessageBox.Show("Edit Product"));
        public ICommand DeleteProduct => new Command(ap => MessageBox.Show("Delete Product"));
        #endregion

        public ProductServiceViewModel()
        {
            _productService = new ProductService();
            _pagesService = new PagesService();
            _methods = new List<RoutedEventHandler>()
            {
                SpecifiedNumberPage_Click,
                RightPage_Click,
                LeftPage_Click
            };
            FilterList.AddRange(_productService.GetTypes());

            DisplayProducts();
        }

        private void DisplayProducts()
        {
            _dbProducts = _productService.Query(SortIndex, SearchValue, FilterItem);

            _currentPage = 0;
            _numPages = _pagesService.GetNumberOfPages(_dbProducts.Count, _itemsOnPage);
            Products = _pagesService.GetPage(_dbProducts, _currentPage, _itemsOnPage);
            ButtonsList = _pagesService.GetButtons(_numPages, _methods);
        }

        #region pages

        public void SpecifiedNumberPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = Convert.ToInt32((sender as Button).Content) - 1;
            Products = _pagesService.GetPage(_dbProducts, _currentPage, _itemsOnPage);
        }
        public void RightPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage >= _numPages - 1) return;
            Products = _pagesService.GetPage(_dbProducts, ++_currentPage, _itemsOnPage);
        }
        public void LeftPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage <= 0) return;
            Products = _pagesService.GetPage(_dbProducts, --_currentPage, _itemsOnPage);
        }

        #endregion
    }
}
