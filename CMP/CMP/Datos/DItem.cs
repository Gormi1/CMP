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
    internal class DItem
    {
        public async Task<List<MItem>> ObtenerObj()
        {
            return (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Inventario")
                .Child("Aceites")
                .OnceAsync<MItem>()).Select(Item => new MItem
                {
                    IdItem = Item.Key,
                    NombreItem = Item.Object.NombreItem,
                    Cantidad = Item.Object.Cantidad,
                }).ToList();
        }
        public async Task<List<MItem>> ObtenerObj2()
        {
            return (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Inventario")
                .Child("Filtros")
                .OnceAsync<MItem>()).Select(Item => new MItem
                {
                    IdItem = Item.Key,
                    NombreItem = Item.Object.NombreItem,
                    Cantidad = Item.Object.Cantidad,
                }).ToList();
        }

        public async Task InsertarItem(MItem parametro)
        {
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Servicio")
                .Child(parametro.NombreItem)
                .PostAsync(new MItem()
                {
                    Cantidad = parametro.Cantidad,
                });
        }

        public async Task EditarVehiculo(MItem parametro)
        {
            var data = (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Inventario")
                .OnceAsync<MItem>())
                .Where(a => a.Object.IdItem == parametro.IdItem)
                .FirstOrDefault();
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Inventario")
                .Child(data.Key)
                .PutAsync(new MItem()
                {
                    IdItem = parametro.IdItem,
                    NombreItem = parametro.NombreItem,
                    Cantidad = parametro.Cantidad,
                });
        }

        public async Task EliminarVehiculo(MItem parametro)
        {
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Inventario")
                .Child(parametro.IdItem)
                .DeleteAsync();
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
                    .SelectMany(fechaNode => fechaNode.Object
                    .Select(servicioNode => new MServicios
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
                var a = serviciosPorFecha.Where(servicio => DateTime.TryParseExact(servicio.Fechas, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                                       && DateTime.ParseExact(servicio.Fechas, "dd-MM-yyyy", CultureInfo.InvariantCulture) >= DateTime.Today);

                result.AddRange(a);
            }
            catch (Exception ex)
            {
                // Maneja la excepción de manera adecuada (por ejemplo, registrándola o lanzándola nuevamente)
                Console.WriteLine($"Error al obtener servicios: {ex.Message}");
                throw;
            }

            return result;
        }
    }
}
