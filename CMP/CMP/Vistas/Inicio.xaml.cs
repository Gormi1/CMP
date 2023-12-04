using CMP.Datos;
using CMP.Modelo;
using CMP.Servicios;
using CMP.VistaModelo;
using Firebase.Database;
using Firebase.Database.Query;
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
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
            BindingContext = new VMInicio(Navigation);

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
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