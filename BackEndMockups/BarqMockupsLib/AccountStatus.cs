using System;
using System.Collections.Generic;

namespace BarqMockupsLib
{
    public partial class AccountStatus
    {
        public AccountStatus()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<Account> Account { get; set; }
    }
}
