using CMP.Datos;
using CMP.Modelo;
using CMP.Servicios;
using CMP.Vistas;
using CMP.Vistas.Formularios;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CMP.VistaModelo.Formularios
{
    public class VMAddVehiculo : BaseViewModel
    {
        #region VARIABLES
        public string NumeroEconomico { get; set; }
        public string Modelo { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string NumeroDeSerie { get; set; }
        public int Kilometraje { get; set; }
        public int HoraInicial { get; set; }
        public int HoraFinal { get; set; }
        public int CantLlantas { get; set; }
        public string DatosExtras { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
        public double Combustible { get; set; }

        private ICommand _pickPhotoCommand;
        private ImageSource _Selectedphoto;
        #endregion

        #region CONSTRUCTOR
        public VMAddVehiculo(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region OBJETOS
        public ImageSource Selectedphoto
        {
            get { return _Selectedphoto; }
            set
            {
                if (_Selectedphoto != value)
                {
                    _Selectedphoto = value;
                    OnPropertyChanged(nameof(Selectedphoto));
                }
            }
        }
        #endregion

        #region PROCESOS

        public async Task InsertarVehiculo()
        {
            if (string.IsNullOrEmpty(NumeroEconomico) ||
                string.IsNullOrEmpty(NumeroDeSerie) ||
                string.IsNullOrEmpty(Nombre) ||
                string.IsNullOrEmpty(Modelo) ||
                string.IsNullOrEmpty(Tipo) ||
                string.IsNullOrEmpty(Estado) ||
                CantLlantas == 0 ||
                string.IsNullOrEmpty(DatosExtras) ||
                string.IsNullOrEmpty(Observaciones))
            {

                await DisplayAlert("Error", "Llene todos los datos", "Aceptar");
                VaciarCampos();
            }
            else
            {
                var funcion = new Dvehiculos();
                var datos = new MVehiculos
                {
                    NumeroEconomico = NumeroEconomico,
                    NumeroDeSerie = NumeroDeSerie,
                    Nombre = Nombre,
                    Modelo = Modelo,
                    Tipo = Tipo,
                    Estado = Estado,
                    CantLlantas = CantLlantas,
                    TiempoVidaLlantas = new List<int>(new int[CantLlantas]),
                    Kilomtraje = Kilometraje,
                    HoraInicial = HoraInicial,
                    HoraFinal = HoraFinal,
                    DatosExtras = DatosExtras,
                    Observaciones = Observaciones,
                    Combustible = Combustible,
                    Icono = Selectedphoto?.ToString(),
                };

                await funcion.InsertarVehiculo(datos);
                await DisplayAlert("Vehiculo Guardado", "Datos guardados Correctamente", "Aceptar");
                VaciarCampos();
                await RegresarAVehiculos();
            }
        }

        private async Task<string> SubirFotoFirebase()
        {
            try
            {
                var Foto = await MediaPicker.PickPhotoAsync();

                if (Foto != null)
                {
                    using (var stream = await Foto.OpenReadAsync())
                    {
                        byte[] fileBytes = new byte[stream.Length];
                        await stream.ReadAsync(fileBytes, 0, fileBytes.Length);

                        var firebaseStorage = new FirebaseStorage("cmpsoft-260301.appspot.com").Child("vehiculos");

                        // Genera un nombre único para el archivo en Firebase Storage
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Foto.FullPath);

                        // Sube la foto al bucket de Firebase Storage
                        var task = firebaseStorage
                            .Child(fileName)
                            .PutAsync(new MemoryStream(fileBytes));

                        Console.WriteLine("Subiendo foto...");

                        try
                        {
                            var link = await task; 
                            await DisplayAlert("titulo", "Foto subida correctamente.", "ok");
                            Console.WriteLine($"Enlace: {link}");

                            Selectedphoto = ImageSource.FromUri(new Uri(link));
                            return link;
                        }
                        catch (Exception ex)
                        {

                            await DisplayAlert("titulo", $"Error al subir la foto: {ex.Message}", "ok");
                            return null;
                        }

                        
                    }
                }
                else
                {
                    await DisplayAlert("titulo", "Error al subir la foto", "ok");
                    return null;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("titulo", $"Error al seleccionar la foto: \n{ex.Message}", "ok");
                return null;
            }
        }

        public void VaciarCampos()
        {
            NumeroEconomico = "";
            Modelo = "";
            Nombre = "";
            Tipo = "";
            NumeroDeSerie = "";
            DatosExtras = "";
            Observaciones = "";
            Estado = "";
        }
        public async Task RegresarAVehiculos()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region COMANDOS
        public ICommand VolverMenuCommand => new Command(async () => await RegresarAVehiculos());
        public ICommand GuardarDatosCommando => new Command(async () => await InsertarVehiculo());
        public ICommand AgregarImgCommand => _pickPhotoCommand ?? (_pickPhotoCommand = new Command(async () => await SubirFotoFirebase()));

        #endregion
    }
}