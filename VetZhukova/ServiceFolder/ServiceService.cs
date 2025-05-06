using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetZhukova.DB;

namespace VetZhukova.ServiceFolder
{
    class ServiceService
    {
        public ServiceService() { }

        public List<Service> GetServices()
        {
            var listService = App.AC.Services.ToList();
            return listService;
        }

        public Service GetServiceByID(int id)
        {
            return App.AC.Services.Where(c => c.ServiceID == id).FirstOrDefault();
        }
    }
}
