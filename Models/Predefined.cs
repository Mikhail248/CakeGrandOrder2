using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeGrandOrder.Models
{
    class Predefined
    {
        public enum Tags { Weeding, Birthday, birthday50, birthday100 }
        public string Id { get; set; }
        //public Cake cake { get; set; }
        public string name { get; set; }
        public Tags tag { get; set; }
    }
}
