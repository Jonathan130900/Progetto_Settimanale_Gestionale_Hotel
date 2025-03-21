using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_Settimanale_Gestionale_Hotel.Data;
using Progetto_Settimanale_Gestionale_Hotel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Linq;

namespace Progetto_Settimanale_Gestionale_Hotel.Controllers
{
    public class PrenotazioneController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PrenotazioneController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var prenotazioni = await _context.Prenotazioni
                .Include(p => p.Cliente)
                .Include(p => p.Camera)
                .ToListAsync();
            return View(prenotazioni);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context.Prenotazioni
                .Include(p => p.Cliente)
                .Include(p => p.Camera)
                .FirstOrDefaultAsync(m => m.PrenotazioneId == id);

            if (prenotazione == null)
            {
                return NotFound();
            }

            return View(prenotazione);
        }

        public IActionResult Create()
        {
            ViewBag.Clienti = new SelectList(_context.Clienti, "ClienteId", "Nome");
            ViewBag.Camere = new SelectList(_context.Camere, "CameraId", "Numero");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,CameraId,DataInizio,DataFine,Stato")] Prenotazione prenotazione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prenotazione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prenotazione);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            if (prenotazione == null)
            {
                return NotFound();
            }
            ViewBag.Clienti = new SelectList(_context.Clienti, "ClienteId", "Nome", prenotazione.ClienteId);
            ViewBag.Camere = new SelectList(_context.Camere, "CameraId", "Numero", prenotazione.CameraId);
            return View(prenotazione);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrenotazioneId,ClienteId,CameraId,DataInizio,DataFine,Stato")] Prenotazione prenotazione)
        {
            if (id != prenotazione.PrenotazioneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prenotazione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrenotazioneExists(prenotazione.PrenotazioneId))
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
            return View(prenotazione);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context.Prenotazioni
                .Include(p => p.Cliente)
                .Include(p => p.Camera)
                .FirstOrDefaultAsync(m => m.PrenotazioneId == id);

            if (prenotazione == null)
            {
                return NotFound();
            }

            return View(prenotazione);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            _context.Prenotazioni.Remove(prenotazione);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrenotazioneExists(int id)
        {
            return _context.Prenotazioni.Any(e => e.PrenotazioneId == id);
        }
    }
}
