using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace Pigalev_travel_around_russia
{
    public partial class Tour
    {
        public string Actual
        {
            get
            {
                if(IsActual == true)
                {
                    return "Актуален";
                }
                else
                {
                    return "Не актуален";
                }
            }
        }
        public SolidColorBrush ForegroundActual
        {
            get
            {
                if (IsActual == true)
                {
                    SolidColorBrush actual = new SolidColorBrush(Color.FromRgb(186, 227, 232));
                    return actual;
                }
                else
                {
                    SolidColorBrush notActual = new SolidColorBrush(Color.FromRgb(227, 30, 36));
                    return notActual;
                }
            }
        }
    }
}
