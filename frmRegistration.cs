using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganizationProfile
{
    public partial class frmRegistration : Form
    {
        public frmRegistration()
        {
            InitializeComponent();
        }

        private String _FullName;  
        private int _Age;         
        private long _ContactNo;   
        private long _StudentNo;

        private void btnRegistar_Click(object sender, EventArgs e)
        {
            try
            {
                // Perform validation checks for all fields
                long studentNo = StudentNumber(txtStudentNo.Text);
                long contactNo = ContactNo(txtContactNo.Text);
                int age = Age(txtAge.Text);

                // If any of the validation methods return an invalid result, stop the process
                if (studentNo == -1 || contactNo == -1 || age == -1)
                {
                    MessageBox.Show("Please correct the highlighted errors before proceeding.");
                    return; // Do not proceed to show the confirmation form
                }

                // If all fields are valid, proceed with setting information
                StudentInformationClass.SetFullName = $"{txtLastName.Text}, {txtFirstName.Text}, {txtMiddleInitial.Text}";
                StudentInformationClass.SetStudentNo = studentNo;
                StudentInformationClass.SetProgram = cbPrograms.Text;
                StudentInformationClass.SetGender = cbGender.Text;
                StudentInformationClass.SetContactNo = contactNo;
                StudentInformationClass.SetAge = age;
                StudentInformationClass.SetBirthday = datePickerBirthday.Value.ToString("yyyy-MM-dd");

                // Show the confirmation form if all validations passed
                frmConfirmation frm = new frmConfirmation();
                frm.ShowDialog();
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Format error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Registration process completed.");
            }
        }


        private void frmRegistration_Load(object sender, EventArgs e)
        {

            string[] ListOfProgram = new string[]
             {
                "BS Information Technology",
                "BS Computer Science",
                "BS Information Systems",
                "BS in Accountancy",
                "BS in Hospitality Management",
                "BS in Tourism Management"
             };

            try
            {
                cbPrograms.Items.AddRange(ListOfProgram);
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("An error occurred while loading the programs: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Program loading process completed.");
            }
        }

        /////return methods 
        public long StudentNumber(string studNum)
        {
            try
            {
                if (long.TryParse(studNum, out _StudentNo))
                {
                    return _StudentNo;
                }
                else
                {
                    throw new FormatException("Invalid student number format.");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return -1;  
            }
            catch (OverflowException ex)
            {
                MessageBox.Show("Error: Student number is too large. " + ex.Message);
                return -1; 
            }
            finally
            {
                Console.WriteLine("Student number processing complete.");
            }
        }

        public long ContactNo(string contact)
        {
            try
            {
                if (Regex.IsMatch(contact, @"^\+?\d{9,13}$"))
                {
                    string processedContact = contact.StartsWith("+") ? contact.Substring(1) : contact;
                    _ContactNo = long.Parse(processedContact);
                    return _ContactNo;
                }
                else
                {
                    throw new FormatException("Invalid contact number format. It should start with '+' followed by 9 to 13 digits.");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return -1; 
            }
            catch (OverflowException ex)
            {
                MessageBox.Show("Error: Contact number is too large. " + ex.Message);
                return -1; 
            }
            finally
            {
                Console.WriteLine("Contact number processing complete.");
            }
        }

        public string FullName(string LastName, string FirstName, string MiddleInitial)
        {
            try
            {
                if (string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(FirstName))
                {
                    throw new ArgumentNullException("Name fields cannot be empty.");
                }

                if (Regex.IsMatch(LastName, @"^[a-zA-Z]+$") && Regex.IsMatch(FirstName, @"^[a-zA-Z]+$") && Regex.IsMatch(MiddleInitial, @"^[a-zA-Z]+$"))
                {
                    _FullName = $"{LastName}, {FirstName}, {MiddleInitial}";
                    return _FullName;
                }
                else
                {
                    throw new FormatException("Names can only contain letters.");
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return string.Empty;  
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return string.Empty;  
            }
            finally
            {
                Console.WriteLine("Full name processing complete.");
            }
        }

        public int Age(string age)
        {
            try
            {
                if (int.TryParse(age, out _Age))
                {
                    return _Age;
                }
                else
                {
                    throw new FormatException("Invalid age format.");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return -1;  
            }
            catch (OverflowException ex)
            {
                MessageBox.Show("Error: Age is too large. " + ex.Message);
                return -1; 
            }
            finally
            {
                Console.WriteLine("Age processing complete.");
            }
        }
    }
}
