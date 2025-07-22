using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.Utilities
{
    public class CustomWaits
    {
        protected IWebDriver driver;
        protected By locator;
        protected WebDriverWait wait;
        protected Actions actions;

        public CustomWaits(By locator, IWebDriver driver, TimeSpan timeout)
        {
            this.locator = locator;
            this.driver = driver;
            this.actions = new Actions(driver);
            this.wait = new WebDriverWait(driver, timeout);
        }
        public void WaitUntilVisible()
        {
            wait.Until(driver =>
            {
                var element = driver.FindElement(locator);
                return element.Displayed;

            });
        }

        public void WaitUntilHidden()
        {
            wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return !element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }

            });
        }

        public void WaitUntilEnabled()
        {
            wait.Until(driver =>
            {
                var element = driver.FindElement(locator);
                return element.Enabled;
            });
        }
    }
}
