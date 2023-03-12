using Lopushok1.Entities;
using Lopushok1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lopushok1.Views
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private EditWindowViewModel? _viewModel = null;

        public EditWindow(string type, Product? product)
        {
            InitializeComponent();

            _viewModel = (EditWindowViewModel)DataContext;

            Title = (type == "edit") ? "Edit Window" : "Add Window";
            AddButton.Visibility = (type == "edit") ? Visibility.Collapsed : Visibility.Visible;
            SaveButton.Visibility = (type == "edit") ? Visibility.Visible : Visibility.Collapsed;

            if (_viewModel != null && product != null)
                _viewModel.SetProduct(product);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = (_viewModel != null) ? _viewModel.IsChanged : true;
            this.Close();
        }
    }
}
