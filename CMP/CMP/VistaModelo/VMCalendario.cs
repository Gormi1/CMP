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
using XamForms.Controls;

namespace CMP.VistaModelo
{
    internal class VMCalendario : BaseViewModel
    {
        #region VARIABLES
        private List<MServicios> _Lista;
        private DateTime _Fecha;
        private string _ResultadoFecha;
        public MServicios Servicios { get; set; }
        #endregion

        #region CONSTRUCTOR
        public VMCalendario(INavigation navigation)
        {
            Navigation = navigation;
            Fecha = DateTime.Now;
        }
        #endregion

        #region OBJETOS

        public DateTime Fecha
        {
            get { return _Fecha; }
            set
            {
                SetValue(ref _Fecha, value);
                ResultadoFecha = _Fecha.ToString("dd-MM-yyyy");
            }
        }
        public string ResultadoFecha
        {
            get { return _ResultadoFecha; }
            set { SetValue(ref _ResultadoFecha, value); }
        }
        public List<MServicios> ListaFecha
        {
            get { return _Lista; }
            set { SetValue(ref _Lista, value); }
        }
        #endregion

        #region PROCESOS
        public async Task RegresarAMenu()
        {
            await Navigation.PopAsync();
        }

        public async Task<List<MServicios>> ObtenerFechas()
        {
            var funcion = new DServicios();

            ListaFecha = await funcion.ObtenerServiciosxfecha(ResultadoFecha);

            Console.Write($"resultados: {ListaFecha}");

            return ListaFecha;

        }

        public async Task IraDataServicio(MServicios parametro)
        {
            await Navigation.PushAsync(new DataServicios(parametro));
        }
        #endregion

        #region COMANDOS

        public ICommand VolverMenuCommand => new Command(async () => await RegresarAMenu());
        public ICommand NavDataServicioCommand => new Command<MServicios>(async (p) => await IraDataServicio(p));
        public ICommand BuscarFechaCommand => new Command(async () => await ObtenerFechas());
        //public ICommand ProcesoSimpleCommand => new Command(ProcesoSimple);
        #endregion
    }
}
