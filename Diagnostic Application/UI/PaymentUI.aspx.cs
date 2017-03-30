using System;
using System.Drawing;
using Diagnostic_Application.Manager;
using Diagnostic_Application.Models;

namespace Diagnostic_Application.UI {
    public partial class PaymentUI : System.Web.UI.Page
    {

        private double amount;
        private decimal totalAmount;
        private decimal dueAmount;
        private DateTime dueDate;

        private PaymentManager paymentManager = new PaymentManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["success"] == null)
            {
                PaymentButton.Enabled = false;
                AmountTextBox.Enabled = false;
                DueDateTextBox.Enabled = false;
            }
            else
            {
                if (item_check.Checked)
                {
                    item_check.ForeColor = Color.Green;
                    PaymentButton.Enabled = true;
                }
                else
                {
                    item_check.ForeColor = Color.Red;
                    PaymentButton.Enabled = false;
                }
                 //PaymentButton.Enabled = true;
                //AmountTextBox.Enabled = true;
            }
            AmountTextBox.Enabled = false;
            DueDateTextBox.Enabled = false;
            
        }



        protected void SearchButton_Click(object sender, EventArgs e) {

            if (BillNoTextBox.Text == String.Empty & MobileNoTextBox.Text == String.Empty){
                InfoMessageLabel.Visible = true;
                InfoMessageLabel.Text = "Sorry! you have to provide atleast Customers Bill Number or Mobile No.";
                InfoMessageLabel.BackColor = Color.DarkRed;
            }
            else if (BillNoTextBox.Text != String.Empty & MobileNoTextBox.Text != String.Empty)
            {
                InfoMessageLabel.Visible = true;
                InfoMessageLabel.Text = "Sorry! Either Mobile No or Bill No should be provided not both. ";
                InfoMessageLabel.BackColor = Color.DarkRed;
                BillNoTextBox.Text = String.Empty;
                MobileNoTextBox.Text = String.Empty;
            }
            else if (BillNoTextBox.Text != String.Empty & MobileNoTextBox.Text == String.Empty)
            {
                string billNo = BillNoTextBox.Text;
                string message = paymentManager.IsBillNoExists(billNo);

                if (message == "success")
                {
                    ViewState["success"] = true;
                    InfoMessageLabel.Visible = true;
                    InfoMessageLabel.Text = "Found the Patient Information By Bill Number!.";
                    InfoMessageLabel.BackColor = Color.ForestGreen;
                    ShowPaymentInformationByBillNo(billNo);
                }
                else if (message == "failed")
                {

                    InfoMessageLabel.Visible = true;
                    InfoMessageLabel.Text = "No Bill Found by this Bill Number!.";
                    InfoMessageLabel.BackColor = Color.DarkRed;
                }
                BillNoTextBox.Text = String.Empty;
                MobileNoTextBox.Text = String.Empty;
            }
            else if (BillNoTextBox.Text == String.Empty & MobileNoTextBox.Text != String.Empty)
            {
                string mobileNo;
                mobileNo = MobileNoTextBox.Text;
                string message = paymentManager.IsMobileNoExists(mobileNo);

                if (message == "success")
                {
                    ViewState["success"] = true;
                    InfoMessageLabel.Visible = true;
                    InfoMessageLabel.Text = "Found the Patient Information By Mobile Number!.";
                    InfoMessageLabel.BackColor = Color.ForestGreen;
                    ShowPaymentInformationByMobileNo(mobileNo);
                }
                else if (message == "failed")
                {

                    InfoMessageLabel.Visible = true;
                    InfoMessageLabel.Text = "No Bill Found by this Mobile Number!.";
                    InfoMessageLabel.BackColor = Color.DarkRed;
                }
                BillNoTextBox.Text = String.Empty;
                MobileNoTextBox.Text = String.Empty;
            }




        }

        private void ShowPaymentInformationByMobileNo(string mobileNo)
        {
            //TestEntry testEntry = paymentManager.SearchByMobileNo(mobileNo);
            Patient patient = paymentManager.SearchPatientInfoByMobileNo(mobileNo);

           // totalAmount = Convert.ToDecimal(testEntry.TotalAmount);
            dueAmount = Convert.ToDecimal(patient.DueAmount.ToString());
            dueDate = Convert.ToDateTime(patient.DueDate.ToString());


            AmountTextBox.Text = dueAmount + " Taka";
            DueDateTextBox.Text = dueDate.ToString();

            //BillDateLabel.Text = patient.DueDate.ToString();
            //TotalFeeLabel.Text = totalAmount + " Taka";
            //DueAmountLabel.Text = dueAmount + " Taka";
            //PaidAmountLabel.Text = (totalAmount - dueAmount).ToString() + " Taka";

            ViewState["DueAmount"] = dueAmount;
            ViewState["dueDate"] = dueDate;
            ViewState["mobileNo"] = mobileNo;
            ViewState["billNo"] = patient.BillNumber;

           // ViewState["billNo"] = billNo;
            AmountTextBox.Enabled = false;
            DueDateTextBox.Enabled = false;
           // PaymentButton.Enabled = true;
        }

        private void ShowPaymentInformationByBillNo(string billNo)
        {
            //TestEntry testEntry = paymentManager.SearchByBill(billNo);
            Patient patient = paymentManager.SearchPatientInfoByBillNo(billNo);

            //totalAmount = Convert.ToDecimal(testEntry.TotalAmount);
            dueAmount = Convert.ToDecimal(patient.DueAmount.ToString());
            dueDate = Convert.ToDateTime(patient.DueDate.ToString());


            AmountTextBox.Text = dueAmount + " Taka";
            DueDateTextBox.Text = dueDate.ToString();


            //BillDateLabel.Text = patient.DueDate.ToString();
            //TotalFeeLabel.Text = totalAmount + " Taka";
            //DueAmountLabel.Text = dueAmount + " Taka";
            //PaidAmountLabel.Text = (totalAmount - dueAmount).ToString() + " Taka";

            ViewState["DueAmount"] = dueAmount;
            ViewState["dueDate"] = dueDate;
            ViewState["billNo"] = billNo;

            //Enable Payment Button and textbox
            AmountTextBox.Enabled = false;
            DueDateTextBox.Enabled = false;
           // PaymentButton.Enabled = true;
            //AmountTextBox.Enabled = true;
        }


        protected void PaymentButton_Click(object sender, EventArgs e) {

            //check empty
            //if (AmountTextBox.Text == "")
            //{
            //    InfoMessageLabel.Visible = true;
            //    InfoMessageLabel.Text = "Empty Amount.";
            //    InfoMessageLabel.BackColor = Color.DarkRed;
            //    return;
            //}

            //collect the amount;

           
            
           // decimal paidAmount = Convert.ToDecimal(AmountTextBox.Text);

           // dueAmount = (decimal)ViewState["DueAmount"];


            //check amount is greater than the dueAmount
            //if (_paidAmount > dueAmount)
            //{
            //    InfoMessageLabel.Visible = true;
            //    InfoMessageLabel.Text = "Cannot Proced. Payment Amount greater than Due Amount.";
            //    InfoMessageLabel.BackColor = Color.DarkRed;
            //    return;
            //}

            //if (_paidAmount == dueAmount)
            //{
            //    //save information with UpdateStatus = 1
            //    decimal _newDueAmount = dueAmount - _paidAmount;
            //    string message = paymentManager.UpdatePaymentWithStatus(_billNo, _newDueAmount, 1);
            //    if (message == "success")
            //    {

            //        ViewState["success"] = true;
            //        InfoMessageLabel.Visible = true;
            //        InfoMessageLabel.Text = "Full Paid!! Update customer information.";
            //        InfoMessageLabel.BackColor = Color.ForestGreen;
            //        ShowPaymentInformationByBillNo(_billNo);
            //    }

            //    return;
            //}
            //else
            //{
                //everything is fine.
               // decimal newDueAmount = dueAmount - paidAmount;
                //check the newDueAmount
                //string message = paymentManager.UpdatePayment(billNo, newDueAmount);
            if (item_check.Checked)
            {
                string billNo = (string)ViewState["billNo"];

                string message = paymentManager.UpdatePayment(billNo);
                if (message == "success")
                {
                    ViewState["success"] = true;
                    InfoMessageLabel.Visible = true;
                    InfoMessageLabel.Text = "Successfully Paid!";
                    item_check.Checked = false;
                    item_check.ForeColor = Color.Red;
                    PaymentButton.Enabled = false;
                    InfoMessageLabel.BackColor = Color.ForestGreen;
                    ShowPaymentInformationByBillNo(billNo);
                }

            }
            else
            {
                InfoMessageLabel.Visible = true;
                InfoMessageLabel.Text = "Tick Check Box and make sure the payment confirmation!";
                InfoMessageLabel.BackColor = Color.Red;
                item_check.Focus();
            }

               

                //return;
            //}

        }

        protected void item_check_CheckedChanged(object sender, EventArgs e)
        {
            if (item_check.Checked)
            {
                item_check.ForeColor = Color.Green;
                PaymentButton.Enabled = true;
            }
            else
            {
                item_check.ForeColor = Color.Red;
                PaymentButton.Enabled = false;
            }
        }
    }
}