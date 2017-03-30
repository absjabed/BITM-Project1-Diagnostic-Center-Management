using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diagnostic_Application.Models {
    public class Patient {

        public int Id { get; set; }
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MobileNo { get; set; }
        public int TestId { get; set; }
        public string BillNumber { get; set; }
        public decimal DueAmount { get; set; }
        public DateTime DueDate { get; set; }


        public string GetGeneratedBillNo(){
            String timeStamp = GetTimestamp(DateTime.Now);
            Random random = new Random();
            int rand = random.Next(1000, 9999);
            string BillNo = "DIAG-" +timeStamp.ToString() + rand.ToString();
            BillNumber = BillNo;
            return BillNumber;
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("mmFt");
        }
    }
}