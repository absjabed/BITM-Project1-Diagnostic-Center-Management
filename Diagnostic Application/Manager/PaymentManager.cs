using Diagnostic_Application.Gateway;
using Diagnostic_Application.Models;

namespace Diagnostic_Application.Manager {
    public class PaymentManager {
        PaymentGetway paymentGateway = new PaymentGetway();

        //public TestEntry SearchByBill(string billNo){
        //    return paymentGateway.SearchByBill(billNo);
        //}

        //public TestEntry SearchByMobileNo(string mobileNo)
        //{
        //    return paymentGateway.SearchByMobile(mobileNo);
        //}

        public string UpdatePayment(string billNo){
            int rowAffected =  paymentGateway.UpdatePaymentWithStatus(billNo);
            if (rowAffected > 0)
            {
                return "success";
            }
            else
            {
                return "failed";
            }
        }

        //public bool IsExistMobileNo(string mobileNo) {
        //    if (SearchByBill(mobileNo) != null) {
        //        return true;
        //    } else {
        //        return false;
        //    }
        //}



        public string IsBillNoExists(string billNo) {
            if (paymentGateway.IsBillNoExists(billNo))
            {
                return "success";
            } else {
                return "failed";
            }
        }

        public string IsMobileNoExists(string mobileNo)
        {
            if (paymentGateway.IsMobileNoExists(mobileNo))
            {
                return "success";
            }
            else
            {
                return "failed";
            }
        }


        public Patient SearchPatientInfoByBillNo(string billNo){

            return paymentGateway.GetPatientInfoUsingBillNo(billNo);
        }

        public Patient SearchPatientInfoByMobileNo(string mobileNo)
        {
            return paymentGateway.GetPatientInfoUsingMobileNo(mobileNo);
        }

        //public string UpdatePaymentWithStatus(string billNo, decimal paidAmount, int status)
        //{
        //    int rowAffected = paymentGateway.UpdatePaymentWithStatus(billNo, paidAmount, status);
        //    //throw new NotImplementedException();
        //    if (rowAffected > 0)
        //    {
        //        return "success";
        //    }
        //    else
        //    {
        //        return "failed";
        //    }
        //}


       
    }

}