using AgenciaDeTours.Datos;
using AgenciaDeTours.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace AgenciaDeTours.Controllers
{

    public class DestinosController : Controller
    {
        private readonly AgenciaDeToursDbContext context;

        public DestinosController(AgenciaDeToursDbContext context)
        {
            this.context = context;
        }

        public ActionResult Lista()
        {
            var destinos = context.Destinos.Include(x => x.Pais).ToList();

            return View(destinos);

        }

        public IActionResult Crear()
        {
            var modelo = new DestinoViewModel();
            modelo.PaisesOpciones = ObtenerPaises();
            return View(modelo);
        }

        [HttpPost]
        public IActionResult Crear(DestinoViewModel model)
        {
            ValidarModelo(model);
            if (!ModelState.IsValid)
            {
                model.PaisesOpciones = ObtenerPaises();
                return View(model);
            }

            context.Add(model);
            context.SaveChanges();
            return RedirectToAction("Lista");
        }

        private void ValidarModelo(DestinoViewModel model)
        {
            if (model.DuracionDias == 0 && model.DuracionHoras == 0)
            {
                ModelState.AddModelError("", "El destino debe tener una duración");
            }
        }

        public ActionResult Editar(int id)
        {
            var model = context.Destinos.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return RedirectToAction("Lista");

            model.PaisesOpciones = ObtenerPaises();
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(DestinoViewModel model)
        {
            ValidarModelo(model);
            if (!ModelState.IsValid)
            {
                model.PaisesOpciones = ObtenerPaises();
                return View(model);
            }

            context.Update(model);
            context.SaveChanges();

            return RedirectToAction("Lista");
        }

        public ActionResult Eliminar(int id)
        {
            var materia = context.Destinos.FirstOrDefault(e => e.Id == id);
            if (materia != null)
            {
                context.Remove(materia);
                context.SaveChanges();
            }

            return RedirectToAction("Lista");
        }

        private IEnumerable<SelectListItem> ObtenerPaises()
        {
            var paises = context.Paises.OrderBy(x => x.Nombre).ToList();
            return paises.Select(pais => new SelectListItem(pais.Nombre, pais.Id.ToString())).ToList();
        }

        public async Task<IActionResult> ExportarCsv()
        {
            var destinos = await context.Destinos.Include(x => x.Pais).ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Id,Nombre,PaisId,Pais,DuracionDias,DuracionHoras");

            foreach (var r in destinos)
            {
                sb.AppendLine($"{r.Id},{r.Nombre},{r.PaisId},{r.Pais.Nombre},{r.DuracionDias},{r.DuracionHoras}");
            }


            var bytes = Encoding.UTF8.GetPreamble()
                      .Concat(Encoding.UTF8.GetBytes(sb.ToString()))
                      .ToArray();

            return File(bytes, "text/csv", "destinos.csv");
        }

        [HttpGet]
        public IActionResult PorPais(int id)
        {
            var destinos = context.Destinos
                .Where(c => c.PaisId == id)
                .ToList();

            return Json(destinos);
        }
    }
}
