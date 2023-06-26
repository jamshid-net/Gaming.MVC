using Gaming.Application.Common.Exceptions;
using Gaming.Domain.Entities;
using Gaming.Infrastructure.DataAccsess;
using Gaming.MVC.Attributes;
using Gaming.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;


namespace Gaming.MVC.Controllers
{

    [Authorize(Roles ="admin")]
    public class ProductsController : BaseController
    {


        [EnableRateLimiting("TokenBucketLimiter")]
        [AddLazyCache]
        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        [EnableRateLimiting("ConcurrencyLimiter")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [RemoveLazyCache]
        [EnableRateLimiting("SlidingLimiter")]
        public IActionResult Create()
        {
            ViewData["CategoryName"] = new SelectList(_context.Categories, "CategoryName", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [EnableRateLimiting("SlidingLimiter")]
        public async Task<IActionResult> Create([FromForm] ProductView product)
        {
            if (ModelState.IsValid)
            {
                string rootpath = _webHostEnvironment.WebRootPath;

                string filename = Guid.NewGuid() + product.ProductImage.FileName;

                string combinedPath = Path.Combine(rootpath + @"\images", filename);

                var foundCategory = await _context.Categories.FirstOrDefaultAsync(x=> x.CategoryName == product.CategoryName);
                if (foundCategory is null)
                    throw new NotFoundException(nameof(Category), product.CategoryName);

                using (var stream = new FileStream(combinedPath, FileMode.Create))
                {
                    await product.ProductImage.CopyToAsync(stream);

                }
                Product entityProduct = new Product()
                {
                    ProductId = Guid.NewGuid(),
                    ProductImage = "/images/" + filename,
                    Price = product.Price,
                    ProductName = product.ProductName,
                    CategoryId = foundCategory.Id,
                    Description = product.Description,
                    Discount = product.Discount
                   

                    
                };


                _context.Products.Add(entityProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           ViewData["CategoryName"] = new SelectList(_context.Categories, "CategoryName", "CategoryName", product.CategoryName);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            
            if (product == null)
            {
                return NotFound();
            }

            var productView = new ProductView()
            {
                ProductId = product.ProductId,
                Price = product.Price,
                ProductName = product.ProductName,
                Discount = product.Discount,
                Description = product.Description,
                CategoryName =product.Category.CategoryName

            };
            ViewData["CategoryName"] = new SelectList(_context.Categories, "CategoryName", "CategoryName",productView.CategoryName);
            return View(productView);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [EnableRateLimiting("SlidingLimiter")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] ProductView product,CancellationToken cancellationToken)
        {
           

            if (ModelState.IsValid)
            {
                try
                {
                    string rootpath = _webHostEnvironment.WebRootPath;
                    string filename = "";

                    if (product.ProductImage is not null)
                    {
                        filename = Guid.NewGuid() + product.ProductImage.FileName;

                        string combinedPath = Path.Combine(rootpath + @"\images", filename);
                        using (var stream = new FileStream(combinedPath, FileMode.Create))
                        {
                            await product.ProductImage.CopyToAsync(stream);

                        }
                    }
                        

                    var foundCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == product.CategoryName);
                    if (foundCategory is null)
                        throw new NotFoundException(nameof(Category), product.CategoryName);

                   
                    var foundProduct= await _context.Products.FindAsync(new object[] { product.ProductId }, cancellationToken);
                    if (foundProduct is null)
                        throw new NotFoundException(nameof(Product), product.ProductName);

                    if(product.ProductImage is not null)
                    {
                     foundProduct.ProductImage = "/images/" + filename;

                    }
                    foundProduct.CategoryId = foundCategory.Id;
                    foundProduct.ProductName = product.ProductName;
                    foundProduct.Price= product.Price;
                    foundProduct.Description = product.Description;
                    foundProduct.Discount = product.Discount;


                     await _context.SaveChangesAsync(cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryName"] = new SelectList(_context.Categories, "CategoryName", "CategoryName", product.CategoryName);
            return View(product);
        }

        // GET: Products/Delete/5
        [EnableRateLimiting("SlidingLimiter")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [EnableRateLimiting("SlidingLimiter")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [EnableRateLimiting("SlidingLimiter")]
        private bool ProductExists(Guid id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
