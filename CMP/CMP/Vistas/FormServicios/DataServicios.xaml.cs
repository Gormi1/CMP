using CMP.Modelo;
using CMP.VistaModelo.FormServicios;
using CMP.VistaModelo.Formularios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMP.Vistas.FormServicios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataServicios : ContentPage
    {
        public DataServicios(MServicios parametros)
        {
            InitializeComponent();
            BindingContext = new VMDataServicios(Navigation, parametros);
        }
    }
}