
using CMP.Servicios;
using CMP.VistaModelo.FormServicios;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CMP.Vistas.FormServicios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddServicios : ContentPage
    {
        VMAddServicios vm;
        public AddServicios()
        {
            InitializeComponent();
            vm = new VMAddServicios(Navigation);
            BindingContext = vm;
            Appearing += AddServicios_Appearing;
        }

        private async void AddServicios_Appearing(object sender, EventArgs e)
        {
            await LlenarPickerConKeys();
        }

        private async Task LlenarPickerConKeys()
        {
            try
            {
                var keys = await ConexionFirebase.FBCliente
                    .Child("Servicios")
                    .Child("Vehiculos")
                    .OnceSingleAsync<Dictionary<string, object>>();

                TxtNumeroEconomico.Items.Clear();

                foreach (var key in keys.Keys)
                {
                    TxtNumeroEconomico.Items.Add(key);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener keys: {ex.Message}");
            }
        }
    }
}