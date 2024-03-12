using CMP.Datos;
using CMP.Modelo;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo.FormServicios
{
    public class VMEditarServicios : BaseViewModel
    {
        #region VARIABLES
        public string TipoServicio { get; set; }
        public string IdServicios { get; set; }
        public string NumeroEconomico { get; set; }
        public string Fecha { get; set; }
        public string InventarioText { get; set; }
        public MServicios Parametrosrecive { get; set; }
        #endregion

        #region CONSTRUCTOR
        // Constructor que recibe parámetros y asigna la navegación y los parámetros recibidos
        public VMEditarServicios(INavigation navigation, MServicios parametrotrae)
        {
            Navigation = navigation;
            Parametrosrecive = parametrotrae;
            LlenarCampos(); // Llena los campos con los datos recibidos
        }
        #endregion

        #region PROCESOS
        // Método para realizar la edición del vehículo
        public async Task EditarServicios()
        {
            var funcion = new DServicios();

            // Asigna los nuevos valores al objeto MServicios
            Parametrosrecive.TipoServicio = TipoServicio;
            Parametrosrecive.Inventario = InventarioText;

            // Llama al método de edición de servicios en la capa de datos
            await funcion.EditarServicios(Parametrosrecive);

            // Muestra un mensaje de éxito
            await DisplayAlert("Vehiculo Guardado", "Datos guardados Correctamente", "Aceptar");

            VaciarCampos(); // Limpia los campos después de la edición
            await RegresaraMenu(); // Regresa al menú principal
        }

        // Método para llenar los campos con los datos recibidos
        public void LlenarCampos()
        {
            // Convierte la fecha de string a DateTime y la formatea
            DateTime fechaDateTime = DateTime.ParseExact(Parametrosrecive.Fechas, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            string fechaFormateada = fechaDateTime.ToString("MM-dd-yyyy");

            // Asigna los valores a las propiedades
            IdServicios = Parametrosrecive.IdServicios;
            NumeroEconomico = Parametrosrecive.NumeroEconomico;
            Fecha = fechaFormateada;
            TipoServicio = Parametrosrecive.TipoServicio;
            InventarioText = Parametrosrecive.Inventario;
        }

        // Método para vaciar los campos
        public void VaciarCampos()
        {
            IdServicios = "";
            NumeroEconomico = "";
            Fecha = "";
            TipoServicio = "";
            InventarioText = "";
        }

        // Método para regresar al menú principal
        public async Task RegresaraMenu()
        {
            await Navigation.PopToRootAsync();
        }

        // Método para regresar a la pantalla de detalles del vehículo
        public async Task RegresarAVehiculos()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region COMANDOS
        // Comando para volver a la pantalla de detalles del vehículo
        public ICommand VolverdetallesCommand => new Command(async () => await RegresarAVehiculos());

        // Comando para volver al menú principal
        public ICommand VolverMenuCommand => new Command(async () => await RegresaraMenu());

        // Comando para editar los datos del vehículo
        public ICommand EditarDatosCommando => new Command(async () => await EditarServicios());
        #endregion
    }
}
