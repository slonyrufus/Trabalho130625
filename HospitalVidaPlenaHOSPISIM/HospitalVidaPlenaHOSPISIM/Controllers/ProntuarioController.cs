using System.Net;
using HospitalVidaPlenaHOSPISIM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HospitalVidaPlenaHOSPISIM.Controllers
{
    public class ProntuarioController : Controller
    {
        private readonly HospisimContext.Data.HospisimContext _context;

        public ProntuarioController(HospisimContext.Data.HospisimContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var prontuarios = _context.Prontuarios
                .Include(p => p.Paciente)
                .ToList();
            return View(prontuarios);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null) return BadRequest();

            var prontuario = _context.Prontuarios
                .Include(p => p.Paciente)
                .FirstOrDefault(p => p.Id == id);

            if (prontuario == null) return NotFound();

            return View(prontuario);
        }

        public IActionResult Create()
        {
            ViewBag.PacienteId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Pacientes, "Id", "NomeCompleto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,PacienteId,Descricao,DataCriacao")] Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                prontuario.Id = Guid.NewGuid();
                _context.Prontuarios.Add(prontuario);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PacienteId = new SelectList(_context.Pacientes, "Id", "NomeCompleto", prontuario.PacienteId);
            return View(prontuario);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null) return BadRequest();

            var prontuario = _context.Prontuarios.Find(id);
            if (prontuario == null) return NotFound();

            ViewBag.PacienteId = new SelectList(_context.Pacientes, "Id", "NomeCompleto", prontuario.PacienteId);
            return View(prontuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,PacienteId,Descricao,DataCriacao")] Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(prontuario).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PacienteId = new SelectList(_context.Pacientes, "Id", "NomeCompleto", prontuario.PacienteId);
            return View(prontuario);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null) return BadRequest();
            var prontuario = _context.Prontuarios.Find(id);
            if (prontuario == null) return NotFound();
            return View(prontuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var prontuario = _context.Prontuarios.Find(id);
            _context.Prontuarios.Remove(prontuario);
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
