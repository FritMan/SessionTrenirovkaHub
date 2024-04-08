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

namespace Session1WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для EventMenu.xaml
    /// </summary>
    public partial class EventMenu : Page
    {
        public EventMenu()
        {
            InitializeComponent();
        }

        private void SpacesBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SpacesDG());
        }

        private void TypesEventBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TypesDG());
        }
    }
}
