using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using AventStack.ExtentReports;

namespace Portal_Copa_Test_Aut.PageObjectModel
{
    class PaginaPrueba : Base
    {
        public PaginaPrueba() : base()
        {
            
        }

        public void GoToDistribucionDeFondos()
        {
            //Click en opción Distribucion de Fondos
            Click(By.XPath("/html/body/div/div/div/main/div/main/div/div[2]/div/div[1]/div/h3"));

            ChromeDriver driver = GetChromeDriver();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div/div/div/main/div/main/div/div[2]/div/div[1]/h1")));
        }

    }
}
