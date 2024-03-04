using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Especialidades
    {
        public static Dictionary<string, object> GetAll()
        {
            ML.Especialidad especialidad = new ML.Especialidad();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Especialidad", especialidad }, { "Excepcion", excepcion }, { "Resultado", false } };
            try
            {
                using (DL.SgomezHospital2Context context = new DL.SgomezHospital2Context())
                {
                    var listaEspecialidades = (from tableEspecialidad in context.Especialidads
                                               select new
                                               {
                                                   IdEspecialidad = tableEspecialidad.IdEspecialidad,
                                                   Nombre = tableEspecialidad.Nombre
                                               }).ToList();
                    if (listaEspecialidades.Count > 0)
                    {
                        especialidad.Especialidads = new List<object>();
                        foreach (var registro in listaEspecialidades)
                        {
                            ML.Especialidad especialidades = new ML.Especialidad();
                            especialidades.IdEspecialidad = registro.IdEspecialidad;
                            especialidades.Nombre = registro.Nombre;
                            especialidad.Especialidads.Add(especialidades);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Especialidad"] = especialidad;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
    }
}
