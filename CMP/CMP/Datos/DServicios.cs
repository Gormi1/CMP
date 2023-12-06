using CMP.Modelo;
using CMP.Servicios;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP.Datos
{
    internal class DServicios
    {
        public async Task<List<MServicios>> ObtenerServicios()
        {
            return (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Servicios")
                .OnceAsync<MServicios>())
                .Select(Item => new MServicios
                {
                    IdServicios = Item.Key,
                    NumeroEconomico = Item.Object.NumeroEconomico,
                    Fechas = Item.Object.Fechas,
                    Icono = Item.Object.Icono,
                    Inventario = Item.Object.Inventario,
                    TipoServicio = Item.Object.TipoServicio,
                }).ToList();
        }
        public async Task<List<MServicios>> ObtenerServiciosxIdPersonalizada()
        {
            var result = new List<MServicios>();

            try
            {
                var serviciosPorFecha = (await ConexionFirebase.FBCliente
                    .Child("Servicios")
                    .Child("Servicios")
                    .OnceAsync<Dictionary<string, MServicios>>())
                    .SelectMany(fechaNode => fechaNode.Object.Select(servicioNode => new MServicios
                    {
                        IdServicios = servicioNode.Key,
                        NumeroEconomico = servicioNode.Value.NumeroEconomico,
                        Fechas = servicioNode.Value.Fechas,
                        Icono = servicioNode.Value.Icono,
                        Inventario = servicioNode.Value.Inventario,
                        TipoServicio = servicioNode.Value.TipoServicio,
                    }))
                    .ToList();

                result.AddRange(serviciosPorFecha);
            }
            catch (Exception ex)
            {
                // Maneja la excepción de manera adecuada (por ejemplo, registrándola o lanzándola nuevamente)
                Console.WriteLine($"Error al obtener servicios: {ex.Message}");
                throw;
            }

            return result;
        }
        public async Task<List<MServicios>> ObtenerServiciosRecientesOFuturos()
        {
            var result = new List<MServicios>();

            try
            {
                var serviciosPorFecha = (await ConexionFirebase.FBCliente
                    .Child("Servicios")
                    .Child("Servicios")
                    .OnceAsync<Dictionary<string, MServicios>>())
                    .SelectMany(fechaNode => fechaNode.Object.Select(servicioNode => new MServicios
                    {
                        IdServicios = servicioNode.Key,
                        NumeroEconomico = servicioNode.Value.NumeroEconomico,
                        Fechas = servicioNode.Value.Fechas,
                        Icono = servicioNode.Value.Icono,
                        Inventario = servicioNode.Value.Inventario,
                        TipoServicio = servicioNode.Value.TipoServicio,
                    }))
                    .ToList();

                // Filtrar solo los servicios con fechas recientes o futuras
                result.AddRange(serviciosPorFecha
                    .Where(servicio => DateTime.TryParseExact(servicio.Fechas, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                                       && DateTime.ParseExact(servicio.Fechas, "dd-MM-yyyy", CultureInfo.InvariantCulture) >= DateTime.Today));


            }
            catch (Exception ex)
            {
                // Maneja la excepción de manera adecuada (por ejemplo, registrándola o lanzándola nuevamente)
                Console.WriteLine($"Error al obtener servicios: {ex.Message}");
                throw;
            }

            return result;
        }
        public async Task<List<MServicios>> MostrarServiciosxIdServicios(string IdServicios)
        {
            return (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Servicios")
                .OnceAsync<MServicios>())
                .Where(a => a.Object.IdServicios == IdServicios).Select(Item => new MServicios
                {
                    NumeroEconomico = Item.Object.NumeroEconomico
                }).ToList();
        }

        public async Task<List<MServicios>> ObtenerServiciosxId(string IdServicios)
        {
            return (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Servicios")
                .OnceAsync<MServicios>())
                .Where(a => a.Object.IdServicios == IdServicios).Select(Item => new MServicios
                {
                    IdServicios = Item.Key,
                    NumeroEconomico = Item.Object.NumeroEconomico,
                    Fechas = Item.Object.Fechas,
                    Icono = Item.Object.Icono,
                    Inventario = Item.Object.Inventario,
                    TipoServicio = Item.Object.TipoServicio,
                }).ToList();
        }
        public async Task InsertarServicios(MServicios parametro)
        {
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Servicios")
                .Child(parametro.Fechas)
                .PostAsync(new MServicios()
                {
                    IdServicios = parametro.IdServicios,
                    NumeroEconomico = parametro.NumeroEconomico,
                    Fechas = parametro.Fechas,
                    Inventario = parametro.Inventario,
                    TipoServicio = parametro.TipoServicio,
                });
        }
        public async Task EditarServicios(MServicios parametro)
        {

            //se busca al item
            var data = (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Servicios")
                .Child(parametro.Fechas)
                .OnceAsync<MServicios>())
                .Where(a => a.Key == parametro.IdServicios)
                .FirstOrDefault();

            //se actualizan los d
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Servicios")
                .Child(parametro.Fechas)
                .Child(data.Key)
                .PutAsync(new MServicios()
                {
                    NumeroEconomico = parametro.NumeroEconomico,
                    Fechas = parametro.Fechas,
                    Icono = parametro.Icono,
                    Inventario = parametro.Inventario,
                    TipoServicio = parametro.TipoServicio,
                });
        }
        public async Task EliminarServicios(MServicios parametro)
        {
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Servicios")
                .Child(parametro.Fechas) // Utiliza la clave de la fecha
                .Child(parametro.IdServicios) // Utiliza la clave del servicio dentro de la fecha
                .DeleteAsync();
        }

    }
}
