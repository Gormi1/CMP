using CMP.Modelo;
using CMP.VistaModelo.Formularios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMP.Vistas.Formularios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataVehiculo : ContentPage
    {
        public DataVehiculo(MVehiculos parametros)
        {
            InitializeComponent();
            BindingContext = new VMDataVehiculo(Navigation, parametros, GridPorcentajes);
        }
    }
}