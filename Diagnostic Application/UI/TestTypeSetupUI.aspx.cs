using System;
using System.Collections.Generic;
using System.Drawing;
using Diagnostic_Application.Manager;
using Diagnostic_Application.Models;

namespace Diagnostic_Application.UI {
    public partial class TestTypeSetupUI : System.Web.UI.Page {

        TestTypeManager testTypeManager = new TestTypeManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

            }

            DisplayAllTestType();
        }

        private void DisplayAllTestType()
        {
            List<TestType> testTypeList = testTypeManager.GetAllTestType();
            TestSetupGridView.DataSource = testTypeList;
            TestSetupGridView.DataBind();
        }

        protected void SaveButton_Click(object sender, EventArgs e) {

            TestType testType = new TestType();


            if (TestTypeTextBox.Text == string.Empty)
            {
                DisplayInfoMessage("Error! Test Type Name is Required.", Color.DarkRed);
                TestTypeTextBox.Focus();
            }
            else
            {
                //collecting data
                testType.TestTypeName = TestTypeTextBox.Text;
        

                string message = testTypeManager.SaveTestType(testType);


                if (message == "success")
                {
                    DisplayInfoMessage("Success! Test Type Created.", Color.ForestGreen);

                }
                else if (message == "failed")
                {
                    DisplayInfoMessage("Failed! Try Again.", Color.DarkRed);

                }
                else if (message == "exists")
                {
                    DisplayInfoMessage("Error! Test type already exists.", Color.Crimson);

                }
                else if (message == "empty")
                {
                    DisplayInfoMessage("Empty! Please Insert Something.", Color.Crimson);
                }

                TestTypeTextBox.Text = String.Empty;
            }

          

            DisplayAllTestType();

        }


        private void DisplayInfoMessage(string text, Color color) {
            InfoMessageLabel.Text = text;
            InfoMessageLabel.Visible = true;
            InfoMessageLabel.BackColor = color;
        }
    }
}