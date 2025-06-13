using HospitalVidaPlenaHOSPISIM.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalVidaPlenaHOSPISIM.Controllers
{
    public class InternacaoController : Controller
    {
        private readonly HospisimContext.Data.HospisimContext _context;

        public InternacaoController(HospisimContext.Data.HospisimContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var internacoes = _context.Internacoes
                .Include(i => i.Paciente)
                .Include(i => i.Atendimento)
                .ToList();
            return View(internacoes);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null) return BadRequest();

            var internacao = _context.Internacoes
                .Include(i => i.Paciente)
                .Include(i => i.Atendimento)
                .Include(i => i.AltaHospitalar)
                .FirstOrDefault(i => i.Id == id);

            if (internacao == null) return NotFound();

            return View(internacao);
        }

        public IActionResult Create()
        {
            ViewBag.PacienteId = new SelectList(_context.Pacientes, "Id", "NomeCompleto");
            ViewBag.AtendimentoId = new SelectList(_context.Atendimentos, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PacienteId,AtendimentoId,DataEntrada,PrevisaoAlta,MotivoInternacao,Leito,Quarto,Setor,PlanoSaudeUtilizado,ObservacoesClinicas,StatusInternacao")] Internacao internacao)
        {
            if (ModelState.IsValid)
            {
                internacao.Id = Guid.NewGuid();
                _context.Internacoes.Add(internacao);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PacienteId = new SelectList(_context.Pacientes, "Id", "NomeCompleto", internacao.PacienteId);
            ViewBag.AtendimentoId = new SelectList(_context.Atendimentos, "Id", "Id", internacao.AtendimentoId);
            return View(internacao);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null) return BadRequest();

            var internacao = _context.Internacoes.Find(id);
            if (internacao == null) return NotFound();

            ViewBag.PacienteId = new SelectList(_context.Pacientes, "Id", "NomeCompleto", internacao.PacienteId);
            ViewBag.AtendimentoId = new SelectList(_context.Atendimentos, "Id", "Id", internacao.AtendimentoId);
            return View(internacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,PacienteId,AtendimentoId,DataEntrada,PrevisaoAlta,MotivoInternacao,Leito,Quarto,Setor,PlanoSaudeUtilizado,ObservacoesClinicas,StatusInternacao")] Internacao internacao)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(internacao).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PacienteId = new SelectList(_context.Pacientes, "Id", "NomeCompleto", internacao.PacienteId);
            ViewBag.AtendimentoId = new SelectList(_context.Atendimentos, "Id", "Id", internacao.AtendimentoId);
            return View(internacao);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null) return BadRequest();

            var internacao = _context.Internacoes.Find(id);
            if (internacao == null) return NotFound();

            return View(internacao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var internacao = _context.Internacoes.Find(id);
            _context.Internacoes.Remove(internacao);
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
