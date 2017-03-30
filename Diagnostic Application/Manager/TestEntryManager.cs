using System.Collections.Generic;
using Diagnostic_Application.Gateway;
using Diagnostic_Application.Models;

namespace Diagnostic_Application.Manager {
    public class TestEntryManager {

        TestEntryGetway testEntryGetway = new TestEntryGetway();

        public List<TestSetup> GetAllTestSetup(){
            return testEntryGetway.GetAllTestSetup();
        }

        public int SavePatient(Patient patient){
            //bool isTestNameExists = _testEntryGetway.IsTestNameExists(testSetup);
            return testEntryGetway.SavePatient(patient);
        }


        public TestSetup GetTestSetup(string testID){
            return testEntryGetway.GetTestSetup(testID);
        }


        public string SavePatientInformation(PatientTest patientTest){

            int rowAffected = testEntryGetway.SavePatientTestInformation(patientTest);
            if (rowAffected > 0){
                return "success";
            }
            else{
                return "failed";
            }
        }


        public bool IsExistsMobileNo(string mobileNo)
        {
            return testEntryGetway.IsExistsMobileNo(mobileNo);
        }
    }
}