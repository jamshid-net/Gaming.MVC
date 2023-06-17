using Gaming.Application.Common.Exceptions;
using Gaming.Domain.Entities;
using Gaming.Infrastructure.DataAccsess;
using Gaming.MVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Gaming.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoriesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CategoryView category)
        {
            if (ModelState.IsValid)
            {
                string rootpath = _webHostEnvironment.WebRootPath;

                string filename = Guid.NewGuid() + category.CategoryImage.FileName;

                string combinedPath = Path.Combine(rootpath + @"\images\categoryImages", filename);

                

                using (var stream = new FileStream(combinedPath, FileMode.Create))
                {
                    await category.CategoryImage.CopyToAsync(stream);

                }
                Category entityCategory = new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryImage = "/images/categoryImages/" + filename,
                    CategoryName = category.CategoryName

                };

                
                _context.Categories.Add(entityCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
          
            if (category == null)
            {
                return NotFound();
            }
            CategoryView categoryView = new CategoryView
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };

            return View(categoryView);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] CategoryView category,CancellationToken cancellation)
        {
           

            if (ModelState.IsValid)
            {
                string rootpath = _webHostEnvironment.WebRootPath;
                string filename = "";

                try
                {
                    if(category.CategoryImage is not null)
                    {
                        filename = Guid.NewGuid() + category.CategoryImage.FileName;
                        string combinedPath = Path.Combine(rootpath + @"\images\categoryImages", filename);
                        using (var stream = new FileStream(combinedPath, FileMode.Create))
                        {
                            await category.CategoryImage.CopyToAsync(stream, cancellation);

                        }
                    }
                    var foundCategory = await _context.Categories.FindAsync(new object[] {category.Id}, cancellation);
                    if (foundCategory is null)
                        throw new NotFoundException(nameof(Category), category.CategoryName);
                    
                    if (category.CategoryImage is not null)
                    {
                        foundCategory.CategoryImage = "/images/categoryImages/" + filename;

                    }
                    foundCategory.CategoryName = category.CategoryName;
                   
                    await _context.SaveChangesAsync(cancellation);

                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(Guid id)
        {
          return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
