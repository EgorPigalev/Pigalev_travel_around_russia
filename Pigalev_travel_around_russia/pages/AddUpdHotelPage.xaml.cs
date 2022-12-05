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
            tbDescription.Text = hotel.Description;
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

        private void btnAddUpd_Click(object sender, RoutedEventArgs e) // добавление или изменение отеля в базе
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbName.Text))
                {
                    MessageBox.Show("Наименование отеля должно быть заполнено!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(tbCountOfStars.Text))
                {
                    MessageBox.Show("Количество звёзд у отеля должно быть заполнено!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(cbCountry.Text))
                {
                    MessageBox.Show("Страна отеля должна быть указана!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(tbDescription.Text))
                {
                    MessageBox.Show("Поле описание должно быть заполнено!");
                    return;
                }
                if (flagUpdate == false)
                {
                    hotel = new Hotel();
                }
                hotel.Name = Convert.ToString(tbName.Text);
                hotel.CountOfStars = Convert.ToInt32(tbCountOfStars.Text);
                hotel.CountryCode = Convert.ToString(cbCountry.SelectedValue);
                hotel.Description = Convert.ToString(tbDescription.Text);
                if (flagUpdate == false)
                {
                    Base.BE.Hotel.Add(hotel);
                }
                Base.BE.SaveChanges();
                if (flagUpdate == true)
                {
                    MessageBox.Show("Запись успешно изменена");
                }
                else
                {
                    MessageBox.Show("Запись добавлена в базу");
                }
                FrameClass.MainFrame.Navigate(new HotelsPage());
            }
            catch
            {
                if (flagUpdate == true)
                {
                    MessageBox.Show("При изменение данных возникла ошибка");
                }
                else
                {
                    MessageBox.Show("При добавление данных возникла ошибка");
                }
            }
        }
    }
}
