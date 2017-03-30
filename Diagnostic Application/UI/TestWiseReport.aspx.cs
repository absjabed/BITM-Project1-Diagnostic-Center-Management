using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Diagnostic_Application.Manager;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Diagnostic_Application.UI {
    public partial class TestWiseReport : System.Web.UI.Page {

        private decimal total = 0;
        private int serialNo = 0;
        TestWiseReportManager testWiseReportManager = new TestWiseReportManager();


        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                LoadEmptyTestGridView();

            }

            PdfButton.Visible = false;
        }


        private void LoadEmptyTestGridView() {
            DataTable dataTable = new DataTable();
            TestWiseReportGridView.DataSource = dataTable;
            TestWiseReportGridView.DataBind();
        }

        protected void ShowButton_Click(object sender, EventArgs e) {

            try
            {
                if (FormDateTextBox.Text == string.Empty || ToDateTextBox.Text == string.Empty)
                {
                    InfoMessageLabel.Text = "Please select both date";
                    InfoMessageLabel.ForeColor = Color.DarkRed;
                    InfoMessageLabel.Visible = true;
                }

                string startDate = FormDateTextBox.Text;
                string endDate = ToDateTextBox.Text;
                ViewState["startDate"] = startDate;
                ViewState["endDate"] = endDate;
                LoadTestGridView(startDate, endDate);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }

        private void LoadTestGridView(string startDate, string endDate){
            //throw new NotImplementedException();

            List<View.View_Model.TestWiseReport> testWiseReportList = testWiseReportManager.GetAllTypeWiseReport(startDate, endDate);

            if (testWiseReportList.Count != 0){
                TestWiseReportGridView.DataSource = testWiseReportList;
                TestWiseReportGridView.DataBind();
                //display pdf button
                PdfButton.Visible = true;
                //total items
            }
            else{
                TestWiseReportGridView.DataSource = null;
                TestWiseReportGridView.DataBind();
                PdfButton.Visible = false;
            }
        }

        protected void PdfButton_Click(object sender, EventArgs e) {
            int columnsCount = TestWiseReportGridView.HeaderRow.Cells.Count;

            string start = (string) ViewState["startDate"];
            string end = (string) ViewState["endDate"];
            PdfPTable pdfTable = new PdfPTable(columnsCount);


            foreach (TableCell gridViewHeaderCell in TestWiseReportGridView.HeaderRow.Cells) {

                iTextSharp.text.Font font = new iTextSharp.text.Font();
                font.Color = new BaseColor(TestWiseReportGridView.HeaderStyle.ForeColor);

                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));

                pdfTable.AddCell(pdfCell);
            }

            foreach (GridViewRow gridViewRow in TestWiseReportGridView.Rows) {
                if (gridViewRow.RowType == DataControlRowType.DataRow) {
                    foreach (TableCell gridViewCell in gridViewRow.Cells) {
                        iTextSharp.text.Font font = new iTextSharp.text.Font();
                        font.Color = new BaseColor(TestWiseReportGridView.RowStyle.ForeColor);

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text, font));

                        pdfTable.AddCell(pdfCell);
                    }
                }
            }

            foreach (TableCell gridViewHeaderCell in TestWiseReportGridView.FooterRow.Cells) {
                iTextSharp.text.Font font = new iTextSharp.text.Font();
                font.Color = new BaseColor(TestWiseReportGridView.FooterStyle.ForeColor);
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));
                pdfTable.AddCell(pdfCell);
            }


            Document pdfDocument = new Document(PageSize.A4, 20f, 10f, 10f, 10f);

            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            string Name = "GRANOSTIC Diagnostic Care Centre";
            string moduleName = "        ----> Test Wise Report";
            pdfDocument.Open();
            iTextSharp.text.Image logoImage = iTextSharp.text.Image.GetInstance("C:\\Users\\Zamil\\Desktop\\BITM_Project\\Diagnostic Application\\images\\logo1.jpg");
            logoImage.ScalePercent(50f);
            // logoImage.SetAbsolutePosition(0,0);
            logoImage.ScalePercent(24F);
            pdfDocument.Add(logoImage);
            pdfDocument.Add(new Paragraph("        " + DateTime.Now.ToString()));
            pdfDocument.Add(new Paragraph("        " + Name));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph("\t" + moduleName));
            pdfDocument.Add(new Paragraph(" \n\n"));
            pdfDocument.Add(new Paragraph("                                                  " + "Test Report between date: "+start+" and "+end));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(pdfTable);
            pdfDocument.Close();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition", "attachment;filename=Test_Wise_Report("+start+"_to_"+end+").pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }

        protected void TestWiseReportGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                e.Row.Cells[0].Text = (serialNo += 1).ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "Testname").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "TotalCount").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "TotalFee").ToString();

                total = total + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalFee"));

            } else if (e.Row.RowType == DataControlRowType.Footer) {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "Total Amount: ";
                e.Row.Cells[3].Text = total.ToString();
            }
        }
    }
}