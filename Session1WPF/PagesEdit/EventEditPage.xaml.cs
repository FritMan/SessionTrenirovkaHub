using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Session1WPF.Data;
using Session1WPF.PagesDG;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для EventEditPage.xaml
    /// </summary>
    public partial class EventEditPage : Page
    {
        private long _id;
        public EventEditPage(long Id)
        {
            InitializeComponent();
            _id = Id;
            LoadData();
        }

        private void LoadData()
        {
            Db.TypeOfEvents.Load();
            Db.Spaces.Load();
            Db.Events.Load();

            TypeCb.ItemsSource = Db.TypeOfEvents.ToList();
            SpacesCb.ItemsSource = Db.Spaces.ToList();

            if(_id != -1)
            {
                EventStack.DataContext = Db.Events.FirstOrDefault(el => el.Id == _id);
                DateCmb.SelectedDate = DateTime.Parse(Db.Events.First(el => el.Id == _id).DateStart);

            }
            else
            {
                EventStack.DataContext = new Event();
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (_id == -1)
                {
                    if(TimeStartTb.Text.Contains(":") && TimeEndTb.Text.Contains(":"))
                    {
                        bool NotError = true;
                        for (int i = 0; i < TimeStartTb.Text.Length; i++)
                        {
                            if (TimeStartTb.Text[i].ToString() != ":")
                            {
                                NotError = char.IsDigit(TimeStartTb.Text[i]);
                                if(NotError == false)
                                {
                                    MessageBox.Show("Поажлуйста, введите время в указанном формате с двоеточием и в виде цифр");
                                    break;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }

                        for (int i = 0; i < TimeEndTb.Text.Length; i++)
                        {
                            if (TimeEndTb.Text[i].ToString() != ":")
                            {
                                NotError = char.IsDigit(TimeEndTb.Text[i]);
                                if (NotError == false)
                                {
                                    MessageBox.Show("Поажлуйста, введите время в указанном формате с двоеточием и в виде цифр");
                                    break;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }

                        if (NotError == true && TimeEndTb.Text.Length == TimeStartTb.Text.Length)
                        {
                            var created_event = EventStack.DataContext as Event;
                            created_event.DateStart = DateCmb.SelectedDate.Value.ToString("d");
                            Db.Events.Add(created_event);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, введите время в указаном формате с двоеточием");
                    }
                    
                }

                Db.SaveChanges();
                NavigationService.Navigate(new EventsPage());
            }
            catch(Exception ex)
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
