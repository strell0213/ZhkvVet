using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetZhukova.DB;

namespace VetZhukova.ServiceFolder
{
    class ConsumableService
    {
        public ConsumableService() { }

        public List<Consumable> GetConsumables()
        {
            var consumbales = App.AC.Consumables.ToList();
            return consumbales;
        }
    }
}
