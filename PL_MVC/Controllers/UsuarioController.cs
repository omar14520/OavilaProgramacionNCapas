using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuarioBA)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAllEF(usuarioBA);

            if (result.Correct)
            {
                //obtiene la informacion
                //result.Objects
                usuario.Rol = new ML.Rol();
                usuario.Usuarios = result.Objects;
            }
            else
            {
                //imprimir mensaje de error
                usuario.Usuarios = new List<object>();
            }
            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects;

            return View(usuario);//solo puedes pasar un valor 
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;
            

            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Correct)
            {
                //obtiene la informacion
                //result.Objects
                usuario.Rol = new ML.Rol();
                usuario.Usuarios = result.Objects;
            }
            else
            {
                //imprimir mensaje de error
                usuario.Usuarios = new List<object>();
            }
            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects;
            return View(usuario);//solo puedes pasar un valor 
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            //ML.Result resultDDL = BL.Rol.GetAll();

            //ML.Result resultDDL = BL.Rol.GetAll();


            if (IdUsuario == null)
            {
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Colonias = new List<object>();
                usuario.Direccion.Colonia.Municipio.Municipios = new List<object>();
            }

            else
            {

                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
                usuario = (ML.Usuario)result.Object;
                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

            }
            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects;

            ML.Result resultEstado = BL.Estado.GetAll();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

            //ML.Result resultEstado = BL.Estado.GetAll();
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            HttpPostedFileBase file = Request.Files["inptFileImagen"];
            if (file != null)
            {
                usuario.Imagen = ConvertirAArrayBytes(file);
            }
            if (usuario.IdUsuario == 0)
            {

                //BL.Usuario.GetAllEF(usuario);  
                BL.Usuario.AddEF(usuario);
            }
            else
            {

                BL.Usuario.UpdateEF(usuario);

            }
            return RedirectToAction("GetAll");
              
        }
        public byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            return data;
        }


        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(IdUsuario);
            if (result.Correct)
            {

                return RedirectToAction("GetAll");

            }
            else
            {
                return View("GetAll");
            }

        }
        public JsonResult CambioEstatus(int IdUsuario, bool Estatus)
        {

            ML.Result JsonResult = BL.Usuario.CambioEstatus(IdUsuario, Estatus);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetByIdEstado(int IdEstado)
        {
            ML.Result JsonResult = BL.Municipio.GetByIdEstado(IdEstado);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result JsonResult = BL.Colonia.GetByIdMunicipio(IdMunicipio);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }
    }
}