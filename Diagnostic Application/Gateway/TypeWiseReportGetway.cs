using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Configuration;
using Diagnostic_Application.Models.View_Model;

namespace Diagnostic_Application.Gateway {
    public class TypeWiseReportGetway {


            //connection string
        string connectionString = WebConfigurationManager.ConnectionStrings["DiagnosticCareDB"].ConnectionString;


            public List<TypeWiseTestReport> GetTypeWiseTestReport(string startDate, string endDate) {
                SqlConnection connection = new SqlConnection(connectionString);

                string query = @"SELECT TT.Test_type_name AS TypeName, COUNT(PT.id) as TotalCount, ISNULL(SUM(DISTINCT TS.FEE) * COUNT(PT.ID), 0) as TotalFee 
                                FROM TestType TT LEFT JOIN TestSetup TS ON TS.type_id = TT.id
                                LEFT JOIN PatientTest PT ON PT.Test_setup_id = TS.id
                                AND PT.Created_at BETWEEN @startDate AND @endDate GROUP BY TT.Test_type_name";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("startDate", startDate);
                command.Parameters.AddWithValue("endDate", endDate);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<TypeWiseTestReport> testWiseReportList = new List<TypeWiseTestReport>();
                while (reader.Read()) {
                    TypeWiseTestReport typeWiseTestReport = new TypeWiseTestReport();

                    typeWiseTestReport.TypeName = reader["TypeName"].ToString();
                    typeWiseTestReport.TotalCount = Convert.ToInt32(reader["TotalCount"].ToString());
                    typeWiseTestReport.TotalFee = Convert.ToDecimal(reader["TotalFee"].ToString());

                    testWiseReportList.Add(typeWiseTestReport);
                }



                reader.Close();
                connection.Close();

                return testWiseReportList;

            }
        }
}