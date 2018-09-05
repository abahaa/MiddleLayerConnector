using System;
using System.Collections.Generic;

namespace BarqMockupsLib
{
    public partial class Otpstatus
    {
        public Otpstatus()
        {
            Otp = new HashSet<Otp>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<Otp> Otp { get; set; }
    }
}
