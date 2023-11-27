using System;
using System.Collections.Generic;
using System.Text;

namespace CMP.Modelo
{
    class MUsuario
    {
        public int Id { get; set; }
        public string claveDeEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string image {  get; set; }
    }
}
