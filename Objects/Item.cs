using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingDemo.Objects
{
    public class Item(string name, int amount, float price)
    {
        public string Name { get; set; } = name;
        public int Amount { get; set; } = amount;
        public float Price { get; set; } = price;
    }
}
