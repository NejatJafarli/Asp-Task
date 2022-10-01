using App.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace App.MvcWebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(int page = 1, int category = 0, string Orderby = "",string Order="asc")
        {
            int pageSize = 10;
            var products = _productService.GetByCategory(category);

            if (Orderby == "ProductName")
            {
                if (Order == "asc")
                    products = products.OrderBy(p => p.ProductName).ToList();
                else
                    products = products.OrderByDescending(p => p.ProductName).ToList();
            }
            else if (Orderby == "UnitPrice")
            {
                if (Order == "asc")
                    products = products.OrderBy(p => p.UnitPrice).ToList();
                else
                products = products.OrderByDescending(p => p.UnitPrice).ToList();
            }

            var model = new ProductListViewModel
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageCount = (int)Math.Ceiling(products.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentCategory = category,
                CurrentPage = page,
                OrderBy=Orderby,
                Order=Order
            };
            return View(model);
        }
    }
}
