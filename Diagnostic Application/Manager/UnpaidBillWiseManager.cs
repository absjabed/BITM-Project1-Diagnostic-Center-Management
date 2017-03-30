using System.Collections.Generic;
using Diagnostic_Application.Gateway;
using Diagnostic_Application.View.View_Model;

namespace Diagnostic_Application.Manager {
    public class UnpaidBillWiseManager {
        UnPaidBillGatway unPaidBillGatway = new UnPaidBillGatway();

        public List<UnpaidBillWiseModel> UnpaidBillReport(string fromDate, string toDate) {
            return unPaidBillGatway.UnpaidBillReport(fromDate, toDate);
        }
    }
}