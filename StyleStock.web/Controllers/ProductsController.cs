using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StyleStock.domain;
using StyleStock.domain.DTOS;
using StyleStock.domain.Entities;

namespace StyleStock.web.Controllers
{
	public class ProductsController : Controller
	{
		private readonly StyleStockContext _context;

		public ProductsController(StyleStockContext context)
        {
			_context = context;
		}

        public IActionResult Index()
		{
			var products = _context.Products.Select(p => new ProductDTO
			{
				ProductId = p.ProductId,
				Name = p.Name,
				Description = p.Description,
				Price = p.Price,
				Size = p.Size,
				Color = p.Color,
				CategoryId = p.Category.CategoryId,
				CategoryName = p.Category.Name,
				StockQuantity = p.StockQuantity,
				EntryDate = p.EntryDate,
			}).ToList();
			return View(products);
		}

		[HttpGet]
		public IActionResult Create()
		{
			var categories = _context.Categories.Select(c => new { c.CategoryId, c.Name }).ToList();
			ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateProductDTO productDTO)
		{
			if (!ModelState.IsValid)
			{
				var categories = _context.Categories.Select(c => new { c.CategoryId, c.Name }).ToList();
				ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");

				return View(productDTO); 
			}

			var newProduct = new Product
			{
				Name = productDTO.Name,
				Description = productDTO.Description,
				Price = productDTO.Price,
				Size = productDTO.Size,
				Color = productDTO.Color,
				CategoryId = productDTO.CategoryId,
				StockQuantity = productDTO.StockQuantity,
				EntryDate = productDTO.EntryDate,
			};

			_context.Products.Add(newProduct);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

	}
}
