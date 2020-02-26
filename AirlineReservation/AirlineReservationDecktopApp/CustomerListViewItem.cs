using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace AirlineReservationDecktopApp
{
    class CustomerListViewItem : ListViewItem
    {
        Customer aCustomer;
        public CustomerListViewItem(Customer c)
        {
            aCustomer = c;
            this.Text = "" + c.getId();
            string[] customerRow = { c.getFirstName(), c.getLastName(), c.getPhone() };
            this.SubItems.AddRange(customerRow);
        }

        public int getID()
        {
            return aCustomer.getId();
        }
    }
}
