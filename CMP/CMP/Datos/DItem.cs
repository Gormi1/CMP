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
    internal class DItem
    {
        public async Task<List<MItem>> ObtenerObj()
        {
            return (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Objetos")
                .OnceAsync<MItem>()).Select(Item => new MItem
                {
                    IdItem = Item.Key,
                    NombreItem = Item.Object.NombreItem,
                    Cantidad = Item.Object.Cantidad,
                }).ToList();
        }

        public async Task InsertarVehiculo(MItem parametro)
        {
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Objetos")
                .PostAsync(new MItem()
                {
                    IdItem = parametro.IdItem,
                    NombreItem = parametro.NombreItem,
                    Cantidad = parametro.Cantidad,
                });
        }

        public async Task EditarVehiculo(MItem parametro)
        {
            var data = (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Vehiculos")
                .OnceAsync<MItem>())
                .Where(a => a.Object.IdItem == parametro.IdItem)
                .FirstOrDefault();
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Vehiculos")
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
                .Child("Vehiculos")
                .Child(parametro.IdItem)
                .DeleteAsync();
        }
    }
}
