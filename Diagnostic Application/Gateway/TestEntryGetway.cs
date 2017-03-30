using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using Diagnostic_Application.Models;

namespace Diagnostic_Application.Gateway {
    public class TestEntryGetway {
        //connection string
        string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCareDB"].ConnectionString;

        public List<TestSetup> GetAllTestSetup(){
            
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM TestSetup";
            SqlCommand command = new SqlCommand(query, connection);
            
            List<TestSetup> testSetupList = new List<TestSetup>();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows){
                while (reader.Read()){
                    TestSetup testSetup = new TestSetup();
                    testSetup.Id = int.Parse(reader["Id"].ToString());
                    testSetup.TestName = reader["Test_name"].ToString();
                    testSetup.Fee = Convert.ToDecimal(reader["Fee"].ToString());

                    testSetupList.Add(testSetup);
                }
                reader.Close();
            }

            connection.Close();

            return testSetupList;
        }



        public int SavePatient(Patient patient){

            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO Patient (Patient_name,Date_of_birth,Bill_no,Mobile,Payment_status,Created_at, Due_amount) " +
                           "VALUES(@PatientName,@DateOfBirth,@BillNumber,@MobileNo, 0 , GETDATE(), @DueAmount); SELECT SCOPE_IDENTITY()";


            SqlCommand command = new SqlCommand(query, connection);
            int patientID;
            command.CommandType = CommandType.Text;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("PatientName", patient.PatientName);
            command.Parameters.AddWithValue("DateOfBirth", patient.DateOfBirth);
            command.Parameters.AddWithValue("BillNumber", patient.BillNumber);
            command.Parameters.AddWithValue("MobileNo", patient.MobileNo);
            command.Parameters.AddWithValue("DueAmount", patient.DueAmount);
            connection.Open();

            patientID = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return patientID;
        }


        public TestSetup GetTestSetup(string testID){
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT Id, Test_name, Fee FROM TestSetup WHERE Id=@testId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("testId", testID);
            connection.Open();
            TestSetup testSetup = null;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows){
                while (reader.Read()){

                    testSetup = new TestSetup();
                    testSetup.Id = int.Parse(reader["Id"].ToString());
                    testSetup.TestName = reader["Test_name"].ToString();
                    testSetup.Fee = Convert.ToDecimal(reader["Fee"].ToString());
                    
                }

                reader.Close();
            }

            connection.Close();
            return testSetup;
        }


        public int SavePatientTestInformation(PatientTest patientTest){
            int rowAffected;

            //throw new NotImplementedException();
            //connection string
            string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCareDB"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO PatientTest(Patient_id, Test_setup_id, Created_at) VALUES(@PatientID,@TestTypeID, GETDATE())";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("PatientID", patientTest.PatientID);
            command.Parameters.AddWithValue("TestTypeID", patientTest.TestTypeID);
            connection.Open();

            rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }

        public bool IsExistsMobileNo(string mobileNo)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            // SELECT Query
            string query = "SELECT * FROM Patient WHERE Mobile=@mobile";

            // Execute
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("mobile", mobileNo);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                return true;
            }
            connection.Close();
            return false;
        }
    }

}