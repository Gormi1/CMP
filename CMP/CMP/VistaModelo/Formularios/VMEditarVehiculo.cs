using CMP.Datos;
using CMP.Modelo;
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
        public int HoraInicial { get; set; }
        public int HoraFinal { get; set; }
        public int CantLlantas { get; set; }
        public string DatosExtras { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
        public double Combustible { get; set; }
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
            //objeto DVehiculos
            var funcion = new Dvehiculos();

            //guarda los datos modificados en el estado
            Parametrosrecive.Estado = Estado;
            Parametrosrecive.DatosExtras = DatosExtras;
            Parametrosrecive.Observaciones = Observaciones;
            Parametrosrecive.HoraInicial += HoraFinal;
            Parametrosrecive.HoraFinal = HoraFinal;
            Parametrosrecive.Kilomtraje += Kilometraje;
            Parametrosrecive.Combustible += Combustible;
            //realiza calculos necesarios sobre la información del vehiculo
            int res = Parametrosrecive.HoraInicial - Parametrosrecive.HoraFinal;
            Parametrosrecive.HoraDeUso += Parametrosrecive.HoraInicial;
            double Rendimiento = Parametrosrecive.Combustible / res;
            Parametrosrecive.RendimientoxMes = Rendimiento;
            //se guardan los datos modirficados y se mandan a Firebase
            await funcion.EditarVehiculo(Parametrosrecive);
            await DisplayAlert("Vehiculo Guardado", "Datos guardados Correctamente", "Aceptar");
            VaciarCampos();
            await RegresaraMenu();
        }
        public void LlenarCampos()
        {
            IdVehiculo = Parametrosrecive.IdVehiculo;
            NumeroEconomico = Parametrosrecive.NumeroEconomico;
            Modelo = Parametrosrecive.Modelo;
            Nombre = Parametrosrecive.Nombre;
            Tipo = Parametrosrecive.Tipo;
            NumeroDeSerie = Parametrosrecive.NumeroDeSerie;
            CantLlantas = Parametrosrecive.CantLlantas;
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
            DatosExtras = "";
            Observaciones = "";
            Estado = "";
        }
        public async Task RegresaraMenu()
        {
            await Navigation.PopToRootAsync();
        }
        public async Task RegresarAVehiculos()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region COMANDOS

        public ICommand VolverdetallesCommand => new Command(async () => await RegresarAVehiculos());
        public ICommand VolverMenuCommand => new Command(async () => await RegresaraMenu());
        public ICommand EditarDatosCommando => new Command(async () => await EditarVehiculo());
        #endregion
    }
}
