using CMP.Datos;
using CMP.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo.FormServicios
{
    internal class VMEditarServicios : BaseViewModel
    {
        #region VARIABLES
        public string TipoServicio { get; set; }
        public string IdServicios { get; set; }
        public string NumeroEconomico { get; set; }
        public string Fecha { get; set; }
        public string InventarioText { get; set; }
        public MServicios Parametrosrecive { get; set; }

        #endregion

        #region CONSTRUCOR
        public VMEditarServicios(INavigation navigation, MServicios parametrotrae)
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
            var funcion = new DServicios();

            Parametrosrecive.TipoServicio = TipoServicio;
            Parametrosrecive.Inventario = InventarioText;

            await funcion.EditarServicios(Parametrosrecive);
            await DisplayAlert("Vehiculo Guardado", "Datos guardados Correctamente", "Aceptar");
            VaciarCampos();
            await RegresaraMenu();
        }
        public void LlenarCampos()
        {
            DateTime fechaDateTime = DateTime.ParseExact(Parametrosrecive.Fechas, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            string fechaFormateada = fechaDateTime.ToString("MM-dd-yyyy");

            IdServicios = Parametrosrecive.IdServicios;
            NumeroEconomico = Parametrosrecive.NumeroEconomico;
            Fecha = fechaFormateada;
            TipoServicio = Parametrosrecive.TipoServicio;
            InventarioText = Parametrosrecive.Inventario;
        }
        public void VaciarCampos()
        {
            IdServicios = "";
            NumeroEconomico = "";
            Fecha = "";
            TipoServicio = "";
            InventarioText = "";
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
