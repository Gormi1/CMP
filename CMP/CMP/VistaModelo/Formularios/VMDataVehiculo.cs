using CMP.Datos;
using CMP.Modelo;
using CMP.Vistas.Formularios;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Firebase.Database.Query;
using System.IO;
using System.Net.Http;
using CMP.Servicios;
using System.Net;

namespace CMP.VistaModelo.Formularios
{
    internal class VMDataVehiculo : BaseViewModel
    {
        #region VARIABLES

        public ImageSource _imagen;
        public MVehiculos Parametrosrecive { get; set; }
        public List<int> PorcentajesRuedas { get; set; }
        #endregion

        #region CONSTRUCTOR
        public VMDataVehiculo(INavigation navigation, MVehiculos parametrotrae, Grid parametro)
        {
            Navigation = navigation;
            Parametrosrecive = parametrotrae;
            PorcentajesRuedas = Parametrosrecive.TiempoVidaLlantas;
            DibujarPorcentajesRuedas(parametro);
            CargarImagen(parametrotrae.Icono);
        }
        #endregion

        #region OBJETOS
        public ImageSource Imagen
        {
            get { return _imagen; }
            set
            {
                if (_imagen != value)
                {
                    _imagen = value;
                    OnPropertyChanged(nameof(Imagen));
                }
            }
        }
        #endregion

        #region PROCESOS
        private async Task<ImageSource> CargarImagen(string urlimagen)
        {
            var webClient = new WebClient();
            var nombreArchivo = Path.GetFileName(urlimagen);

            var urlDescarga = await new FirebaseStorage("cmpsoft-260301.appspot.com")
                    .Child(nombreArchivo)
                    .GetDownloadUrlAsync();

            string url = urlDescarga;
            byte[] imgBytes = webClient.DownloadData(url);
            Imagen = ImageSource.FromStream(() => new MemoryStream(imgBytes));
            return Imagen;

        }

        public async Task IrAEditar()
        {
            await Navigation.PushAsync(new EditarVehiculo(Parametrosrecive));
        }
        public async Task RegresarVehiculo()
        {
            await Navigation.PopAsync();
        }
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

        public void DibujarPorcentajesRuedas(Grid GridPorcentajes)
        {
            GridPorcentajes.Children.Clear();
            string color = "";
            if (PorcentajesRuedas != null && PorcentajesRuedas.Count > 0)
            {
                for (int i = 0; i < PorcentajesRuedas.Count; i++)
                {
                    int indice = i;

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

                    Rueda.Clicked += (sender, args) => CambiarPorcentajeRuedaClick(indice, GridPorcentajes);


                    GridPorcentajes.Children.Add(Rueda, i % 2, i / 2);

                }
            }

        }

        private async void CambiarPorcentajeRuedaClick(int indiceRueda, Grid parametro)
        {
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
                    PorcentajesRuedas[indiceRueda] = porcentaje;

                    DibujarPorcentajesRuedas(parametro);

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
        public async Task EditarRuedas()
        {
            var funcion = new Dvehiculos();

            Parametrosrecive.TiempoVidaLlantas = new List<int>(PorcentajesRuedas);

            await funcion.EditarVehiculo(Parametrosrecive);
            await DisplayAlert("Vehiculo Guardado", "Datos guardados Correctamente", "Aceptar");
        }

        #endregion

        #region COMANDOS
        public ICommand NavVehiculoCommand => new Command(async () => await RegresarVehiculo());
        public ICommand IrAEditarCommando => new Command(async () => await IrAEditar());
        public ICommand EliminarVehiculoCommand => new Command(async () => await EliminarVehiculo());

        #endregion
    }
}
