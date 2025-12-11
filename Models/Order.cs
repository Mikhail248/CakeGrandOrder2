using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeGrandOrder.Models
{
    class Order
    {
        public string Id { get; set; }
        public Cake cake { get; set; }
        public DateTime date { get; set; }
    }
}
