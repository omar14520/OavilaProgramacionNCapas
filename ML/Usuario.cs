using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [DisplayName("User Name")]
        [Required(ErrorMessage = "User Name Obligatorio")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Nombre solo acepta letras")]
        public string UserName { get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Nombre Obligatorio")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "User Name solo acepta letras")]
        public string Nombre { get; set; }
        [DisplayName("Apelldio Paterno")]
        [Required(ErrorMessage = "Apellido Paterno Obligatorio")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Apellido Paterno solo acepta letras")]
        public string ApellidoPaterno { get; set; }
        [DisplayName("Apellido Materno")]
        [Required(ErrorMessage = "Apellido Materno Obligatorio")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Apellido Materno solo acepta letras")]
        public string ApellidoMaterno { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email Obligatorio")]
        [RegularExpression(@"^[^\s@@]+@[^\s@@]+\.[^\s@@]+$", ErrorMessage = "Email solo acepta Correos Validos")]
        public string Email { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Password Obligatorio")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_]).{8}$", ErrorMessage = "Apellido Materno solo acepta letras")]
        public string Password { get; set; }
        public string confirpass { get; set; }
        [DisplayName("Confirmar Contraseña")]
        [Required(ErrorMessage = "Confirmar Contraseña Obligatorio")]
        
        public string FechaNacimiento { get; set; }
        [DisplayName("Sexo")]
        [Required(ErrorMessage = "El sexo es Obligatorio")]
        public string Sexo { get; set; }
        [DisplayName("Telefono")]
        [Required(ErrorMessage = "El Telefono es Obligatorio")]
        [RegularExpression("^([0-9]{10})*$", ErrorMessage = "El Telefono solo acepta numeros")]
        public string Telefono { get; set; }
        [DisplayName("Celular")]
        [Required(ErrorMessage = "El Celular es Obligatorio")]
        [RegularExpression("^([0-9]{10})*$", ErrorMessage = "El Celular solo acepta numeros")]
        public string Celular { get; set; }
        [DisplayName("Estatus")]
        [Required(ErrorMessage = "El Estatus es Obligatorio")]
        public bool Estatus { get; set; }
        [DisplayName("Curp")]
        [Required(ErrorMessage = "La Curp es Obligatoria")]
        [RegularExpression(@"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$", ErrorMessage = "La Curp es Obligatoria")]
        public string Curp { get; set; }
        //public int  IdColonia { get; set; }
        //public int IdRol { get; set; }

        public byte[] Imagen { get; set; }

        public ML.Rol Rol { get; set; }
        public List<object> Usuarios {  get; set; }
        public ML.Direccion Direccion { get; set; }

    }

}
