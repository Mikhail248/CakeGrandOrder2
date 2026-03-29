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
        public List<Cake> Cakes { get; set; }
        public DateTime Date { get; set; }
    }
}
