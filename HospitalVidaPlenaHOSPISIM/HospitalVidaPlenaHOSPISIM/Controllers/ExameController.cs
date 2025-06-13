using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalVidaPlenaHOSPISIM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HospitalVidaPlenaHOSPISIM.Controllers
{
    public class ExameController : Controller
    {
        private readonly HospisimContext.Data.HospisimContext _context;

        public ExameController(HospisimContext.Data.HospisimContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var exames = _context.Exames.Include(e => e.Atendimento);
            return View(exames.ToList());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null) return BadRequest();

            var exame = _context.Exames
                .Include(e => e.Atendimento)
                .FirstOrDefault(e => e.Id == id);

            if (exame == null) return NotFound();

            return View(exame);
        }

        public IActionResult Create()
        {
            ViewBag.Atendimentos = new SelectList(_context.Atendimentos, "Id", "Status");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Tipo,DataSolicitacao,DataRealizacao,Resultado,AtendimentoId")] Exame exame)
        {
            if (ModelState.IsValid)
            {
                exame.Id = Guid.NewGuid();
                _context.Exames.Add(exame);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AtendimentoId = new SelectList(_context.Atendimentos, "Id", "Status", exame.AtendimentoId);
            return View(exame);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null) return BadRequest();

            var exame = _context.Exames.Find(id);
            if (exame == null) return NotFound();

            ViewBag.AtendimentoId = new SelectList(_context.Atendimentos, "Id", "Status", exame.AtendimentoId);
            return View(exame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Tipo,DataSolicitacao,DataRealizacao,Resultado,AtendimentoId")] Exame exame)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(exame).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AtendimentoId = new SelectList(_context.Atendimentos, "Id", "Status", exame.AtendimentoId);
            return View(exame);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null) return BadRequest();

            var exame = _context.Exames
                .Include(e => e.Atendimento)
                .FirstOrDefault(e => e.Id == id);

            if (exame == null) return NotFound();

            return View(exame);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var exame = _context.Exames.Find(id);
            _context.Exames.Remove(exame);
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
