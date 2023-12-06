using CMP.Datos;
using CMP.Modelo;
using CMP.Vistas.FormServicios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo
{
    public class VMCompartidoInicioServicios : BaseViewModel
    {
        #region Variables

        DServicios función = new DServicios();
        List<MServicios> _ListaServicios;
        private List<MServicios> _ListaServiciosMostrados;

        #endregion

        public string Icono { get; set; }
        public string NumeroEconomico { get; set; }
        public string Fechas { get; set; }
        public string TipoServicio { get; set; }
        #region CONSTRUCTOR
        public VMCompartidoInicioServicios() { }

        public VMCompartidoInicioServicios(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region OBJETOS
        public List<MServicios> ListaServicios
        {
            get { return _ListaServicios; }
            set { SetValue(ref _ListaServicios, value); }
        }

        public List<MServicios> ListaServiciosMostrados
        {
            get { return _ListaServiciosMostrados; }
            set
            {
                if (_ListaServiciosMostrados != value)
                {
                    _ListaServiciosMostrados = value;
                    OnPropertyChanged(); // Asegúrate de llamar a OnPropertyChanged
                }
            }
        }

        #endregion

        #region PROCESOS
        public async Task GetServicios()
        {
            función = new DServicios();
            MServicios ServiciosData = new MServicios
            {
                Icono = Icono,
                NumeroEconomico = NumeroEconomico,
                Fechas = Fechas,
                TipoServicio = TipoServicio,
            };

            ListaServicios = await función.ObtenerServiciosRecientesOFuturos();
        }

        public async Task GetServicioslimite()
        {
            try
            {
                función = new DServicios();
                MServicios ServiciosData = new MServicios
                {
                    Icono = Icono,
                    NumeroEconomico = NumeroEconomico,
                    Fechas = Fechas,
                    TipoServicio = TipoServicio,
                };

                // Obtén todos los servicios recientes o futuros
                _ListaServiciosMostrados = await función.ObtenerServiciosRecientesOFuturos();

                Console.WriteLine("Lista de servicios obtenida:");

                foreach (var servicio in _ListaServiciosMostrados)
                {
                    Console.WriteLine($"Id: {servicio.IdServicios}, NumeroEconomico: {servicio.NumeroEconomico}, Fechas: {servicio.Fechas}, TipoServicio: {servicio.TipoServicio}");
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    // Filtra para incluir también servicios de hoy
                    if (_ListaServiciosMostrados != null)
                    {
                        ListaServiciosMostrados = _ListaServiciosMostrados
                            .Where(servicio =>
                                DateTime.TryParseExact(servicio.Fechas, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _) &&
                                DateTime.ParseExact(servicio.Fechas, "dd-MM-yyyy", CultureInfo.InvariantCulture) >= DateTime.Today)
                            .Take(3)
                            .ToList();

                        OnPropertyChanged("ListaServiciosMostrados"); // Actualiza la vista
                    }
                    else
                    {
                        // Manejo de caso en que _ListaServiciosMostrados es nulo
                        Console.WriteLine("La lista de servicios mostrados es nula.");
                    }
                });

                Console.WriteLine("Lista de servicios filtrada:");

                foreach (var servicio in ListaServiciosMostrados)
                {
                    Console.WriteLine($"Id: {servicio.IdServicios}, NumeroEconomico: {servicio.NumeroEconomico}, Fechas: {servicio.Fechas}, TipoServicio: {servicio.TipoServicio}");
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción de manera adecuada (por ejemplo, registrándola o lanzándola nuevamente)
                Console.WriteLine($"Error en GetServicioslimite: {ex.Message}");
                throw;
            }
        }




        public async Task IraDataServicio(MServicios parametro)
        {
            await Navigation.PushAsync(new DataServicios(parametro));
        }
        #endregion

        #region COMMANDS
        public ICommand NavDataServicioCommand => new Command<MServicios>(async (p) => await IraDataServicio(p));

        #endregion

    }
}
