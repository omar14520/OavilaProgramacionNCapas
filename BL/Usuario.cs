using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario


    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UsuarioAdd";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = query; //query que va a ejecutar
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    cmd.Parameters.AddWithValue("@Curp", usuario.Curp);
                    cmd.Parameters.AddWithValue("@Direccion", usuario.Direccion);


                    context.Open(); //abrir la conexion con la BD
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("EL registro se inserto correctamente");
                    }
                    else
                    {
                        Console.WriteLine("Error al insertar");
                    }
                }
                //CERRAR

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de conexion: " + ex.Message);

            }
            return result;



        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UsuarioUpdate";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    cmd.Parameters.AddWithValue("@Curp", usuario.Curp);
                    cmd.Parameters.AddWithValue("@Direccion", usuario.Direccion);

                    context.Open(); //abrir la conexion con la BD
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("EL registro se inserto correctamente");
                    }
                    else
                    {
                        Console.WriteLine("Error al insertar");
                    }
                }
                //CERRAR

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de conexion: " + ex.Message);

            }
            return result;




        }
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UsuarioDelete";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("IdUsuario", IdUsuario);

                    context.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                        result.ErrorMessage = "Se elimino correcto el registro";

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo borrar el registro en la base de datos";
                    }


                }

            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UsuarioGetAll";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                            usuario.UserName = row[1].ToString();
                            usuario.Nombre = row[2].ToString();
                            usuario.ApellidoPaterno = row[3].ToString();
                            usuario.ApellidoMaterno = row[4].ToString();
                            usuario.Email = row[5].ToString();
                            usuario.Password = row[6].ToString();
                            usuario.FechaNacimiento = Convert.ToString(row[7].ToString());
                            usuario.Sexo = row[8].ToString();
                            usuario.Telefono = row[9].ToString();
                            usuario.Celular = row[10].ToString();
                            usuario.Estatus = Convert.ToBoolean(row[11].ToString());
                            usuario.Curp = row[12].ToString();


                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error no se encontraron datos";
                    }
                }

            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection Context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UsuarioGetByID";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = Context;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        //result.Object = new Object();
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                        usuario.UserName = row[1].ToString();
                        usuario.Nombre = row[2].ToString();
                        usuario.ApellidoPaterno = row[3].ToString();
                        usuario.ApellidoMaterno = row[4].ToString();
                        usuario.Email = row[5].ToString();
                        usuario.Password = row[6].ToString();
                        usuario.FechaNacimiento = Convert.ToString(row[7].ToString());
                        usuario.Sexo = row[8].ToString();
                        usuario.Telefono = row[9].ToString();
                        usuario.Celular = row[10].ToString();
                        usuario.Estatus = Convert.ToBoolean(row[11].ToString());
                        usuario.Curp = row[12].ToString();



                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el registro de id";

                    }



                }

            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }

        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OAvilaProgramacionencapasEntities context = new DL_EF.OAvilaProgramacionencapasEntities())
                {
                    int rowsAffect = context.UsuarioAdd(usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno, usuario.Email, usuario.Password, Convert.ToDateTime(usuario.FechaNacimiento),
                        usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.Curp, usuario.Imagen, usuario.Rol.IdRol,
                        usuario.Direccion.Calle, usuario.Direccion.NumeroExterior, usuario.Direccion.NumeroInterior, usuario.Direccion.Colonia.IdColonia);
                    if (rowsAffect > 2)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OAvilaProgramacionencapasEntities context = new DL_EF.OAvilaProgramacionencapasEntities())
                {
                    int rowsAffect = context.UsuarioUpdate(usuario.IdUsuario, usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno, usuario.Email, usuario.Password,
                        Convert.ToDateTime(usuario.FechaNacimiento), usuario.Sexo, usuario.Telefono,
                        usuario.Celular, usuario.Estatus, usuario.Curp, usuario.Imagen, usuario.Rol.IdRol,
                        usuario.Direccion.Calle, usuario.Direccion.NumeroInterior,
                        usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result DeleteEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OAvilaProgramacionencapasEntities context = new DL_EF.OAvilaProgramacionencapasEntities())
                {
                    int rowsAffect = context.UsuarioDelete(IdUsuario);
                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetAllEF(ML.Usuario usuarioBA)
        {
            ML.Result result = new ML.Result();


            try
            {
                using (DL_EF.OAvilaProgramacionencapasEntities context = new DL_EF.OAvilaProgramacionencapasEntities())
                {
                    var query = context.UsuarioGetAll
                        (usuarioBA.Nombre ?? "",
                         usuarioBA.ApellidoPaterno ?? "",
                         usuarioBA.ApellidoMaterno ?? "",
                         usuarioBA.Rol.IdRol).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.IdUsuario = objBD.IdUsuario;
                            usuario.UserName = objBD.UserName;
                            usuario.Nombre = objBD.NombreUsuario;
                            usuario.ApellidoPaterno = objBD.ApellidoPaterno;
                            usuario.ApellidoMaterno = objBD.ApellidoMaterno;
                            usuario.Email = objBD.Email;
                            usuario.Password = objBD.Password;
                            usuario.FechaNacimiento = objBD.Fecha.ToString();
                            usuario.Sexo = objBD.Sexo;
                            usuario.Telefono = objBD.Telefono;
                            usuario.Celular = objBD.Celular;
                            usuario.Estatus = Convert.ToBoolean(objBD.Estatus);
                            usuario.Curp = objBD.CURP;
                            usuario.Imagen = objBD.Imagen;
                            usuario.Rol.Nombre = objBD.RolNombre;
                            usuario.Direccion.Calle = objBD.Calle;
                            usuario.Direccion.NumeroInterior = objBD.NumeroInterior;
                            usuario.Direccion.NumeroExterior = objBD.NumeroExterior;

                            result.Objects.Add(usuario);

                        } result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.OAvilaProgramacionencapasEntities context = new DL_EF.OAvilaProgramacionencapasEntities())
                {
                    var query = context.UsuarioGetById(IdUsuario).SingleOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Nombre = query.NombreUsuario;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento;
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.Imagen = query.Imagen;
                        usuario.Rol.IdRol = query.IdRol;
                        usuario.Direccion.Calle = query.Calle;
                        usuario.Direccion.NumeroExterior = query.NumeroExterior;
                        usuario.Direccion.NumeroInterior = query.NumeroInterior;
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia;
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;
                        if (query.Estatus != true)
                        {
                            usuario.Estatus = true;
                        }
                        else
                        {
                            usuario.Estatus = false;
                        }

                        result.Object = usuario;

                        result.Correct = true;

                    }
                    else
                    {
                        //no hay registros
                        result.Correct = false;
                        result.ErrorMessage = "No hay datos/registros";
                    }

                }

            }
            catch (Exception ex)
            {
                //HUBO UN ERROR
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;




        }
        public static ML.Result AddLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OAvilaProgramacionencapasEntities context = new DL_EF.OAvilaProgramacionencapasEntities())
                {
                    DL_EF.Usuario usuarioBD = new DL_EF.Usuario();
                    usuarioBD.UserName = usuario.UserName;
                    usuarioBD.Nombre = usuario.Nombre;
                    usuarioBD.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioBD.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioBD.Email = usuario.Email;
                    usuarioBD.Password = usuario.Password;
                    usuarioBD.FechaNacimiento = DateTime.Parse(usuario.FechaNacimiento);
                    usuarioBD.Sexo = usuario.Sexo;
                    usuarioBD.Telefono = usuario.Telefono;
                    usuarioBD.Celular = usuario.Celular;
                    usuarioBD.Estatus = usuario.Estatus;
                    usuarioBD.Curp = usuario.Curp;
                    //usuarioBD.Direccion = usuario.Direccion;

                    context.Usuarios.Add(usuarioBD);

                    int filasAfectadas = context.SaveChanges();
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "ERROR AL INSERTAR";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result UpdateLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.OAvilaProgramacionencapasEntities context = new DL_EF.OAvilaProgramacionencapasEntities())
                {
                    var busqueda = (from usuarioBD in context.Usuarios
                                    where usuarioBD.IdUsuario == usuario.IdUsuario
                                    select usuarioBD).SingleOrDefault();
                    if (busqueda != null)
                    {
                        busqueda.IdUsuario = Convert.ToInt32(usuario.IdUsuario);
                        busqueda.UserName = usuario.UserName;
                        busqueda.Nombre = usuario.Nombre;
                        busqueda.ApellidoPaterno = usuario.ApellidoPaterno;
                        busqueda.ApellidoMaterno = usuario.ApellidoMaterno;
                        busqueda.Email = usuario.Email;
                        busqueda.Password = usuario.Password;
                        busqueda.FechaNacimiento = Convert.ToDateTime(usuario.FechaNacimiento);
                        busqueda.Sexo = usuario.Sexo;
                        busqueda.Telefono = usuario.Telefono;
                        busqueda.Celular = usuario.Celular;
                        busqueda.Estatus = Convert.ToBoolean(usuario.Estatus);
                        busqueda.Curp = usuario.Curp;
                        //busqueda.Direccion = usuario.Direccion;

                        int filasafectadas = context.SaveChanges();

                        if (filasafectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "ERROR AL ACTUALIZAR ";

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result DeleteLINQ(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OAvilaProgramacionencapasEntities context = new DL_EF.OAvilaProgramacionencapasEntities())
                {
                    var soloDelete = (from usuario in context.Usuarios
                                      where usuario.IdUsuario == idUsuario
                                      select usuario).SingleOrDefault();


                    var buscarBien = (from usuario in context.Usuarios
                                      where usuario.IdUsuario == idUsuario
                                      select new
                                      {
                                          IdUsuario = usuario.IdUsuario,
                                          UserName = usuario.UserName,
                                          Nombre = usuario.Nombre,
                                          ApellidoPaterno = usuario.ApellidoPaterno,
                                          ApellidoMaterno = usuario.ApellidoMaterno,
                                          Email = usuario.Email,
                                          Password = usuario.Password,
                                          FechaNacimiento = usuario.FechaNacimiento,
                                          Sexo = usuario.Sexo,
                                          Telefono = usuario.Telefono,
                                          Celular = usuario.Celular,
                                          Estatus = usuario.Estatus,
                                          Curp = usuario.Curp,
                                          //Direccion = usuario.Direccion,
                                      }).SingleOrDefault();
                    if (soloDelete != null)
                    {
                        context.Usuarios.Remove(soloDelete);

                        int filasAfectadas = context.SaveChanges();

                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo Eliminar";
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;


        }

        public static ML.Result CambioEstatus(int IdUsuario, bool Estatus)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OAvilaProgramacionencapasEntities context = new DL_EF.OAvilaProgramacionencapasEntities())
                {
                    int rowsAffect = context.CambioEstatus(IdUsuario, Estatus);
                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;
        }

        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();

            string ruta = @"C:\Users\digis\Documents\Omar Daniel Avila Yañez\Usuario.txt";
            try
            {
                StreamReader streamReader = new StreamReader(ruta);
                string fila = "";
                streamReader.ReadLine();
                while ((fila = streamReader.ReadLine()) != null)
                {
                    string[] valores = fila.Split('|');
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                    usuario.UserName = valores[0];
                    usuario.Nombre = valores[1];
                    usuario.ApellidoPaterno = valores[2];
                    usuario.ApellidoMaterno = valores[3];
                    usuario.Email = valores[4];
                    usuario.Password = valores[5];
                    usuario.FechaNacimiento = valores[6];
                    usuario.Sexo = valores[7];
                    usuario.Telefono = valores[8];
                    usuario.Celular = valores[9];
                    usuario.Estatus = Convert.ToBoolean(valores[10].ToString());
                    usuario.Curp = valores[11];
                    usuario.Rol.IdRol = int.Parse(valores[12]);
                    usuario.Direccion.Calle = valores[13];
                    usuario.Direccion.NumeroInterior = valores[14];
                    usuario.Direccion.NumeroExterior = valores[15];
                    usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(valores[16].ToString());

                    BL.Usuario.AddEF(usuario);
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;


        }


        public static ML.Result LeerExcel(string cadenaConexion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(cadenaConexion))
                {
                    string query = "Select * from[Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter adapter = new OleDbDataAdapter();
                        adapter.SelectCommand = cmd;

                        DataTable tablaUsuario = new DataTable();
                        adapter.Fill(tablaUsuario);

                        if (tablaUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Rol = new ML.Rol();
                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                                usuario.UserName = row[0].ToString();
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Password = row[5].ToString();
                                usuario.FechaNacimiento = row[6].ToString();
                                usuario.Sexo = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                usuario.Curp = row[10].ToString();
                                if (row[11] == DBNull.Value || string.IsNullOrEmpty(row[11].ToString()))
                                {
                                    usuario.Rol.IdRol = 0;
                                }
                                else
                                {
                                    usuario.Rol.IdRol = Convert.ToInt32(row[11]);
                                }
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroInterior = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();
                                if (row[15] == DBNull.Value || string.IsNullOrEmpty(row[15].ToString()))
                                {
                                    usuario.Direccion.Colonia.IdColonia = 0;
                                }
                                else
                                {
                                    usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(row[15]);
                                }
                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;

        }

        public static ML.Result ValidarExcel(List<object> registros)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            int contador = 1;

            foreach (ML.Usuario usuario in registros)
            {
                ML.ResultExcel resultValidacion = new ML.ResultExcel();
                resultValidacion.NumeroRegistro = contador;

                if (usuario.UserName.Length > 50 || usuario.UserName == "" || usuario.UserName == null)
                {
                    resultValidacion.ErrorMessage += "El username es muy largo o esta vacio";
                }
                if (usuario.Nombre.Length > 50 || usuario.Nombre == "" || usuario.Nombre == null)
                {
                    resultValidacion.ErrorMessage += "EL nombre es muy largo o esta vacio";
                }
                if (usuario.ApellidoPaterno.Length > 50 || usuario.ApellidoPaterno == "" || usuario.ApellidoPaterno == null)
                {
                    resultValidacion.ErrorMessage += "El apellido paterno es muy largo o esta vacio";
                }
                if (usuario.ApellidoMaterno.Length > 50 || usuario.ApellidoMaterno == "" || usuario.ApellidoMaterno == null)
                {
                    resultValidacion.ErrorMessage += "El apellido materno es muy largo o esta vacio";
                }
                if (usuario.Email.Length > 50 || usuario.Email == "" || usuario.Email == null)
                {
                    resultValidacion.ErrorMessage += "el email es muy largo o esta vacio";
                }
                if (usuario.Password.Length > 50 || usuario.Password == "" || usuario.Password == null)
                {
                    resultValidacion.ErrorMessage += "el password es muy largo o esta vacio";
                }
                if (usuario.FechaNacimiento == "" || usuario.FechaNacimiento == null)
                {
                    resultValidacion.ErrorMessage += "la fecha esta vacia";
                }
                if (usuario.Sexo.Length > 3 || usuario.Sexo == "" || usuario == null)
                {
                    resultValidacion.ErrorMessage += "el sexo esta vacio";
                }
                if (usuario.Telefono.Length > 20 || usuario.Telefono == "" || usuario == null)
                {
                    resultValidacion.ErrorMessage += "el telefono es muy largo o esta vacio";
                }
                if (usuario.Celular.Length > 20 || usuario.Celular == "" || usuario == null)
                {
                    resultValidacion.ErrorMessage += "el celular es muy largo o vacio";
                } 
                if (usuario.Curp.Length > 50 || usuario.Curp == "" || usuario.Curp == null)
                {
                    resultValidacion.ErrorMessage += "la curp esta muy larga o esta vacia";
                }
                if (usuario.Rol.IdRol == 0)
                {
                    resultValidacion.ErrorMessage += "el idrol esta vacio";
                }
                if (usuario.Direccion.Calle.Length > 50 || usuario.Direccion.Calle == "" || usuario.Direccion.Calle == null)
                {
                    resultValidacion.ErrorMessage += "el nombre de la calle es muy larga o esta vacia";
                }
                if (usuario.Direccion.NumeroInterior.Length > 20 || usuario.Direccion.NumeroInterior == "" || usuario.Direccion.NumeroInterior == null) 
                {
                    resultValidacion.ErrorMessage += "el numero interior es muy largo o esta vacio";
                }
                if(usuario.Direccion.NumeroExterior.Length > 20 || usuario.Direccion.NumeroExterior == "" || usuario.Direccion.NumeroExterior == null)
                {
                    resultValidacion.ErrorMessage += "el numero exterior es muy largo o esta vacio";
                }
                if(usuario.Direccion.Colonia.IdColonia == 0)
                {
                    resultValidacion.ErrorMessage += "el idcolonia esta vacio";
                }
                if (resultValidacion.ErrorMessage != "" || resultValidacion.ErrorMessage != null)
                {
                    result.Objects.Add(resultValidacion.ErrorMessage);
                }
                contador++;

            }

            return result;

        }
        
    }    

}



    
                

        
            
        
           
        
        
    


   


























