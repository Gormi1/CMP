using CMP.Datos;
using CMP.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMP.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Ïnicio2 : ContentPage
	{
		VMInicio2 VM;
		public Ïnicio2 ()
		{
			InitializeComponent ();

            VM = new VMInicio2(Navigation);
            BindingContext = VM;
            Appearing += Inicio2_Appearing;
        }

        private async void Inicio2_Appearing(object sender, EventArgs e)
        {
            ListaIni.ItemsSource = await VM.GetServicios();
            await ContarVehiculosPorEstado();
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