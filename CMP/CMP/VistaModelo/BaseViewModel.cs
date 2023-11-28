// using System;
// using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CMP.VistaModelo
{
    public class BaseViewModel : INotifyPropertyChanged //notificador de que cuando una lista se alteralo toficia y lo cambia
    {
        // poder  cambiar de paginas

        public INavigation Navigation;

        //visualizar que funcion se ha modificado del controlador
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnpropertyChanger([CallerMemberName] string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //manjear fotos en la app
        private ImageSource _foto;
        public ImageSource Foto
        {
            get { return _foto; }
            set
            {
                _foto = value;
                OnpropertyChanger();
            }
        }

        //propiedades btn lbl
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //alertas 

        //1 btn
        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }


        //2 btns
        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public async Task<string> DisplayPromptAsync(string title, string message, string accept, string cancel, string placeholder)
        {
            return await Application.Current.MainPage.DisplayPromptAsync(title, message, accept, cancel, placeholder);
        }
        //manejar un string o un entry
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            OnpropertyChanger(propertyName);

            return true;
        }

        //tipo string
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        //trabajar tipos booleanos
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }

        //encargado de revicir informacion
        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return;
            }

            backingField = value;
            OnpropertyChanger(propertyName);
        }
    }
}
