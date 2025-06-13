using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using HospitalVidaPlenaHOSPISIM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalVidaPlenaHOSPISIM.Controllers
{
    public class AtendimentoController : Controller
    {
        private readonly HospisimContext.Data.HospisimContext _context;

        public AtendimentoController(HospisimContext.Data.HospisimContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var atendimentos = _context.Atendimentos
                .Include(a => a.Prontuario)
                .Include(a => a.Profissional);
            return View(atendimentos.ToList());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null) return BadRequest();

            var atendimento = _context.Atendimentos
                .Include(a => a.Profissional)
                .Include(a => a.Prontuario)
                .FirstOrDefault(a => a.Id == id);

            if (atendimento == null) return NotFound();
            return View(atendimento);
        }

        public IActionResult Create()
        {
            ViewBag.Prontuarios = new SelectList(_context.ProfissionaisSaude.ToList(), "Id", "NumeroProntuario");
            ViewBag.Profissionais = new SelectList(_context.Prontuarios.ToList(), "Id", "NomeCompleto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,DataHora,Tipo,Status,Local,ProntuarioId,ProfissionalId")] Atendimento atendimento)
        {
            if (ModelState.IsValid)
            {
                atendimento.Id = Guid.NewGuid();
                _context.Atendimentos.Add(atendimento);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfissionalId = new SelectList(_context.ProfissionaisSaude.ToList(), "Id", "NomeCompleto", atendimento.ProfissionalId);
            ViewBag.ProntuarioId = new SelectList(_context.Prontuarios.ToList(), "Id", "NumeroProntuario", atendimento.ProntuarioId);
            return View(atendimento);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null) return BadRequest();

            var atendimento = _context.Atendimentos.Find(id);
            if (atendimento == null) return NotFound();

            ViewBag.ProntuarioId = new SelectList(_context.Prontuarios.ToList(), "Id", "NumeroProntuario", atendimento.ProntuarioId);
            ViewBag.ProfissionalId = new SelectList(_context.ProfissionaisSaude.ToList(), "Id", "NomeCompleto", atendimento.ProfissionalId);
            return View(atendimento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,DataHora,Tipo,Status,Local,ProntuarioId,ProfissionalId")] Atendimento atendimento)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(atendimento).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProntuarioId = new SelectList(_context.Prontuarios.ToList(), "Id", "NumeroProntuario", atendimento.ProntuarioId);
            ViewBag.ProfissionalId = new SelectList(_context.ProfissionaisSaude.ToList(), "Id", "NomeCompleto", atendimento.ProfissionalId);
            return View(atendimento);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null) return BadRequest();

            var atendimento = _context.Atendimentos.Find(id);
            if (atendimento == null) return NotFound();

            return View(atendimento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var atendimento = _context.Atendimentos.Find(id);
            _context.Atendimentos.Remove(atendimento);
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
