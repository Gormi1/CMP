using CMP.Datos;
using CMP.Modelo;
using CMP.Vistas.FormServicios;
using CMP.Vistas.Formularios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo
{
    public class VMServicios : BaseViewModel
    {
        #region VARIABLES

        DServicios función = new DServicios();
        List<MServicios> _ListaServicios;

        public MServicios ServiciosData;
        public string Icono { get; set; }
        public string NumeroEconómico {  get; set; }
        public string Fechas {  get; set; }
        public string TipoServicio {  get; set; }
        #endregion

        #region CONSTRUCTOR
        public VMServicios(INavigation navigation)
        {
            Navigation = navigation;
        }

        public VMServicios() { }
        #endregion

        #region OBJETOS
        public List<MServicios> ListaServicios
        {
            get { return _ListaServicios; }
            set { SetValue(ref _ListaServicios, value); }
        }

        #endregion

        #region PROCESOS
        public async Task GetServicios()
        {
            función = new DServicios();
            ServiciosData = new MServicios
            {
                Icono = Icono,
                NumeroEconomico = NumeroEconómico,
                Fechas = Fechas,
                TipoServicio = TipoServicio,
            };

            ListaServicios = await función.ObtenerServicios();
        }

        public async Task IraAddServicios()
        {
            await Navigation.PushAsync(new AddServicios());
        }
        #endregion

        #region COMANDOS

        public ICommand NavAddServiciosCommand => new Command(async () => await IraAddServicios());
        #endregion
    }
}
