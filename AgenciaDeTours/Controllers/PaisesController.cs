using AgenciaDeTours.Datos;
using AgenciaDeTours.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgenciaDeTours.Controllers
{
    public class PaisesController : Controller
    {
        private readonly AgenciaDeToursDbContext context;

        public PaisesController(AgenciaDeToursDbContext context)
        {
            this.context = context;
        }

        public ActionResult Lista()
        {
            var paises = context.Paises.ToList();

            return View(paises);

        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(PaisViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            context.Add(model);
            context.SaveChanges();
            return RedirectToAction("Lista");
        }

        public ActionResult Editar(int id)
        {
            var model = context.Paises.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return RedirectToAction("Lista");

            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(PaisViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            context.Update(model);
            context.SaveChanges();

            return RedirectToAction("Lista");
        }

        public ActionResult Eliminar(int id)
        {
            var materia = context.Paises.FirstOrDefault(e => e.Id == id);
            if (materia != null)
            {
                context.Remove(materia);
                context.SaveChanges();
            }

            return RedirectToAction("Lista");
        }
    }
}
