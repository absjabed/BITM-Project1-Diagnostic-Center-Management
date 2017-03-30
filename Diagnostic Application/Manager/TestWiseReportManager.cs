using System.Collections.Generic;
using Diagnostic_Application.Gateway;
using Diagnostic_Application.View.View_Model;

namespace Diagnostic_Application.Manager {
    public class TestWiseReportManager {
        TestWiseReportGetway tesWiseReportGetway = new TestWiseReportGetway();
        public List<TestWiseReport> GetAllTypeWiseReport(string fromDate, string toDate){

            return tesWiseReportGetway.GetDateWiseTestReport(fromDate, toDate);
        }
    }
}