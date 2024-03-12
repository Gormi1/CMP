using CMP.Datos;
using CMP.Modelo;
using CMP.Vistas.Formularios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo.Formularios
{
    // Clase que representa el ViewModel para la vista de detalles de un vehículo
    internal class VMDataVehiculo : BaseViewModel
    {
        #region VARIABLES

        // Propiedad que almacena los parámetros recibidos para la vista del vehículo
        public MVehiculos Parametrosrecive { get; set; }
        // Propiedad que almacena los porcentajes de vida de las llantas del vehículo
        public List<int> PorcentajesRuedas { get; set; }
        #endregion

        #region CONSTRUCTOR

        // Constructor de la clase que recibe la navegación, los parámetros del vehículo y un Grid como parámetro
        public VMDataVehiculo(INavigation navigation, MVehiculos parametrotrae, Grid parametro)
        {
            Navigation = navigation;
            Parametrosrecive = parametrotrae;
            PorcentajesRuedas = Parametrosrecive.TiempoVidaLlantas;
            DibujarPorcentajesRuedas(parametro);
        }
        #endregion

        #region OBJETOS
        #endregion

        #region PROCESOS

        // Método para navegar a la página de edición del vehículo
        public async Task IrAEditar()
        {
            await Navigation.PushAsync(new EditarVehiculo(Parametrosrecive));
        }
        // Método para regresar a la vista anterior
        public async Task RegresarVehiculo()
        {
            await Navigation.PopAsync();
        }
        // Método para eliminar el vehículo
        public async Task EliminarVehiculo()
        {
            var response = await DisplayAlert("Eliminar", "Desea eliminar este auto?", "Si", "No");
            if (response)
            {
                var funcion = new Dvehiculos();
                await funcion.EliminarVehiculo(Parametrosrecive);
            }
            else
            {
                await DisplayAlert("Error", "vehiculo no existe", "ok");
            }
            await DisplayAlert("Eliminar", "Vehiculo eliminado correctamente", "ok");
            await RegresarVehiculo();
        }

        // Método para dibujar los porcentajes de vida de las llantas en un Grid
        public void DibujarPorcentajesRuedas(Grid GridPorcentajes)
        {
            // Limpia los elementos existentes en el Grid
            GridPorcentajes.Children.Clear();
            string color = "";
            //llena el grid con botones dependiendo de la cantidad de ruedas del vehiculo (4,6,10)
            if (PorcentajesRuedas != null && PorcentajesRuedas.Count > 0)
            {
                for (int i = 0; i < PorcentajesRuedas.Count; i++)
                {
                    int indice = i;


                    // Asigna colores según el porcentaje de vida de la llanta
                    if (PorcentajesRuedas[i] <= 33)
                    {
                        color = "#CA1D27";
                    }
                    else if (PorcentajesRuedas[i] > 33 && PorcentajesRuedas[i] <= 66)
                    {
                        color = "#D49E3D";
                    }
                    else if (PorcentajesRuedas[i] > 67)
                    {
                        color = "#35B121";
                    }

                    // Crea un botón para representar la llanta en el Grid
                    Button Rueda = new Button
                    {
                        Text = $"Rueda {i + 1}: {PorcentajesRuedas[i]}%",
                        FontSize = 12,
                        FontAttributes = FontAttributes.None,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.FromHex(color),
                        TextColor = Color.White,
                        CornerRadius = 10,

                    };

                    // Asigna un manejador de eventos al botón
                    Rueda.Clicked += (sender, args) => CambiarPorcentajeRuedaClick(indice, GridPorcentajes);


                    // Agrega el botón al Grid
                    GridPorcentajes.Children.Add(Rueda, i % 2, i / 2);

                }
            }

        }

        // Método invocado al hacer clic en una llanta para cambiar su porcentaje de vida
        private async void CambiarPorcentajeRuedaClick(int indiceRueda, Grid parametro)
        {
            // Muestra un cuadro de diálogo para ingresar el nuevo porcentaje
            string nuevoPorcentaje = await DisplayPromptAsync("Cambiar Porcentaje", $"Nuevo porcentaje para Rueda {indiceRueda}:", "OK", "Cancelar", "0-100");

            if (nuevoPorcentaje == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(nuevoPorcentaje))
            {
                await DisplayAlert("Error", "Debes ingresar un valor válido.", "OK");
                return;
            }

            if (int.TryParse(nuevoPorcentaje, out int porcentaje))
            {
                if (porcentaje >= 0 && porcentaje <= 100)
                {
                    // Actualiza el porcentaje de la llanta y redibuja los porcentajes
                    PorcentajesRuedas[indiceRueda] = porcentaje;

                    DibujarPorcentajesRuedas(parametro);

                    // Realiza la edición de las llantas en la base de datos
                    await EditarRuedas();
                }
                else
                {
                    await DisplayAlert("Error", "El porcentaje debe estar entre 0 y 100.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Debes ingresar un número válido.", "OK");
            }
        }
        // Método para editar los porcentajes de las llantas en la base de datos
        public async Task EditarRuedas()
        {
            var funcion = new Dvehiculos();

            Parametrosrecive.TiempoVidaLlantas = new List<int>(PorcentajesRuedas);

            await funcion.EditarVehiculo(Parametrosrecive);
            await DisplayAlert("Vehiculo Guardado", "Datos guardados Correctamente", "Aceptar");
        }

        #endregion

        #region COMANDOS
        // Comando para navegar de regreso a la vista anterior
        public ICommand NavVehiculoCommand => new Command(async () => await RegresarVehiculo());
        // Comando para navegar a la página de edición del vehículo
        public ICommand IrAEditarCommando => new Command(async () => await IrAEditar());
        // Comando para eliminar el vehículo
        public ICommand EliminarVehiculoCommand => new Command(async () => await EliminarVehiculo());

        #endregion
    }
}
