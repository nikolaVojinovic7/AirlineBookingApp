using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationDecktopApp
{
 // NV  Booking CLASS


 public class Booking
    {
        private Customer bookedCustomer;
        private Flight bookedFlight;
        private DateTime bokingDateTime;
        private int bookingNumber;

        public Booking(int bn, Customer bc, Flight bf)
        {
            bookedCustomer = bc;
            bookedFlight = bf;
            bokingDateTime = DateTime.Now;
            bookingNumber = bn;
        }

        public int getNumber() { return bookingNumber; }
        public Customer getCustomer() { return bookedCustomer; }
        public Flight getFlight() { return bookedFlight; }
        public DateTime getDateTime() { return bokingDateTime; }
        public string toString()
        {
            string s = "Booking " + bookingNumber;
            s = s + "\nBooked at: " + bokingDateTime.ToString(@"MM\/dd\/yyyy h\:mm tt");
            s = s + "\n\n " + bookedCustomer.toString();
            s = s + "\n\n " + bookedFlight.toString();

            return s;
        }

    }
}
