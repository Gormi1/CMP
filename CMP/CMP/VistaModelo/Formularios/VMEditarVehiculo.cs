using CMP.Datos;
using CMP.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo.Formularios
{
    internal class VMEditarVehiculo : BaseViewModel
    {
        #region VARIABLES
        public string IdVehiculo { get; set; }
        public string NumeroEconomico { get; set; }
        public string Modelo { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string NumeroDeSerie { get; set; }
        public int Kilometraje { get; set; }
        public int HoraDeUso { get; set; }
        public int CantLlantas { get; set; }
        public int TiempoVidaLlantas { get; set; }
        public string DatosExtras { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
        public MVehiculos Parametrosrecive { get; set; }
        #endregion

        #region CONSTRUCTOR
        public VMEditarVehiculo(INavigation navigation, MVehiculos parametrotrae)
        {
            Navigation = navigation;
            Parametrosrecive = parametrotrae;
            LlenarCampos();
        }

        #endregion

        #region OBJETOS


        #endregion

        #region PROCESOS
        public async Task EditarVehiculo()
        {
            var funcion = new Dvehiculos();
            Parametrosrecive = new MVehiculos
            {
                IdVehiculo = IdVehiculo,
                NumeroEconomico = NumeroEconomico,
                Modelo = Modelo,
                Nombre = Nombre,
                Tipo = Tipo,
                NumeroDeSerie = NumeroDeSerie,
                Kilomtraje = Kilometraje,
                HoraDeUso = HoraDeUso,
                CantLlantas = CantLlantas,
                TiempoVidaLlantas = new List<int>(new int[CantLlantas]),
                DatosExtras = DatosExtras,
                Observaciones = Observaciones,
                Estado = Estado
            };

            await funcion.EditarVehiculo(Parametrosrecive);
            await DisplayAlert("Vehiculo Guardado", "Datos guardados Correctamente", "Aceptar");
            VaciarCampos();
            await RegresarAVehiculos();
        }
        public void LlenarCampos()
        {
            IdVehiculo = Parametrosrecive.IdVehiculo;
            NumeroEconomico = Parametrosrecive.NumeroEconomico;
            Modelo = Parametrosrecive.Modelo;
            Nombre = Parametrosrecive.Nombre;
            Tipo = Parametrosrecive.Tipo;
            NumeroDeSerie = Parametrosrecive.NumeroDeSerie;
            Kilometraje = Parametrosrecive.Kilomtraje;
            HoraDeUso = Parametrosrecive.HoraDeUso;
            CantLlantas = Parametrosrecive.CantLlantas;
            //TiempoVidaLlantas = Parametrosrecive.TiempoVidaLlantas;
            DatosExtras = Parametrosrecive.DatosExtras;
            Observaciones = Parametrosrecive.Observaciones;
            Estado = Parametrosrecive.Estado; 
        }
        public void VaciarCampos()
        {
            IdVehiculo = "";
            NumeroEconomico = "";
            Modelo = "";
            Nombre = "";
            Tipo = "";
            NumeroDeSerie = "";
            Kilometraje = 0;
            HoraDeUso = 0;
            CantLlantas = 0;
            TiempoVidaLlantas = 0;
            DatosExtras = "";
            Observaciones = "";
            Estado = "";
        }
        public async Task RegresarAVehiculos()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region COMANDOS

        public ICommand VolverMenuCommand => new Command(async () => await RegresarAVehiculos());
        public ICommand EditarDatosCommando => new Command(async () => await EditarVehiculo());
        #endregion
    }
}
