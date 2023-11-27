using System;
using System.Collections.Generic;
using System.Text;

namespace CMP.Modelo
{
    public class MVehiculos
    {
        public string IdVehiculo { get; set; }
        public string Icono { get; set; }
        public string NumeroEconomico { get; set; }
        public string NumeroDeSerie { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public List<int> TiempoVidaLlantas { get; set; }
        public int Kilomtraje { get; set; }
        public int HoraInicial { get; set; }
        public int HoraFinal { get; set; }
        public int HoraDeUso { get; set; }
        public int  CantLlantas { get; set; }
        public string DatosExtras { get; set; }
        public string Observaciones { get; set; }
        public double Combustible { get; set; }


        // (sumatoria de combustible total) / Horas trabajadas = RendimientoMensual de combustible
        public double RendimientoxMes { get; set; }
    }
}

