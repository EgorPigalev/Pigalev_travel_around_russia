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

namespace Pigalev_travel_around_russia
{
    /// <summary>
    /// Логика взаимодействия для AddUpdHotelPage.xaml
    /// </summary>
    public partial class AddUpdHotelPage : Page
    {
        public AddUpdHotelPage()
        {
            InitializeComponent();
        }
        public AddUpdHotelPage(Hotel hotel)
        {
            InitializeComponent();
            tbHeader.Text = "Изменение существующего отеля";
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new HotelsPage());
        }
    }
}
