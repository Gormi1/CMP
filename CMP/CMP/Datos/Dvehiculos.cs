using CMP.Modelo;
using CMP.Servicios;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace CMP.Datos
{

    internal class Dvehiculos
    {
        public async Task<List<MVehiculos>> ObtenerVehiculos()
        {
            // regresa los datos obtenidos de firebase, primer hace busqueda de la ruta
            return (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Vehiculos")
                .OnceAsync<MVehiculos>())
                .Select(Item => new MVehiculos
                {
                    // esta sección se encarga de tomar los datos de firebase y agregarlos a su
                    // respectiva variable en el modelo
                    IdVehiculo = Item.Key,
                    NumeroEconomico = Item.Object.NumeroEconomico,
                    Modelo = Item.Object.Modelo,
                    Nombre = Item.Object.Nombre,
                    Tipo = Item.Object.Tipo,
                    NumeroDeSerie = Item.Object.NumeroDeSerie,
                    Icono = Item.Object.Tipo == "pesado" ? "BttnTruck.png" : "BttnCar.png",
                    Kilomtraje = Item.Object.Kilomtraje,
                    HoraInicial = Item.Object.HoraInicial,
                    HoraFinal = Item.Object.HoraFinal,
                    HoraDeUso = Item.Object.HoraDeUso,
                    CantLlantas = Item.Object.CantLlantas,
                    TiempoVidaLlantas = Item.Object.TiempoVidaLlantas,
                    DatosExtras = Item.Object.DatosExtras,
                    Observaciones = Item.Object.Observaciones,
                    Estado = Item.Object.Estado,
                    Combustible = Item.Object.Combustible,
                    RendimientoxMes = Item.Object.RendimientoxMes,
                }).ToList(); // regresa los datos como una lista para ser mostrados en una colección
        }


        public async Task InsertarVehiculo(MVehiculos parametro)
        {
            parametro.HoraInicial = 0;
            parametro.HoraFinal = 0;
            int res = parametro.HoraFinal - parametro.HoraInicial;
            parametro.HoraDeUso = res;
            double Rendimiento = parametro.Combustible / res;
            parametro.RendimientoxMes = Rendimiento;
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Vehiculos")
                .Child(parametro.NumeroEconomico)
                .PutAsync(new MVehiculos()
                {
                    IdVehiculo = parametro.IdVehiculo,
                    NumeroEconomico = parametro.NumeroEconomico,
                    NumeroDeSerie = parametro.NumeroDeSerie,
                    Nombre = parametro.Nombre,
                    Modelo = parametro.Modelo,
                    Tipo = parametro.Tipo,
                    Estado = parametro.Estado,
                    CantLlantas = parametro.CantLlantas,
                    TiempoVidaLlantas = parametro.TiempoVidaLlantas,
                    Kilomtraje = parametro.Kilomtraje,
                    HoraInicial = parametro.HoraInicial,
                    HoraFinal = parametro.HoraFinal,
                    HoraDeUso = parametro.HoraDeUso,
                    DatosExtras = parametro.DatosExtras,
                    Observaciones = parametro.Observaciones,
                    Combustible = parametro.Combustible,
                    RendimientoxMes = parametro.RendimientoxMes,
                });
        }
        public async Task EditarVehiculo(MVehiculos parametro)
        {

            //se busca al item
            var data = (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Vehiculos")
                .OnceAsync<MVehiculos>())
                .Where(a => a.Key == parametro.IdVehiculo)
                .FirstOrDefault();

            //se actualizan los datos
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Vehiculos")
                .Child(data.Key)
                .PutAsync(new MVehiculos()
                {
                    NumeroEconomico = parametro.NumeroEconomico,
                    Modelo = parametro.Modelo,
                    Nombre = parametro.Nombre,
                    Tipo = parametro.Tipo,
                    NumeroDeSerie = parametro.NumeroDeSerie,
                    Kilomtraje = parametro.Kilomtraje,
                    HoraDeUso = parametro.HoraDeUso,
                    CantLlantas = parametro.CantLlantas,
                    TiempoVidaLlantas = parametro.TiempoVidaLlantas,
                    DatosExtras = parametro.DatosExtras,
                    Observaciones = parametro.Observaciones,
                    Estado = parametro.Estado,
                    HoraInicial = parametro.HoraInicial,
                    HoraFinal = parametro.HoraFinal,
                    Combustible = parametro.Combustible,
                    RendimientoxMes = parametro.RendimientoxMes,
                });
        }
        public async Task EliminarVehiculo(MVehiculos parametro)
        {
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Vehiculos")
                .Child(parametro.IdVehiculo)
                .DeleteAsync();
        }
    }
}
