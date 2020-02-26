using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationDecktopApp
{
//    Booking MANAGER CLASS

class BookingManager
    {
        private static int currentBookingNumber;
        private int maxNumBookings;
        private int numBookings;
        private Booking[] bookingList;

        public BookingManager(int cbn, int max)
        {
            currentBookingNumber = cbn;
            maxNumBookings = max;
            numBookings = 0;
            bookingList = new Booking[maxNumBookings];
        }

        public bool addBooking(Customer bc, Flight bf)
        {
            if (bf.addPassenger(bc) && numBookings < maxNumBookings)
            {
                bc.bookingAdded();
                Booking b = new Booking(currentBookingNumber, bc, bf);
                currentBookingNumber++;
                bookingList[numBookings] = b;
                numBookings++;
                return true;
            }
            return false;
        }

        public int findBooking(int bn)
        {
            for (int x = 0; x < numBookings; x++)
            {
                if (bookingList[x].getNumber() == bn)
                    return x;
            }
            return -1;
        }

        public bool BookingExist(int bn)
        {
            int loc = findBooking(bn);
            if (loc == -1) { return false; }
            return true;
        }

        public Booking getBooking(int bn)
        {
            int loc = findBooking(bn);
            if (loc == -1) { return null; }
            return bookingList[loc];
        }

        public bool deleteBooking(int bn)
        {
            int loc = findBooking(bn);
            if (loc == -1) { return false; }
            Flight bf = bookingList[loc].getFlight();
            Customer bc = bookingList[loc].getCustomer();

            if (bf.removePassenger(bc.getId()))
            {
                bc.bookingRemoved();
                bookingList[loc] = bookingList[numBookings - 1];
                numBookings--;
                return true;
            }
            return false;
        }

        public string getBookingList()
        {
            string s = "Booking List:";
            s = s + "\nNumber \t Time \t Cutmer Id  \t Flight Number";
            for (int x = 0; x < numBookings; x++)
            {
                s = s + "\n" + bookingList[x].getNumber() + "\t" + bookingList[x].getDateTime() + "\t" + bookingList[x].getCustomer().getId() + "\t" + bookingList[x].getFlight().getFlightNumber();
            }
            return s;
        }

        public Booking getLastBooking() { return bookingList[numBookings - 1]; }

    }
}
