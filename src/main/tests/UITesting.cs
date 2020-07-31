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
    public class UITesting 
    {
        private SeleniumMethods _SeleniumMethodInst;
        private ExtentTest _CurrentTest;

        private IWebDriver driver;
        public UITesting(SeleniumMethods SeleniumMethodInst, ExtentTest CurrentTest)
        {
            _SeleniumMethodInst = SeleniumMethodInst;
            _CurrentTest = CurrentTest;
            driver = _SeleniumMethodInst.GetDriver;
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
            _SeleniumMethodInst.WaitForElement(FirstName(), 5, _CurrentTest);

            for (int i = 0; i < 2; i++)
            {
                int k = _SeleniumMethodInst.GenerateRandomNumber(100);
                DataTable CSVRead = new DataReader().readCSVfile(CSVFileName);

                _SeleniumMethodInst.WaitForElement(FirstName(), 5, _CurrentTest);
                _SeleniumMethodInst.ClickElement(AddUser(), _CurrentTest);

                string fname = new DataReader().RetrieveDataFromDataTable(CSVRead, "firstname", i);
                string lname = new DataReader().RetrieveDataFromDataTable(CSVRead, "lastname", i);

                _SeleniumMethodInst.EnterText(AddFNameField(), fname, _CurrentTest, true);
                _SeleniumMethodInst.EnterText(AddLNameField(), lname, _CurrentTest, true);
                _SeleniumMethodInst.EnterText(AddUNameField(), new DataReader().RetrieveDataFromDataTable(CSVRead, "username", i), _CurrentTest, true);
                _SeleniumMethodInst.EnterText(AddPasswordField(), new DataReader().RetrieveDataFromDataTable(CSVRead, "password", i), _CurrentTest, true);
                _SeleniumMethodInst.ClickElement(RB_CompanyAAA(), _CurrentTest);
                _SeleniumMethodInst.ClickElement(AdminRole(), _CurrentTest);
                _SeleniumMethodInst.EnterText(AddEmailField(), new DataReader().RetrieveDataFromDataTable(CSVRead, "email", i), _CurrentTest, true);
                _SeleniumMethodInst.EnterText(AddCellPhoneField(), new DataReader().RetrieveDataFromDataTable(CSVRead, "cell", i), _CurrentTest, true);

                Base.Report.StepPassedWithScreenshot("Added Details", driver, _CurrentTest);
                _SeleniumMethodInst.ClickElement(SaveUser(), _CurrentTest);
                _SeleniumMethodInst.Pause();

                _SeleniumMethodInst.WaitForElement(ValidateAddedUser(fname), 5, _CurrentTest);
                Base.Report.StepPassedWithScreenshot("Validate User " + fname + " has been added to the list", driver, _CurrentTest);
            }
        }

        public void JSONTest(string File)
        {
            _SeleniumMethodInst.WaitForElement(FirstName(), 5, _CurrentTest);

            for(int i = 0; i < 2; i++)
            {
                int k = _SeleniumMethodInst.GenerateRandomNumber(100);
                List<User> user = new DataReader().LoadJson(File);
                string firstname = user[i].Firstname;
                string lastname = user[i].Lastname;
                string username = user[i].Username;
                string password = user[i].Password;
                string customer = user[i].Customer;
                string role = user[i].Role;
                string email = user[i].Email;
                string cell = user[i].Cell.ToString();

                _SeleniumMethodInst.WaitForElement(FirstName(), 5, _CurrentTest);
                _SeleniumMethodInst.ClickElement(AddUser(), _CurrentTest);
                _SeleniumMethodInst.EnterText(AddFNameField(), firstname + k, _CurrentTest , true);
                _SeleniumMethodInst.EnterText(AddLNameField(), lastname + k, _CurrentTest, true);
                _SeleniumMethodInst.EnterText(AddUNameField(), username + k, _CurrentTest,  true);
                _SeleniumMethodInst.EnterText(AddPasswordField(), password + k, _CurrentTest, true);
                _SeleniumMethodInst.ClickElement(CompanyRadioButton(customer), _CurrentTest);
                _SeleniumMethodInst.ClickElement(CustomerRole(), _CurrentTest);
                _SeleniumMethodInst.EnterText(AddEmailField(), email, _CurrentTest, true);
                _SeleniumMethodInst.EnterText(AddCellPhoneField(), cell, _CurrentTest, true);
                Base.Report.StepPassedWithScreenshot("Added Details", driver, _CurrentTest);
                _SeleniumMethodInst.ClickElement(SaveUser(), _CurrentTest);
                _SeleniumMethodInst.Pause();
                _SeleniumMethodInst.WaitForElement(ValidateAddedUser(firstname), 5, _CurrentTest);
                Base.Report.StepPassedWithScreenshot("Validate User " + firstname + " has been added to the list", driver, _CurrentTest);
            }
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
    }
}