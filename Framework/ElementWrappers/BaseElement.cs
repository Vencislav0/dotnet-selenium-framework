﻿using Automation_Framework.Framework.Constants;
using Automation_Framework.Framework.Logging;
using Automation_Framework.Framework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.ElementWrappers;

public class BaseElement
{
    protected By locator;
    protected string name;
    protected IWebDriver driver;
    protected WebDriverWait wait;
    protected CustomWaits customWaits;
    protected Actions actions;
    protected TimeSpan timeout = Timeouts.DEFAULT_WAIT;
    public BaseElement(IWebDriver driver, By locator, string name) 
    {
        this.locator = locator;
        this.name = name;
        this.driver = driver;
        this.actions = new Actions(driver);
        this.wait = new WebDriverWait(driver, timeout);
        this.customWaits = new CustomWaits(locator, driver, timeout);
    }

    public IWebElement GetElement()
    {
       var element = wait.Until(dr => dr.FindElement(locator));
        return element;
    }
   
    public void Click()
    {
        try
        {
            Logger.Debug($"Clicking on: {name}");
            customWaits.WaitUntilVisible();
            customWaits.WaitUntilEnabled();
            GetElement().Click();

        }
        catch (Exception ex)
        {

            Logger.Error($"Failed to click on element: {name}", ex);
            throw;
        }
    }

    public string GetText()
    {
        try
        {
            Logger.Debug($"Getting text from: {name}");
            string text = GetElement().Text;
            Logger.Debug($"Text: {text}");
            return text;

        }
        catch (Exception ex)
        {

            Logger.Error($"Failed to get text from element: {name}", ex);
            throw;
        }

    }

    public bool IsDisplayed()
    {
        try
        {
            Logger.Debug($"Checking if {name} is displayed.");
            bool isDisplayed = GetElement().Displayed;
            Logger.Debug($"Visible: {isDisplayed}");
            return isDisplayed;

        }
        catch (Exception)
        {
            Logger.Warn($"Element {name} was not displayed");
            return false;
        }
    }

    public void Hover()
    {
        try
        {
            Logger.Debug($"Hovering on: {name}");
            customWaits.WaitUntilVisible();
            actions.MoveToElement(GetElement()).Perform();

        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to hover element: {name}", ex);
            throw;

        }

    }

    public void ScrollToElement()
    {
        try
        {
            Logger.Debug($"Scrolling to element: {name}");
            customWaits.WaitUntilVisible();
            actions.ScrollToElement(GetElement()).Perform();

        }
        catch (Exception ex)
        {

            Logger.Error($"Failed to hover element: {name}", ex);
            throw;
        }
    }

    public bool IsEnabled()
    {
        try
        {
            Logger.Debug($"Checking if {name} is enabled.");
            customWaits.WaitUntilEnabled();
            return true;

        }
        catch (Exception ex)
        {
            Logger.Error($"Element {name} was not enabled after timeout: {timeout.TotalSeconds} seconds.", ex);
            return false;
        }
    }

    public string GetAttribute(string attributeName)
    {
        try
        {
            Logger.Debug($"Getting {attributeName} attribute from: {name}");
            customWaits.WaitUntilVisible();
            customWaits.WaitUntilEnabled();
            return GetElement().GetAttribute(attributeName);

        }
        catch (Exception ex)
        {

            Logger.Error($"Failed to get {attributeName} attribute on element: {name}", ex);
            throw;
        }

    }

    public string GetCssValue(string propertyName)
    {
        try
        {
            Logger.Debug($"Getting CSS value {propertyName} from: {name}");
            customWaits.WaitUntilVisible();
            customWaits.WaitUntilEnabled();
            return GetElement().GetCssValue(propertyName);

        }
        catch (Exception ex)
        {

            Logger.Error($"Failed to get {propertyName} attribute on element: {name}", ex);
            throw;
        }

    }
    
}
