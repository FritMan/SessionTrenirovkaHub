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
    /// Логика взаимодействия для EditTypes.xaml
    /// </summary>
    public partial class EditTypes : Page
    {
        private long _Id;
        public EditTypes(long Id)
        {
            InitializeComponent();
            _Id = Id;
            LoadData();
        }

        private void LoadData()
        {

            Db.TypeOfEvents.Load();
            if (_Id != -1)
            {
                TypesStack.DataContext = Db.TypeOfEvents.FirstOrDefault(el => el.Id == _Id);
            }
            else
            {
                TypesStack.DataContext = new TypeOfEvent();
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_Id == -1)
                {
                    Db.TypeOfEvents.Add(TypesStack.DataContext as TypeOfEvent);
                }
                Db.SaveChanges();
                NavigationService.Navigate(new TypesDG());
            }
            catch { }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
