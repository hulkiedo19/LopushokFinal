using Lopushok1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Media;

namespace Lopushok1.Services
{
    public class PagesService
    {
        public int GetNumberOfPages(int Count, int ItemsOnPage)
        {
            return (Count / ItemsOnPage) + ((Count % ItemsOnPage) > 0 ? 1 : 0);
        }
        public List<Product> GetPage(List<Product> Products, int CurrentPage, int ItemsOnPage)
        {
            return Products
                .Skip(CurrentPage * ItemsOnPage)
                .Take(ItemsOnPage)
                .ToList();
        }
        public List<Button> GetButtons(int PagesNum, List<RoutedEventHandler> Methods)
        {
            List<Button> buttons = new List<Button>();

            Button leftPage = new Button();
            leftPage.Content = "<";
            leftPage.Margin = new Thickness(0, 0, 2, 0);
            leftPage.BorderBrush = new SolidColorBrush(Colors.White);
            leftPage.BorderThickness = new Thickness(0);
            leftPage.Background = new SolidColorBrush(Colors.White);
            leftPage.Click += Methods[2];
            buttons.Add(leftPage);

            for (int i = 0; i < PagesNum; i++)
            {
                Button specifiedNumberPage = new Button();
                specifiedNumberPage.Content = $"{i + 1}";
                specifiedNumberPage.Margin = new Thickness(0, 0, 2, 0);
                specifiedNumberPage.BorderBrush = new SolidColorBrush(Colors.White);
                specifiedNumberPage.BorderThickness = new Thickness(0);
                specifiedNumberPage.Background = new SolidColorBrush(Colors.White);
                specifiedNumberPage.Click += Methods[0];
                buttons.Add(specifiedNumberPage);
            }

            Button rightPage = new Button();
            rightPage.Content = ">";
            rightPage.Margin = new Thickness(0, 0, 2, 0);
            rightPage.BorderBrush = new SolidColorBrush(Colors.White);
            rightPage.BorderThickness = new Thickness(0);
            rightPage.Background = new SolidColorBrush(Colors.White);
            rightPage.Click += Methods[1];
            buttons.Add(rightPage);

            return buttons;
        }
    }
}
