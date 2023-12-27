using CMP.Datos;
using CMP.Modelo;
using CMP.Vistas.FormServicios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo.FormServicios
{
    public class VMDataBitacora : BaseViewModel
    {
        #region VARIABLES

        public MServicios Parametrosrecive { get; set; }
        #endregion

        #region CONSTRUCTOR
        public VMDataBitacora(INavigation navigation, MServicios parametrotrae)
        {
            Navigation = navigation;
            Parametrosrecive = parametrotrae;
        }
        #endregion

        #region OBJETOS
        #endregion

        #region PROCESOS

        public async Task RegresarVehiculo()
        {
            await Navigation.PopAsync();
        }


        #endregion

        #region COMANDOS
        public ICommand VolverMenuCommand => new Command(async () => await RegresarVehiculo());

        #endregion
    }
}
