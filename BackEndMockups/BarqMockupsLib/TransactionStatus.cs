using System;
using System.Collections.Generic;

namespace BarqMockupsLib
{
    public partial class TransactionStatus
    {
        public TransactionStatus()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<Transaction> Transaction { get; set; }
    }
}
