using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using Diagnostic_Application.View.View_Model;

namespace Diagnostic_Application.Gateway {
    public class UnPaidBillGatway {


        //connection string
        string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCareDB"].ConnectionString;


        public List<UnpaidBillWiseModel> UnpaidBillReport(string fromDate, string toDate) {

            SqlConnection con = new SqlConnection(connectionString);
            string query = @"SELECT * FROM unpaidBillView WHERE  Payment_status!=1 
                            and Created_at BETWEEN @startDate AND @endDate 
                            ORDER BY Created_at DESC";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("startDate", fromDate);
            cmd.Parameters.AddWithValue("endDate", toDate);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<UnpaidBillWiseModel> unpaBillViewModels = new List<UnpaidBillWiseModel>();
            while (reader.Read()) {
                UnpaidBillWiseModel unpaidViewModel = new UnpaidBillWiseModel();

                unpaidViewModel.PatientName = (reader["Patient_name"].ToString());
                unpaidViewModel.BillNo = reader["Bill_no"].ToString();
                unpaidViewModel.MobileNo = reader["Mobile"].ToString();
                unpaidViewModel.TotalAmount = Convert.ToDecimal(reader["Total"].ToString());
                unpaBillViewModels.Add(unpaidViewModel);
            }
            reader.Close();
            con.Close();
            return unpaBillViewModels;
        }
    }
}
