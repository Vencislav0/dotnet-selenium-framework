using Allure.NUnit.Attributes;
using Allure.NUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Allure.Net.Commons;
using log4net;
using Automation_Framework.Framework.Logging;
using Automation_Framework.Framework.WebDriver;


namespace Automation_Framework
{
    [AllureNUnit]
    [AllureSuite("Default Suite")]
    public class BaseTest
    {
        protected IWebDriver driver;        
        [OneTimeSetUp]
        public void GlobalSetup()
        {            
            AllureLifecycle.Instance.CleanupResultDirectory();
            driver = WebDriverFactory.GetChromeDriver();
        }

        [SetUp]
        public void Setup()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            Logger.SetLogFileForTest(testName);
        }

        [TearDown]
        public void Teardown()
        {
            string? logFilePath = Logger.GetCurrentLogFilePath();

            if (File.Exists(logFilePath))
            {
                byte[] logBytes = File.ReadAllBytes(logFilePath);
                AllureApi.AddAttachment("Test Execution Logs", "text/plain", logBytes, ".txt");
            }

            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;

            if (testStatus == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var screenshotBytes = screenshot.AsByteArray;
                AllureApi.AddAttachment("Screenshot", "image/png", screenshotBytes);
            }
        }

        [OneTimeTearDown]
        public void GlobalTearDown() 
        {
            driver.Dispose();
            LogManager.Shutdown();
        }

        [Test]
        public void Test1()
        {
            Logger.Error("AAA");
            Assert.Pass();
        }
    }
}