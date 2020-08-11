using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AbsaAutomation.src.main.Core;
using AbsaAutomation.src.main.Tools;
using AbsaAutomation.src.Models;
using AbsaAutomation.src.tests;
using AngleSharp;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

//Next Steps
//File Reader to read in values 
//JSON Models { GET; SET; }
//serialization
//populate user class

namespace AbsaAutomation.src.tests
{
    public class UserPage : SeleniumController
    {
        public UserPage(IWebDriver driver, ExtentTest CurrentTest) : base(driver)
        {
            _curTest = CurrentTest;            
        }

        private By FirstName() { return By.XPath("//span[contains(text(), 'First Name')]"); }
        private By AddUser() { return By.XPath("//button[@type = 'add']"); }
        private By ValidateAddedUser(string fname) { return By.XPath("//tr[@class= 'smart-table-data-row ng-scope']//td[contains(text(), '" + fname + "')]"); }
        private By AddFNameField() { return By.XPath("//input[@name= 'FirstName']"); }
        private By AddLNameField() { return By.XPath("//input[@name= 'LastName']"); }
        private By AddUNameField() { return By.XPath("//input[@name= 'UserName']"); }
        private By AddPasswordField() { return By.XPath("//input[@name= 'Password']"); }
        private By AddEmailField() { return By.XPath("//input[@name= 'Email']"); }
        private By AddCellPhoneField() { return By.XPath("//input[@name= 'Mobilephone']"); }
        private By RB_CompanyAAA() { return By.XPath("//input[@value= '15']"); }
        private By RB_CompanyBBB() { return By.XPath("//input[@value= '16']"); }
        private By AdminRole() { return By.XPath("//option[@value= '2']"); }
        private By CustomerRole() { return By.XPath("//option[@value= '1']"); }
        private By SaveUser() { return By.XPath("//button[@ng-click= 'save(user)']"); }
        private By CompanyRadioButton(string Company) { return By.XPath("//label[text()='"+ Company +"']");  }

        public void CSVTest(string CSVFileName)
        {
            WaitForElement(FirstName());

            for (int i = 0; i < 2; i++)
            {
                int k = GenerateRandomNumber(100);
                DataTable CSVRead = new DataReader().readCSVfile(CSVFileName);

                WaitForElement(FirstName());
                ClickElement(AddUser());

                string fname = new DataReader().RetrieveDataFromDataTable(CSVRead, "firstname", i);
                string lname = new DataReader().RetrieveDataFromDataTable(CSVRead, "lastname", i);

                EnterText(AddFNameField(), fname, true);
                EnterText(AddLNameField(), lname, true);
                EnterText(AddUNameField(), new DataReader().RetrieveDataFromDataTable(CSVRead, "username", i), true);
                EnterText(AddPasswordField(), new DataReader().RetrieveDataFromDataTable(CSVRead, "password", i), true);
                ClickElement(RB_CompanyAAA());
                ClickElement(AdminRole());
                EnterText(AddEmailField(), new DataReader().RetrieveDataFromDataTable(CSVRead, "email", i), true);
                EnterText(AddCellPhoneField(), new DataReader().RetrieveDataFromDataTable(CSVRead, "cell", i), true);

                StepPassScreenshot("Added Details");
                ClickElement(SaveUser());
                Pause();

                WaitForElement(ValidateAddedUser(fname), 5);
                StepPassScreenshot("Validate User " + fname + " has been added to the list");
            }
        }

        public void JSONTest(string File)
        {
            WaitForElement(FirstName());

            for(int i = 0; i < 2; i++)
            {
                int k = GenerateRandomNumber(100);
                List<User> user = DataReader.LoadJson<List<User>>(File);
                string firstname = user[i].Firstname;
                string lastname = user[i].Lastname;
                string username = user[i].Username;
                string password = user[i].Password;
                string customer = user[i].Customer;
                string role = user[i].Role;
                string email = user[i].Email;
                string cell = user[i].Cell.ToString();

                WaitForElement(FirstName());
                ClickElement(AddUser());
                EnterText(AddFNameField(), firstname + k, true);
                EnterText(AddLNameField(), lastname + k, true);
                EnterText(AddUNameField(), username + k , true);
                EnterText(AddPasswordField(), password + k, true);
                ClickElement(CompanyRadioButton(customer));
                ClickElement(CustomerRole());
                EnterText(AddEmailField(), email, true);
                EnterText(AddCellPhoneField(), cell, true);
                StepPassScreenshot("Added Details");
                ClickElement(SaveUser());
                Pause();
                WaitForElement(ValidateAddedUser(firstname), 5);
                StepPassScreenshot("Validate User " + firstname + " has been added to the list");
            }
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
    }
}