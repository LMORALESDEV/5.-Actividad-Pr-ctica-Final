using AgenciaDeTours.Datos;
using AgenciaDeTours.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace AgenciaDeTours.Controllers
{
    public class ToursController: Controller
    {
        private readonly AgenciaDeToursDbContext context;

        public ToursController(AgenciaDeToursDbContext context)
        {
            this.context = context;
        }

        public ActionResult Lista()
        {
            var destinos = context.Tours
                .Include(x => x.Destino)
                .ThenInclude(x => x.Pais)
                .ToList();

            return View(destinos);

        }

        public IActionResult Crear()
        {
            var modelo = new TourViewModel();
            modelo.PaisesOpciones = ObtenerPaises();
            return View(modelo);
        }

        [HttpPost]
        public IActionResult Crear(TourViewModel model)
        {
            ValidarModelo(model);

            if (!ModelState.IsValid)
            {
                model.PaisesOpciones = ObtenerPaises();
                var destinos = context.Destinos.Where(x => x.PaisId == model.PaisId).ToList();
                model.DestinosOpciones = ConvertirDestinosAOpciones(destinos);
                return View(model);
            }

            CalcularColumnas(model);

            context.Add(model);
            context.SaveChanges();
            return RedirectToAction("Lista");
        }

        private void ValidarModelo(TourViewModel model)
        {
            if (model.Fecha < DateTime.Now)
            {
                ModelState.AddModelError(nameof(model.Fecha), "La fecha no puede estar en el pasado");
            }
        }

        private void CalcularColumnas(TourViewModel model)
        {
            var destino = context.Destinos.FirstOrDefault(x => x.Id == model.DestinoId);
            model.ITBIS = model.Precio * 0.18m;
            model.DuracionDias = destino.DuracionDias;
            model.DuracionHoras = destino.DuracionHoras;
            model.FechaFin = model.Fecha.AddDays(destino.DuracionDias).AddHours(destino.DuracionHoras);
        }

        public ActionResult Editar(int id)
        {
            var model = context.Tours.Include(x => x.Destino).FirstOrDefault(x => x.Id == id);
            if (model == null)
                return RedirectToAction("Lista");

            model.PaisId = model.Destino.PaisId;
            model.PaisesOpciones = ObtenerPaises(model.PaisId);

            var destinos = context.Destinos.Where(x => x.PaisId == model.Destino.PaisId).ToList();
            model.DestinosOpciones = ConvertirDestinosAOpciones(destinos);
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(TourViewModel model)
        {
            ValidarModelo(model);

            if (!ModelState.IsValid)
            {
                var destino = context.Destinos.FirstOrDefault(x => x.Id == model.DestinoId);
                model.Destino = destino;
                model.PaisesOpciones = ObtenerPaises(model.Destino.PaisId);
                var destinos = context.Destinos.Where(x => x.PaisId == model.Destino.PaisId).ToList();
                model.DestinosOpciones = ConvertirDestinosAOpciones(destinos);
                return View(model);
            }

            CalcularColumnas(model);

            context.Update(model);
            context.SaveChanges();

            return RedirectToAction("Lista");
        }

        public ActionResult Eliminar(int id)
        {
            var materia = context.Tours.FirstOrDefault(e => e.Id == id);
            if (materia != null)
            {
                context.Remove(materia);
                context.SaveChanges();
            }

            return RedirectToAction("Lista");
        }

        private IEnumerable<SelectListItem> ObtenerPaises(int paisSeleccionado = 0)
        {
            var paises = context.Paises.OrderBy(x => x.Nombre).ToList();
            var opciones = paises.Select(pais => new SelectListItem(pais.Nombre, pais.Id.ToString(), selected: paisSeleccionado == pais.Id)).ToList();
            opciones.Insert(0, new SelectListItem("--Seleccione un país--", "0"));
            return opciones;
        }

        private IEnumerable<SelectListItem> ConvertirDestinosAOpciones(List<DestinoViewModel> destinos)
        {
            var opciones = destinos.Select(destino => new SelectListItem(destino.Nombre, destino.Id.ToString())).ToList();
            return opciones;
        }

        public async Task<IActionResult> ExportarCsv()
        {
            var tours = await context.Tours
                .Include(x => x.Destino)
                    .ThenInclude(x => x.Pais)
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Id,Nombre,PaisId,Pais,Destino,DestinoId,Fecha,Precio,ITBIS,DuracionDias,DuracionHoras,FechaFin,EstadoTour");

            foreach (var r in tours)
            {
                sb.AppendLine($"{r.Id},{r.Nombre},{r.Destino.PaisId},{r.Destino.Pais.Nombre},{r.Destino.Nombre},{r.DestinoId},{r.Fecha},{r.Precio},{r.ITBIS},{r.DuracionDias},{r.DuracionHoras},{r.FechaFin},{r.EstadoTour}");
            }

            var bytes = Encoding.UTF8.GetPreamble()
                     .Concat(Encoding.UTF8.GetBytes(sb.ToString()))
                     .ToArray();
            return File(bytes, "text/csv", "tours.csv");
        }
    }
}
