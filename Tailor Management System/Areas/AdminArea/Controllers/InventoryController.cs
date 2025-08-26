using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Tailor_Management_System.Data;
using Tailor_Management_System.Models;

namespace Tailor_Management_System.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var inventories = _context.Inventory.ToList();
            return View(inventories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]  
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Inventory.Add(inventory);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventory);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var inventory = _context.Inventory.FirstOrDefault(u=>u.InventoryId==id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Inventory.Update(inventory);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventory);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Inventory.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int InventoryId)
        {
            var item = await _context.Inventory.FindAsync(InventoryId);
            if (item != null)
            {
                _context.Inventory.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
