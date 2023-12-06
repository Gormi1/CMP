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
    public class VMServicios : VMCompartidoInicioServicios
    {
        #region VARIABLES

        #endregion

        #region CONSTRUCTOR
        public VMServicios(INavigation navigation): base(navigation)
        {
            Navigation = navigation;
        }

        public VMServicios() :base() { }
        #endregion

        #region PROCESOS
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
