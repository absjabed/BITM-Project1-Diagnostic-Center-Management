using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using Diagnostic_Application.View.View_Model;

namespace Diagnostic_Application.Gateway {
    public class TestWiseReportGetway {


        //connection string
        string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCareDB"].ConnectionString;

        public List<TestWiseReport> GetDateWiseTestReport(string startDate, string endDate) {
            SqlConnection connection = new SqlConnection(connectionString);

//            string query = @"SELECT ts.Test_name, count(pt.Id) as TotalCount, count(pt.Id) * sum(DISTINCT ts.Fee) as TotalFee 
//FROM TestSetup ts LEFT JOIN PatientTest pt on pt.Test_setup_id = ts.Id and 
//pt.Created_at BETWEEN '2017-02-14'  AND '2017-02-25' GROUP BY ts.Test_name";

            string query = @"SELECT ts.Test_name, count(pt.Id) as TotalCount, count(pt.Id) * sum(DISTINCT ts.Fee) as TotalFee 
                                FROM TestSetup ts LEFT JOIN PatientTest pt on pt.Test_setup_id = ts.Id and 
                                pt.Created_at BETWEEN @startDate  AND @endDate GROUP BY ts.Test_name";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("startDate", startDate);
            command.Parameters.AddWithValue("endDate", endDate);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<TestWiseReport> testWiseReportList = new List<TestWiseReport>();
            while (reader.Read()) {

                TestWiseReport testWiseReport = new TestWiseReport();

                testWiseReport.TestName = reader["Test_name"].ToString();
                testWiseReport.TotalCount = Convert.ToInt32(reader["TotalCount"].ToString());
                testWiseReport.TotalFee = Convert.ToDecimal(reader["TotalFee"].ToString());

                testWiseReportList.Add(testWiseReport);
            }



            reader.Close();
            connection.Close();

            return testWiseReportList;

        }
    }
}