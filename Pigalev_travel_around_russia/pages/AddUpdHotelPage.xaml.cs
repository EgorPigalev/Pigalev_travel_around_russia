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

namespace Pigalev_travel_around_russia
{
    /// <summary>
    /// Логика взаимодействия для AddUpdHotelPage.xaml
    /// </summary>
    public partial class AddUpdHotelPage : Page
    {
        Hotel hotel;
        bool flagUpdate = false;
        private void UploadFields()
        {
            cbCountry.ItemsSource = Base.BE.Country.ToList();
            cbCountry.SelectedValuePath = "Code";
            cbCountry.DisplayMemberPath = "Name";
            cbCountry.SelectedIndex = 0;
        }
            public AddUpdHotelPage()
        {
            InitializeComponent();
            UploadFields();

        }
        public AddUpdHotelPage(Hotel hotel)
        {
            InitializeComponent();
            UploadFields();
            this.hotel = hotel;
            flagUpdate = true;
            tbHeader.Text = "Изменение существующего отеля";
            btnAddUpd.Content = "Измененить отель";
            tbName.Text = hotel.Name;
            tbCountOfStars.Text = Convert.ToString(hotel.CountOfStars);
            cbCountry.SelectedValue = hotel.CountryCode;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new HotelsPage());
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!((e.Text == "0") || (e.Text == "1") || (e.Text == "2") || (e.Text == "3") || (e.Text == "4") || (e.Text == "5")))
            {
                e.Handled = true;
            }
        }
    }
}
