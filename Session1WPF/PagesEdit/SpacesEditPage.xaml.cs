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
    /// Логика взаимодействия для SpacesEditPage.xaml
    /// </summary>
    public partial class SpacesEditPage : Page
    {
        private long _Id;
        public SpacesEditPage(long Id)
        {
            InitializeComponent();
            _Id = Id;
            LoadData();
        }

        private void LoadData()
        {
            Db.Spaces.Load();

            if (_Id != -1)
            {
                SpacesStack.DataContext = Db.Spaces.FirstOrDefault(el => el.Id == _Id);
            }
            else
            {
                SpacesStack.DataContext = new Space();
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_Id == -1)
                {
                    Db.Spaces.Add(SpacesStack.DataContext as Space);
                }
                Db.SaveChanges();
                NavigationService.Navigate(new SpacesDG());
            }
            catch
            {
                MessageBox.Show("Поле вместимость не может быть текстом, пожалуйста, введите число");
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
