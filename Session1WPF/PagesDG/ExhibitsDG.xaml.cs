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
    /// Логика взаимодействия для ExhibitsDG.xaml
    /// </summary>
    public partial class ExhibitsDG : Page
    {
        private string file_name;
        public ExhibitsDG()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            Db.Exhibits.Load();
            Db.Studios.Load();
        }

        private void DeletExhibitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected_Exhibit = ExhibitDG.SelectedItem as Exhibit;

                if (selected_Exhibit != null)
                {
                    if(MessageBox.Show("Удалить экспонат?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Db.Exhibits.Remove(selected_Exhibit);
                        Db.SaveChanges();
                    }
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Выберите экспонат");
                }
            }
            catch { }
        }

        private void EditExhibitBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected_Exhibit = ExhibitDG.SelectedItem as Exhibit;

            if (selected_Exhibit != null)
            {
                NavigationService.Navigate(new ExhibitsEditPage(selected_Exhibit.Id));
            }
            else
            {
                MessageBox.Show("Выберите экспонат");
            }
        }

        private void AddExhibitBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ExhibitsEditPage(-1));
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
                                Db.Studios.Load();
                                var created_Exhibit = new Exhibit();

                                created_Exhibit.Name = row[0].ToString();
                                var searched_Studio = Db.Studios.FirstOrDefault(el => el.Name == row[1].ToString());

                                if(searched_Studio != null)
                                {
                                    created_Exhibit.Studios = searched_Studio;
                                }
                                else
                                {
                                    created_Exhibit.Studios = new Studio();
                                }

                                if(Db.Exhibits.FirstOrDefault(el => el.Name == created_Exhibit.Name && el.Studios == created_Exhibit.Studios) != null)
                                {
                                    continue;
                                }
                                else
                                {
                                    Db.Exhibits.Add(created_Exhibit);
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
            Db.Exhibits.Load();
            ExhibitDG.ItemsSource = Db.Exhibits.ToList();
        }
    }
}
