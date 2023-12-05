
using CMP.Datos;
using CMP.Modelo;
using CMP.Servicios;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMP.VistaModelo.FormServicios
{
    internal class VMAddServicios : BaseViewModel
    {
        #region VARIABLES
        public MItem data;
        private string _ResultadoFecha;
        private double _Cantidad;
        DateTime _Fecha;


        #endregion

        #region CONSTRUCTOR
        public VMAddServicios(INavigation navigation)
        {
            Navigation = navigation;
            Fecha = DateTime.Now;
        }
        #endregion

        #region OBJETOS



        public double Cantidad
        {
            get { return _Cantidad; }
            set
            {
                if (_Cantidad != value)
                {
                    _Cantidad = value;
                    OnPropertyChanged(nameof(Cantidad));
                }
            }
        }
        public DateTime Fecha
        {
            get { return _Fecha; }
            set
            {
                SetValue(ref _Fecha, value);
                ResultadoFecha = _Fecha.ToString("dd-MM-yyyy");
            }
        }
        public string ResultadoFecha
        {
            get { return _ResultadoFecha; }
            set { SetValue(ref _ResultadoFecha, value); }
        }
        #endregion

        #region PROCESOS

        public async Task RegresarAVehiculos()
        {
            await Navigation.PopAsync();
        }


        #endregion

        #region COMANDOS
        public ICommand VolverMenuCommand => new Command(async () => await RegresarAVehiculos());

        #endregion
    }
}