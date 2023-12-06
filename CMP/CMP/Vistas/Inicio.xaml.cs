using CMP.Datos;
using CMP.VistaModelo;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMP.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        VMInicio VM;
        public Inicio()
        {
            InitializeComponent();
            VM = new VMInicio(Navigation);
            BindingContext=VM;
            Appearing += Inicio_Appearing;
        }

        private async void Inicio_Appearing(object sender, EventArgs e)
        {
            base.OnAppearing();
            await ContarVehiculosPorEstado();
            await VM.GetServicioslimite();
        }

        private async Task ContarVehiculosPorEstado()
        {
            Dvehiculos dvehiculos = new Dvehiculos();
            var keys = await dvehiculos.ObtenerVehiculos();

            int disponibles = 0, enUso = 0, enMantenimiento = 0;

            foreach (var vehiculo in keys)
            {
                switch (vehiculo.Estado)
                {
                    case "Disponible":
                        disponibles++;
                        break;
                    case "En uso":
                        enUso++;
                        break;
                    case "En servicio":
                        enMantenimiento++;
                        break;
                        // Agrega más casos según tus estados
                }
            }

            // Ahora actualiza tus etiquetas con las cantidades
            labelDisponibles.Text = $"{disponibles}";
            labelEnUso.Text = $"{enUso}";
            labelEnMantenimiento.Text = $"{enMantenimiento}";
        }
    }
}