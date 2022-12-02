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
            dgHotel.ItemsSource = Base.BE.Hotel.ToList();
            tbCountRecords.Text = Convert.ToString(Base.BE.Hotel.ToList().Count);
            HotelsFilter = Base.BE.Hotel.ToList();
            pc.CountPage = Base.BE.Hotel.ToList().Count;
            tbCountPages.Text = pc.CountPages.ToString();
            tbСurrentPage.Text = pc.CurrentPage.ToString();
            pc.VisibleButton[0] = "Hidden";
            pc.VisibleButton[1] = "Hidden";
            DataContext = pc;
            tbChangeCount.Text = "10";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new ToursPage());
        }

        bool First = false;

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
    }
}
