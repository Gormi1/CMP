using CMP.Datos;
using CMP.Modelo;
using CMP.Servicios;
using CMP.VistaModelo.Formularios;
using CMP.Vistas.Formularios;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo
{
    public class VMVehiculos : BaseViewModel
    {
        #region VARIABLES
        Dvehiculos funcion = new Dvehiculos();
        List<MVehiculos> _ListaVehiculos;
        public MVehiculos VehiculoData { get; set; }
        public string Nombre { get; set; }
        public string Icono { get; set; }
        public int Kilomtraje { get; set; }
        public string Estado { get; set; }

        #endregion

        #region CONSTRUCTOR
        public VMVehiculos(INavigation navigation)
        {
            Navigation = navigation;
        }

        public VMVehiculos()
        {
        }
        #endregion

        #region OBJETOS

        public List<MVehiculos> ListaVehiculos
        {
            get { return _ListaVehiculos; }
            set { SetValue(ref _ListaVehiculos, value); }
        }
        #endregion

        #region PROCESOS
        string Color(string parametro)
        {
            string color = "";
            if (parametro.Equals("En uso"))
            {
                color = "#9A8200";
            }
            else if (parametro.Equals("Disponible"))
            {
                color = "#35B121";
            }
            else if (parametro.Equals("En servicio"))
            {
                color = "#CA1D27";
            }
            return color;
        }
        public async Task GetVehiculo()
        {
            funcion = new Dvehiculos();
            VehiculoData = new MVehiculos
            {
                Icono = Icono,
                Nombre = Nombre,
                Kilomtraje = Kilomtraje,
                Estado = Estado,
            };

            ListaVehiculos = await funcion.ObtenerVehiculosCacheOInternetAsync();
        }
        public async Task IraAddVehiculo()
        {
            await Navigation.PushAsync(new AddVehiculo());
        }
        public async Task IraDataVehiculo(MVehiculos parametro)
        {
            await Navigation.PushAsync(new DataVehiculo(parametro));
        }
        #endregion


        #region COMANDOS
        public ICommand NavAddVehiculoCommand => new Command(async () => await IraAddVehiculo());
        public ICommand NavDataVehiculoCommand => new Command<MVehiculos>(async (p) => await IraDataVehiculo(p));
        public ICommand ColorCommand => new Command<MVehiculos>((p) => Color(p.Estado));
        #endregion
    }
}
