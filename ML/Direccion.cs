using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        [DisplayName("Calle")]
        [Required(ErrorMessage = "La Calle es Obligatorio")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "La Calle solo acepta letras")]
        public string Calle { get; set; }
        [DisplayName("Numero Interior")]
        [Required(ErrorMessage = "El Numero Interior es Obligatorio")]
        [RegularExpression("^([0-9]{2})*$", ErrorMessage = "El Numero Interior solo acepta numeros")]
        public string NumeroInterior { get; set; }
        [DisplayName("Numero Exterior")]
        [Required(ErrorMessage = "El Numero Exterior es Obligatorio")]
        [RegularExpression("^([0-9]{2})$", ErrorMessage = "El Numero Exterior solo acepta numeros")]
        public string NumeroExterior { get; set; }
        public List<object> direccion{ get; set; }
        public ML.Colonia Colonia { get; set; }
    }
}
