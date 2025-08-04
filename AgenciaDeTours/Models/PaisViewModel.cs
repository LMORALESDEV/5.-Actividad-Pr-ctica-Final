using System.ComponentModel.DataAnnotations;

namespace AgenciaDeTours.Models
{
    public class PaisViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Nombre { get; set; }
    }
}
