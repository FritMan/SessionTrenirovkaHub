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
    /// Логика взаимодействия для TeacherEditPage.xaml
    /// </summary>
    public partial class TeacherEditPage : Page
    {
        private long _Id;
        public TeacherEditPage(long Id)
        {
            InitializeComponent();
            _Id = Id;
            LoadData();
        }

        private void LoadData()
        {
           Db.Teachers.Load();

            if (_Id != -1)
            {
                TeacherStack.DataContext = Db.Teachers.FirstOrDefault(el => el.Id == _Id);
            }
            else
            {
                TeacherStack.DataContext = new Teacher();
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_Id == -1)
                {
                    Db.Teachers.Add(TeacherStack.DataContext as Teacher);
                }
                Db.SaveChanges();
                NavigationService.Navigate(new TeachersDG());
            }
            catch
            { }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
