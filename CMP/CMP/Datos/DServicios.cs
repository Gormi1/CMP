using CMP.Modelo;
using CMP.Servicios;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

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
                .Select(servicioNode => new MServicios
                {
                    IdServicios = servicioNode.Key,
                    NumeroEconomico = servicioNode.Object.NumeroEconomico,
                    Fechas = servicioNode.Object.Fechas,
                    Icono = servicioNode.Object.Icono,
                    Inventario = servicioNode.Object.Inventario,
                    TipoServicio = servicioNode.Object.TipoServicio,
                })
                .ToList();
        }

        public async Task InsertarServicios(MServicios parametro)
        {
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Servicios")
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
                .OnceAsync<MServicios>())
                .Where(a => a.Key == parametro.IdServicios)
                .FirstOrDefault();

            //se actualizan los datos
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Servicios")
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
                .Child(parametro.IdServicios)
                .DeleteAsync();
        }

    }
}

