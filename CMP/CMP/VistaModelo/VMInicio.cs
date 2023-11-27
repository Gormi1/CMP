using CMP.Vistas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo
{
    internal class VMInicio : BaseViewModel
    {
        #region VARIABLES
        #endregion

        #region CONSTRUCTOR
        public VMInicio(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region 
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
