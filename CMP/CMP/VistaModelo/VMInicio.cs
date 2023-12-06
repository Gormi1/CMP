using CMP.Vistas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo
{
    internal class VMInicio : VMCompartidoInicioServicios
    {
        #region VARIABLES

        #endregion

        #region CONSTRUCTOR
        public VMInicio(INavigation navigation): base(navigation)
        {
            Navigation = navigation;
        }

        public VMInicio() : base() { }
        #endregion

        #region PROCESOS
        public async Task IrACalendario()
        {
            await Navigation.PushAsync(new Calendario());
        }
        #endregion

        #region COMANDOS
        public ICommand NavCalendarioCommand => new Command(async () => await IrACalendario());
        #endregion
    }
}
