using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetZhukova.DB
{
    public class Visit
    {
        public int VisitID { get; set; }
        public int PatientID { get; set; }
        public int EmployeeID { get; set; }
        public int ServiceID { get; set; }
        public int Status { get; set; }
        private string VisitDate, Notes;

        public string visitDate { get { return VisitDate; } set { VisitDate = value; } }
        public string notes { get { return Notes; } set { Notes = value; } }

        public Visit() { }

        public Visit(int PatientID, int EmployeeID, int ServiceID, string VisitDate, string Notes, int status)
        {
            this.PatientID = PatientID;
            this.EmployeeID = EmployeeID;
            this.ServiceID = ServiceID;
            this.VisitDate = VisitDate;
            this.Notes = Notes;
            this.Status = status;
        }
    }
}
