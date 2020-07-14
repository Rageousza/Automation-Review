using System;
using System.Collections.Generic;
using System.Text;
using AbsaAutomation.src.main.Core;
using AbsaAutomation.src.main.Tools;
using OpenQA.Selenium;


namespace AbsaAutomation.src.tests
{
    public class UITesting : Base
    {
        static Random random = new Random();
        //POM
        private static By FirstName() { return By.XPath("//span[contains(text(), 'First Name')]"); }
        private static By AddUser() { return By.XPath("//button[@type = 'add']"); }
        private static By AddFNameField() { return By.XPath("//input[@name= 'FirstName']"); }
        private static By AddLNameField() { return By.XPath("//input[@name= 'LastName']"); }
        private static By AddUNameField() { return By.XPath("//input[@name= 'UserName']"); }
        private static By AddPasswordField() { return By.XPath("//input[@name= 'Password']"); }
        private static By AddEmailField() { return By.XPath("//input[@name= 'Email']"); }
        private static By AddCellPhoneField() { return By.XPath("//input[@name= 'Mobilephone']"); }
        private static By RB_CompanyAAA() { return By.XPath("//input[@value= '15']"); }
        private static By RB_CompanyBBB() { return By.XPath("//input[@value= '16']"); }
        private static By AdminRole() { return By.XPath("//option[@value= '2']"); }
        private static By CustomerRole() { return By.XPath("//option[@value= '1']"); }
        private static By SaveUser() { return By.XPath("//button[@ng-click= 'save(user)']"); }
        private static By ValidateAddedUser(string fname) { return By.XPath("//tr[@class= 'smart-table-data-row ng-scope']//td[contains(text(), '" + fname + "')]"); }

        public static string Test1()
        {
            //Generates a random number to ensure Fname, Lname and User is unique
            int i = random.Next(50);

            //Stores variable to verify that user has been added to table
            string Fname = "FName" + i;

            //This is the second instance of the UI Test - Adding an Admin
            ObjectCallsInstance.WaitForElement(FirstName());
            ObjectCallsInstance.ClickElement(AddUser());
            ObjectCallsInstance.EnterText(AddFNameField(), "FName" + i);
            ObjectCallsInstance.EnterText(AddLNameField(), "LName" + i);
            ObjectCallsInstance.EnterText(AddUNameField(), "User" + i);
            ObjectCallsInstance.EnterText(AddPasswordField(), "Pass" + i);
            ObjectCallsInstance.ClickElement(RB_CompanyAAA());
            ObjectCallsInstance.ClickElement(AdminRole());
            ObjectCallsInstance.EnterText(AddEmailField(), "admin@mail.com");
            ObjectCallsInstance.EnterText(AddCellPhoneField(), "082555");
            Reporting.StepPassedWithScreenshot("Added Details");
            ObjectCallsInstance.ClickElement(SaveUser());
            ObjectCallsInstance.Pause();
            ObjectCallsInstance.WaitForElement(ValidateAddedUser(Fname));
            Reporting.StepPassedWithScreenshot("Validate User " + Fname + " has been added to the list");

            return null;
        }

        public static string Test2()
        {
            //Generates a random number to ensure Fname, Lname and User is unique
            int i = random.Next(50);

            //Stores variable to verify that user has been added to table
            string Fname = "FName" + i;

            //This is the second instance of the UI Test - Adding a Customer
            ObjectCallsInstance.WaitForElement(FirstName());
            ObjectCallsInstance.ClickElement(AddUser());
            ObjectCallsInstance.EnterText(AddFNameField(), "FName" + i);
            ObjectCallsInstance.EnterText(AddLNameField(), "LName" + i);
            ObjectCallsInstance.EnterText(AddUNameField(), "User" + i);
            ObjectCallsInstance.EnterText(AddPasswordField(), "Pass" + i);
            ObjectCallsInstance.ClickElement(RB_CompanyBBB());
            ObjectCallsInstance.ClickElement(CustomerRole());
            ObjectCallsInstance.EnterText(AddEmailField(), "customer@mail.com");
            ObjectCallsInstance.EnterText(AddCellPhoneField(), "082444");
            Reporting.StepPassedWithScreenshot("Added Details");
            ObjectCallsInstance.ClickElement(SaveUser());
            ObjectCallsInstance.Pause();
            ObjectCallsInstance.WaitForElement(ValidateAddedUser(Fname));
            Reporting.StepPassedWithScreenshot("Validate User " + Fname + " has been added to the list");
            return null;
        }

    }
}
