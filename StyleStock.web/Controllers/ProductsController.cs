using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StyleStock.domain;
using StyleStock.domain.DTOS;
using StyleStock.domain.Entities;

namespace StyleStock.web.Controllers
{
    public class ProductsController : Controller
    {

        private readonly HttpClient _client;

        public ProductsController(HttpClient client)
        {

            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _client.GetFromJsonAsync<IEnumerable<ProductDTO>>("http://localhost:5068/api/Product/GetProducts");
            if (products == null)
            {
                ViewBag.ErrorMessage = "No se pudo encontrar los productos";
                return View();
            }
            return View(products);
        }

		[HttpGet]
		public async Task<IActionResult> Create()
		{
            try
            {
                var response = await _client.GetAsync("http://localhost:5068/api/Products/CreateProducts");

                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();

                    
                    ViewBag.Categories = new SelectList(products, "Id", "Name");
                }
                else
                {
                    ViewBag.ErrorMessage = "No se pudieron obtener los productos.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al obtener los productos: " + ex.Message;
            }

            return View();
		}



		[HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO newProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(newProduct);
            }
            try
            {

                var response = await _client.PostAsJsonAsync("http://localhost:5068/api/Product/CreateProducts", newProduct);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Hubo un error al crear el producto.";
                    return View(newProduct);
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar crear la categoría.");
                return View();
            }

        }
    }
}
