using CMP.Modelo;
using CMP.VistaModelo.FormServicios;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMP.Vistas.FormServicios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarServicio : ContentPage
    {
        public EditarServicio(MServicios parametros)
        {
            InitializeComponent();
            BindingContext = new VMEditarServicios(Navigation, parametros);
        }
    }
}