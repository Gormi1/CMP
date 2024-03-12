using CMP.VistaModelo;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMP.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Servicios : ContentPage
    {
        VMServicios vm;
        public Servicios()
        {
            InitializeComponent();
            vm = new VMServicios(Navigation);
            BindingContext = vm;
            Appearing += Servicios_Appearing;
        }

        private async void Servicios_Appearing(object sender, EventArgs e)
        {
            await vm.GetServicios();
        }
    }
}