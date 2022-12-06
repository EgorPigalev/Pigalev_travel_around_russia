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
    /// Логика взаимодействия для HotelsPage.xaml
    /// </summary>
    public partial class HotelsPage : Page
    {
        PageChange pc = new PageChange();
        List<Hotel> HotelsFilter = new List<Hotel>();
        public HotelsPage()
        {
            InitializeComponent();
            uploadFields();            
            tbChangeCount.Text = "10";
            pc.VisibleButton[0] = "Hidden";
            if (pc.CountPages > 5)
            {
                pc.VisibleButton[1] = "Visible";
            }
            else
            {
                pc.VisibleButton[1] = "Hidden";
            }
        }
        public void uploadFields()
        {
            dgHotel.ItemsSource = Base.BE.Hotel.ToList();
            tbCountRecords.Text = Convert.ToString(Base.BE.Hotel.ToList().Count);
            HotelsFilter = Base.BE.Hotel.ToList();
            pc.CountPage = Base.BE.Hotel.ToList().Count;
            tbCountPages.Text = pc.CountPages.ToString();
            tbСurrentPage.Text = pc.CurrentPage.ToString();
            DataContext = pc;
        }
        public HotelsPage(string count)
        {
            InitializeComponent();
            uploadFields();
            tbChangeCount.Text = count;
            pc.VisibleButton[0] = "Hidden";
            if (pc.CountPages > 5)
            {
                pc.VisibleButton[1] = "Visible";
            }
            else
            {
                pc.VisibleButton[1] = "Hidden";
            }
        }

        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            switch (tb.Uid)
            {
                case "prev":
                    pc.CurrentPage--;
                    break;
                case "next":
                    pc.CurrentPage++;
                    break;
                default:
                    pc.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            dgHotel.ItemsSource = HotelsFilter.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();
            tbСurrentPage.Text = pc.CurrentPage.ToString();
        }

        private void tbChangeCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }

        private void txtNextFirst_MouseDown(object sender, MouseButtonEventArgs e) // Переход к первой странице
        {
            pc.CurrentPage = 1;
            dgHotel.ItemsSource = HotelsFilter.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();
            tbСurrentPage.Text = pc.CurrentPage.ToString();
        }

        private void txtNextLast_MouseDown(object sender, MouseButtonEventArgs e) // Переход к последней странице
        {
            pc.CurrentPage = pc.CountPages;
            dgHotel.ItemsSource = HotelsFilter.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();
            tbСurrentPage.Text = pc.CurrentPage.ToString();
        }
        bool First = false;
        private void tbChangeCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                pc.CountPage = Convert.ToInt32(tbChangeCount.Text);
            }
            catch
            {
                pc.CountPage = HotelsFilter.Count;
            }
            pc.Countlist = HotelsFilter.Count;
            dgHotel.ItemsSource = HotelsFilter.Skip(0).Take(pc.CountPage).ToList();
            if (First == true)
            {
                pc.CurrentPage = 1;
            }
            else
            {
                First = true;
            }
            tbCountPages.Text = pc.CountPages.ToString();
            tbСurrentPage.Text = pc.CurrentPage.ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new AddUpdHotelPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new ToursPage());
        }

        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Hotel hotel = Base.BE.Hotel.FirstOrDefault(x => x.Id == index);
            FrameClass.MainFrame.Navigate(new AddUpdHotelPage(hotel));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) // Удаление выделенных отелей
        {
            if(dgHotel.SelectedItems.Count == 0)
            {
                MessageBox.Show("Для удаления не выбран ни один элемент");
            }
            else
            {
                int k = 0; // Колличество удалённых отелей
                foreach (Hotel hotel in dgHotel.SelectedItems)
                {
                    List<HotelOfTour> hotelOfTour = Base.BE.HotelOfTour.Where(x => x.HotelId == hotel.Id).ToList(); // Проверка что отель не входит в число подходящих для актуальных туров
                    foreach (HotelOfTour hotelOfTour1 in hotelOfTour)
                    {
                        if (hotelOfTour1.Tour.IsActual == true)
                        {
                            MessageBox.Show("Отель: \"" + hotel.Name + "\" не  может быть удалён так как он находится в числе подходящих отелей для актуальных туров");
                            return;
                        }
                    }
                    // Удаление отеля
                    if (MessageBox.Show("Вы уверены что хотите удалить отель: \"" + hotel.Name + "\"?", "Системное сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        Base.BE.Hotel.Remove(hotel);
                        MessageBox.Show("Отель: \"" + hotel.Name + "\" был удалён");
                        k++;
                    }
                }
                if(k != 0)
                {
                    Base.BE.SaveChanges();
                    FrameClass.MainFrame.Navigate(new HotelsPage(tbChangeCount.Text));
                }
            }
        }
    }
}
