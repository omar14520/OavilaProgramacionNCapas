using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.OAvilaProgramacionencapasEntities context = new DL_EF.OAvilaProgramacionencapasEntities())
                {
                    var query = context.ColoniaGetByIdMunicipio(IdMunicipio).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var item in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = item.IdColonia;
                            colonia.Nombre = item.Nombre;
                            colonia.CodigoPostal = item.CodigoPostal;
                    
                            result.Objects.Add(colonia);
                        }
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
    }
}

