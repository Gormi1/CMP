using CMP.Datos;
using CMP.Modelo;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo.FormServicios
{
    public class VMAddServicios : BaseViewModel
    {
        #region VARIABLES
        public string TipoServicio { get; set; }
        private string _ResultadoFecha;
        private string _Inventario, _NumeroEconomico;
        DateTime _Fecha;


        #endregion

        #region CONSTRUCTOR
        public VMAddServicios(INavigation navigation)
        {
            Navigation = navigation;
            Fecha = DateTime.Now;
        }
        #endregion

        #region OBJETOS

        public string InventarioText
        {
            get { return _Inventario; }
            set
            {
                if (_Inventario != value)
                {
                    _Inventario = value;
                    OnPropertyChanged(nameof(InventarioText));
                }
            }
        }
        public string NumeroEconomico
        {
            get { return _NumeroEconomico; }
            set
            {
                if (_NumeroEconomico != value)
                {
                    _NumeroEconomico = value;
                    OnPropertyChanged(nameof(NumeroEconomico));
                }
            }
        }

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
        #endregion

        #region PROCESOS

        public async Task AgregarServicio()
        {
            if (string.IsNullOrEmpty(NumeroEconomico) ||
                string.IsNullOrEmpty(InventarioText) ||
                string.IsNullOrEmpty(TipoServicio))
            {
                await DisplayAlert("Error", "Llene todos los datos", "Aceptar");
                VaciarCampos();
            }
            else
            {
                var función = new DServicios();
                var data = new MServicios
                {
                    NumeroEconomico = NumeroEconomico,
                    Fechas = ResultadoFecha,
                    TipoServicio = TipoServicio,
                    Inventario = InventarioText,
                };

                await función.InsertarServicios(data);
                await DisplayAlert("Vehiculo Guardado", "Datos guardados Correctamente", "Aceptar");
                VaciarCampos();
                await RegresarAMenu();
            }
        }

        public async Task RegresarAVehiculos()
        {
            await Navigation.PopAsync();
        }

        public async Task RegresarAMenu()
        {
            await Navigation.PopToRootAsync();
        }
        public void VaciarCampos()
        {
            NumeroEconomico = "";
            InventarioText = "";
            TipoServicio = "";
            Fecha = DateTime.Now;

        }

        #endregion

        #region COMANDOS
        public ICommand VolverMenuCommand => new Command(async () => await RegresarAVehiculos());

        public ICommand GuardarServicioCommand => new Command(async () => await AgregarServicio());

        #endregion
    }

}

