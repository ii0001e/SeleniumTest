using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTest
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;
        private readonly string testUrl = "https://ivanivanenko22.thkit.ee/form.html";

        [SetUp]
        public void SetUp()
        {
            InitializeWebDriver();
            NavigateToTestPage();
            CloseCookiePopup();
        }

        [Test]
        public void TestPage()
        {
            FillForms();
            ClickReadButton();
        }

        [TearDown]
        public void TearDown()
        {
            QuitWebDriver();
        }

        private void InitializeWebDriver()
        {
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";

            driver = new FirefoxDriver("D://Users//ivani//source//repos//SeleniumTest//SeleniumTest//drivers//geckodriver.exe", firefoxOptions);
            driver.Manage().Window.Maximize();
        }

        private void NavigateToTestPage()
        {
            driver.Navigate().GoToUrl(testUrl);
        }

        private void CloseCookiePopup()
        {
            try
            {
                IWebElement cookieButton = driver.FindElement(By.CssSelector(".agree-button.eu-cookie-compliance-secondary-button"));
                cookieButton.Click();
            }
            catch (NoSuchElementException) { }
        }

        private void FillForms()
        {
            foreach (var input in driver.FindElements(By.CssSelector("input[type='text']")))
            {
                try
                {
                    input.Click();
                    input.SendKeys("JustRandomName");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при заполнении текстового поля: {ex.Message}");
                }
            }

            foreach (var radio in driver.FindElements(By.CssSelector("input[type='radio']")))
            {
                try
                {
                    if (!radio.Selected)
                    {
                        radio.Click();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при выборе радиокнопки: {ex.Message}");
                }
            }

            foreach (var checkbox in driver.FindElements(By.CssSelector("input[type='checkbox']")))
            {
                try
                {
                    if (!checkbox.Selected)
                    {
                        checkbox.Click();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при выборе чекбокса: {ex.Message}");
                }
            }

            foreach (var select in driver.FindElements(By.CssSelector("select")))
            {
                try
                {
                    new SelectElement(select).SelectByIndex(2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при выборе из выпадающего списка: {ex.Message}");
                }
            }

            foreach (var textarea in driver.FindElements(By.CssSelector("textarea")))
            {
                try
                {
                    textarea.Click();
                    textarea.SendKeys("JustRandomText");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при заполнении текстового поля: {ex.Message}");
                }
            }
        }


        private void ClickReadButton()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//input[@value='Loeandmed']"))); //special error

            try
            {
                IWebElement readButton = driver.FindElement(By.XPath("//input[@value='Loeandmed']"));//special error
                readButton.Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Кнопка неактивна");
            }
        }

        private void QuitWebDriver()
        {
            driver.Quit();
        }
    }
}
