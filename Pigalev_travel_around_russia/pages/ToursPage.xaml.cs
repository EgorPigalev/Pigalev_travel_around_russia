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
            tbTotalCost.Text = Convert.ToString(GetTotalCost()) + " РУБ";
        }
        private double GetTotalCost() // Подсчёт общей стоимости туров
        {
            double summa = 0;
            List<Tour> tours = Base.BE.Tour.ToList();
            foreach(Tour tour in tours)
            {
                summa = summa + (double)tour.Price * (double)tour.TicketCount;
            }
            return summa;
        }
    }
}
