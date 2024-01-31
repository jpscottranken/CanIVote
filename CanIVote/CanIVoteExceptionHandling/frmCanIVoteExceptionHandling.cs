using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

namespace CanIVoteExceptionHandling
{
    public partial class frmCanIVoteExceptionHandling : Form
    {
        //  Declare and initialize program constants
        const int MINAGE = 1;
        const int MAXAGE = 125;
        const int LEGALV = 18;

        public frmCanIVoteExceptionHandling()
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
            string ageStr = txtInputAge.Text.Trim();
            try
            {
                if (ageStr == "")
                {
                    throw new ArgumentNullException();
                }

                return true;
            }
            catch (ArgumentException ane)
            {
                ShowErrorMessage("System Message:\t" + ane.Message +
                                 "\n\nSystem Type:\t" + ane.GetType().Name.ToString() +
                                 "\n\nSystem Trace:\t" + ane.StackTrace +
                                 "\n\nAge Cannot Be Empty",
                                 "NOTHING IN AGE TEXTBOX");
                txtInputAge.Focus();
                return false;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("System Message:\t" + ex.Message +
                                 "\n\nSome Other Problem Occurred",
                                 "ERROR IN CODE");
                txtInputAge.Focus();
                return false;
            }
        }

        private bool ValidateAgeIsNumeric()
        {
            bool result;
            string ageStr = txtInputAge.Text.Trim();

            try
            {
                result = int.TryParse(ageStr, out _);
                if (!result)
                {
                    throw new ArithmeticException();
                }

                return true;
            }
            catch (ArithmeticException ae)
            {
                ShowErrorMessage("System Message:\t" + ae.Message +
                                 "\n\nAge Must Be Numeric",
                                 "NON-NUMERIC IN AGE TEXTBOX");
                txtInputAge.Text = "";
                lblResult.Text = "";
                txtInputAge.Focus();
                return false;
            }
        }

        private bool ValidateAgeIsWithinRange()
        {
            int age = int.Parse(txtInputAge.Text);

            try
            {
                if (age < MINAGE || age > MAXAGE)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return true;
            }
            catch(ArgumentOutOfRangeException aoore)
            {
                ShowErrorMessage("System Message:\t" + aoore.Message +
                                 "\n\nAge Must Be Between " +
                                 MINAGE + " and " + MAXAGE,
                                 "OUT-OF-RANGE VALUE IN AGE TEXTBOX");
                txtInputAge.Text = "";
                lblResult.Text = "";
                txtInputAge.Focus();
                return false;
            }
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
