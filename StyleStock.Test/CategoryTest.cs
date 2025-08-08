using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace StyleStock.Tests
{
    [TestClass]
    public class CategoryTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string baseUrl = "http://localhost:5161";

        private static ExtentReports extent;
        private ExtentTest test;

        [ClassInitialize]
        public static void ReportInit(TestContext context)
        {
            var reporter = new ExtentSparkReporter("Reporte_Categorias.html");
            extent = new ExtentReports();
            extent.AttachReporter(reporter);
        }

        [TestInitialize]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--ignore-certificate-errors");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        [TestMethod]
        public void CrearCategoria_CaminoFeliz()
        {
            test = extent.CreateTest("CrearCategoria_CaminoFeliz");

            try
            {
                driver.Navigate().GoToUrl($"{baseUrl}/Category/Create");

                wait.Until(d => d.FindElement(By.Id("Name"))).SendKeys("Jeans");
                driver.FindElement(By.Id("Description")).SendKeys("Ropa en general");

                driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                Assert.IsTrue(driver.Url.Contains("/Category"));
                test.Pass("Categoría creada correctamente.");
            }
            catch (Exception ex)
            {
                CapturarError(test, ex);
                throw;
            }
        }

        [TestMethod]
        public void CrearCategoria_ValidacionCamposVacios()
        {
            test = extent.CreateTest("CrearCategoria_ValidacionCamposVacios");

            try
            {
                driver.Navigate().GoToUrl($"{baseUrl}/Category/Create");

                wait.Until(d => d.FindElement(By.CssSelector("button[type='submit']"))).Click();

                Assert.IsTrue(driver.Url.EndsWith("/Category/Create"));

                var nameField = driver.FindElement(By.Id("Name"));
                bool isNameValid = (bool)((IJavaScriptExecutor)driver)
                    .ExecuteScript("return arguments[0].checkValidity();", nameField);

                Assert.IsFalse(isNameValid);
                test.Pass("Validación de campos vacíos funciona correctamente.");
            }
            catch (Exception ex)
            {
                CapturarError(test, ex);
                throw;
            }
        }

        [TestMethod]
        public void EditarCategoria_Existente()
        {
            test = extent.CreateTest("EditarCategoria_Existente");

            try
            {
                int idCategoria = 1; // Asegúrate de que este ID exista
                driver.Navigate().GoToUrl($"{baseUrl}/Category/Update/{idCategoria}");

                var nombre = wait.Until(d => d.FindElement(By.Id("Name")));
                nombre.Clear();
                nombre.SendKeys("Ropa Deportiva");

                var descripcion = driver.FindElement(By.Id("Description"));
                descripcion.Clear();
                descripcion.SendKeys("Ropa para ejercicios y deportes");

                driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                Assert.IsTrue(driver.Url.Contains("/Category"));
                test.Pass("Categoría editada correctamente.");
            }
            catch (Exception ex)
            {
                CapturarError(test, ex);
                throw;
            }
        }

        [TestMethod]
        public void EliminarCategoria()
        {
            test = extent.CreateTest("EliminarCategoria");

            try
            {
                driver.Navigate().GoToUrl($"{baseUrl}/Category");

                var eliminar = wait.Until(d => d.FindElement(By.LinkText("Eliminar")));
                eliminar.Click();

                Assert.IsTrue(driver.Url.Contains("/Category"));
                test.Pass("Categoría eliminada correctamente.");
            }
            catch (Exception ex)
            {
                CapturarError(test, ex);
                throw;
            }
        }

        [TestMethod]
        public void VerListadoCategorias()
        {
            test = extent.CreateTest("VerListadoCategorias");

            try
            {
                driver.Navigate().GoToUrl($"{baseUrl}/Category");

                var listado = wait.Until(d => d.FindElements(By.TagName("tr")).Count > 1);
                Assert.IsTrue(listado);
                test.Pass("Listado de categorías visible correctamente.");
            }
            catch (Exception ex)
            {
                CapturarError(test, ex);
                throw;
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }

        [ClassCleanup]
        public static void EndReport()
        {
            extent.Flush();
        }

        // Método común para capturar errores y guardar pantallazo
        private void CapturarError(ExtentTest test, Exception ex)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string path = $"screenshot_{timestamp}.png";

                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                // Opción 1: Usando File.WriteAllBytes (más compatible)
                File.WriteAllBytes(path, screenshot.AsByteArray);

                // Opción 2: Si prefieres usar SaveAsFile con formato explícito
                // screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);

                test.Fail("Error: " + ex.Message)
                    .AddScreenCaptureFromPath(path);
            }
            catch (Exception screenshotEx)
            {
                // Si falla la captura de pantalla, al menos registrar el error original
                test.Fail("Error: " + ex.Message + " (No se pudo capturar screenshot: " + screenshotEx.Message + ")");
            }
        }
    }
}