using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using Diagnostic_Application.Models;

namespace Diagnostic_Application.Gateway {
    public class PaymentGetway {

        //connection string
        string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCareDB"].ConnectionString;

        public int UpdatePaymentWithStatus(string billNo)
        {

            decimal dueAmount = 0;
            int status = 1;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "UPDATE Patient SET Payment_status=@status,Due_amount=@amount WHERE Bill_no=@billNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("status", status);
            command.Parameters.AddWithValue("amount", dueAmount);
            command.Parameters.AddWithValue("billNo", billNo);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }

        


        //public bool IsPatientTestExists(int patientID, int testID) {
        //    string query = "SELECT * FROM PatientTest WHERE PatientId=" + patientID + " AND TestId=" + patientID;
        //    SqlConnection connection = new SqlConnection(connectionString);

        //    SqlCommand command = new SqlCommand(query, connection);
        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    bool isPatientTestExist = false;
        //    if (reader.HasRows) {
        //        isPatientTestExist = true;
        //    }
        //    reader.Close();
        //    connection.Close();
        //    return isPatientTestExist;
        //}


        public bool IsBillNoExists(string billNo) {
            string query = "SELECT * FROM Patient WHERE Bill_no=@billNo";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("billNo", billNo);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            bool isBillNoExists = reader.HasRows;
            reader.Close();
            connection.Close();
            return isBillNoExists;
        }
      

        public bool IsMobileNoExists(string mobileNo)
        {
            string query = "SELECT * FROM Patient WHERE Mobile=@mobileNo";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("mobileNo", mobileNo);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            bool isMobileNoExists = reader.HasRows;
            reader.Close();
            connection.Close();
            return isMobileNoExists;
        }



//        public TestEntry SearchByBill(string billNo) {
//            //get the total Fee
//            string query = @"SELECT sum(fee) as totalFee FROM Patient 
//                             INNER JOIN PatientTest ON Patient.id = PatientTest.Patient_id 
//                             INNER JOIN TestSetup ON PatientTest.Test_setup_id = TestSetup.id 
//                             WHERE Patient.Bill_no=@billNo";

//            SqlConnection connection = new SqlConnection(connectionString);

//            SqlCommand command = new SqlCommand(query, connection);
//            command.Parameters.Clear();
//            command.Parameters.AddWithValue("billNo", billNo);
//            connection.Open();
//            SqlDataReader reader = command.ExecuteReader();

//            TestEntry FeesTestEntry = null;

//            if (reader.HasRows){
//                reader.Read();
//                FeesTestEntry = new TestEntry();
//                FeesTestEntry.TotalAmount = Convert.ToDecimal(reader["totalFee"].ToString());
//            }
//            connection.Close();

//            //check due payment using bill number



//            return FeesTestEntry;
//        }

//        public TestEntry SearchByMobile(string mobileNo)
//        {
//            string query = @"SELECT sum(fee) as totalFee FROM Patient 
//                             INNER JOIN PatientTest ON Patient.id = PatientTest.Patient_id 
//                             INNER JOIN TestSetup ON PatientTest.Test_setup_id = TestSetup.id 
//                             WHERE Patient.Mobile=@mobileNo";

//            SqlConnection connection = new SqlConnection(connectionString);

//            SqlCommand command = new SqlCommand(query, connection);
//            command.Parameters.Clear();
//            command.Parameters.AddWithValue("mobileNo", mobileNo);
//            connection.Open();
//            SqlDataReader reader = command.ExecuteReader();

//            TestEntry FeesTestEntry = null;

//            if (reader.HasRows)
//            {
//                reader.Read();
//                FeesTestEntry = new TestEntry();
//                FeesTestEntry.TotalAmount = Convert.ToDecimal(reader["totalFee"].ToString());
//            }
//            connection.Close();

//            //check due payment using bill number



//            return FeesTestEntry;
//        }

        //check patient due amount.
        public Patient GetPatientInfoUsingBillNo(string billNo) {
            //get the total Fee
            string query = @"SELECT * FROM Patient WHERE Bill_no =@billNo";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("billNo", billNo);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Patient patient = new Patient();

            if (reader.HasRows) {
                reader.Read();
                patient.DueAmount = Convert.ToDecimal(reader["Due_amount"].ToString());
                patient.DueDate = Convert.ToDateTime(reader["Created_at"].ToString());
            }
            connection.Close();
            

            return patient;
        }


        public Patient GetPatientInfoUsingMobileNo(string mobileNo)
        {
            string query = @"SELECT * FROM Patient WHERE Mobile = @mobileNo";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("mobileNo", mobileNo);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Patient patient = new Patient();

            if (reader.HasRows)
            {
                reader.Read(); 
                patient.DueAmount = Convert.ToDecimal(reader["Due_amount"].ToString());
                patient.DueDate = Convert.ToDateTime(reader["Created_at"].ToString());
                patient.BillNumber = reader["Bill_no"].ToString();
            }
            connection.Close();


            return patient;
        }


        //public int SavePatientTests(int patientId, int testId, string requestDate) {
        //    SqlConnection connection = new SqlConnection(connectionString);
        //    string query = "INSERT INTO PatientTests(PatientId,TestId,RequestDate) VALUES(" + patientId + "," + testId + ",'" + requestDate + "')";
        //    SqlCommand command = new SqlCommand(query, connection);
        //    connection.Open();
        //    int rowAffected = command.ExecuteNonQuery();
        //    connection.Close();
        //    return rowAffected;
        //}

//        public List<TestSetup> GetAllSetup(string billNoOrMob) {
//            SqlConnection con = new SqlConnection(connectionString);
//            string query = @"SELECT TestSetup.TestName,TestSetup.Fee FROM Patient  
//                                INNER JOIN PatientTest ON Patient.Id = PatientTest.PatientId 
//                                INNER JOIN TestSetup ON PatientTest.TestSetupId = TestSetup.Id  
//                            WHERE Patient.BillNo='" + billNoOrMob + "' or Patient.Mobile='" + billNoOrMob + "'";
//            SqlCommand cmd = new SqlCommand(query, con);
//            List<TestSetup> testSetups = new List<TestSetup>();
//            con.Open();
//            SqlDataReader reader = cmd.ExecuteReader();
//            if (reader.HasRows) {
//                while (reader.Read()) {
//                    TestSetup testSetup = new TestSetup();
//                    testSetup.TestName = reader["TestName"].ToString();
//                    testSetup.Fee = Convert.ToDecimal(reader["Fee"]);
//                    testSetups.Add(testSetup);
//                }
//                reader.Close();
//            }
//            con.Close();
//            return testSetups;
//        }


      
    }
}