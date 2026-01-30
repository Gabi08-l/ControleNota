using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleNota.Data;
using ControleNota.Models;
using Microsoft.AspNetCore.Authorization;

namespace ControleNota.Controllers
{
    [Authorize]
    public class NotasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Notas.Include(n => n.Aluno).Include(n => n.Materia);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Aluno)
                .Include(n => n.Materia)
                .FirstOrDefaultAsync(m => m.NotaId == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "AlunoId", "AlunoId");
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "MateriaId");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotaId,Valor,DataLancamento,AlunoId,MateriaId")] Nota nota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "AlunoId", "AlunoId", nota.AlunoId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "MateriaId", nota.MateriaId);
            return View(nota);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "AlunoId", "AlunoId", nota.AlunoId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "MateriaId", nota.MateriaId);
            return View(nota);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotaId,Valor,DataLancamento,AlunoId,MateriaId")] Nota nota)
        {
            if (id != nota.NotaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaExists(nota.NotaId))
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
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "AlunoId", "AlunoId", nota.AlunoId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "MateriaId", "MateriaId", nota.MateriaId);
            return View(nota);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Aluno)
                .Include(n => n.Materia)
                .FirstOrDefaultAsync(m => m.NotaId == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                _context.Notas.Remove(nota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaExists(int id)
        {
            return _context.Notas.Any(e => e.NotaId == id);
        }
    }
}
