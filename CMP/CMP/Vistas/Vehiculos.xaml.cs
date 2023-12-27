using CMP.Modelo;
using CMP.VistaModelo;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMP.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Vehiculos : ContentPage
    {
        VMVehiculos vm;
        public Vehiculos()
        {
            InitializeComponent();
            vm = new VMVehiculos(Navigation);
            BindingContext = vm;
            Appearing += Vehiculos_Appearing;
        }

        private async void Vehiculos_Appearing(object sender, EventArgs e)
        {
            await vm.GetVehiculo();
        }
        private void Label_BindingContextChanged(object sender, System.EventArgs e)
        {
            //detecta el label
            if (sender is Label label)
            {
                if (label.BindingContext is MVehiculos viewModel)
                {
                    // Obtén el estado del vehículo desde el ViewModel
                    string estadoVehiculo = viewModel.Estado;
                    // llama al metodo ColorPorEstado
                    label.TextColor = ColorPorEstado(estadoVehiculo);
                }
            }
        }

        private Color ColorPorEstado(string estado)
        {
            // se asigna el color al TextColor basado en el estado del vehículo
            switch (estado)
            {
                case "En uso":
                    return Color.FromHex("#9A8200");
                case "Disponible":
                    return Color.FromHex("#35B121");
                case "En servicio":
                    return Color.FromHex("#CA1D27");
                default:
                    return Color.Default;
            }
        }
    }
}
