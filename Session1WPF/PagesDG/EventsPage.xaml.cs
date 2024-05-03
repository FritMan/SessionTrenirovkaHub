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
    /// Логика взаимодействия для EventsPage.xaml
    /// </summary>
    public partial class EventsPage : Page
    {
        public EventsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Db.TypeOfEvents.Load();
            Db.Spaces.Load();
            Db.Events.Load();

            EventDG.ItemsSource = Db.Events.ToList();
        }

        private void AddEventBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EventEditPage(-1));
        }

        private void EditEventBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected_event = EventDG.SelectedItem as Event;

            if (selected_event != null)
            {
                NavigationService.Navigate(new EventEditPage(selected_event.Id));
            }
            else
            {
                MessageBox.Show("Выберите мероприятия");
            }
        }

        private void DeletEventBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected_event = EventDG.SelectedItem as Event;

            if (selected_event != null)
            {
                if (MessageBox.Show("Удалить мероприятие?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Db.Events.Remove(selected_event);
                    Db.SaveChanges();
                }
                LoadData();
            }
            else
            {
                MessageBox.Show("Выберите мероприятия");
            }
        }
    }
}
