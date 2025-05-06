using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetZhukova.DB
{
    public class VisitMedication
    {
        public int VisitMedicationID { get; set; }
        public int VisitID { get; set; }
        public int MedicationID { get; set; }
        public int QuantityUsed { get; set; }

        public VisitMedication() { }

        public VisitMedication(int VisitID, int MedicationID, int QuantityUsed)
        {
            this.VisitID = VisitID;
            this.MedicationID = MedicationID;
            this.QuantityUsed = QuantityUsed;
        }
    }
}
