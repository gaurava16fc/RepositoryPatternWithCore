using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GL.Data;
using GL.Entities;
using Microsoft.Extensions.Configuration;
using GL.Repository;

namespace RepositoryPattern.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository _repo;
        public AccountController(IConfiguration configuration)
        {
            DbContextOptionsBuilder<MasterContext> _optionsBuilder = new DbContextOptionsBuilder<MasterContext>();
            _optionsBuilder.UseSqlServer(configuration.GetConnectionString("MasterContext"));
            MasterContext dbContext = new MasterContext(_optionsBuilder.Options);
            _repo = new AccountRepository(dbContext);
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _repo.Read().ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountName")] Account account)
        {
            if (ModelState.IsValid)
            {
                await _repo.Create(account);
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountName")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.Update(account);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
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
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await FindAsync(id);
            await _repo.Delete(account);
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _repo.Read().Any(e => e.Id == id);
        }


        private async Task<Account> FindAsync(int? id)
        {
            return await _repo.Read().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
