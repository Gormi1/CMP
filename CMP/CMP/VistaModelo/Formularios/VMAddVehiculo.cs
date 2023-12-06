using CMP.Datos;
using CMP.Modelo;
using CMP.Servicios;
using CMP.Vistas;
using CMP.Vistas.Formularios;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo.Formularios
{
    public class VMAddVehiculo : BaseViewModel
    {
        #region VARIABLES
        public string NumeroEconomico { get; set; }
        public string Modelo { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string NumeroDeSerie { get; set; }
        public int Kilometraje { get; set; }
        public int HoraInicial { get; set; }
        public int HoraFinal { get; set; }
        public int CantLlantas { get; set; }
        public string DatosExtras { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
        public double Combustible {  get; set; }

        #endregion

        #region CONSTRUCTOR
        public VMAddVehiculo(INavigation navigation)
        {
            Navigation = navigation;
        }


        #endregion

        #region OBJETOS

        #endregion

        #region PROCESOS
        //procesos en 2ndo plano
        public async Task InsertarVehiculo()
        {
            if (string.IsNullOrEmpty(NumeroEconomico) ||
                string.IsNullOrEmpty(NumeroDeSerie) ||
                string.IsNullOrEmpty(Nombre) ||
                string.IsNullOrEmpty(Modelo) ||
                string.IsNullOrEmpty(Tipo) ||
                string.IsNullOrEmpty(Estado) ||
                CantLlantas == 0 ||
                string.IsNullOrEmpty(DatosExtras) ||
                string.IsNullOrEmpty(Observaciones))
            {

                await DisplayAlert("Error", "Llene todos los datos", "Aceptar");
                VaciarCampos();
            }
            else
            {
                var funcion = new Dvehiculos();
                var datos = new MVehiculos
                {
                    NumeroEconomico = NumeroEconomico,
                    NumeroDeSerie = NumeroDeSerie,
                    Nombre = Nombre,
                    Modelo = Modelo,
                    Tipo = Tipo,
                    Estado = Estado,
                    CantLlantas = CantLlantas,
                    TiempoVidaLlantas = new List<int>(new int[CantLlantas]),                   
                    Kilomtraje = Kilometraje,
                    HoraInicial = HoraInicial,
                    HoraFinal = HoraFinal,
                    DatosExtras = DatosExtras,
                    Observaciones = Observaciones,
                    Combustible = Combustible,
                };

                await funcion.InsertarVehiculo(datos);
                await DisplayAlert("Vehiculo Guardado", "Datos guardados Correctamente", "Aceptar");
                VaciarCampos();
                await RegresarAMenu();
            }
        }

        public void VaciarCampos()
        {
            NumeroEconomico = "";
            Modelo = "";
            Nombre = "";
            Tipo = "";
            NumeroDeSerie = "";
            DatosExtras = "";
            Observaciones = "";
            Estado = "";
        }
        public async Task RegresarAVehiculos()
        {
            await Navigation.PopAsync();
        }

        public async Task RegresarAMenu()
        {
            await Navigation.PopToRootAsync();
        }
        #endregion

        #region COMANDOS
        public ICommand VolverMenuCommand => new Command(async () => await RegresarAVehiculos());
        public ICommand GuardarDatosCommando => new Command(async () => await InsertarVehiculo());
        #endregion
    }
}
