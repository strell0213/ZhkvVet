using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetZhukova.DB
{
    public class Service
    {
        public int ServiceID { get; set; }
        public int? Price { get; set; }
        private string ServiceName, Description;

        public string serviceName { get { return ServiceName; } set { ServiceName = value; } }
        public string description { get { return Description; } set { Description = value; } }

        public Service() { }

        public Service(string ServiceName, string Description, int? Price)
        {
            this.ServiceName = ServiceName;
            this.Description = Description;
            this.Price = Price;
        }
    }
}
