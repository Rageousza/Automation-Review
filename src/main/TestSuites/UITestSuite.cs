using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using AbsaAutomation.src.main.Tools;
using System.Linq;

namespace AbsaAutomation.src.main.TestSuites
{
    [TestClass]
    public class UITestSuite 
    {
       public TestContext TestContext { get; set; }

       System.Random random = new System.Random();

        //Xpath Locators
       private static By FirstName() { return By.XPath("//span[contains(text(), 'First Name')]");  }
       private static By AddUser() { return By.XPath("//button[@type = 'add']");  }
       private static By AddFNameField() { return By.XPath("//input[@name= 'FirstName']"); }
       private static By AddLNameField() { return By.XPath("//input[@name= 'LastName']");  }
       private static By AddUNameField() { return By.XPath("//input[@name= 'UserName']");  }
       private static By AddPasswordField() { return By.XPath("//input[@name= 'Password']"); }
       private static By AddEmailField() { return By.XPath("//input[@name= 'Email']"); }
       private static By AddCellPhoneField() { return By.XPath("//input[@name= 'Mobilephone']"); }
       private static By RB_CompanyAAA() { return By.XPath("//input[@value= '15']");  }
       private static By RB_CompanyBBB() { return By.XPath("//input[@value= '16']"); }
       private static By AdminRole() { return By.XPath("//option[@value= '2']");  }
       private static By CustomerRole() { return By.XPath("//option[@value= '1']"); }
       private static By SaveUser() { return By.XPath("//button[@ng-click= 'save(user)']"); }
       private static By ValidateAddedUser(string fname) { return By.XPath("//tr[@class= 'smart-table-data-row ng-scope']//td[contains(text(), '" + fname + "')]"); }


        [ClassInitialize]
        public static void Initialize(TestContext TestContext)
        {
            string[] s = TestContext.FullyQualifiedTestClassName.Split('.');
            var suiteName = s[s.Length - 1];
            Reporting.ReportName = suiteName;
        }

        [TestInitialize]
        public void BeforeEach()
        {
            Reporting.starttime = DateTime.Now;
            Reporting.TestName = TestContext.TestName;

            Reporting.CreateTest();
            Reporting.WriteToLogFile("[START] - Test Started - " + Reporting.TestName);

            Reporting.DriverInstance = new ObjectCalls();

            ObjectCalls.Navigate();
        }

        [TestCleanup]
        public void AfterEach()
        {
            ObjectCalls.DriverClose();
        }

        [TestMethod]
        public void UITest()
        {
            //Generates a random number to ensure Fname, Lname and User is unique
            int i = random.Next(50);

            //Stores variable to verify that user has been added to table
            string Fname = "FName" + i;

            //This is the second instance of the UI Test - Adding an Admin
            ObjectCalls.WaitForElement(FirstName());
            ObjectCalls.ClickElement(AddUser());
            ObjectCalls.EnterText(AddFNameField(), "FName" + i);
            ObjectCalls.EnterText(AddLNameField(), "LName" + i);
            ObjectCalls.EnterText(AddUNameField(), "User" + i);
            ObjectCalls.EnterText(AddPasswordField(), "Pass" + i);
            ObjectCalls.ClickElement(RB_CompanyAAA());
            ObjectCalls.ClickElement(AdminRole());
            ObjectCalls.EnterText(AddEmailField(), "admin@mail.com");
            ObjectCalls.EnterText(AddCellPhoneField(), "082555");
            Reporting.StepPassedWithScreenshot("Added Details");
            ObjectCalls.ClickElement(SaveUser());
            ObjectCalls.Pause();
            ObjectCalls.WaitForElement(ValidateAddedUser(Fname));
            Reporting.StepPassedWithScreenshot("Validate User "+ Fname +" has been added to the list");
        }

        [TestMethod]
        public void UITest2()
        {
            //Generates a random number to ensure Fname, Lname and User is unique
            int i = random.Next(50);

            //Stores variable to verify that user has been added to table
            string Fname = "FName" + i;

            //This is the second instance of the UI Test - Adding a Customer
            ObjectCalls.WaitForElement(FirstName());
            ObjectCalls.ClickElement(AddUser());
            ObjectCalls.EnterText(AddFNameField(), "FName" + i);
            ObjectCalls.EnterText(AddLNameField(), "LName" + i);
            ObjectCalls.EnterText(AddUNameField(), "User" + i);
            ObjectCalls.EnterText(AddPasswordField(), "Pass" + i);
            ObjectCalls.ClickElement(RB_CompanyBBB());
            ObjectCalls.ClickElement(CustomerRole());
            ObjectCalls.EnterText(AddEmailField(), "customer@mail.com");
            ObjectCalls.EnterText(AddCellPhoneField(), "082444");
            Reporting.StepPassedWithScreenshot("Added Details");
            ObjectCalls.ClickElement(SaveUser());
            ObjectCalls.Pause();
            ObjectCalls.WaitForElement(ValidateAddedUser(Fname));
            Reporting.StepPassedWithScreenshot("Validate User " + Fname + " has been added to the list");
        }
    }
}
