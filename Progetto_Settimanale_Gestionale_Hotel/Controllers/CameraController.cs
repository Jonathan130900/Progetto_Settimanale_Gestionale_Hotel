using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_Settimanale_Gestionale_Hotel.Data;
using Progetto_Settimanale_Gestionale_Hotel.Models;

namespace Progetto_Settimanale_Gestionale_Hotel.Controllers
{
    public class CameraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CameraController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Camere.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numero,Tipo,Prezzo")] Camera camera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(camera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(camera);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _context.Camere.FindAsync(id);
            if (camera == null)
            {
                return NotFound();
            }
            return View(camera);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CameraId,Numero,Tipo,Prezzo")] Camera camera)
        {
            if (id != camera.CameraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(camera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Camere.Any(e => e.CameraId == camera.CameraId))
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
            return View(camera);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camera = await _context.Camere
                .FirstOrDefaultAsync(m => m.CameraId == id);
            if (camera == null)
            {
                return NotFound();
            }

            return View(camera);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var camera = await _context.Camere.FindAsync(id);
            _context.Camere.Remove(camera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
