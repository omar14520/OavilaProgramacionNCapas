using ML;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PL
{
    public class Usuario
    {
        public static void Add() //logica para pedir la informacion
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("ingresa el username:");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("ingresa el nombre:");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("ingresa el apeliidopaterno:");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("ingresa el apellidomaterno:");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("ingresa el email:");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("ingresa el password:");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("ingresa el fechanacimiento:");
            usuario.FechaNacimiento = Console.ReadLine();
            Console.WriteLine("ingresa el sexo:");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("ingresa el celular:");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("ingresa el telefono:");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("ingresa el estatus:");
            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("ingresa el curp:");
            usuario.Curp = Console.ReadLine();
            //console.writeline("ingresa el imagen:");
            //usuario.imagen = console.readline();
            Console.WriteLine("ingresa el idrol:");
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());



            BL.Usuario.AddEF(usuario);
        }

        public static void Delete() //logica para pedir la informacion
        {
            Console.WriteLine("Ingresa el Id del usuario:");

            int IdUsuario = Convert.ToInt32(Console.ReadLine());


            BL.Usuario.DeleteEF(IdUsuario);
        }

        public static void Update() //logica para pedir la informacion
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingresa el IdUsuario:");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingresa el username:");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("Ingresa el nombre:");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingresa el apeliidopaterno:");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("Ingresa el apellidomaterno:");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Ingresa el email:");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingresa el password:");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("Ingresa el fechanacimiento:");
            usuario.FechaNacimiento = Console.ReadLine();
            Console.WriteLine("Ingresa el sexo:");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("Ingresa el celular:");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("Ingresa el telefono:");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("Ingresa el estatus:");
            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Ingresa el curp:");
            usuario.Curp = Console.ReadLine();
            //Console.WriteLine("Ingresa el imagen:");
            //usuario.Imagen = Console.ReadLine();
            Console.WriteLine("Ingresa el idrol:");
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

            BL.Usuario.UpdateLINQ(usuario);

        }
        //public static void GetAll() //logica para pedir la informacion
        //{

        //    ML.Result result = BL.Usuario.GetAllEF();

        //    if (result.Correct)
        //    {
        //        //mostrar los registros
        //        foreach (ML.Usuario usuario in result.Objects)
        //        {
        //            Console.WriteLine(usuario.IdUsuario);
        //            Console.WriteLine(usuario.UserName);
        //            Console.WriteLine(usuario.Nombre);
        //            Console.WriteLine(usuario.ApellidoPaterno);
        //            Console.WriteLine(usuario.ApellidoPaterno);
        //            Console.WriteLine(usuario.Email);
        //            Console.WriteLine(usuario.Password);
        //            Console.WriteLine(usuario.FechaNacimiento);
        //            Console.WriteLine(usuario.Sexo);
        //            Console.WriteLine(usuario.Telefono);
        //            Console.WriteLine(usuario.Celular);
        //            Console.WriteLine(usuario.Estatus);
        //            Console.WriteLine(usuario.Curp);
        //            Console.WriteLine(usuario.Rol.IdRol);


        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Hubo un error " + result.ErrorMessage);
        //    }
        //}
        public static void GetById() //logica para pedir la informacion
        {
            Console.WriteLine("Dame el id que quieres buscar:");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);

            if (result.Correct)
            {
                //mostrar los registros
                ML.Usuario usuario = (ML.Usuario)result.Object;

                Console.WriteLine(usuario.IdUsuario);
                Console.WriteLine(usuario.UserName);
                Console.WriteLine(usuario.ApellidoPaterno);
                Console.WriteLine(usuario.ApellidoMaterno);
                Console.WriteLine(usuario.Email);
                Console.WriteLine(usuario.Password);
                Console.WriteLine(usuario.FechaNacimiento);
                Console.WriteLine(usuario.Sexo);
                Console.WriteLine(usuario.Celular);
                Console.WriteLine(usuario.Telefono);
                Console.WriteLine(usuario.Estatus);
                Console.WriteLine(usuario.Curp);
                Console.WriteLine(usuario.Rol.IdRol);
            }
            else
            {
                Console.WriteLine("Hubo un error " + result.ErrorMessage);
            }
        }
    }
}

