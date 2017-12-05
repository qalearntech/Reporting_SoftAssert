using System;
using MyTestApp;
using NUnit.Framework;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace CalculatorTest
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator testObj = null;
        private ExtentHtmlReporter htmlReporter = null;
        private ExtentReports extent = null;
        private ExtentTest test = null;

        [SetUp]
        public void InitialSetup()
        {
            string reportPath = @"D:\MyOwnReport.html";
            extent = new ExtentReports();
            htmlReporter = new ExtentHtmlReporter(reportPath);
            
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Krishna");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("User Name", "Kishore G");
            Thread.Sleep(2000);
            Console.WriteLine($"Setup method for {TestContext.CurrentContext.Test.MethodName}");
            testObj = new Calculator();
        }
        
        [Test]
        [Parallelizable]
        [Category("1")]
        [Category("3")]
        public void AddTest()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            try
            {
                test.Log(Status.Info, "This is step 1");
                test.Log(Status.Debug, "This is step 2");
                test.Pass("Step 1 and 2 passed");
                Assert.AreEqual(6, testObj.Add(2, 3), "Verified add functionality");
                test.Pass("Addition operation is success");
            }
            catch(Exception e)
            {
                test.Fail(e.Message);
            }
        }

        [Test]
        [Parallelizable]
        [Category("2")]
        public void DiffTest()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            test.Info("This is info log");
            Assert.AreEqual(4, testObj.Diff(8, 4), "Verified difference functionality");
        }

        [Test]
        [Category("4")]
        public void SoftAssertTest()
        {
            Soft.Assert(() => Assert.AreEqual(3, 2, "AreEqual"));
            Console.WriteLine("Continuing with the test after soft assert failure");
            Assert.AreEqual(3,5,"3 and 5 are equal");
        }

        /// <summary>
        /// Called after each test method. Hence, checking if the test method had a soft assert failure and if so, marked as fail.
        /// </summary>
        [TearDown]
        public void BeforeEndTest()
        {
            if (Soft.softAssertFailed)
                Assert.Fail("Soft assert failed");            
        }

        /// <summary>
        /// This method will be called once for entire fixture. Hence, report writing is deleted to this method
        /// </summary>
        [OneTimeTearDown]
        public void SuiteComplete()
        {
            extent.Flush();
        }
    }
}
