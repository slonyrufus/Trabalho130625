using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using HospitalVidaPlenaHOSPISIM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalVidaPlenaHOSPISIM.Controllers
{
    public class ProfissionalSaudeController : Controller
    {
        private readonly HospisimContext.Data.HospisimContext _context;

        public ProfissionalSaudeController(HospisimContext.Data.HospisimContext context)
        {
            _context = context;
        }

        [HttpGet("/ProfissionalSaude")]
        public IActionResult Index()
        {
            return View(_context.ProfissionaisSaude.ToList());
        }

        [HttpGet("Details")]
        public IActionResult Details(Guid? id)
        {
            if (id == null) return BadRequest();
            var profissional = _context.ProfissionaisSaude.Find(id);
            if (profissional == null) return NotFound();
            return View(profissional);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nome,Especialidade,RegistroProfissional")] ProfissionalSaude profissional)
        {
            if (ModelState.IsValid)
            {
                profissional.Id = Guid.NewGuid();
                _context.ProfissionaisSaude.Add(profissional);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profissional);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null) return BadRequest();
            var profissional = _context.ProfissionaisSaude.Find(id);
            if (profissional == null) return NotFound();
            ViewBag.Especialidades = new SelectList(_context.Especialidades.ToList(), "Id", "Nome", profissional.EspecialidadeId);
            return View(profissional);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Nome,Especialidade,RegistroProfissional")] ProfissionalSaude profissional)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(profissional).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profissional);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null) return BadRequest();
            var profissional = _context.ProfissionaisSaude.Find(id);
            if (profissional == null) return NotFound();
            return View(profissional);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var profissional = _context.ProfissionaisSaude.Find(id);
            _context.ProfissionaisSaude.Remove(profissional);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
