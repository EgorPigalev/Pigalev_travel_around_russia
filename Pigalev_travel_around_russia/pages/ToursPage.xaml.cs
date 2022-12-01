using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    {
        public ToursPage()
        {
            InitializeComponent();
            lvListTour.ItemsSource = Base.BE.Tour.ToList();
            List<Type> types = Base.BE.Type.ToList();
            cbType.Items.Add("Все типы");
            for (int i = 0; i < types.Count; i++)
            {
                cbType.Items.Add(types[i].Name);
            }
            cbType.SelectedIndex = 0;
            cbSorting.SelectedIndex = 0;
            tbTotalCost.Text = GetTotalCost(Base.BE.Tour.ToList()).ToString("F3") + " РУБ";
        }
        private double GetTotalCost(List<Tour> tours) // Подсчёт общей стоимости туров
        {
            double summa = 0;
            foreach(Tour tour in tours)
            {
                summa = summa + (double)tour.Price * (double)tour.TicketCount;
            }
            return summa;
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }
        private List<Tour> GetTourType(int index) // метод для нахождения туров с заданным типом (index - id типа тура)
        {
            List<Tour> tours = new List<Tour>();
            List<TypeOfTour> typeOfTours = Base.BE.TypeOfTour.ToList();
            foreach(TypeOfTour typeOfTour in typeOfTours)
            {
                if(typeOfTour.TypeId == index)
                {
                    tours.Add(typeOfTour.Tour);
                }
            }
            tours.Distinct();
            return tours;
        }
        private List<Tour> GetTourDescription(List<Tour> tours, string Description) // метод для нахождения туров с таким описанием
        {
            List<Tour> toursDescription = new List<Tour>();
            foreach(Tour tour in tours)
            {
                if(tour.Description != null)
                {
                    if (tour.Description.ToLower().Contains(Description.ToLower()))
                    {
                        toursDescription.Add(tour);
                    }
                }
            }
            return toursDescription;
        }

        void Filter()  // метод для одновременной фильтрации, поиска и сортировки
        {
            List<Tour> tours = new List<Tour>();

            string nameType = cbType.SelectedValue.ToString();
            int index = cbType.SelectedIndex;
            if (index != 0)
            {
                index = Base.BE.Type.FirstOrDefault(x => x.Name == nameType).Id;
                tours = GetTourType(index);
            }
            else
            {
                tours = Base.BE.Tour.ToList();
            }
            if (!string.IsNullOrWhiteSpace(tbSearchName.Text)) // Поиск по наименованию
            {
                tours = tours.Where(x => x.Name.ToLower().Contains(tbSearchName.Text.ToLower())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(tbSearchDescription.Text)) // Поиск по описанию
            {
                tours = GetTourDescription(tours, tbSearchDescription.Text);
            }
            if (cbActual.IsChecked == true)
            {
                tours = tours.Where(x => x.IsActual == true).ToList();
            }
            switch (cbSorting.SelectedIndex)
            {
                case 1:
                    {
                        tours.Sort((x, y) => x.Price.CompareTo(y.Price));
                    }
                    break;
                case 2:
                    {
                        tours.Sort((x, y) => x.Price.CompareTo(y.Price));
                        tours.Reverse();
                    }
                    break;
            }
            lvListTour.ItemsSource = tours;
            if (tours.Count == 0)
            {
                MessageBox.Show("В базе данных отсутствуют данные удовлетворяющие заданным условиям");
            }
            tbTotalCost.Text = GetTotalCost(tours).ToString("F3") + " РУБ";
        }

        private void cbActual_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new HotelsPage());
        }
    }
}
