using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Hospital
    {
        public static Dictionary<string, object> Add(ML.Hospital hospital)
        {
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Excepcion", excepcion }, { "Resultado", false } };
            try
            {
                using (DL.SgomezHospital2Context context = new DL.SgomezHospital2Context())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"HospitalAdd '{hospital.Nombre}', '{hospital.Direccion}', '{hospital.AñoDeConstruccion}', {hospital.Capacidad}, {hospital.Especialidad.IdEspecialidad}");
                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
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
        public static Dictionary<string, object> GetAll()
        {
            ML.Hospital hospital = new ML.Hospital();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Hospital", hospital }, { "Excepcion", excepcion }, { "Resultado", false } };
            try
            {
                using (DL.SgomezHospital2Context context = new DL.SgomezHospital2Context())
                {
                    var Usuarios = (from tableHospital in context.Hospitals
                                         join tableEspecialidad in context.Especialidads on tableHospital.IdEspecialidad equals tableEspecialidad.IdEspecialidad
                                         select new
                                         {
                                             IdHospital = tableHospital.IdHospital,
                                             Nombre = tableHospital.Nombre,
                                             Direccion = tableHospital.Direccion,
                                             AñoDeConstruccion = tableHospital.AñoDeConstruccion,
                                             Capacidad = tableHospital.Capacidad,
                                             IdEspecialidad = tableEspecialidad.IdEspecialidad,
                                             TipoEspecialidad = tableEspecialidad.Nombre
                                         }).ToList();
                    if (Usuarios.Count > 0)
                    {
                        hospital.Hospitals = new List<object>();
                        foreach (var registro in Usuarios)
                        {
                            ML.Hospital hospital1 = new ML.Hospital();
                            hospital1.IdHospital = registro.IdHospital;
                            hospital1.Nombre = registro.Nombre;
                            hospital1.Direccion = registro.Direccion;
                            hospital1.AñoDeConstruccion = (int)registro.AñoDeConstruccion;
                            hospital1.Capacidad = (int)registro.Capacidad;
                            hospital1.Especialidad = new ML.Especialidad();
                            hospital1.Especialidad.IdEspecialidad = registro.IdEspecialidad;
                            hospital1.Especialidad.Nombre = registro.TipoEspecialidad;
                            hospital.Hospitals.Add(hospital1);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Hospital"] = hospital;
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
        public static Dictionary<string, object> GetById(int IdHospital)
        {
            ML.Hospital hospital = new ML.Hospital();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Hospital", hospital }, { "Excepcion", excepcion }, { "Resultado", false } };
            try
            {
                using (DL.SgomezHospital2Context context = new DL.SgomezHospital2Context())
                {
                    var objHospital = (from Hospital in context.Hospitals
                                       join tableEspecialidad in context.Especialidads on hospital.IdEspecialidad equals tableEspecialidad.IdEspecialidad
                                       where Hospital.IdHospital == IdHospital
                                       select new
                                       {
                                           IdHospital = Hospital.IdHospital,
                                           Nombre = Hospital.Nombre,
                                           Direccion = Hospital.Direccion,
                                           AñoDeConstruccion =Hospital.AñoDeConstruccion,
                                           Capacidad = Hospital.Capacidad,
                                           IdEspecialidad = tableEspecialidad.IdEspecialidad,
                                           TipoEspecialidad = tableEspecialidad.Nombre
                                       }).FirstOrDefault();
                    if (objHospital != null)
                    {
                        hospital.IdHospital = objHospital.IdHospital;
                        hospital.Nombre = objHospital.Nombre;
                        hospital.Direccion = objHospital.Direccion;
                        hospital.AñoDeConstruccion = (int)objHospital.AñoDeConstruccion;
                        hospital.Capacidad = (int)objHospital.Capacidad;
                        hospital.Especialidad = new ML.Especialidad();
                        hospital.Especialidad.IdEspecialidad = objHospital.IdEspecialidad;
                        hospital.Especialidad.Nombre = objHospital.TipoEspecialidad;
                        diccionario["Resultado"] = true;
                        diccionario["Hospital"] = hospital;
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
        public static Dictionary<string, object> Delete(int IdHospital)
        {
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Excepcion", excepcion } };
            try
            {
                using (DL.SgomezHospital2Context context = new DL.SgomezHospital2Context())
                {
                    int filasAfectadas = context.Database.ExecuteSql($"HospitalDelete {IdHospital}");
                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
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
                diccionario["Execepcion"] = ex.Message;
            }
            return diccionario;
        }

    }
}



//    public  class Hospital
//    {
//        //LinQ
//        public static Dictionary<string, object> GetAll()
//        {
//            ML.Hospital hospital = new ML.Hospital();
//            string exepcion = "";
//            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Excepcion", exepcion }, { "Hospital", hospital } };  

//            try
//            {
//                using(DL.SgomezHospital2Context context = new DL.SgomezHospital2Context())
//                {
//                    var query = (from Hospital in context.Hospitals
//                                 join Especialidad in context.Especialidads on Hospital.IdEspecialidad equals Especialidad.IdEspecialidad
//                                 select new
//                                 {
//                                     IdHospital = Hospital.IdHospital,
//                                     Nombre = Hospital.Nombre,
//                                     Direccion = Hospital.Direccion,
//                                     AñoDeConstruccion = Hospital.AñoDeConstruccion,
//                                     Capacidad = Hospital.Capacidad,
//                                     IdEspecialidad = Hospital.IdEspecialidad,
//                                 }).ToList();

//                    if(query != null)
//                    {
//                        hospital.Hospitals = new List<ML.Hospital>();
//                        foreach (var registro in query)
//                        {
//                            ML.Hospital hospital1 = new ML.Hospital();

//                            hospital1.IdHospital= registro.IdHospital;
//                            hospital1.Nombre = registro.Nombre;
//                            hospital1.Direccion = registro.Direccion;
//                            hospital1.AñoDeConstruccion = (int)registro.AñoDeConstruccion;
//                            hospital1.Capacidad = (int)registro.Capacidad;
//                            hospital1.Especialidad = new ML.Especialidad();
//                            hospital1.Especialidad.IdEspecialidad = (int)registro.IdEspecialidad;

//                            hospital.Hospitals.Add(hospital1);
//                        }

//                        diccionario["Resultado"] = true;
//                    }
//                }
//            }
//            catch(Exception ex)
//            { 
//            }
//            return diccionario;
//        }

//        public static bool DeleteLinQ(int IdHospital)
//        {
//            bool result = false;
//            try
//            {
//                using (DL.SgomezHospital2Context context = new DL.SgomezHospital2Context())
//                {
//                    var query = (from a in context.Hospitals
//                                 where a.IdHospital == IdHospital
//                                 select a).First();

//                    context.Hospitals.Remove(query);
//                    context.SaveChanges();

//                    int filasAfectadas = context.Hospitals.Count();
//                    if (filasAfectadas != null)
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                return result = false;
//            }
//            return result;
//        }
//    }
//}
