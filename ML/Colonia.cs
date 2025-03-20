using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Colonia
    {

        public List<object> Colonias { get; set; }
        [DisplayName("ID de la Colonia")]
        [Required(ErrorMessage = "Selecciona una Colonia es Obligatoria")]
        public int IdColonia { get; set; }
        public string Nombre { get; set; }
        public string CodigoPostal { get; set; }
        public ML.Municipio Municipio { get; set; }
    }
}
