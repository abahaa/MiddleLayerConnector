using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarqMockupsLib
{
    public class TransactionRep
    {
        private BarqBECoreMockContext Context;

        public TransactionRep(BarqBECoreMockContext Context)
        {
            this.Context = Context;
        }

        public void InsertTransaction(Transaction transaction)
        {
            Context.Transaction.Add(transaction);
            Context.SaveChanges();
        }

        public Transaction GetByTransactionID(string TransactionID)
        {
           return Context.Transaction.Where(transaction => transaction.TransactionId == TransactionID).FirstOrDefault();
        }
    }
}
