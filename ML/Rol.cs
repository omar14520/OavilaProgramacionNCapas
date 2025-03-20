using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Rol
    {
        public List<object> Roles { get; set; }
        [DisplayName("Rol derl Usuario")]
        [Required(ErrorMessage = "Selecciona un rol es Obligatorio")]
        public int IdRol { get; set; }
        public string Nombre { get; set; }
    }
}
