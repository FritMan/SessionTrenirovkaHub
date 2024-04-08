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
    /// Логика взаимодействия для TeachersDG.xaml
    /// </summary>
    public partial class TeachersDG : Page
    {
        private string file_name;
        public TeachersDG()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Db.Teachers.Load();
        }
        private void AddTeacherBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TeacherEditPage(-1));
        }

        private void EditTeacherBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected_teahcer = TeacherDG.SelectedItem as Teacher;

            if (selected_teahcer != null)
            {
                NavigationService.Navigate(new TeacherEditPage(selected_teahcer.Id));
            }
            else
            {
                MessageBox.Show("Выберите учителя");
            }
        }

        private void DeleteTeacherBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected_teahcer = TeacherDG.SelectedItem as Teacher;

            if (selected_teahcer != null)
            {
                if(MessageBox.Show("Удалить учителя?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Db.Teachers.Remove(selected_teahcer);
                    Db.SaveChanges();
                }
                LoadData();
            }
            else
            {
                MessageBox.Show("Выберите учителя");
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
                                string[] elements = row[0].ToString().Split(' ');
                                var created_Teacher = new Teacher();

                                    if (elements.Length < 3)
                                    {
                                        created_Teacher.Name = elements[0];
                                        created_Teacher.Surname = elements[1];
                                    }
                                    else
                                    {
                                        created_Teacher.Name = elements[0];
                                        created_Teacher.Surname = elements[1];
                                        created_Teacher.Patronymic = elements[2];
                                    }

                                if (Db.Teachers.FirstOrDefault(el => el.Name == created_Teacher.Name && el.Surname == created_Teacher.Surname && el.Patronymic == created_Teacher.Patronymic) != null)
                                {
                                    continue;
                                }
                                else
                                {
                                    Db.Teachers.Add(created_Teacher);
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
            Db.Teachers.Load();
            TeacherDG.ItemsSource = Db.Teachers.ToList();
        }
    }
}
