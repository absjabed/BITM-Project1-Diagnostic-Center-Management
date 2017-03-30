using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Diagnostic_Application.Manager;
using Diagnostic_Application.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Diagnostic_Application.UI {
    public partial class TestRequestEntryUI : System.Web.UI.Page
    {
        private string patientName;
        private string mobileNo;
        private string billNo;
        private int patientID;
        private decimal dueAmount;


        TestEntryManager testEntryManager = new TestEntryManager();
        protected void Page_Load(object sender, EventArgs e) {

            if (!IsPostBack){
                LoaddAllTestSetup();
                SaveButton.Enabled = false ;
            }
            
            CreateGrid();
        }
            
        private void CreateGrid(){

            if (!IsPostBack){
                DataTable dataTable = new DataTable();
                dataTable.Columns.AddRange(new DataColumn[3]
                {
                new DataColumn("ID"),
                new DataColumn("TEST"),
                new DataColumn("FEE")
                });

                ViewState["TestEntry"] = dataTable;
                this.BindToGridView();
            }
            
        }

        private void BindToGridView(){
            TestEntryGridView.DataSource = (DataTable) ViewState["TestEntry"];
            TestEntryGridView.DataBind();
        }

        private void LoaddAllTestSetup(){
            List<TestSetup> allTestSetupList = testEntryManager.GetAllTestSetup();

            TestNameDropDownList.DataSource = allTestSetupList;
            TestNameDropDownList.DataTextField = "TestName";
            TestNameDropDownList.DataValueField = "Id";
            TestNameDropDownList.DataBind();
            TestNameDropDownList.Items.Insert(0, "---SELECT---");

        }

        protected void AddButton_Click(object sender, EventArgs e){

            if (PatientNameTextBox.Text == string.Empty
                || DateOfBirthTextBox.Text == string.Empty
                || MobileNoTextBox.Text == string.Empty
                || TestNameDropDownList.SelectedIndex == 0)
            {
                DisplayInfoMessage("Empty! All Fields are required.", Color.Crimson);
                return;
            }
            else
            {
                if (testEntryManager.IsExistsMobileNo(MobileNoTextBox.Text.Trim()))
                {
                    DisplayInfoMessage("Error! This Mobile Number already used.", Color.Crimson);
                    MobileNoTextBox.Text = String.Empty;
                    MobileNoTextBox.Focus();
                    return;
                }
                else
                {
                    DisplayInfoMessage("Success! Successfully Added.", Color.DarkGreen); 
                }
               
            }




            DataTable dataTable = (DataTable) ViewState["TestEntry"];
            dataTable.Rows.Add(TestSetupIDHiddenrField.Value, TestNameDropDownList.SelectedItem, FeeTextBox.Text.Trim());

            ViewState["TestEntry"] = dataTable;
            this.BindToGridView();


            //Dropdown Move to default.
            TestNameDropDownList.SelectedIndex = 0;
            FeeTextBox.Text = string.Empty;
            TestSetupIDHiddenrField.Value = string.Empty;
            SaveButton.Enabled = true;
        }


        private void DisplayInfoMessage(string text, Color color) {
            //Exists - Test Setup.
            InfoMessageLabel.Text = text;
            InfoMessageLabel.Visible = true;
            InfoMessageLabel.BackColor = color;
        }


        private void CreatePdf() {

            int columnsCount = TestEntryGridView.HeaderRow.Cells.Count;


            PdfPTable pdfTable = new PdfPTable(columnsCount);


            foreach (TableCell gridViewHeaderCell in TestEntryGridView.HeaderRow.Cells) {

                iTextSharp.text.Font font = new iTextSharp.text.Font();
                font.Color = new BaseColor(TestEntryGridView.HeaderStyle.ForeColor);

                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));

                pdfTable.AddCell(pdfCell);
            }

            foreach (GridViewRow gridViewRow in TestEntryGridView.Rows) {
                if (gridViewRow.RowType == DataControlRowType.DataRow) {
                    foreach (TableCell gridViewCell in gridViewRow.Cells) {
                        iTextSharp.text.Font font = new iTextSharp.text.Font();
                        font.Color = new BaseColor(TestEntryGridView.RowStyle.ForeColor);

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text, font));

                        pdfTable.AddCell(pdfCell);
                    }
                }
            }

            foreach (TableCell gridViewHeaderCell in TestEntryGridView.FooterRow.Cells) {
                iTextSharp.text.Font font = new iTextSharp.text.Font();
                font.Color = new BaseColor(TestEntryGridView.FooterStyle.ForeColor);
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));
                pdfTable.AddCell(pdfCell);
            }


            Document pdfDocument = new Document(PageSize.A4, 20f, 10f, 10f, 10f);

            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            string Name = "GRANOSTIC Diagnostic Care Centre";
            string moduleName = "           ----> Patient Test Report";
            pdfDocument.Open();
            iTextSharp.text.Image logoImage = iTextSharp.text.Image.GetInstance("C:\\Users\\Zamil\\Desktop\\BITM_Project\\Diagnostic Application\\images\\logo1.jpg");
            logoImage.ScalePercent(50f);
           // logoImage.SetAbsolutePosition(0,0);
            logoImage.ScalePercent(24F);
            pdfDocument.Add(logoImage);
            pdfDocument.Add(new Paragraph("        " + DateTime.Now.ToString()));
            pdfDocument.Add(new Paragraph("        "+Name));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph("\t" + moduleName));
            pdfDocument.Add(new Paragraph("\n\n\n"));
            pdfDocument.Add(new Paragraph("                                                               "+"Patient Name: " + patientName));
            pdfDocument.Add(new Paragraph("                                                               " + "Mobile Number: " + mobileNo));
            pdfDocument.Add(new Paragraph("                                                               " + "Bill No: " + billNo));
            pdfDocument.Add(new Paragraph(" \n\n"));
            pdfDocument.Add(new Paragraph(" \n\n"));
            pdfDocument.Add(new Paragraph("                                                        "+"Test Informations Invoice:"));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(pdfTable);
            pdfDocument.Add(new Paragraph(" \n\n\n")); 
            pdfDocument.Add(new Paragraph("                     " +"---Signature---"));
            pdfDocument.Close();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition", "attachment;filename=PatientTestReport_"+billNo+".pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }

        private void CreatePdf1()
        {

        }

        private void SavePatient(){

            dueAmount = Convert.ToDecimal(total);

            Patient patient = new Patient();
            patient.PatientName = PatientNameTextBox.Text;
            patient.DateOfBirth = Convert.ToDateTime(DateOfBirthTextBox.Text);

            //if (testEntryManager.IsExistsMobileNo(MobileNoTextBox.Text.Trim()))
            //{
                
            //}
            //else
            //{
            //    DisplayInfoMessage();
            //}
            patient.MobileNo = MobileNoTextBox.Text;
            patient.DueAmount = Convert.ToDecimal(ViewState["totalAmount"]);

            billNo = patient.GetGeneratedBillNo();
            patientName = PatientNameTextBox.Text;
            mobileNo = MobileNoTextBox.Text;

            patientID = testEntryManager.SavePatient(patient);
        }


        private void ClearAll(){
            PatientNameTextBox.Text = string.Empty;
            MobileNoTextBox.Text = string.Empty;
            DateOfBirthTextBox.Text = string.Empty;
            TestNameDropDownList.SelectedIndex = 0;
            FeeTextBox.Text = string.Empty;

        }

        protected void TestNameDropDownList_SelectedIndexChanged(object sender, EventArgs e){

            TestSetup testSetup = testEntryManager.GetTestSetup(TestNameDropDownList.SelectedValue);

            FeeTextBox.Text = testSetup.Fee.ToString();
            TestSetupIDHiddenrField.Value = testSetup.Id.ToString();
            //ID display into the hidden value
        }


        //private int _serialNo = 0;
        //private decimal _total = 0;

        //protected void TestEntryGridView_RowDataBound(object sender, GridViewRowEventArgs e) {

        //    if (e.Row.RowType == DataControlRowType.DataRow){
        //        _total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FEE"));
        //    }

        //    if (e.Row.RowType == DataControlRowType.Footer){
        //        TotalTextBox.Text = _total.ToString();
        //    }
        //}


        private int serialNo = 0;
        private decimal total = 0;
        protected void TestWiseReportGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                e.Row.Cells[0].Text = (serialNo += 1).ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "Test").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "Fee").ToString();
                total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FEE"));

            } else if (e.Row.RowType == DataControlRowType.Footer) {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "Total Amount: ";
                e.Row.Cells[2].Text = total.ToString() + " TAKA";
                e.Row.Cells[3].Text = string.Empty;
            }

            ViewState["totalAmount"] = total;
        }


        protected void SaveButton_Click(object sender, EventArgs e){
            SavePatient();
            SavePatientTestInfo();
            InfoMessageLabel.Text = "Success!Test Entry Created.";
            InfoMessageLabel.BackColor = Color.DarkGreen;
            ClearAll();
            CreatePdf();
            

        }

        private void SavePatientTestInfo(){

            foreach (GridViewRow row1 in TestEntryGridView.Rows){
                PatientTest patientTest = new PatientTest();
                patientTest.TestTypeID = Convert.ToInt32((row1.FindControl("testTypeIdLabel") as Label).Text);
                patientTest.PatientID = patientID;
                testEntryManager.SavePatientInformation(patientTest);
            }

        }
    }
}