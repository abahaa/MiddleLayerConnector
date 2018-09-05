using System;
using System.Collections.Generic;

namespace BarqMockupsLib
{
    public partial class Account
    {
        public Account()
        {
            TransactionFromAccountNavigation = new HashSet<Transaction>();
            TransactionToAccountNavigation = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Msisdn { get; set; }
        public decimal Balance { get; set; }
        public string Password { get; set; }
        public string Mpin { get; set; }
        public int Status { get; set; }
        public int? LastOtpid { get; set; }
        public int PasswordTrials { get; set; }
        public int MpinTrials { get; set; }

        public Otp LastOtp { get; set; }
        public AccountStatus StatusNavigation { get; set; }
        public ICollection<Transaction> TransactionFromAccountNavigation { get; set; }
        public ICollection<Transaction> TransactionToAccountNavigation { get; set; }
    }
}
