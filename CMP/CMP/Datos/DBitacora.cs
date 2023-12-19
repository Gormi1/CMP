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
    class DBitacora
    {
        public async Task InsertarServicios(MServicios parametro)
        {
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Bitacora")
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
        public async Task<List<MServicios>> ObtenerServiciosxfecha(string Fecha)
        {
            return (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Bitacora")
                .Child(Fecha)
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


    }
}
