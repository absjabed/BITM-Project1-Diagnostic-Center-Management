using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using Diagnostic_Application.Models;

namespace Diagnostic_Application.Gateway {
    public class TestSetupGateway {

        //connection string
        string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCareDB"].ConnectionString;

        public List<TestType> GetAllTestType(){
            
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM TestType";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<TestType> testTypeList = new List<TestType>();

            if (reader.HasRows)
            {
                while (reader.Read()){
                    TestType testType = new TestType();
                    testType.Id = int.Parse(reader["Id"].ToString());
                    testType.TestTypeName = reader["Test_type_name"].ToString();

                    testTypeList.Add(testType);
                }

                reader.Close();
            }

            connection.Close();
            return testTypeList;

        }


        public int SaveTestSetup(TestSetup testSetup){

            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            //string query = "INSERT INTO TestSetup VALUES('"+ testSetup.TestName + "', "+ testSetup.Fee +","+ testSetup.TestTypeId +", GETDATE())";  
            string query = "INSERT INTO TestSetup VALUES(@testName, @testFee,@testTypeId, GETDATE())";  

            //Console.Write(query);
            SqlCommand command = new SqlCommand(query, connection);

            //Parametrized SQL Query
            command.Parameters.Clear();
            command.Parameters.AddWithValue("testName", testSetup.TestName);
            command.Parameters.AddWithValue("testFee", testSetup.Fee);
            command.Parameters.AddWithValue("testTypeId", testSetup.TestTypeId);

            connection.Open();

            rowAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowAffected;
        }

        public bool IsTestNameExists(TestSetup testSetup){

            SqlConnection connection = new SqlConnection(connectionString);
//            string query = "SELECT * FROM TestSetup WHERE testname='" + testSetup.TestName + "'";
            string query = "SELECT * FROM TestSetup WHERE Test_name=@testName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();
            command.Parameters.AddWithValue("testName", testSetup.TestName);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows){
                return true;
            }
            reader.Close();

            connection.Close();
            return false;
        }


        public List<TestSetupViewModel> GetAllTestSetup(){
            
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT TestName,TestFee,TestTypeName from AllTestsInfo_view";
            SqlCommand command = new SqlCommand(query, connection);

            List<TestSetupViewModel> testSetupViewModelList = new List<TestSetupViewModel>();
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows){
                while (reader.Read()){
                    TestSetupViewModel testSetupViewModel = new TestSetupViewModel();
                    testSetupViewModel.TestName = reader["TestName"].ToString();
                    testSetupViewModel.Fee = Convert.ToDecimal(reader["TestFee"].ToString());
                    testSetupViewModel.Name = reader["TestTypeName"].ToString();

                    testSetupViewModelList.Add(testSetupViewModel);
                }

                reader.Close();
            }

            connection.Close();

            return testSetupViewModelList;
        }

    }
}