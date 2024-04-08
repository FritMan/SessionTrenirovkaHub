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
    /// Логика взаимодействия для StudiosEditPage.xaml
    /// </summary>
    public partial class StudiosEditPage : Page
    {
        private long _Id;

        public StudiosEditPage(long Id)
        {
            InitializeComponent();
            _Id = Id;
            LoadData();
        }

        private void LoadData()
        {
            Db.Studios.Load();

            if (_Id != -1)
            {
                StudiosStack.DataContext = Db.Studios.FirstOrDefault(el => el.Id == _Id);
            }
            else
            {
                StudiosStack.DataContext = new Studio();
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_Id == -1)
                {
                    Db.Studios.Add(StudiosStack.DataContext as Studio);
                }
                Db.SaveChanges();
                NavigationService.Navigate(new StudiosDG());
            }
            catch
            {
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
