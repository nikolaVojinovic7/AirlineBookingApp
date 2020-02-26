/*
    The application is developed by 
        Nikola Vojinovic - 101181089, 
        Cora Dinatele - 100645103
        Tanner Rizapoulos - 101072045
        Jeffrey Taseen 100972776
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationConsoleApp
{
//    AIRLINE COORDINATOR CLASS

class AirlineCoordinator
    {
        FlightManager flManager;
        CustomerManager custManager;
        BookingManager bookingManager;


        public AirlineCoordinator(int custIdSeed, int maxCust, int maxFlights, int bookingIdSeed)
        {
            flManager = new FlightManager(maxFlights);
            custManager = new CustomerManager(custIdSeed, maxCust);
            bookingManager = new BookingManager(bookingIdSeed, maxFlights * maxCust);
        }

        public bool addFlight(int flightNo, string origin, string destination, int maxSeats)
        {
            return flManager.addFlight(flightNo, origin, destination, maxSeats);
        }

        public bool addCustomer(string fname, string lname, string phone)
        {
            return custManager.addCustomer(fname, lname, phone);
        }

        public bool addBooking(int cid, int fn)
        {
            if (custManager.findCustomer(cid) > -1 && flManager.findFlight(fn) > -1)
            {
                Customer bc = custManager.getCustomer(cid);
                Flight bf = flManager.getFlight(fn);
                return bookingManager.addBooking(bc, bf);
            }
            return false;
        }

        public string flightList()
        {
            return flManager.getFlightList();
        }

        public string customerList()
        {
            return custManager.getCustomerList();
        }

        public string bookingList()
        {
            return bookingManager.getBookingList();
        }

        public bool deleteCustomer(int id)
        {
            return custManager.deleteCustomer(id);
        }

        public bool deleteFlight(int fid)
        {
            return flManager.deleteFlight(fid);
        }

        public bool deleteBooking(int bn)
        {
            return bookingManager.deleteBooking(bn);
        }

        public string getPassengerList(int fn)
        {
            Flight f = flManager.getFlight(fn);
            if(f == null) { return null; }
            return (f.getPassengerList());
        }

        public bool canBook(int flightNum, int customerId)
        {
            bool canBook = flManager.flightExists(flightNum) && custManager.customerExist(customerId);

            return canBook;
        }
    }
}
