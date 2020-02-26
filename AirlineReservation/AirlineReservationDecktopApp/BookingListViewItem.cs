using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace AirlineReservationDecktopApp
{
    class BookingListViewItem : ListViewItem
    {
        
        Booking aBooking;
        public BookingListViewItem(Booking b)
        {
            aBooking = b;
            this.Text = Convert.ToString(b.getNumber());
            string[] flightRow = { Convert.ToString(b.getCustomer().getId()), Convert.ToString(b.getFlight().getFlightNumber()), Convert.ToString(b.getDateTime()) };
            this.SubItems.AddRange(flightRow);
        }

        public int getbookingID()
        {
            return aBooking.getNumber();
        }
    }
}
