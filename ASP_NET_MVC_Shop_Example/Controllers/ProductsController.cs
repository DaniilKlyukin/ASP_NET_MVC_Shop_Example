using ASP_NET_MVC_Shop_Example.Data;
using ASP_NET_MVC_Shop_Example.Extensions;
using ASP_NET_MVC_Shop_Example.Filters;
using ASP_NET_MVC_Shop_Example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_MVC_Shop_Example.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
            _context.RegisterSQLiteToLower();
        }

        [TypeFilter(typeof(SortStateAttribute))]
        [TypeFilter(typeof(FilterStateAttribute))]
        public async Task<IActionResult> Index(string? productName, SortState sortState, FilterState filterState)
        {
            if (!ModelState.IsValid)
            {
                var allViewModel = new IndexViewModel
                {
                    Products = await _context.Products.ToListAsync(),
                    SortState = new SortState(sortState.SortField, sortState.SortDirection),
                    FilterState = filterState
                };

                return View(allViewModel);
            }

            IQueryable<Product> productsExpression = _context.Products;

            if (!string.IsNullOrEmpty(productName))
                productsExpression = productsExpression.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{productName.ToLower()}%"));

            if (filterState.MinPrice.HasValue)
                productsExpression = productsExpression.Where(p => p.Price >= filterState.MinPrice);

            if (filterState.MaxPrice.HasValue)
                productsExpression = productsExpression.Where(p => p.Price <= filterState.MaxPrice);

            var products = await productsExpression
                                            .OrderBy(sortState.SortField, sortState.SortDirection)
                                            .AsNoTracking()
                                            .ToListAsync();

            var viewModel = new IndexViewModel
            {
                Products = products,
                SearchProduct = productName,
                SortState = new SortState(sortState.SortField, sortState.SortDirection),
                FilterState = filterState
            };

            ViewData["TotalMinPrice"] = products.Count > 0 ? products.Min(p => p.Price) : 0;
            ViewData["TotalMaxPrice"] = products.Count > 0 ? products.Max(p => p.Price) : 0;

            return View(viewModel);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Products/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Products/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}