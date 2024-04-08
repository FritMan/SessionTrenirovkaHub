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
    /// Логика взаимодействия для TypesDG.xaml
    /// </summary>
    public partial class TypesDG : Page
    {
        public TypesDG()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Db.TypeOfEvents.Load();
            EventsTypeDG.ItemsSource = Db.TypeOfEvents.ToList();
        }

        private void TypesAddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditTypes(-1));
        }

        private void TypesEditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedEvent = EventsTypeDG.SelectedItem as TypeOfEvent;

                if (selectedEvent != null)
                {
                    NavigationService.Navigate(new EditTypes(selectedEvent.Id));
                }
                else
                {
                    MessageBox.Show("Выберите тип мероприятия");
                }
            }
            catch { }
        }

        private void TypesDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedEvent = EventsTypeDG.SelectedItem as TypeOfEvent;

                if (selectedEvent != null)
                {
                    if(MessageBox.Show("Удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Db.TypeOfEvents.Remove(selectedEvent);
                        Db.SaveChanges();
                    }
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Выберите тип мероприятия");
                }
            }
            catch { }
        }
    }
}
