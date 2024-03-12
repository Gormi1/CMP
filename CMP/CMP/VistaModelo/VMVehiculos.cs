using CMP.Datos;
using CMP.Modelo;
using CMP.Vistas.Formularios;
using System.Collections.Generic;
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
        public async Task GetVehiculo()
        {
            //objeto DVehiculos
            funcion = new Dvehiculos();
            //guarda los datos del modelo y de firebase en ListaVehiculos el cual esta vunculada al source(de donde viene) del CollectionView
            ListaVehiculos = await funcion.ObtenerVehiculos();
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
        #endregion
    }
}
