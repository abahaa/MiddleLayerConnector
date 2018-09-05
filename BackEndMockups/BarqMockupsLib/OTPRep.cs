using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarqMockupsLib
{
    public class OTPRep
    {
        private BarqBECoreMockContext Context;
        private static Random random = new Random();

        public OTPRep(BarqBECoreMockContext Context)
        {
            this.Context = Context;
        }

        public Otp GetBYID(int id)
        {
            return Context.Otp.Find(id);
        }

        public static string GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }



        public bool ValidateOTP(string OTPCode,int? OtpID)
        {
            var OTP = Context.Otp.Where(otp => otp.Code == OTPCode && otp.Id == OtpID && otp.Status == 1).FirstOrDefault();
            if(OTP != null)
            {
                Close(OTP);
                return true;
            }
            return false;
        }

        private void Close(Otp otp)
        {
            otp.Status = 2;
            otp.ConsumptionTime = DateTime.Now;
            Context.Otp.Update(otp);
            Context.SaveChanges();
        }

        public Otp GenerateOTP()
        {
            string OTPCode = GenerateRandomString();
            int Status = 1;
            DateTime CreationTime = DateTime.Now;
            Otp GeneratedOTP = new Otp() { Code = OTPCode, Status = Status, CreationTime = CreationTime };
            Context.Otp.Add(GeneratedOTP);
            Context.SaveChanges();
            return GeneratedOTP;
        }
    }
}
