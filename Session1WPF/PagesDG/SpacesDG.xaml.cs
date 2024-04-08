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
    /// Логика взаимодействия для SpacesDG.xaml
    /// </summary>
    public partial class SpacesDG : Page
    {
        private string file_name;
        public SpacesDG()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Db.Spaces.Load();
        }

        private void AddSpaceBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SpacesEditPage(-1));
        }

        private void EditSpaceBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected_space = SpaceDG.SelectedItem as Space;

            if (selected_space != null)
            {
                NavigationService.Navigate(new SpacesEditPage(selected_space.Id));
            }
            else
            {
                MessageBox.Show("Выберите пространство");
            }
        }

        private void DeleteSpaceBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected_space = SpaceDG.SelectedItem as Space;

            if (selected_space != null)
            {
                if (MessageBox.Show("Удалить пространство?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Db.Spaces.Remove(selected_space);
                    Db.SaveChanges();
                }
                LoadData();
            }
            else
            {
                MessageBox.Show("Выберите пространство");
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
                                var created_Spaces = new Space();

                                created_Spaces.Name = row[0].ToString();
                                created_Spaces.Capacity = int.Parse(row[1].ToString());
                                created_Spaces.Description = row[2].ToString();


                                if(Db.Spaces.FirstOrDefault(el => el.Name == created_Spaces.Name && el.Description == created_Spaces.Description) != null)
                                {
                                    continue;
                                }
                                else
                                {
                                    Db.Spaces.Add(created_Spaces);
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
            Db.Spaces.Load();
            SpaceDG.ItemsSource = Db.Spaces.ToList();
        }
    }
}
