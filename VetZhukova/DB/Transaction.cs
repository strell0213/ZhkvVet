using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetZhukova.DB
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int VisitID { get; set; }
        public double Amount { get; set; }
        private string TransactionDate;

        public string transactionDate { get { return TransactionDate; } set { TransactionDate = value; } }

        public Transaction() { }

        public Transaction(int VisitID, double Amount, string TransactionDate)
        {
            this.VisitID = VisitID;
            this.Amount = Amount;
            this.TransactionDate = TransactionDate;
        }
    }
}
