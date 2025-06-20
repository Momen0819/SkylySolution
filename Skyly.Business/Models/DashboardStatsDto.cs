using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyly.Business.Models
{
    public class DashboardStatsDto
    {
        public int HotelsCount { get; set; }
        public int RoomsCount { get; set; }
        public int AdminsCount { get; set; }
        public int BookingsCount { get; set; }
    }

}
