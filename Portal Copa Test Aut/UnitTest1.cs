using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using Portal_Copa_Test_Aut.PageObjectModel;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Portal_Copa_Test_Aut
{
    [TestFixture]
    public class Tests
    {
        private ChromeDriver driver;
        private PaginaPrueba pagina;
        private static ExtentTest test;
        private static ExtentReports report;

        private string pathReporte = "";

        [SetUp]
        public void Setup()
        {
            pagina = new PaginaPrueba();
            driver = pagina.ChromeDriverConnection();
            pagina.PageLoadTime(TimeSpan.FromSeconds(150));
            pagina.Maximize();
            pagina.Visit("http://www.google.com.ar");
        }

        [OneTimeSetUp]
        public void ReportStart()
        {
            pathReporte = GetDirectorioReportes();

            pathReporte = pathReporte + "\\TestPrueba.html";
            report = new ExtentReports();
            report.AddSystemInfo("Autores:", "");
            report.AddSystemInfo("Sistema a testear:", "P");
            var htmlReporter = new ExtentHtmlReporter(pathReporte);

            report.AttachReporter(htmlReporter);
        }

        private string GetDirectorioReportes()
        {
            string path = Assembly.GetCallingAssembly().Location;
            path = path.Substring(0, path.LastIndexOf("bin"));
            path = path + "Reports\\Prueba";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        [Test]
        public void TestPrueba1()
        {
            test = report.CreateTest("Prueba");
            ExtentTest nodo = test.CreateNode("Prueba");

            Thread.Sleep(TimeSpan.FromSeconds(10));

            Assert.True(true);
        }

        [OneTimeTearDown]
        public void ReportClose()
        {
            report.Flush();
        }

        [TearDown]
        public void Clean()
        {
            if (driver != null)
            {
                driver.Dispose();
            }
        }
    }
}