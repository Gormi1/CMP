using CMP.Datos;
using CMP.Modelo;
using CMP.Vistas;
using CMP.Vistas.FormServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo
{
    class VMInicio2 : BaseViewModel
    {
        #region VARIABLES
        List<MServicios> _Listainicio;

        private string numeroEconomico;

        private string fechas;

        private string tipoServicio;
        #endregion

        #region CONSTRUCTOR
        public VMInicio2(INavigation navigation)
        {
            Navigation = navigation;
        }

        #endregion

        #region Objetos
        public string TipoServicio { get => tipoServicio; set => SetProperty(ref tipoServicio, value); }
        public string NumeroEconomico { get => numeroEconomico; set => SetProperty(ref numeroEconomico, value); }
        public string Fechas { get => fechas; set => SetProperty(ref fechas, value); }
        public List<MServicios> Listainicio
        {
            get { return _Listainicio; }
            set { SetValue(ref _Listainicio, value); }
        }

        #endregion

        #region PROCESO
        public async Task<List<MServicios>> GetServicios()
        {
            var función = new DServicios();

            Listainicio = await función.ObtenerServicios();

            return Listainicio.Take(3).ToList();
        }
        public async Task IrACalendario()
        {
            await Navigation.PushAsync(new Calendario2());
        }
        public async Task IraDataServicio(MServicios parametro)
        {
            await Navigation.PushAsync(new DataServicios(parametro));
        }
        #endregion

        #region COMANDOS
        public ICommand NavCalendarioCommand => new Command(async () => await IrACalendario());

        public ICommand NavDataServicioCommand => new Command<MServicios>(async (p) => await IraDataServicio(p));

        #endregion
    }
}
