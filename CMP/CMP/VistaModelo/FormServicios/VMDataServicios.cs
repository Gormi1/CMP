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

namespace CMP.VistaModelo.FormServicios
{
    public class VMDataServicios : BaseViewModel
    {
        #region VARIABLES

        public MServicios Parametrosrecive { get; set; }
        #endregion

        #region CONSTRUCTOR
        public VMDataServicios(INavigation navigation, MServicios parametrotrae)
        {
            Navigation = navigation;
            Parametrosrecive = parametrotrae;
        }
        #endregion

        #region OBJETOS
        #endregion

        #region PROCESOS

        public async Task IrAEditar()
        {
            await Navigation.PushAsync(new EditarServicio(Parametrosrecive));
        }
        public async Task RegresarVehiculo()
        {
            await Navigation.PopAsync();
        }
        public async Task EliminarServicio()
        {
            var response = await DisplayAlert("Eliminar", "el servicio ha sido realizado?", "Si", "No");
            if (response)
            {
                var funcion = new DServicios();
                var funcion2 = new DBitacora();
                await funcion2.InsertarServicios(Parametrosrecive);
                await funcion.EliminarServicios(Parametrosrecive);

                await DisplayAlert("Exito", "Servicio Completado", "ok");
            }
            else
            {
                await DisplayAlert("Error", "Servicio no existe", "ok");
            }
            await RegresarVehiculo();
        }


        #endregion

        #region COMANDOS
        public ICommand VolverMenuCommand => new Command(async () => await RegresarVehiculo());
        public ICommand IrAEditarCommando => new Command(async () => await IrAEditar());
        public ICommand EliminarServicioCommand => new Command(async () => await EliminarServicio());

        #endregion

    }
}
