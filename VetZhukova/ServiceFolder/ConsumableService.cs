using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetZhukova.DB;

namespace VetZhukova.ServiceFolder
{
    class SetConsumable : Consumable
    {
        public int count { get; set; }

        SetConsumable(int ID,string Name, string Unit, string LastUpdated, int count)
        {
            this.ConsumableID = ID;
            this.name = Name;
            this.unit = Unit;
            this.lastUpdated = lastUpdated;
            this.count = count;
        }

        public SetConsumable()
        {
        }
    }
    class ConsumableService
    {
        public ConsumableService() { }

        public List<Consumable> GetConsumables()
        {
            var consumbales = App.AC.Consumables.ToList();
            return consumbales;
        }

        public List<SetConsumable> GetSetConsumables(string search="")
        {
            List<SetConsumable> setConsumables = new List<SetConsumable>();

            var consumbales = App.AC.Consumables.ToList();
            foreach (var consumbale in consumbales)
            {
                SetConsumable setConsumable = new SetConsumable()
                {
                    ConsumableID = consumbale.ConsumableID,
                    name = consumbale.name,
                    unit = consumbale.unit,
                    lastUpdated = consumbale.lastUpdated,
                    count = 0,
                };
                setConsumables.Add(setConsumable);
            }

            return setConsumables;

        }
    }
}
