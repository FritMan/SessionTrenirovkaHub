﻿using System;
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

namespace Session1WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if(MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReafreshAll();
        }
    }
}
