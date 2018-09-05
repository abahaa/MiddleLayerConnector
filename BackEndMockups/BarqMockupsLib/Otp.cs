using System;
using System.Collections.Generic;

namespace BarqMockupsLib
{
    public partial class Otp
    {
        public Otp()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ConsumptionTime { get; set; }

        public Otpstatus StatusNavigation { get; set; }
        public ICollection<Account> Account { get; set; }
    }
}
