using Microsoft.EntityFrameworkCore;
using Session1WPF.Data;
using Session1WPF.PagesDG;
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

namespace Session1WPF.PagesEdit
{

    /// <summary>
    /// Логика взаимодействия для EditLocationsPage.xaml
    /// </summary>
    public partial class EditLocationsPage : Page
    {
        private long _id;
        public EditLocationsPage(long Id)
        {
            InitializeComponent();
            _id = Id;
            LoadData();
        }

        private void LoadData()
        {
            Db.Locations.Load();

            if(_id != -1)
            {
                LocationsStack.DataContext = Db.Locations.FirstOrDefault(el => el.Id == _id);
            }
            else
            {
                LocationsStack.DataContext = new Location();
            }

        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string msg = CheckErrors(LocationsStack.Children);
                if (msg != "")
                {
                    MessageBox.Show(msg);
                    return;
                }
                
                if (_id == -1)
                {
                    Db.Locations.Add(LocationsStack.DataContext as Location);
                }
                Db.SaveChanges();
                NavigationService.Navigate(new LocationsDG());
            }
            catch
            {
                MessageBox.Show("В поля кол-во рядом и кол-во мест в ряду необходимо ввести число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
