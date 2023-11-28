﻿using CMP.Modelo;
using CMP.Servicios;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace CMP.Datos
{

    internal class Dvehiculos
    {
        public async Task<List<MVehiculos>> ObtenerVehiculos()
        {
            return (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Vehiculos")
                .OnceAsync<MVehiculos>())
                .Select(Item => new MVehiculos
                {
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
                }).ToList();
        }
                

        public async Task InsertarVehiculo(MVehiculos parametro)
        {
            int res = parametro.HoraFinal - parametro.HoraInicial;
            parametro.HoraDeUso = res;
            double Rendimiento = parametro.Combustible/res;
            parametro.RendimientoxMes = Rendimiento;
            await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Vehiculos")
                .PostAsync(new MVehiculos()
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
        public async Task<List<MVehiculos>> MostrarVehiculosxIdVehiculo(string IdVehiculo)
        {
            return (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Vehiculos")
                .OnceAsync<MVehiculos>())
                .Where(a => a.Object.IdVehiculo == IdVehiculo).Select(Item => new MVehiculos
                {
                    NumeroEconomico = Item.Object.NumeroEconomico
                }).ToList();
        }

        public async Task<List<MVehiculos>> ObtenerVehiculoxId(string IdVehiculo)
        {
            return (await ConexionFirebase.FBCliente
                .Child("Servicios")
                .Child("Vehiculos")
                .OnceAsync<MVehiculos>())
                .Where(a => a.Object.IdVehiculo == IdVehiculo).Select(Item => new MVehiculos
                {
                    IdVehiculo = Item.Key,
                    NumeroEconomico = Item.Object.NumeroEconomico,
                    NumeroDeSerie = Item.Object.NumeroDeSerie,
                    Nombre = Item.Object.Nombre,
                    Modelo = Item.Object.Modelo,
                    Tipo = Item.Object.Tipo,
                    Estado = Item.Object.Estado,
                    CantLlantas = Item.Object.CantLlantas,
                    TiempoVidaLlantas = Item.Object.TiempoVidaLlantas,
                    Kilomtraje = Item.Object.Kilomtraje,
                    HoraInicial = Item.Object.HoraInicial,
                    HoraFinal = Item.Object.HoraFinal,
                    HoraDeUso = Item.Object.HoraDeUso,
                    DatosExtras = Item.Object.DatosExtras,
                    Observaciones = Item.Object.Observaciones,
                    Combustible = Item.Object.Combustible,
                    RendimientoxMes = Item.Object.RendimientoxMes,
                }).ToList();
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

            //se actualizan los d
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
                    Estado = parametro.Estado
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
