using CMP.Datos;
using CMP.Modelo;
using CMP.Vistas;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMP
{
    public partial class App : Application
    {
        private DServicios _servicios;
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MenuPrincipal());
        }

        protected override void OnStart()
        {
            base.OnStart();

            // Puedes cargar datos aquí de manera asíncrona
            LoadDataAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        private async void LoadDataAsync()
        {
            try
            {
                var data = await _servicios.ObtenerServiciosRecientesOFuturos();
                (Current as App).ServiciosData = data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar datos: {ex.Message}");
            }
        }

        public List<MServicios> ServiciosData { get; set; }
    }
}
