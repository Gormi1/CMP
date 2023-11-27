using CMP.Vistas;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMP.Modelo
{
    class MServicios
    {
        public MVehiculos vehiculos { get; set; }
        public MFecha Fechas {  get; set; }
        public string TipoServicio { get; set; }
        public MItem Objeto {  get; set; }
    }
}
