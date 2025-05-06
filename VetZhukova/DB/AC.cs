using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetZhukova.DB
{
    public class AC : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Consumable> Consumables { get; set; }
        public DbSet<ConsumableUsage> ConsumableUsages { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<VisitMedication> VisitMedications { get; set; }

        public AC() : base("DefaultConnection") { }
    }
}
