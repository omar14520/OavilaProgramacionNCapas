using BL;
using Microsoft.Owin.Security.Provider;
using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["inptFileImagen"];
                if (file != null && file.ContentLength > 0)
                {
                    usuario.Imagen = ConvertirAArrayBytes(file);

                }

                if (usuario.IdUsuario == 0)
                {
                    ML.Result result = BL.Usuario.AddEF(usuario);
                    if (result.Correct)
                    {
                        ViewBag.Add = true;
                        ViewBag.Message = "El usuario se inserto correctamente";
                    }
                    else
                    {
                        ViewBag.Add = false;
                        ViewBag.Message = "ERROR" + result.ErrorMessage;
                    }

                    return PartialView("_Mensajes");

                }

                else
                {
                    ML.Result result = BL.Usuario.UpdateEF(usuario);
                    if (result.Correct)
                    {
                        ViewBag.Add = true;
                        ViewBag.Message = "El usuario se actualizo correctamente";
                    }
                    else
                    {
                        ViewBag.Add = false;
                        ViewBag.Message = "ERROR" + result.ErrorMessage;
                    }


                    //return View("GetAll");
                }

                //return RedirectToAction("GetAll");
                return PartialView("_Mensajes");


            }
            else
            {
                ML.Result resultDDL = BL.Rol.GetAll();
                usuario.Rol.Roles = resultDDL.Objects;

                ML.Result resultEstado = BL.Estado.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;


                //usuario.Direccion.Colonia.Municipio.Municipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado).Objects;

                //usuario.Direccion.Colonia.Colonias = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio).Objects;

                if (usuario.Direccion.Colonia.Municipio.Estado.IdEstado > 0)
                {
                    usuario.Direccion.Colonia.Municipio.Municipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado).Objects;
                }

                // Si el usuario ya seleccionó un municipio, cargamos las colonias de ese municipio
                if (usuario.Direccion.Colonia.Municipio.IdMunicipio > 0)
                {
                    usuario.Direccion.Colonia.Colonias = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio).Objects;
                }

                return View(usuario);

            }


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
                ViewBag.Add = true;
                ViewBag.Message = "El usuario se borro correctamente";

                //return RedirectToAction("GetAll");

            }
            else
            {
                ViewBag.Add = false;
                ViewBag.Message = "Error" + result.ErrorMessage;
                //return View("GetAll");
            }
            return PartialView("_Mesajes");

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
        [HttpPost]
        public ActionResult CargaMasiva()
        {
            if (Session["RutaExcel"] == null)
            {

                HttpPostedFileBase excelUsuario = Request.Files["Excel"];

                string extensionPermitida = ".xlsx";

                if (excelUsuario.ContentLength > 0)
                {
                    //t
                    string extensionObtenida = Path.GetExtension(excelUsuario.FileName);

                    if (extensionObtenida == extensionPermitida)
                    {
                        string ruta = Server.MapPath("~/CargaMasiva/") + Path.GetFileNameWithoutExtension(excelUsuario.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyHmmssff") + ".xlsx";

                        if (!System.IO.File.Exists(ruta))
                        {
                            excelUsuario.SaveAs(ruta);

                            string cadenaConexion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + ruta;

                            ML.Result resultExcel = BL.Usuario.LeerExcel(cadenaConexion);

                            //BL.Usuario.LeerExcel(cadenaConexion);

                            if (resultExcel.Objects.Count > 0)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultExcel.Objects);

                                if (resultValidacion.Objects.Count > 0)
                                {
                                    ViewBag.ErroresExcel = resultValidacion.Objects;
                                    ViewBag.ErroresExcel = "Se encontraron errores en el archivo.";
                                    //ViewBag.MensajeExito = null;
                                    //ViewBag.EsExito = false;
                                    return PartialView("_Modal");
                                }
                                else
                                {
                                    Session["RutaExcel"] = ruta;
                                    ViewBag.ErroresExcel = "La validacion del Excel es correcta";
                                    //ViewBag.MensajeError = null;
                                    //ViewBag.EsExito = true;
                                    //return RedirectToAction("GetAll");
                                    return PartialView("_Modal");
                                }

                            }

                        }
                        else
                        {
                            ViewBag.ErroresExcel = "Vuelve a cargar el archivo, porque ya existe";
                            return PartialView("_Modal");

                        }

                    }
                    else
                    {
                        ViewBag.ErroresExcel = "El archivo no es un Excel";
                        return PartialView("_Modal");

                    }

                }
                else
                {
                    ViewBag.ErroresExcel = "No me diste ningun archivo";
                    return PartialView("_Modal");

                }

            }
            else
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + Session["RutaExcel"].ToString();

                ML.Result resultLeer = BL.Usuario.LeerExcel(cadenaConexion);

                if (resultLeer.Objects.Count > 0)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    int registrosCorrectos = 0;
                    int registrosIncorrectos = 0;

                    List<ML.Usuario> usuariosCorrectos = new List<ML.Usuario>();

                    List<ML.Usuario> usuariosIncorrectos = new List<ML.Usuario>();


                    foreach (ML.Usuario usuario in resultLeer.Objects)
                    {
                        ML.Result resultInsertar = BL.Usuario.AddEF(usuario);
                        if (!resultInsertar.Correct)
                        {

                            registrosIncorrectos++;
                            usuariosIncorrectos.Add(usuario);
                        }
                        else
                        {
                            registrosCorrectos++;
                            usuariosCorrectos.Add(usuario);
                        }

                    }

                    //if (resultErrores.Objects.Count > 0)
                    //{
                    //    ViewBag.ErroresExcel = resultErrores.Objects;
                    //}

                    // Mostrar los conteos de inserciones correctas e incorrectas
                    ViewBag.UsuariosCorrectos = usuariosCorrectos;
                    ViewBag.UsuariosIncorrectos = usuariosIncorrectos;
                    ViewBag.RegistrosCorrectos = registrosCorrectos;
                    ViewBag.RegistrosIncorrectos = registrosIncorrectos;

                    if (registrosIncorrectos > 0)
                    {
                        // Si hay errores, mostrar el modal con los errores
                        ViewBag.ErroresExcel = "Se hizo el proceso de registro" + resultLeer.Objects.Count + ", usuarios de los cuales fallaron" + registrosIncorrectos + ", y se insertaron" + registrosCorrectos;
                        Session["RutaExcel"] = null;
                        return PartialView("_Modal");

                    }
                    else
                    {
                        // Si no hay errores, mostrar un mensaje de éxito
                        ViewBag.ErroresExcel = "La carga masiva fue realizada con éxito.";
                        Session["RutaExcel"] = null;

                        return PartialView("_Modal");
                    }

                }
                else
                {
                    ViewBag.ErroresExcel = "No se puede insertar";
                    Session["RutaExcel"] = null;
                    return PartialView("_Modal");

                }
                //archivo

            }
            Session["RutaExcel"] = null;

            return RedirectToAction("GetAll");

        }


    }
}