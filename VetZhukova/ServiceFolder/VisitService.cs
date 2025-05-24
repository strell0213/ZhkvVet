using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetZhukova.DB;

namespace VetZhukova
{
    public class VisitDTO
    {
        public int VisitID { get; set; }
        public string VisitDate { get; set; }
        public string ServiceName { get; set; }
        public string PatientName { get; set; }
        public string EmployeeName { get; set; }
    }
    class VisitService
    {
        public VisitService() { }

        public List<VisitDTO> GetLastVisit()
        {
            var listVisit = App.AC.Visits
                .AsEnumerable()
                .Where(v => DateTime.TryParse(v.visitDate, out DateTime visitDate) &&
                            visitDate >= DateTime.Now &&
                            visitDate < DateTime.Now.AddDays(7) &&
                            v.Status == 1)
                .Join(App.AC.Services, v => v.ServiceID, s => s.ServiceID, (v, s) => new { v, s })
                .Join(App.AC.Patients, vs => vs.v.PatientID, p => p.PatientID, (vs, p) => new { vs, p })
                .Join(App.AC.Employees, vsp => vsp.vs.v.EmployeeID, e => e.EmployeeID, (vsp, e) => new
                {
                    VisitDate = vsp.vs.v.visitDate,
                    ServiceName = vsp.vs.s.serviceName,
                    PatientName = vsp.p.name,
                    EmployeeName = e.fullName,
                    VisitID = vsp.vs.v.VisitID
                })
                .ToList()
                .Select(v => new VisitDTO 
                {
                    VisitDate = v.VisitDate,
                    ServiceName = v.ServiceName,
                    PatientName = v.PatientName,
                    EmployeeName = v.EmployeeName,
                    VisitID = v.VisitID
                })
                .ToList();

            return listVisit;
        }

        public List<VisitDTO> GetAllVisit()
        {
            var listVisit = App.AC.Visits
                .Where(c => c.Status == 1)
                .Join(App.AC.Services, v => v.ServiceID, s => s.ServiceID, (v, s) => new { v, s })
                .Join(App.AC.Patients, vs => vs.v.PatientID, p => p.PatientID, (vs, p) => new { vs, p })
                .Join(App.AC.Employees, vsp => vsp.vs.v.EmployeeID, e => e.EmployeeID, (vsp, e) => new
                {
                    VisitDate = vsp.vs.v.visitDate,
                    ServiceName = vsp.vs.s.serviceName,
                    PatientName = vsp.p.name,
                    EmployeeName = e.fullName,
                    VisitID = vsp.vs.v.VisitID
                })
                .ToList()
                .Select(v => new VisitDTO  // Преобразуем в нужный формат после выполнения запроса
                {
                    VisitDate = v.VisitDate,
                    ServiceName = v.ServiceName,
                    PatientName = v.PatientName,
                    EmployeeName = v.EmployeeName,
                    VisitID = v.VisitID
                })
                .ToList(); 

            return listVisit;
        }

        public List<VisitDTO> GetDoneVisitPatient()
        {
            var listVisit = App.AC.Visits
                .AsEnumerable()
                .Where(v => v.Status == 2)
                .Join(App.AC.Services, v => v.ServiceID, s => s.ServiceID, (v, s) => new { v, s })
                .Join(App.AC.Patients, vs => vs.v.PatientID, p => p.PatientID, (vs, p) => new { vs, p })
                .Join(App.AC.Employees, vsp => vsp.vs.v.EmployeeID, e => e.EmployeeID, (vsp, e) => new
                {
                    VisitDate = vsp.vs.v.visitDate,
                    ServiceName = vsp.vs.s.serviceName,
                    PatientName = vsp.p.name,
                    EmployeeName = e.fullName,
                    VisitID = vsp.vs.v.VisitID
                })
                .ToList()
                .OrderBy(x => DateTime.Parse(x.VisitDate))
                .Select(v => new VisitDTO
                {
                    VisitDate = v.VisitDate,
                    ServiceName = v.ServiceName,
                    PatientName = v.PatientName,
                    EmployeeName = v.EmployeeName,
                    VisitID = v.VisitID
                })
                .ToList();

            return listVisit;
        }

        public List<VisitDTO> GetDoneVisitDTOByPatient(int id)
        {
            var listVisit = App.AC.Visits
                .AsEnumerable()
                .Where(v => v.Status == 2 && v.PatientID == id)
                .Join(App.AC.Services, v => v.ServiceID, s => s.ServiceID, (v, s) => new { v, s })
                .Join(App.AC.Patients, vs => vs.v.PatientID, p => p.PatientID, (vs, p) => new { vs, p })
                .Join(App.AC.Employees, vsp => vsp.vs.v.EmployeeID, e => e.EmployeeID, (vsp, e) => new
                {
                    VisitDate = vsp.vs.v.visitDate,
                    ServiceName = vsp.vs.s.serviceName,
                    PatientName = vsp.p.name,
                    EmployeeName = e.fullName,
                    VisitID = vsp.vs.v.VisitID
                })
                .ToList()
                .OrderBy(x => DateTime.Parse(x.VisitDate))
                .Select(v => new VisitDTO
                {
                    VisitDate = v.VisitDate,
                    ServiceName = v.ServiceName,
                    PatientName = v.PatientName,
                    EmployeeName = v.EmployeeName,
                    VisitID = v.VisitID
                })
                .ToList();

            return listVisit;
        }

        public List<Visit> GetDoneVisitByPatient(int id)
        {
            var listVisit = App.AC.Visits
                .Where(v => v.Status == 2 && v.PatientID == id)
                .ToList();

            return listVisit;
        }

        public Visit GetVisitByID(int id)
        {
            return App.AC.Visits.Where(c => c.VisitID == id).FirstOrDefault();
        }

        public bool AddVisit(int employeeID, int patientID, int serviceID, string date, string notes, string time)
        {
            string datetime = GenerateDateTime(date, time);
            Visit visit = new Visit()
            {
                EmployeeID = employeeID,
                PatientID = patientID,
                ServiceID = serviceID,
                visitDate = datetime,
                notes = notes,
                Status = 1
            };
            App.AC.Visits.Add(visit);
            App.AC.SaveChanges();

            return true;
        }

        public string GenerateDateTime(string date, string time)
        {
            if (DateTime.TryParse($"{date} {time}", out DateTime result))
            {
                return result.ToString("yyyy-MM-dd HH:mm"); // формат для базы
            }
            else
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm"); // запасной вариант
            }
        }


        public void DoneVisit(int id)
        {
            var visit = App.AC.Visits.Where(c => c.VisitID == id).FirstOrDefault();
            visit.Status = 2;

            App.AC.SaveChanges();
        }

        public List<Visit> GetDoneVisitByEmp(int employeeID)
        {
            return App.AC.Visits.Where(c => c.EmployeeID == employeeID && c.Status == 2).ToList();
        }
    }
}
