using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Reflection;

namespace Portal_Copa_Test_Aut.PageObjectModel
{
    public class Base
    {

        private ChromeDriver driver;

        public Base()
        {
            ChromeOptions option = new ChromeOptions();
            // option.AddArgument("no-sandbox");
            this.driver = new ChromeDriver("./chromedriver", option, TimeSpan.FromSeconds(150));
        }

        public ChromeDriver ChromeDriverConnection()
        {
            return this.driver;
        }

        public ChromeDriver GetChromeDriver()
        {
            return driver ;    
        }

        public IWebElement FindElement(By locator)
        {
            return driver.FindElement(locator);
        }

        public IReadOnlyList<IWebElement> FindElements(By locator)
        {
            return driver.FindElements(locator);
        }

        public String GetText(By locator)
        {
            return driver.FindElement(locator).Text;
        }

        public String GetText(IWebElement element)
        {
            return element.Text;
        }

        public void SendKeys(String inputText, By locator)
        {
            driver.FindElement(locator).SendKeys(inputText);
        }

        public void Click(By locator)
        {
            driver.FindElement(locator).Click();
        }

        public void Click(IWebElement element)
        {
            element.Click();
        }

        public bool IsDisplayed(By locator)
        {
            try
            {
                return driver.FindElement(locator).Displayed;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void Visit(String url)
        {
            driver.Url = url;
        }

        public void Maximize()
        {
            driver.Manage().Window.Maximize();
        }

        public void Minimize()
        {
            driver.Manage().Window.Maximize();
        }

        public void FullScreen()
        {
            driver.Manage().Window.FullScreen();
        }

        public void ImplicitWait(TimeSpan time)
        {
            driver.Manage().Timeouts().ImplicitWait = time;
        }

        public void Back()
        {
            driver.Navigate().Back();
        }

        public void PageLoadTime(TimeSpan time)
        {
            driver.Manage().Timeouts().PageLoad = time;

        }

        public Boolean Enabled(By by)
        {
            return driver.FindElement(by).Enabled;
        }

        public Boolean EsEditable(By by)
        {
            string respuesta;
            try
            {
                IWebElement elemento = driver.FindElement(by);
                respuesta = elemento.GetAttribute("disabled");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return respuesta == "" || respuesta == null;
        }

        public Boolean EsEditable(IWebElement elemento)
        {
            string respuesta;
            try
            {
                respuesta = elemento.GetAttribute("disabled");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return respuesta == "" || respuesta == null;
        }

        public void Clear(By by)
        {
            driver.FindElement(by).Clear();
        }

        public void EsperarElementoExista(By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            _ = wait.Until(SeleniumExtras.WaitHelpers.
                ExpectedConditions.ElementExists(locator));
        }

        public string GetPageSource()
        {
            return driver.PageSource;
        }

        public string GetAtributte(string atributte, By by)
        {
            return driver.FindElement(by).GetAttribute(atributte);
        }

        public string GetUrl()
        {
            return driver.Url;
        }
    }
}
