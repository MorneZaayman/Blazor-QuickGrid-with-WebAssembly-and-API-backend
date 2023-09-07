using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGrid.Shared.Dtos
{
    public class CustomersWrapper
    {
        public int TotalCustomerCount { get; set; }

        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}
