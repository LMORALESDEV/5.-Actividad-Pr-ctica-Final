using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenciaDeTours.Models
{
    public class TourViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Nombre { get; set; }
        [DisplayName("Destino")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un destino")]
        public int DestinoId { get; set; }
        public DestinoViewModel? Destino { get; set; }
        public DateTime Fecha { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe colocar un precio")]
        public decimal Precio { get; set; }
        public decimal ITBIS { get; set; }
        public int DuracionDias { get; set; }
        public int DuracionHoras { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoTour EstadoTour { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> PaisesOpciones { get; internal set; } = [];
        [NotMapped]
        [DisplayName("País")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un país")]
        public int PaisId { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> DestinosOpciones { get; internal set; } = [];
    }
}
