using System.Net;
using Microsoft.AspNetCore.Mvc;
using HospitalVidaPlenaHOSPISIM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalVidaPlenaHOSPISIM.Controllers
{
    public class PacienteController : Controller
    {
        private readonly HospisimContext.Data.HospisimContext _context;

        public PacienteController(HospisimContext.Data.HospisimContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var pacientes = _context.Pacientes.ToList();
            return View(pacientes);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null) return BadRequest();
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null) return NotFound();
            return View(paciente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,NomeCompleto,CPF,DataNascimento,Sexo,TipoSanguineo,Telefone,Email,EnderecoCompleto,NumeroCartaoSUS,EstadoCivil,PossuiPlanoSaude")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                paciente.Id = Guid.NewGuid();
                _context.Pacientes.Add(paciente);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paciente);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null) return BadRequest();
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null) return NotFound();
            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,NomeCompleto,CPF,DataNascimento,Sexo,TipoSanguineo,Telefone,Email,EnderecoCompleto,NumeroCartaoSUS,EstadoCivil,PossuiPlanoSaude")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(paciente).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paciente);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null) return BadRequest();
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null) return NotFound();
            return View(paciente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var paciente = _context.Pacientes.Find(id);
            _context.Pacientes.Remove(paciente);
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
