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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Session1WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();

            DoubleAnimation buttonAnimation = new DoubleAnimation();
            buttonAnimation.From = FunLabel.ActualWidth;
            buttonAnimation.To = 180;
            buttonAnimation.Duration = TimeSpan.FromSeconds(5);
            FunLabel.BeginAnimation(Button.WidthProperty, buttonAnimation);
        }

        private void EventPart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EventMenu());
        }

        private void EducPart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EduMenu());
        }

        private void CulturePart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CultureMenu());
        }

        private void TestBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EventEditPage(-1));
        }
    }
}
