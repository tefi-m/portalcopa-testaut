using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
            pagina.Visit("http://localhost:3000/");
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
        [Retry (3)]
        [Author("Estefania Montivero")]
        public void TestDistribucionDeFondosAccionDetalle()
        {
            test = report.CreateTest("Test Acción Detalle en el módulo Distribución de Fondos en Portal de Coparticipación");
            ExtentTest nodoAccionDetalle = test.CreateNode("Test Acción Detalle en el módulo Distribución de Fondos");

            nodoAccionDetalle.Info("En este test se busca dentro del módulo Distribución de Fondos, seleccionando Año, Mes y Quincena," +
            " los Montos Distribuidos a un Municipio o Comuna, validando la acción de Detalle ubicada en la grilla correspondiente, mostrándole al usuario el detalle del Fondo Distribuido seleccionado.");
            pagina.GoToDistribucionDeFondos();

            try 
            { 
                //Se comprueba que se haya abierto la pantalla de Distribución de Fondos
                if (pagina.IsDisplayed(By.XPath("/html/body/div/div/div/main/div/main/div/div[2]/div/div[1]/h1")))
                {
                    nodoAccionDetalle.Info("Se ingresa al módulo Distribución de Fondos");
                    nodoAccionDetalle.Info("Se selecciona el período a consultar");
                    nodoAccionDetalle.Info("Se selecciona el mes: Abril");
                    pagina.Click(By.XPath("/html/body/div/div/div/main/div/main/div/div[2]/div/div[1]/div[1]/div/div/div/div/form/div[1]/div/div[2]/div/div"));
                    pagina.Click(By.XPath("/html/body/div[2]/div[3]/ul/li[4]"));

                    nodoAccionDetalle.Info("Se selecciona el Año: 2019");
                    pagina.Click(By.XPath("/html/body/div/div/div/main/div/main/div/div[2]/div/div[1]/div[1]/div/div/div/div/form/div[2]/div/div[2]/div/div"));
                    pagina.Click(By.XPath("/html/body/div[2]/div[3]/ul/li[8]"));

                    nodoAccionDetalle.Info("Se selecciona la quincena: 1° Quincena");
                    pagina.Click(By.XPath("/html/body/div/div/div/main/div/main/div/div[2]/div/div[1]/div[1]/div/div/div/div/form/div[3]/div/div[2]/div/div"));
                    pagina.Click(By.XPath("/html/body/div[2]/div[3]/ul/li[1]"));

                    nodoAccionDetalle.Info("Se hace click en Consultar");
                    pagina.Click(By.XPath("/html/body/div/div/div/main/div/main/div/div[2]/div/div[1]/div[1]/div/div/div/div/div/button[1]"));               

                    //var waitAlerta = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    //waitAlerta.Until(drv => IsAlertShown(drv));
                    //IAlert alertaActualizacion = driver.SwitchTo().Alert();
                    //string textoAlerta = alertaActualizacion.Text;

                    //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    Thread.Sleep(TimeSpan.FromSeconds(10));

                    if (! pagina.IsDisplayed(By.XPath("/html/body/div[1]/div/div/main/div/main/div/div[2]/div/div[4]/h3")))
                    {
                        nodoAccionDetalle.Fail("No existen datos para los filtros seleccionados: Mes de Abril, Año 2019 y 1° Quincena, por lo tanto no se puede determinar si el funcionamiento de la acción es correcto o no");
                        Assert.Fail("No hay datos con ese filtro de busqueda");                
                    }
                    else
                    {
                        string btnDetalle = "/html/body/div[1]/div/div/main/div/main/div/div[2]/div/div[5]/div/div[2]/div/div/div/table/tbody/tr[1]/td[4]/div/button/span[1]/div/span[1]";
                        
                        if (pagina.IsDisplayed(By.XPath(btnDetalle)))
                        {
                            nodoAccionDetalle.Info("Búsqueda exitosa: se muestran correctamente los Montos Distribuidos");
                            nodoAccionDetalle.Info("Se hace click en el botón Detalle");
                            pagina.Click(By.XPath(btnDetalle));

                            if (pagina.IsDisplayed(By.XPath("/html/body/div[1]/div/div/main/div/main/div/div[2]/div/div[1]/h2")))
                            {
                                nodoAccionDetalle.Pass("CORRECTO: Se muestra la pantalla de Detalle del Fondo Distribuido seleccionado.");
                            }
                        }
                        else
                        {
                            nodoAccionDetalle.Fail("No se puede visualizar la acción de Detalle correctamente.");
                            Assert.Fail("No se puede visualizar la acción de Detalle correctamente.");
                        }
                    }

                    
                }
                nodoAccionDetalle.Info("Test Completado.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.Fail(e.Message);
            }


            //Thread.Sleep(TimeSpan.FromSeconds(10));

            //Assert.True(true);
        }

        bool IsAlertShown(IWebDriver driver2)
        {
            try
            {
                driver2.SwitchTo().Alert();
            }
            catch (NoAlertPresentException e)
            {
                return false;
            }
            return true;
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