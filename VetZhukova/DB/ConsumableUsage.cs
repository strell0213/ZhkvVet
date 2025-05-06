using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetZhukova.DB
{
    public class ConsumableUsage
    {
        public int ConsumableUsageID { get; set; }
        public int UsageID { get; set; }
        public int ConsumableID { get; set; }
        public int QuantityUsed { get; set; }
        private string UsageDate;

        public string usageDate { get { return UsageDate; } set { UsageDate = value; } }

        public ConsumableUsage() { }
        public ConsumableUsage(int UsageID, int ConsumableID, int QuantityUsed, string UsageDate)
        {
            this.UsageID = UsageID;
            this.ConsumableID = ConsumableID;
            this.QuantityUsed = QuantityUsed;
            this.UsageDate = UsageDate;
        }
    }
}
