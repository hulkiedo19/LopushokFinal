using Lopushok1.Commands;
using Lopushok1.Entities;
using Lopushok1.Services;
using Lopushok1.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
        private List<ICommand> _methods;

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

        public ICommand LeftPage => new Command(lp => Products = (_currentPage > 0) ? _pagesService.GetPage(_dbProducts, --_currentPage, _itemsOnPage) : Products);
        public ICommand RightPage => new Command(rp => Products = (_currentPage < _numPages - 1) ? Products = _pagesService.GetPage(_dbProducts, ++_currentPage, _itemsOnPage) : Products);
        public ICommand SpecifiedPage => new Command((object parameter) =>
        {
            _currentPage = Convert.ToInt32(parameter.ToString()) - 1;
            Products = _pagesService.GetPage(_dbProducts, _currentPage, _itemsOnPage);
        });

        public ICommand AddProduct => new Command(ap =>
        {
            if(new EditWindow("add", null).ShowDialog() == true) 
                DisplayProducts();
        });
        public ICommand EditProduct => new Command(ep =>
        {
            if(new EditWindow("edit", _pageProducts[SelectedProduct]).ShowDialog() == true) 
                DisplayProducts();
        });
        public ICommand DeleteProduct => new Command(dp =>
        {
            _productService.DeleteProduct(_pageProducts, SelectedProduct);
            DisplayProducts();
        });
        
        #endregion

        #region methods

        public ProductServiceViewModel()
        {
            _productService = new ProductService();
            _pagesService = new PagesService();
            _methods = new List<ICommand>() { LeftPage, RightPage, SpecifiedPage };
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

        #endregion
    }
}
