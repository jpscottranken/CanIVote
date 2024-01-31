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

namespace CanIVoteIfStatements
{
    public partial class frmCanIVoteIfStatements : Form
    {
        //  Declare and initialize program constants
        const int MINAGE =   1;
        const int MAXAGE = 125;
        const int LEGALV =  18;

        public frmCanIVoteIfStatements()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            bool keepGoing = ValidateAgeIsNotEmpty();

            if (keepGoing)
            {
                keepGoing = ValidateAgeIsNumeric();
            }
            else
            {
                return;
            }

            if (keepGoing)
            {
                keepGoing = ValidateAgeIsWithinRange();
            }
            else
            {
                return;
            }

            if (keepGoing)
            {
                DetermineVoteStatus();
            }
            else
            {
                return;
            }
        }

        private bool ValidateAgeIsNotEmpty()
        {
            bool retVal = true;
            string ageStr = txtInputAge.Text.Trim();

            if (ageStr == "")
            {
                ShowErrorMessage("Age Cannot Be Empty",
                                 "NOTHING IN AGE TEXTBOX");
                txtInputAge.Focus();
                retVal = false;
            }

            return retVal;
        }

        private bool ValidateAgeIsNumeric()
        {
            bool retVal = true;
            bool result;
            string ageStr = txtInputAge.Text.Trim();
            int age;

            result = int.TryParse(ageStr, out age);
            if(!result)
            {
                ShowErrorMessage("Age Must Be Numeric",
                                 "NON-NUMERIC IN AGE TEXTBOX");
                txtInputAge.Text = "";
                lblResult.Text = "";
                txtInputAge.Focus();
                retVal = false;
            }

            return retVal;
        }

        private bool ValidateAgeIsWithinRange()
        {
            bool retVal = true;
            int age = int.Parse(txtInputAge.Text);

            if (age < MINAGE || age > MAXAGE)
            {
                ShowErrorMessage("Age Must Be Between " +
                                 MINAGE + " and " + MAXAGE,
                                 "OUT-OF-RANGE VALUE IN AGE TEXTBOX");
                txtInputAge.Text = "";
                lblResult.Text = "";
                txtInputAge.Focus();
                retVal = false;
            }

            return retVal;
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
