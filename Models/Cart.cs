using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeGrandOrder.Models
{
    internal class Cart
    {
        public string Id { get; set; }
        public List<Order> CartOrders { get; set; }
    }
}
