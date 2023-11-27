using CMP.Modelo;
using CMP.VistaModelo;
using CMP.VistaModelo.Formularios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace CMP.Vistas.Formularios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddVehiculo : ContentPage
    {
        public AddVehiculo()
        {
            InitializeComponent();
            BindingContext = new VMAddVehiculo(Navigation);
        }
    }
}