using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace StyleStock.Tests
{
    [TestClass]
    public class CategoryTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string baseUrl = "http://localhost:5161";

        [TestInitialize]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--ignore-certificate-errors");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [TestMethod]
        public void CrearCategoria_CaminoFeliz()
        {
            driver.Navigate().GoToUrl($"{baseUrl}/Category/Create");

            wait.Until(d => d.FindElement(By.Id("Name"))).SendKeys("Zapatos");
            driver.FindElement(By.Id("Description")).SendKeys("Calzado en general");

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.IsTrue(driver.Url.Contains("/Category"));
        }

        [TestMethod]
        public void CrearCategoria_ValidacionCamposVacios()
        {
            var longWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // 1. Ir a la página de creación
            driver.Navigate().GoToUrl($"{baseUrl}/Category/Create");

            // 2. Enviar formulario vacío
            longWait.Until(d => d.FindElement(By.CssSelector("button[type='submit']"))).Click();

            // 3. Verificar que permanece en la misma página
            Assert.IsTrue(longWait.Until(d => d.Url.EndsWith("/Category/Create")),
                        "Se redireccionó incorrectamente");

            // 4. Verificar estado de validación via JavaScript
            var nameField = driver.FindElement(By.Id("Name"));
            bool isNameValid = (bool)((IJavaScriptExecutor)driver)
                .ExecuteScript("return arguments[0].checkValidity();", nameField);

            Assert.IsFalse(isNameValid, "El campo Name no mostró validación");
        }

        [TestMethod]
        public void EditarCategoria_Existente()
        {
            int idCategoria = 1; // Cambia según un ID válido en tu base de datos

            driver.Navigate().GoToUrl($"{baseUrl}/Category/Update/{idCategoria}");

            var nombre = wait.Until(d => d.FindElement(By.Id("Name")));
            nombre.Clear();
            nombre.SendKeys("Ropa Deportiva");

            var descripcion = driver.FindElement(By.Id("Description"));
            descripcion.Clear();
            descripcion.SendKeys("Ropa para ejercicios y deportes");

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.IsTrue(driver.Url.Contains("/Category"));
        }

        [TestMethod]
        public void EliminarCategoria()
        {
            driver.Navigate().GoToUrl($"{baseUrl}/Category");

            // Buscar botón de eliminar (puedes usar el texto del botón o clase específica)
            var eliminar = wait.Until(d => d.FindElement(By.LinkText("Eliminar")));
            eliminar.Click();

            // Confirmar redirección al listado
            Assert.IsTrue(driver.Url.Contains("/Category"));
        }

        [TestMethod]
        public void VerListadoCategorias()
        {
            driver.Navigate().GoToUrl($"{baseUrl}/Category");

            var listado = wait.Until(d => d.FindElements(By.TagName("tr")).Count > 1); // hay al menos una categoría listada

            Assert.IsTrue(listado);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (driver != null)
                driver.Quit();
        }
    }
}
