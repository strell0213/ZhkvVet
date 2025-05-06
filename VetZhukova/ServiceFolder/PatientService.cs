using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetZhukova.DB;

namespace VetZhukova.ServiceFolder
{
    class PatientService
    {
        public PatientService() { }

        public List<Patient> GetPatients(int OwnerID)
        {
            return App.AC.Patients.Where(c => c.OwnerID == OwnerID).ToList();
        }

        public Patient GetPatientByID(int id)
        {
            return App.AC.Patients.Where(c => c.PatientID == id).FirstOrDefault();
        }

        public int AddPatient(string name, string breed, int OwnerID)
        {
            Patient patient = new Patient()
            {
                name = name,
                breed = breed,
                OwnerID = OwnerID,
                //todo Возраст
            };
            App.AC.Patients.Add(patient);
            App.AC.SaveChanges();

            return patient.PatientID;
        }
    }
}
