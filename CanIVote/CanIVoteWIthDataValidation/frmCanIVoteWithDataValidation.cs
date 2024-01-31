using System;
using System.Windows.Forms;

/*
 *      Assume that the legal age for a person
 *      to vote is 18 (const int LEGALVOTER = 18;).
 *      
 *      Write a C# program that lets the user
 *      input an age. Verify (each check in its
 *      own method) that: 
 *      
 *      -   The age textbox is not empty.
 *      -   The value in the age textbox is
 *          numeric.
 *      -   The value in the age textbox is
 *          between 1 (const int MINAGE = 1;)
 *          and 125 (const int MAXAGE = 125;).
 */

namespace CanIVoteWIthDataValidation
{
    public partial class frmCanIVoteWithDataValidation : Form
    {
        //  Declare and initialize program constants
        const int MINAGE = 1;
        const int MAXAGE = 125;
        const int LEGALV = 18;

        public frmCanIVoteWithDataValidation()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            VoterValidation();
        }

        private void VoterValidation()
        {
            try
            {
                if (IsValidData())
                {
                    DetermineVoteStatus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString() + "\n" + ex.StackTrace, "Exception");
            }
        }

        private bool IsValidData()
        {
            bool success = true; 
            string errorMessage = "";

            // Validate the Input Age text box
            errorMessage += IsPresent(txtInputAge.Text, "Inputted Age");
            errorMessage += IsInt32(txtInputAge.Text, "Inputted Age");
            errorMessage += IsWithinRange(txtInputAge.Text, "Inputted Age", MINAGE, MAXAGE);

            if (errorMessage != "")
            {
                success = false; 
                MessageBox.Show(errorMessage, "Entry Error");
            }
            return success;
        }
        private void DetermineVoteStatus()
        {
            int age = int.Parse(txtInputAge.Text);

            if (age >= LEGALV)
            {
                lblResult.Text = ($"AT AGE {age}, YOU CAN VOTE!");
            }
            else
            {
                lblResult.Text = ($"AT AGE {age}, YOU CANNOT VOTE!");
            }
        }

        private string IsPresent(string value, string name)
        {
            string msg = ""; if (value == "")
            {
                msg = name + " is a required field.\n";
                Reset();
            }
            return msg;
        }

        private string IsInt32(string value, string name)
        {
            string msg = "";
            if (!Int32.TryParse(value, out _))
            {
                msg = name + " must be a valid integer value.\n";
                Reset();
            }
            return msg;
        }

        private string IsWithinRange(string value, string name, decimal min, decimal max)
        {
            string msg = "";
            if (Decimal.TryParse(value, out decimal number))
            {
                if (number < min || number > max)
                {
                    msg = name + " must be between " + min + " and " + max + ".\n";
                    Reset();
                }
            }
            return msg;
        }

        private void Reset()
        {
            txtInputAge.Text = "";
            txtInputAge.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtInputAge.Text = "";
            lblResult.Text = "";
            txtInputAge.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitProgramOrNot();
        }

        private void ExitProgramOrNot()
        {
            DialogResult dialog = MessageBox.Show(
                        "Do You Really Want To Exit The Program?",
                        "EXIT NOW?",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ShowErrorMessage(string msg, string title)
        {
            MessageBox.Show(msg, title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }
    }
}
