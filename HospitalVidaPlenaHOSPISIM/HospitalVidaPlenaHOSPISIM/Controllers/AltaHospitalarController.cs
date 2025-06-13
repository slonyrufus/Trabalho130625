using HospitalVidaPlenaHOSPISIM.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalVidaPlenaHOSPISIM.Controllers
{
    public class AltaHospitalarController : Controller
    {
        private readonly HospisimContext.Data.HospisimContext _context;

        public AltaHospitalarController(HospisimContext.Data.HospisimContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var altas = _context.AltasHospitalares.Include(a => a.Internacao);
            return View(altas.ToList());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null) return BadRequest();

            var alta = _context.AltasHospitalares
                .Include(a => a.Internacao)
                .FirstOrDefault(a => a.Id == id);

            if (alta == null) return NotFound();

            return View(alta);
        }

        public IActionResult Create()
        {
            ViewBag.Internacoes = new SelectList(_context.Internacoes, "Id", "MotivoInternacao");
            ViewBag.Prontuarios = new SelectList(_context.Prontuarios, "Id", "Numero");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,InternacaoId,DataAlta,Observacoes")] AltaHospitalar alta)
        {
            if (ModelState.IsValid)
            {
                alta.Id = Guid.NewGuid();
                _context.AltasHospitalares.Add(alta);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InternacaoId = new SelectList(_context.Internacoes, "Id", "MotivoInternacao", alta.InternacaoId);
            return View(alta);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null) return BadRequest();

            var alta = _context.AltasHospitalares.Find(id);
            if (alta == null) return NotFound();

            ViewBag.InternacaoId = new SelectList(_context.Internacoes, "Id", "MotivoInternacao", alta.InternacaoId);
            return View(alta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,InternacaoId,DataAlta,Observacoes")] AltaHospitalar alta)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(alta).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InternacaoId = new SelectList(_context.Internacoes, "Id", "MotivoInternacao", alta.InternacaoId);
            return View(alta);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null) return BadRequest();

            var alta = _context.AltasHospitalares.Find(id);
            if (alta == null) return NotFound();

            return View(alta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var alta = _context.AltasHospitalares.Find(id);
            _context.AltasHospitalares.Remove(alta);
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
