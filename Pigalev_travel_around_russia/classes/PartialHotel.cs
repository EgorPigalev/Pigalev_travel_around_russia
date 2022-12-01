using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigalev_travel_around_russia
{
    public partial class Hotel
    {
        public int CountTour
        {
            get
            {
                List<HotelOfTour> list = Base.BE.HotelOfTour.Where(x => x.HotelId == Id).ToList();
                return list.Count;
            }
        }
    }
}
