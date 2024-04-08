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
    /// Логика взаимодействия для ExhibitsEditPage.xaml
    /// </summary>
    public partial class ExhibitsEditPage : Page
    {
        private long _Id;
        public ExhibitsEditPage(long Id)
        {
            InitializeComponent();
            _Id = Id;
            LoadData();
        }

        private void LoadData()
        {
           Db.Exhibits.Load();
           Db.Studios.Load();
           StudioCb.ItemsSource = Db.Studios.ToList();
            
            if(_Id != -1)
            {
                ExhibitStack.DataContext = Db.Exhibits.FirstOrDefault(el => el.Id == _Id);
            }
            else
            {
                ExhibitStack.DataContext = new Exhibit();
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_Id == -1)
                {
                    Db.Exhibits.Add(ExhibitStack.DataContext as Exhibit);
                }
                Db.SaveChanges();
                NavigationService.GoBack();
            }
            catch 
            {
                MessageBox.Show("Кнопка ок");
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
