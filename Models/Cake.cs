using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeGrandOrder.Models
{
    class Cake
    {
        public Cake()
        {
        }

        public Cake(string id, string cakeName, int price, int fat, int sugar, int cakeBase, int cakeFilling, int baseNum, int construction, int top)
        {
            Id = id;
            CakeName = cakeName;
            Price = price;
            Fat = fat;
            Sugar = sugar;
            CakeBase = cakeBase;
            CakeFilling = cakeFilling;
            BaseNum = baseNum;
            Construction = construction;
            Top = top;
        }

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

        public bool W1 { get { if (BaseNum >= 1) { return true; } return false; } }
        public bool W2 { get { if (BaseNum >= 2) { return true; } return false; } }
        public bool W3 { get { if (BaseNum >= 3) { return true; } return false; } }
        public bool B1 { get { if (BaseNum >= 1) { return true; } return false; } }
        public bool B2 { get { if (BaseNum >= 2) { return true; } return false; } }
        public bool B3 { get { if (BaseNum >= 3) { return true; } return false; } }
        public int Con1 { get { if (Construction == 1) { return 200; } return 200; } }
        public int Con2 { get { if (Construction == 1) { return 200; } return 100; } }
        public int Con3 { get { if (Construction == 1) { return 200; } return 50; } }

    }
}
