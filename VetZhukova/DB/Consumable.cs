using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetZhukova.DB
{
    public class Consumable
    {
        public int ConsumableID { get; set; }
        public int Quantity { get; set; }
        private string Name, Unit, LastUpdated;

        public string name { get { return Name; } set { Name = value; } }
        public string unit { get { return Unit; } set { Unit = value; } }
        public string lastUpdated { get { return LastUpdated; } set { LastUpdated = value; } }

        public Consumable() { }
        public Consumable(string Name, string Unit, string LastUpdated)
        {
            this.Name = Name;
            this.Unit = Unit;
            this.LastUpdated = LastUpdated;
        }
    }
}
