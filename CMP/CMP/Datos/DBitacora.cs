using CMP.Modelo;
using CMP.Servicios;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
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
            // regresa los datos traidos de la ruta con el agregado que depende de la variable Fecha sacada del modelo
            return (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Bitacora")
                .Child(Fecha) // los servicos guardados en la vitacora estan clasificados por fecha, así que se muestran dependiendo de la fecha seleccionada
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
