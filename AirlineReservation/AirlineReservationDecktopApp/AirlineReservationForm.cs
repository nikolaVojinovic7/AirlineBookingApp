/*
    The application is developed by 
        Nikola Vojinovic - 101181089, 
        Cora Dinatele - 100645103
        Tanner Rizapoulos - 101072045
        Jeffrey Taseen 100972776
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace AirlineReservationDecktopApp
{
    public partial class AirlineReservationForm : Form
    {
        private AirlineCoordinator aCoord;  //binding the form to AirlineCoordinator object

        // constructor used to assign aCoord
        public AirlineReservationForm(AirlineCoordinator ac)
        {
            InitializeComponent();
            aCoord = ac;
        }

        //shows message to confirm that user wants to exit the application
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit application?", "Application Reservation System", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //shows message about the application's developers
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aMessage = "The application is developed by\n\tNikola Vojinovic - 101181089"
                            + ",\n\tCora Dinatele - 100645103\n\tTanner Rizapoulos - 101072045\n\tJeffrey Taseen - 100972776";
            MessageBox.Show(aMessage, "About Airline Reservation Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //checks if customer data is empty 
        private bool isCustomerDataOk(string firstName, string lastName, string phoneNumber )
        {
            bool dataIncorrect = string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(phoneNumber);
            if (dataIncorrect)
            {
                string aMessage = "Please provide first name, last name and phone number";
                MessageBox.Show(aMessage, "Error not all values are filled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return !dataIncorrect;
        }

        //when user clicks add button, adds customer to airline coordinator & list view (table in GUI)
        private void customerAddButton_Click(object sender, EventArgs e)
        {
            if(!isCustomerDataOk(customerFirstNameTextBox.Text, customerLastNameTextBox.Text, customerPhoneNumberTextBox.Text))
            {
                return;
            }
            if (aCoord.addCustomer(customerFirstNameTextBox.Text, customerLastNameTextBox.Text, customerPhoneNumberTextBox.Text))
            {
                CustomerListViewItem clvi = new CustomerListViewItem(aCoord.getLastCustomer());
                customerLstView.Items.Add(clvi);
            }
            else
            {
                string aMessage = "Customer was not added..";
                MessageBox.Show(aMessage, "Error");
            }
            customerFirstNameTextBox.ResetText();
            customerLastNameTextBox.ResetText();
            customerPhoneNumberTextBox.ResetText();
        }


        //deletes selected customers from list view
        private void deleteCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (CustomerListViewItem selectedCLVI in customerLstView.SelectedItems)
            {
                if (aCoord.deleteCustomer(selectedCLVI.getID()))
                {
                    customerLstView.Items.Remove(selectedCLVI);
                }
                else
                {
                    string aMessage = "Customer with id " + selectedCLVI.getID() + " could not be deleted..";
                    MessageBox.Show(aMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //deletes selected flights from list view
        private void deleteFlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (FlightListViewItem selectedFLVI in flightListView.SelectedItems)
            {
                if (aCoord.deleteFlight(selectedFLVI.getFlightID()))
                {
                    flightListView.Items.Remove(selectedFLVI);
                }
                else
                {
                    string aMessage = "Flight with flight number " + selectedFLVI.getFlightID() + " could not be deleted..";
                    MessageBox.Show(aMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        //checks if flight data is empty and if it is valid integer
        private bool isFlightDataOk(string flightNumber, string origin, string destination, string maxSeats)
        {

            try
            {
                Int32.Parse(flightNumber);
                Int32.Parse(maxSeats);
            }
            catch
            {
                string aMessage = "Please input a valid number for flight number and max seats.";
                MessageBox.Show(aMessage, "Error not all values are of the correct type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            bool dataIncorrect = string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination);
            if (dataIncorrect)
            {
                string aMessage = "Please provide origin and destination.";
                MessageBox.Show(aMessage, "Error not all values are filled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return !dataIncorrect;
        }

        //when user clicks add button, adds flight to airline coordinator & list view (table in GUI)
        private void addFlightButton_Click(object sender, EventArgs e)
        {
            if(!isFlightDataOk(flightNumberTextBox.Text, originTextBox.Text, destinationTextBox.Text, maxSeatsTextBox.Text))
            {
                return;
            }
            if (aCoord.addFlight(Convert.ToInt32(flightNumberTextBox.Text), originTextBox.Text, 
                destinationTextBox.Text, Convert.ToInt32(maxSeatsTextBox.Text)))
            {
                FlightListViewItem flvi = new FlightListViewItem(aCoord.getLastFlight());
                flightListView.Items.Add(flvi);
            }
            else
            {
                string aMessage = "Flight was not added..";
                MessageBox.Show(aMessage, "Error");
            }
            flightNumberTextBox.ResetText();
            originTextBox.ResetText();
            destinationTextBox.ResetText();
            maxSeatsTextBox.ResetText();
        }

        //populates the comboBox items with valid customer id and flight number when tab is entered
        private void bookingsTab_Enter(object sender, EventArgs e)
        {
            foreach (CustomerListViewItem customerLVI in customerLstView.Items)
            {
                bookingCustomerIdComboBox.Items.Add(customerLVI.getID());
            }

            foreach (FlightListViewItem flightLVI in flightListView.Items)
            {
                bookingFlightNumberComboBox.Items.Add(flightLVI.getFlightID());
            }

        }

        //clears the comboBox items when user leaves the tab
        private void bookingsTab_Leave(object sender, EventArgs e)
        {
            bookingCustomerIdComboBox.Items.Clear();
            bookingFlightNumberComboBox.Items.Clear();
        }

        //when user clicks add button, adds booking to airline coordinator & list view (table in GUI)
        private void bookingAddButton_Click(object sender, EventArgs e)
        {
            if (!isBookingDataOk(bookingFlightNumberComboBox.Text, bookingCustomerIdComboBox.Text))
            {
                return;
            }
            if (aCoord.addBooking(Convert.ToInt32(bookingCustomerIdComboBox.Text), Convert.ToInt32(bookingFlightNumberComboBox.Text)))
            {
                BookingListViewItem blvi = new BookingListViewItem(aCoord.getLastBooking());
                bookingListView.Items.Add(blvi);
            }
            else
            {
                string aMessage = "Booking was not added..";
                MessageBox.Show(aMessage, "Error");
            }
            bookingCustomerIdComboBox.ResetText();
            bookingFlightNumberComboBox.ResetText();
        }

        //checks if booking flight number & customer id are empty 
        private bool isBookingDataOk(string flightNumber, string customerId)
        {

            try
            {
                Int32.Parse(flightNumber);
                Int32.Parse(customerId);
                return true;
            }
            catch
            {
                string aMessage = "Please provide a valid flight number and customer id.";
                MessageBox.Show(aMessage, "Error not all values are valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //deletes selected bookings from list view
        private void deleteBookingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (BookingListViewItem selectedBLVI in bookingListView.SelectedItems)
            {
                if (aCoord.deleteBooking(selectedBLVI.getbookingID()))
                {
                    bookingListView.Items.Remove(selectedBLVI);
                }
                else
                {
                    string aMessage = "Booking with booking number " + selectedBLVI.getbookingID() + " could not be deleted..";
                    MessageBox.Show(aMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        //populates comboBox with valid flight numbers when the tab is entered
        private void viewParticularFlightTab_Enter(object sender, EventArgs e)
        {
            foreach (FlightListViewItem flightLVI in flightListView.Items)
            {
                particularFlightNumberComboBox.Items.Add(flightLVI.getFlightID());
            }
        }

        //clears comboBox items when you leave the tab
        private void viewParticularFlightTab_Leave(object sender, EventArgs e)
        {
            particularFlightNumberComboBox.Items.Clear();           
        }


        //validation for phone number text box 
        private void customerPhoneNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '+') && (e.KeyChar != '-') &&
                (e.KeyChar != ' ') && (e.KeyChar != ')')  && (e.KeyChar != '('))
            {
                e.Handled = true;
            }
        }

        //validation for any text box that takes just letters (last name, origin, destination, first name)
        private void customerLastNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //validation for any text box that takes just digits (flight number and max number of seats)
        private void flightNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //when new item is selected in combo box (drop list) displays the flight information and passenger list 
        private void particularFlightNumberComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            particularFlightListView.Items.Clear();
            Flight selectedFlight = aCoord.getFlight((int)particularFlightNumberComboBox.SelectedItem);
            particularOriginLabelContent.Text = selectedFlight.getOrigin();
            particularDestinationLabelContent.Text = selectedFlight.getDestination();
            particularMaxSeatsLabelContent.Text = Convert.ToString(selectedFlight.getMaxSeats());

            Customer[] flightPassengers = selectedFlight.getCustomers();

            foreach (Customer passenger in flightPassengers)
            {
                if (passenger == null)
                {
                    break;
                }
                CustomerListViewItem clvi = new CustomerListViewItem(passenger); 
                particularFlightListView.Items.Add(clvi);

            }
        }
    }
}
