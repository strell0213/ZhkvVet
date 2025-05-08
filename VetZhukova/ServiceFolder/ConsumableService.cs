using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetZhukova.DB;

namespace VetZhukova.ServiceFolder
{
    class SetConsumable
    {
        public int ConsumableID { get; set; }
        public int count { get; set; }
        public int quantity { get; set; }
        public string Name { get; set; }

        SetConsumable(int ID,string Name, int count, int Quantity)
        {
            this.ConsumableID = ID;
            this.Name = Name;
            this.count = count;
            this.quantity = Quantity;
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

        public List<SetConsumable> GetSetConsumables()
        {
            List<SetConsumable> setConsumables = new List<SetConsumable>();

            var consumbales = App.AC.Consumables.ToList();
            foreach (var consumbale in consumbales)
            {
                SetConsumable setConsumable = new SetConsumable()
                {
                    ConsumableID = consumbale.ConsumableID,
                    Name = consumbale.name,
                    quantity = consumbale.Quantity,
                    count = 0,
                };
                setConsumables.Add(setConsumable);
            }

            return setConsumables;
        }

        public void UpdateConsumablesDB(List<SetConsumable> setConsumables)
        {
            foreach(var sc in setConsumables)
            {
                var consumbale = App.AC.Consumables.Where(c => c.ConsumableID == sc.ConsumableID).FirstOrDefault();
                consumbale.Quantity = consumbale.Quantity - sc.count;
            }
            App.AC.SaveChanges();
        }
    }
}
