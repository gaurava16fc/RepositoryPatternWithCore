using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GL.Repository;
using GL.Entities;
using GL.Data;
using Microsoft.Extensions.Configuration;

namespace RepositoryPattern.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserRepository _repo;
        //private readonly IConfiguration _configuration;
        //DbContextOptionsBuilder<ForumContext> _optionsBuilder;

        public UsersController(IConfiguration configuration)
        {
            IConfiguration _configuration = configuration;
            DbContextOptionsBuilder<ForumContext> _optionsBuilder = new DbContextOptionsBuilder<ForumContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ForumContext"));
            ForumContext forumDBContext = new ForumContext(_optionsBuilder.Options);
            _repo = new UserRepository(forumDBContext);
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _repo.Read().Where(u => u.IsActive).ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await FindUserAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,IsActive")] Users users)
        {
            if (ModelState.IsValid)
            {
                await _repo.Create(users);
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await FindUserAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,IsActive")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.Update(users);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
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
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await FindUserAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await FindUserAsync(id);
            await _repo.Delete(users);
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _repo.Read().Any(e => e.Id == id);
        }

        private async Task<Users> FindUserAsync(int? id)
        {
            return await _repo.Read().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
