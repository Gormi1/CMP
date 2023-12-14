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
    internal class VMInicio : BaseViewModel
    {
        #region VARIABLES

        public List<MServicios> Listainicio { get; set; }
        public string NumeroEconomico { get; set; }
        public string Fechas { get; set; }
        public string TipoServicio { get; set; }


        #endregion

        #region CONSTRUCTOR
        public VMInicio(INavigation navigation)
        {
            Navigation = navigation;
        }

        #endregion

        #region Objetos

        #endregion

        #region PROCESO
        public async Task<List<MServicios>> GetServicios()
        {
            var función = new DServicios();

            Listainicio = await función.ObtenerServiciosRecientesOFuturos2();

            foreach (var item in Listainicio)
            {
                NumeroEconomico = item.NumeroEconomico;
                Fechas = item.Fechas;
                TipoServicio = item.TipoServicio;
            }
            return Listainicio.Take(3).ToList();
        }
        public async Task IrACalendario()
        {
            await Navigation.PushAsync(new Calendario());
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
