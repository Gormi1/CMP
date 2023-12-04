using CMP.Modelo;
using CMP.VistaModelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}