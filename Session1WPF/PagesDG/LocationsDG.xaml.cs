using Microsoft.EntityFrameworkCore;
using Session1WPF.Data;
using Session1WPF.PagesEdit;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Session1WPF.Classes.Helper;

namespace Session1WPF.PagesDG
{
    /// <summary>
    /// Логика взаимодействия для LocationsDG.xaml
    /// </summary>
    public partial class LocationsDG : Page
    {
        public LocationsDG()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Db.Locations.Load();
            LocationDG.ItemsSource = Db.Locations.ToList();
        }

        private void AddLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditLocationsPage(-1));
        }

        private void EditLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected_locations = LocationDG.SelectedItem as Location;

            if (selected_locations != null)
            {
                NavigationService.Navigate(new EditLocationsPage(selected_locations.Id));
            }
            else
            {
                MessageBox.Show("Выберите локацию");
            }
        }

        private void DeleteLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected_locations = LocationDG.SelectedItem as Location;

            if (selected_locations != null)
            {
                if (MessageBox.Show("Удалить локацию?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Db.Locations.Remove(selected_locations);
                    Db.SaveChanges();
                }
                LoadData();
            }
            else
            {
                MessageBox.Show("Выберите локацию");
            }
        }
    }
}
