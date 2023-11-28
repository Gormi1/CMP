using CMP.Datos;
using CMP.Modelo;
using CMP.Servicios;
using CMP.VistaModelo.Formularios;
using CMP.Vistas;
using CMP.Vistas.Formularios;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo
{
    public class VMVehiculos : BaseViewModel
    {
        #region VARIABLES
        Dvehiculos funcion = new Dvehiculos();
        List<MVehiculos> _ListaVehiculos;
        public MVehiculos VehiculoData { get; set; }
        public string Nombre { get; set; }
        public string Icono { get; set; }
        public int Kilomtraje { get; set; }
        public string Estado { get; set; }

        #endregion
        
        #region CONSTRUCTOR
        public VMVehiculos(INavigation navigation)
        {
            Navigation = navigation;
            GetVehiculo();
        }

        public VMVehiculos()
        {
        }
        #endregion

        #region OBJETOS

        public List<MVehiculos> ListaVehiculos
        {
            get { return _ListaVehiculos; }
            set { SetValue(ref _ListaVehiculos, value); }
        }
        #endregion

        #region PROCESOS
        #region comentado

        //public void DibujarVehiculos(MVehiculos Item, StackLayout Contenedor)
        //{
        //    var carril = Contenedor;
        //    var frame = new Frame
        //    {
        //        CornerRadius = 15,
        //        BackgroundColor = Color.FromRgb(240, 220, 230),
        //        BorderColor = Color.FromHex("#EEEDED"),
        //        HasShadow = false,
        //    };
        //    var grid = new Grid
        //    {
        //        RowDefinitions = new RowDefinitionCollection
        //        {
        //            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
        //            new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
        //        },
        //        ColumnDefinitions = new ColumnDefinitionCollection
        //        {
        //            new ColumnDefinition{ Width = new GridLength(50, GridUnitType.Star) },
        //            new ColumnDefinition{ Width = new GridLength(150, GridUnitType.Star) },
        //            new ColumnDefinition{ Width = new GridLength(83, GridUnitType.Star) },
        //        },
        //        HorizontalOptions = LayoutOptions.FillAndExpand,
        //    };
        //    var image = new Image
        //    {
        //        Source = Item.Icono,
        //        HeightRequest = 50,
        //        WidthRequest = 50,
        //        Margin = new Thickness(0, 0, 6, 0),
        //        HorizontalOptions = LayoutOptions.Start,
        //    };
        //    Grid.SetRowSpan(image, 2);
        //    Grid.SetColumn(image, 0);
        //    var labelNombre = new Label
        //    {
        //        Text = Item.Nombre,
        //        HorizontalOptions = LayoutOptions.StartAndExpand,
        //        VerticalOptions = LayoutOptions.Center,
        //        FontAttributes = FontAttributes.Bold,
        //    };
        //    Grid.SetRowSpan(labelNombre, 2);
        //    Grid.SetColumn(labelNombre, 1);
        //    var labelKilometraje = new Label
        //    {
        //        Text = Item.Kilomtraje.ToString(),
        //        HorizontalOptions = LayoutOptions.End,
        //        VerticalOptions = LayoutOptions.Center,
        //        FontSize = 15,
        //    };
        //    Grid.SetRow(labelKilometraje, 0);
        //    Grid.SetColumn(labelKilometraje, 2);

        //    var labelEstado = new Label
        //    {
        //        Text = Item.Estado,
        //        HorizontalOptions = LayoutOptions.End,
        //        VerticalOptions = LayoutOptions.Center,
        //        FontSize = 15,
        //        TextColor = Color.FromHex(_Color(Item.Estado))
        //    };
        //    Grid.SetRow(labelEstado, 1);
        //    Grid.SetColumn(labelEstado, 2);

        //    grid.Children.Add(image);
        //    grid.Children.Add(labelNombre);
        //    grid.Children.Add(labelKilometraje);
        //    grid.Children.Add(labelEstado);
        //    frame.Content = grid;
        //    var tap = new TapGestureRecognizer();
        //    tap.Tapped += async (Object sender, EventArgs e) =>
        //    {
        //        await Navigation.PushAsync(new DataVehiculo(Item));
        //    };
        //    grid.GestureRecognizers.Add(tap);
        //    carril.Children.Add(frame);
        //}
        //public async Task Mostrarvehiculos(StackLayout Contenedor)
        //{
        //    funcion = new Dvehiculos();
        //    ListaVehiculos = await funcion.ObtenerVehiculos();
        //    foreach (var item in ListaVehiculos)
        //    {
        //        DibujarVehiculos(item, Contenedor);
        //    }
        //}
        #endregion
        string Color(string parametro)
        {
            string color = "";
            if (parametro.Equals("En uso"))
            {
                color = "#9A8200";
            }
            else if (parametro.Equals("Disponible"))
            {
                color = "#35B121";
            }
            else if (parametro.Equals("En servicio"))
            {
                color = "#CA1D27";
            }
            return color;
        }
        public async Task GetVehiculo()
        {
            funcion = new Dvehiculos();
            VehiculoData = new MVehiculos
            {
                Icono = Icono,
                Nombre = Nombre,
                Kilomtraje = Kilomtraje,
                Estado = Estado,
            };
                    
            ListaVehiculos = await funcion.ObtenerVehiculos();
        }
        public async Task IraAddVehiculo()
        {
            await Navigation.PushAsync(new AddVehiculo());
        }
        public async Task IraDataVehiculo(MVehiculos parametro)
        {
            await Navigation.PushAsync(new DataVehiculo(parametro));
        }
        #endregion


        #region COMANDOS
        public ICommand NavAddVehiculoCommand => new Command(async () => await IraAddVehiculo()); 
        public ICommand NavDataVehiculoCommand => new Command<MVehiculos>(async (p) => await IraDataVehiculo(p)); 
        public ICommand ColorCommand => new Command<MVehiculos>((p) => Color(p.Estado));
        #endregion
    }
}
