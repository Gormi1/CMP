using CMP.Modelo;
using CMP.VistaModelo.Formularios;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMP.Vistas.Formularios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarVehiculo : ContentPage
    { 
        // contiene un parametro de tipo MVehiculo para poder llamar a la información del objeto que esta seleccionado
        public EditarVehiculo(MVehiculos parametros)
        {
            InitializeComponent();
            //hace llamada al parametro para traer los datos del modelo
            BindingContext = new VMEditarVehiculo(Navigation, parametros);
        }
    }
}