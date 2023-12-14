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
            var función = new DServicios();

            // Intenta cargar desde la caché local
            ListaServicios = await función.ObtenerServiciosRecientesOFuturos();

            // Si no hay datos en la caché, carga desde Firebase y actualiza la caché
            if (ListaServicios == null || ListaServicios.Count == 0)
            {
                ListaServicios = await función.ObtenerServiciosRecientesOFuturos();
                await función.ActualizarCacheAsync(ListaServicios);
            }
        }

        public async Task IraDataServicio(MServicios parametro)
        {
            await Navigation.PushAsync(new DataServicios(parametro));
        }

        public async Task IraAddServicios()
        {
            await Navigation.PushAsync(new AddServicios());
        }
        #endregion

        #region COMANDOS
        public ICommand NavDataServicioCommand => new Command<MServicios>(async (p) => await IraDataServicio(p));
        public ICommand NavAddServiciosCommand => new Command(async () => await IraAddServicios());
        #endregion
    }
}
