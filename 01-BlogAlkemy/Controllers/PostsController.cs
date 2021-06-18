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
using _01_BlogAlkemy.Helpers;

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
        public async Task<IActionResult> Index(string filtrar)
        {
            var dbBlogContext = _context.Post.Include(p => p.Category);

            dbBlogContext.OrderByDescending(x => x.CreationDate).ToList();

            var posts = dbBlogContext.Select(x=> new PostGetAllModel
            {
                IdPost = x.IdPost,
                Title = x.Title,
                Image = x.Image,
                Category= x.Category,
                CreationDate = x.CreationDate
            });

            if (filtrar != null)
            {
                posts = posts.Where(x => x.Title.Contains(filtrar));
            }

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
        public async Task<IActionResult> Create([Bind("IdPost,IdCategory,Title,Content,Image,CreationDate")] PostModel postmodel)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    IdPost = postmodel.IdPost,
                    IdCategory = postmodel.IdCategory,
                    Title = postmodel.Title,
                    Content = postmodel.Content,
                    Image = postmodel.Image.FileName,
                    CreationDate = postmodel.CreationDate
                };

                _context.Add(post);
                await _context.SaveChangesAsync();

                FileStr.create(_enviroment.ContentRootPath, "wwwwroot/uploads", postmodel.Image.FileName,postmodel.Image);

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "Name", postmodel.IdCategory);
            return View(postmodel);
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
        public async Task<IActionResult> Edit(int id, [Bind("IdPost,IdCategory,Title,Content,Image,CreationDate")] PostEditModel editpost)
        {
            if (id != editpost.IdPost)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var post= await _context.Post.FindAsync(id);

                    post.IdCategory = editpost.IdCategory;
                    post.Title = editpost.Title;
                    post.Content = editpost.Content;

                    if (editpost.Image != null)
                    {
                        FileStr.create(_enviroment.ContentRootPath, "wwwwroot/uploads", editpost.Image.FileName, editpost.Image);
                        FileStr.delete(_enviroment.ContentRootPath, "wwwwroot/uploads", post.Image);
                        post.Image = editpost.Image.FileName;
                    }

                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(editpost.IdPost))
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
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "Name", editpost.IdCategory);
            return View(editpost);
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

            await _context.SaveChangesAsync();

            FileStr.delete(_enviroment.ContentRootPath, "wwwwroot/uploads", post.Image);

            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.IdPost == id);
        }
    }
}
