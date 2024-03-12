using CMP.Datos;
using CMP.Modelo;
using CMP.Vistas;
using CMP.Vistas.FormServicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo
{
    internal class VMCalendario : BaseViewModel
    {
        #region VARIABLES
        private List<MServicios> _Lista; // Lista de servicios para una fecha específica
        private DateTime _Fecha; // Fecha seleccionada en el calendario
        private string _ResultadoFecha; // Representación formateada de la fecha
        public MServicios Servicios { get; set; } // Objeto de tipo MServicios
        #endregion

        #region CONSTRUCTOR
        // Constructor que recibe la navegación y establece la fecha actual
        public VMCalendario(INavigation navigation)
        {
            Navigation = navigation;
            Fecha = DateTime.Now;
        }
        #endregion

        #region OBJETOS
        // Propiedad para la fecha, actualizando el resultado formateado al cambiar la fecha
        public DateTime Fecha
        {
            get { return _Fecha; }
            set
            {
                SetValue(ref _Fecha, value);
                ResultadoFecha = _Fecha.ToString("dd-MM-yyyy");
            }
        }

        // Propiedad para la representación formateada de la fecha
        public string ResultadoFecha
        {
            get { return _ResultadoFecha; }
            set { SetValue(ref _ResultadoFecha, value); }
        }

        // Lista de servicios para una fecha específica
        public List<MServicios> ListaFecha
        {
            get { return _Lista; }
            set { SetValue(ref _Lista, value); }
        }
        #endregion

        #region PROCESOS
        // Método para regresar al menú principal
        public async Task RegresarAMenu()
        {
            await Navigation.PopAsync();
        }

        // Método para obtener servicios para la fecha seleccionada
        public async Task<List<MServicios>> ObtenerFechas()
        {
            var funcion = new DBitacora();

            // Llama a la capa de datos para obtener servicios para la fecha seleccionada
            ListaFecha = await funcion.ObtenerServiciosxfecha(ResultadoFecha);

            Console.Write($"resultados: {ListaFecha}");

            return ListaFecha;
        }

        // Método para navegar a la pantalla de detalles de la bitácora
        public async Task IraDataBitacora(MServicios parametro)
        {
            await Navigation.PushAsync(new DataBitacora(parametro));
        }
        #endregion

        #region COMANDOS
        // Comando para volver al menú principal
        public ICommand VolverMenuCommand => new Command(async () => await RegresarAMenu());

        // Comando para navegar a la pantalla de detalles de la bitácora con un parámetro
        public ICommand NavDataBitacoraCommand => new Command<MServicios>(async (p) => await IraDataBitacora(p));

        // Comando para buscar servicios para la fecha seleccionada
        public ICommand BuscarFechaCommand => new Command(async () => await ObtenerFechas());
        #endregion
    }
}
