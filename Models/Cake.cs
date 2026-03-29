using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeGrandOrder.Models
{
    class Cake
    {
        public string Id { get; set; }
        public string CakeName { get; set; }
        public int Price { get; set; }
        public int Fat { get; set; }
        public int Sugar { get; set; }
        public int CakeBase { get; set; }
        public int CakeFilling { get; set; }
        public int BaseNum { get; set; }
        public int Construction { get; set; }
        public int Top { get; set; }

    }
}
