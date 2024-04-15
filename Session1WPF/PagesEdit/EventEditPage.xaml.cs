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

namespace Session1WPF.PagesEdit
{
    /// <summary>
    /// Логика взаимодействия для EventEditPage.xaml
    /// </summary>
    public partial class EventEditPage : Page
    {
        public EventEditPage()
        {
            InitializeComponent();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LocationVisibBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PaidBtn_Checked(object sender, RoutedEventArgs e)
        {
            int Prof = 0;
            if(PaidBtn.IsChecked == true)
            {
                PaidTb.IsReadOnly = false;
                // признак платноси поставить 1
            }
            else
            {
                // признак платности поставить 0
                PaidTb.Text = "0";
                PaidTb.IsReadOnly = true;
            }
        }
    }
}
