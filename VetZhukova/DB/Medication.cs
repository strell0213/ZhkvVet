using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetZhukova.DB
{
    public class Medication
    {
        public int MedicationID { get; set; }
        public int Quantity { get; set; }
        private string Name, Unit, ExpirationDate, LastUpdated;

        public string name { get { return Name; } set { Name = value; } }
        public string unit { get { return Unit; } set { Unit = value; } }
        public string expirationDate { get { return ExpirationDate; } set { ExpirationDate = value; } }
        public string lastUpdated { get { return LastUpdated; } set { LastUpdated = value; } }

        public Medication() { }
        public Medication(string Name, string Unit, string ExpirationDate, string LastUpdated)
        {
            this.Name = Name;
            this.Unit = Unit;
            this.ExpirationDate = ExpirationDate;
            this.LastUpdated = LastUpdated;
        }
    }
}
