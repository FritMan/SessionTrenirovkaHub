using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Session1WPF.Data;
using Session1WPF.PagesEdit;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Логика взаимодействия для StudiosDG.xaml
    /// </summary>
    public partial class StudiosDG : Page
    {
        private string file_name;
        public StudiosDG()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Db.Studios.Load();
        }

        private void AddStuidosBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StudiosEditPage(-1));
        }

        private void EditStudiosBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected_studio = StudioDG.SelectedItem as Studio;

            if (selected_studio != null)
            {
                NavigationService.Navigate(new StudiosEditPage(selected_studio.Id));
            }
            else
            {
                MessageBox.Show("Выберите студию");
            }
        }

        private void DeleteStudiosBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected_studios = StudioDG.SelectedItem as Studio;

            if (selected_studios != null)
            {
                if (MessageBox.Show("Удалить студию?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Db.Studios.Remove(selected_studios);
                    Db.SaveChanges();
                }
                LoadData();
            }
            else
            {
                MessageBox.Show("Выберите студию");
            }
        }

        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Dio = new OpenFileDialog();
            Dio.Filter = "*.xls|*.xls";
            Dio.Multiselect = false;
            int count_first = 0;
            if (Dio.ShowDialog() == true)
            {
                file_name = Dio.FileName;
            }

            using (var item = File.Open(file_name, FileMode.Open))
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var reader = ExcelReaderFactory.CreateReader(item))
                {
                    var result = reader.AsDataSet();

                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        try
                        {
                            if (count_first != 0)
                            {
                                var created_Studio = new Studio();

                                created_Studio.Name = row[0].ToString();
                                created_Studio.Description = row[1].ToString();


                                var searched_studio = Db.Studios.FirstOrDefault(el => el.Name == created_Studio.Name && el.Description == created_Studio.Description);

                                if (searched_studio != null)
                                {
                                    continue;
                                }
                                else
                                {
                                    Db.Studios.Add(created_Studio);
                                    Db.SaveChanges();
                                }
                                LoadData();
                            }
                            else
                            {
                                count_first++;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Таблица не подходит по формату");
                        }
                    }
                }
            }
            Db.Studios.Load();
            StudioDG.ItemsSource = Db.Studios.ToList();
        }
    }
}
