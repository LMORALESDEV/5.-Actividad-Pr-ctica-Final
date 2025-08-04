using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenciaDeTours.Models
{
    public class DestinoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Nombre { get; set; }
        [DisplayName("País")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un país")]
        public int PaisId { get; set; }
        [DisplayName("Días de Duración")]
        public int DuracionDias { get; set; }
        [DisplayName("Horas de Duración")]
        public int DuracionHoras { get; set; }
        public PaisViewModel? Pais { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> PaisesOpciones { get; set; } = [];
    }
}
