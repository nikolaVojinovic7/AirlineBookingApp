using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace AirlineReservationDecktopApp
{
    class FlightListViewItem : ListViewItem
    {
        Flight aFlight;
        public FlightListViewItem(Flight f)
        {
            aFlight = f;
            this.Text = Convert.ToString(f.getFlightNumber());
            string[] flightRow = { f.getOrigin(), f.getDestination(), Convert.ToString(f.getMaxSeats()) };
            this.SubItems.AddRange(flightRow);
        }

        public int getFlightID()
        {
            return aFlight.getFlightNumber();
        }
    }
}
