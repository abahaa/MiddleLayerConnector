using System;
using System.Collections.Generic;

namespace BarqMockupsLib
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public int Status { get; set; }
        public DateTime IssueTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }

        public Account FromAccountNavigation { get; set; }
        public TransactionStatus StatusNavigation { get; set; }
        public Account ToAccountNavigation { get; set; }
    }
}
