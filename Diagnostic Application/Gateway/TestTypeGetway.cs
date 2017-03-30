using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using Diagnostic_Application.Models;

namespace Diagnostic_Application.Gateway {
    public class TestTypeGetway {


        //connection string
        string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCareDB"].ConnectionString;


        public int SaveTestType(TestType testType) {


            int rowAffected;
            SqlConnection connection = new SqlConnection(connectionString);

            // Insert Query
            string query = "INSERT INTO TestType (Test_type_name, Created_at) VALUES(@testType, GETDATE())";
            
            // Execute
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();
            command.Parameters.AddWithValue("testType", testType.TestTypeName);

            connection.Open();
            rowAffected = command.ExecuteNonQuery();
            // Close Connection
            connection.Close();

            return rowAffected;
        }


        public bool IsTestTypeExists(TestType testType){

            SqlConnection connection = new SqlConnection(connectionString);

            // Insert Query
            string query = "SELECT * FROM TestType WHERE Test_type_name=@testType";

            // Execute
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("testType", testType.TestTypeName);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows){
                return true;
            }
            connection.Close();
            return false;
        }


        public List<TestType> GetAllType(){

            List<TestType> testTypeList = new List<TestType>();

            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM TestType ORDER BY Test_type_name ASC";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            //if any row exists
            if (reader.HasRows){
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
    }
    
}