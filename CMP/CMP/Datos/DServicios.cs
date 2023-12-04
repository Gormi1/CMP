using CMP.Modelo;
using CMP.Servicios;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
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
                .PostAsync(new MServicios()
                {
                    IdServicios = parametro.IdServicios,
                    NumeroEconomico = parametro.NumeroEconomico,
                    Fechas = parametro.Fechas,
                    Icono = parametro.Icono,
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

            //se actualizan los d
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
