using CMP.Modelo;
using CMP.VistaModelo.Formularios;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMP.Vistas.Formularios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataVehiculo : ContentPage
    {   // contiene un parametro de tipo MVehiculo para poder llamar a la información del objeto que esta seleccionado
        public DataVehiculo(MVehiculos parametros)
        {
            InitializeComponent();
            //dentro del BindingContext además de llamar a la nagecación se llama al parametro y aun grid donde se colocan los botones de los vehiculos para insertar la vida de las ruerdas del vehiculo
            BindingContext = new VMDataVehiculo(Navigation, parametros, GridPorcentajes);
        }
    }
}