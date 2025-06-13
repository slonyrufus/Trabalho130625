using System.Net;
using Microsoft.AspNetCore.Mvc;
using HospitalVidaPlenaHOSPISIM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalVidaPlenaHOSPISIM.Controllers
{
    public class PrescricaoController : Controller
    {
        private readonly HospisimContext.Data.HospisimContext _context;

        public PrescricaoController(HospisimContext.Data.HospisimContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var prescricoes = _context.Prescricoes
                .Include(p => p.Profissional)
                .Include(p => p.Atendimento)
                .ToList();
            return View(prescricoes);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null) return BadRequest();

            var prescricao = _context.Prescricoes
                .Include(p => p.Profissional)
                .Include(p => p.Atendimento)
                .FirstOrDefault(p => p.Id == id);

            if (prescricao == null) return NotFound();

            return View(prescricao);
        }

        public IActionResult Create()
        {
            ViewBag.ProfissionalId = new SelectList(_context.ProfissionaisSaude, "Id", "NomeCompleto");
            ViewBag.AtendimentoId = new SelectList(_context.Atendimentos, "Id", "AlgumCampoDescricao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,AtendimentoId,ProfissionalId,Medicamento,Dosagem,Frequencia,ViaAdministracao,DataInicio,DataFim,Observacoes,StatusPrescricao,ReacoesAdversas")] Prescricao prescricao)
        {
            if (ModelState.IsValid)
            {
                prescricao.Id = Guid.NewGuid();
                _context.Prescricoes.Add(prescricao);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfissionalId = new SelectList(_context.ProfissionaisSaude, "Id", "NomeCompleto", prescricao.ProfissionalId);
            ViewBag.AtendimentoId = new SelectList(_context.Atendimentos, "Id", "AlgumCampoDescricao", prescricao.AtendimentoId);
            return View(prescricao);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null) return BadRequest();

            var prescricao = _context.Prescricoes.Find(id);
            if (prescricao == null) return NotFound();

            ViewBag.ProfissionalId = new SelectList(_context.ProfissionaisSaude, "Id", "NomeCompleto", prescricao.ProfissionalId);
            ViewBag.AtendimentoId = new SelectList(_context.Atendimentos, "Id", "Descricao", prescricao.AtendimentoId);
            return View(prescricao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,AtendimentoId,ProfissionalId,Medicamento,Dosagem,Frequencia,ViaAdministracao,DataInicio,DataFim,Observacoes,StatusPrescricao,ReacoesAdversas")] Prescricao prescricao)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(prescricao).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfissionalId = new SelectList(_context.ProfissionaisSaude, "Id", "NomeCompleto", prescricao.ProfissionalId);
            ViewBag.AtendimentoId = new SelectList(_context.Atendimentos, "Id", "Descricao", prescricao.AtendimentoId);
            return View(prescricao);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null) return BadRequest();

            var prescricao = _context.Prescricoes.Find(id);
            if (prescricao == null) return NotFound();

            return View(prescricao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var prescricao = _context.Prescricoes.Find(id);
            _context.Prescricoes.Remove(prescricao);
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
