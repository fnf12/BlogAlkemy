using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _01_BlogAlkemy.Models;
using Microsoft.AspNetCore.Hosting;
using _01_BlogAlkemy.Models.ViewModel;
using Microsoft.AspNetCore.Http;

namespace _01_BlogAlkemy.Controllers
{
    public class PostsController : Controller
    {
        private readonly DbBlogContext _context;
        private readonly IWebHostEnvironment _enviroment;

        public PostsController(DbBlogContext context, IWebHostEnvironment env)
        {
            _context = context;
            _enviroment = env;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var dbBlogContext = _context.Post.Include(p => p.Category);
            dbBlogContext.OrderByDescending(x => x.CreationDate);

            var posts = dbBlogContext.Select(x=> new PostGetAllModel
            {
                IdPost = x.IdPost,
                Title = x.Title,
                Image = x.Image,
                Category= x.Category,
                CreationDate = x.CreationDate
            });

            return View(await posts.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.IdPost == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPost,IdCategory,Title,Content,Image,CreationDate")] PostModel post)
        {
            if (ModelState.IsValid)
            {
                var postReal = new Post
                {
                    IdPost = post.IdPost,
                    IdCategory = post.IdCategory,
                    Title = post.Title,
                    Content = post.Content,
                    Image = post.Image.FileName,
                    CreationDate = post.CreationDate
                };

                _context.Add(postReal);
                await _context.SaveChangesAsync();

                var RutaImagen = System.IO.Path.Combine(_enviroment.ContentRootPath, "uploads", post.Image.FileName);
                var file = new System.IO.FileStream(RutaImagen, System.IO.FileMode.Create);
                file.Dispose();

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "Name", post.IdCategory);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);

            var postedit = new PostEditModel
            {
                IdPost = post.IdPost,
                IdCategory = post.IdCategory,
                Title = post.Title,
                Content = post.Content,
                rutaimagen= post.Image,
                CreationDate = post.CreationDate
            };

            if (post == null)
            {
                return NotFound();
            }
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "Name", post.IdCategory);
            return View(postedit);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPost,IdCategory,Title,Content,Image,CreationDate")] PostEditModel post)
        {
            if (id != post.IdPost)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var postanterior = await _context.Post.FindAsync(id);
                    var rutanterior = System.IO.Path.Combine(_enviroment.ContentRootPath, "uploads", postanterior.Image);

                    postanterior.IdCategory = post.IdCategory;
                    postanterior.Title = post.Title;
                    postanterior.Content = post.Content;

                    if (post.Image != null)
                    {
                        postanterior.Image = post.Image.FileName;
                        var RutaImagen = System.IO.Path.Combine(_enviroment.ContentRootPath, "uploads", post.Image.FileName);
                        var file = new System.IO.FileStream(RutaImagen, System.IO.FileMode.Create);
                        file.Dispose();
                        System.IO.File.Delete(rutanterior);
                    }

                    _context.Update(postanterior);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.IdPost))
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
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "Name", post.IdCategory);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.IdPost == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);

            var RutaImagen = System.IO.Path.Combine(_enviroment.ContentRootPath, "uploads", post.Image);

            await _context.SaveChangesAsync();

            System.IO.File.Delete(RutaImagen);

            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.IdPost == id);
        }
    }
}
