using System.Collections.Generic;
using Diagnostic_Application.Gateway;
using Diagnostic_Application.Models;

namespace Diagnostic_Application.Manager {
    public class TestTypeManager {

        TestTypeGetway _testTypeGetway = new TestTypeGetway();

        public string SaveTestType(TestType testType){

            if (testType.TestTypeName == string.Empty)
            {
                return "empty";
            }

            //Check Unique Type
            bool isSutdentExist = _testTypeGetway.IsTestTypeExists(testType);
            
            if (isSutdentExist){
                return "exists";
            }


            int rowAffected = _testTypeGetway.SaveTestType(testType);
            if (rowAffected > 0)
            {
                return "success";

            }else{
                return "failed";
            }

        }


        public List<TestType> GetAllTestType()
        {
            return _testTypeGetway.GetAllType();
        }

    }
}