using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    using System.Diagnostics;

    using OpenQA.Selenium.Chrome;

    [TestFixture]
    public class Untitled
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private string baseURLin;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.facebook.com/";
            baseURLin = "https://www.linkedin.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                // driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheUntitledTest()
        {
            //driver.Navigate().GoToUrl(baseURL + "/BIMOBWebApps");
            //driver.FindElement(By.Id("txtUserName")).Clear();
            //driver.FindElement(By.Id("txtUserName")).SendKeys("mintu");
            //driver.FindElement(By.Id("txtPassword")).Clear();
            //driver.FindElement(By.Id("txtPassword")).SendKeys("mintu");
            //Thread.Sleep(1500);
            //driver.FindElement(By.XPath("//button[@id='btnLoginUsers']")).SendKeys(Keys.Enter);
        }

        [Test]
        public void TheUnitTestFacebookLigin()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.FindElement(By.Id("email")).Clear();
            driver.FindElement(By.Id("email")).SendKeys("mahafuzhuq@ymail.com");
            driver.FindElement(By.Id("pass")).Clear();
            driver.FindElement(By.Id("pass")).SendKeys("mahafuz@huq");
            driver.FindElement(By.Id("u_0_r")).Click();

            // add friend
            driver.FindElement(By.Id("u_0_5")).Click();
            driver.FindElement(By.XPath("(//button[@value='1'])[3]")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("(//button[@value='1'])[3]")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("(//button[@value='1'])[3]")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("(//button[@value='1'])[3]")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("(//button[@value='1'])[3]")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("(//button[@value='1'])[3]")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("(//button[@value='1'])[3]")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("(//button[@value='1'])[3]")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("(//button[@value='1'])[3]")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("(//button[@value='1'])[3]")).Click();
        }

        [Test]
        public void TheLenkedinLogin()
        {
            driver.Navigate().GoToUrl(baseURLin + "/");
            driver.FindElement(By.Id("login-email")).Clear();
            driver.FindElement(By.Id("login-email")).SendKeys("mahafuzhuq@ymail.com");
            driver.FindElement(By.Id("login-password")).Clear();
            driver.FindElement(By.Id("login-password")).SendKeys("mahafuz$huq");
            driver.FindElement(By.Id("login-submit")).Click();
            Thread.Sleep(1500);

            driver.FindElement(By.CssSelector("#mynetwork-nav-item > a.nav-item__link.js-nav-item-link > span.nav-item__title")).Click();
            Thread.Sleep(1500);

            driver.FindElement(By.CssSelector("button.button-secondary-small")).Click();
            Thread.Sleep(1500);
        
            for (int i = 0; i < 10; i++)
            {
                if (i==9)
                {
                    driver.FindElement(By.CssSelector("#feed-nav-item > a.nav-item__link.js-nav-item-link > span.nav-item__title")).Click();
                    Thread.Sleep(2500);
                    driver.FindElement(By.CssSelector("#mynetwork-nav-item > a.nav-item__link.js-nav-item-link > span.nav-item__title")).Click();
                    Thread.Sleep(2500);
                    i = 0;
                }
                else
                {
                    driver.FindElement(By.CssSelector("#feed-nav-item > a.nav-item__link.js-nav-item-link > span.nav-item__title")).Click();
                    Thread.Sleep(1500);
                    driver.FindElement(By.CssSelector("#mynetwork-nav-item > a.nav-item__link.js-nav-item-link > span.nav-item__title")).Click();
                    Thread.Sleep(1500);
                    driver.FindElement(By.CssSelector("button.button-secondary-small")).Click();
                }
            }
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
