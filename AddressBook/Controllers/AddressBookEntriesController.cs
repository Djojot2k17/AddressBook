using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AddressBook.Data;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class AddressBookEntriesController : Controller
    {
        private readonly AddressBookContext _context;

        public AddressBookEntriesController(AddressBookContext context)
        {
            _context = context;
        }

        // GET: AddressBookEntries
        public async Task<IActionResult> Index()
        {
            return View(await _context.AddressBooks.ToListAsync());
        }

        // GET: AddressBookEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressBookEntry = await _context.AddressBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressBookEntry == null)
            {
                return NotFound();
            }

            return View(addressBookEntry);
        }

        // GET: AddressBookEntries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddressBookEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Avatar,FileName,Adress1,Address2,City,ZipCode,Phone,DateAdded")] AddressBookEntry addressBookEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addressBookEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addressBookEntry);
        }

        // GET: AddressBookEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressBookEntry = await _context.AddressBooks.FindAsync(id);
            if (addressBookEntry == null)
            {
                return NotFound();
            }
            return View(addressBookEntry);
        }

        // POST: AddressBookEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Avatar,FileName,Adress1,Address2,City,ZipCode,Phone,DateAdded")] AddressBookEntry addressBookEntry)
        {
            if (id != addressBookEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addressBookEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressBookEntryExists(addressBookEntry.Id))
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
            return View(addressBookEntry);
        }

        // GET: AddressBookEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressBookEntry = await _context.AddressBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressBookEntry == null)
            {
                return NotFound();
            }

            return View(addressBookEntry);
        }

        // POST: AddressBookEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addressBookEntry = await _context.AddressBooks.FindAsync(id);
            _context.AddressBooks.Remove(addressBookEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressBookEntryExists(int id)
        {
            return _context.AddressBooks.Any(e => e.Id == id);
        }
    }
}
