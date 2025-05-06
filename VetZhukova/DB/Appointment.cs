using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetZhukova.DB
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int EmployeeID { get; set; }
        private string AppointmentDate, Status;

        public string appointmentDate { get { return AppointmentDate; } set { AppointmentDate = value; } }
        public string status { get { return Status; } set { Status = value; } }

        public Appointment() { }
        public Appointment(string AppointmentDate, string Status)
        {
            this.AppointmentDate = AppointmentDate;
            this.Status = Status;
        }
    }
}
